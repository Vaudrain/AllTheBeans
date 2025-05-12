<script setup lang="ts">
import { useBeanStore, type Bean } from '@/stores/beanStore';
import BeanListItem from './BeanListItem.vue';
import { storeToRefs } from 'pinia';
import { computed } from 'vue';

const beanStore = useBeanStore();

const { getBeans, getSelectedBean } = storeToRefs(beanStore);

const beans = computed<Bean[]>(() => getBeans.value || []);

const selectedBean = computed<Bean>(() => getSelectedBean.value || {
    name: 'No bean selected',
    description: '',
    country: '',
    costGBP: -1,
    colour: '',
    image: '',
    index: -1
});

var searchCriteria = {
    name: '',
    country: '',
    colour: '',
    lowerPrice: '',
    higherPrice: '',
};

var inputErrorState: boolean = false;

async function search() {
  try {
    const response = await fetch('http://localhost:5000/api/Beans/search?' + new URLSearchParams(searchCriteria));
    if (response.status === 404) {
      inputErrorState = false;
      beanStore.setBeans([]);
      return;
    }
    if (!response.ok) {
      inputErrorState = true;
      throw new Error(`Error fetching beans: ${response.statusText}`);
    }
    inputErrorState = false;
    beanStore.setBeans(await response.json() as Bean[]);
  } catch (error) {
    console.error(error);
  }
}

</script>

<template>
  <div class="browse-beans">
    <div class="selected-bean" v-if="selectedBean.index >= 0">
      <div class="selected-info-block">
        <h2 class="selected-name coffee-brown">{{ selectedBean.name }}</h2>
        <div class="selected-description">{{ selectedBean.description }}</div>
        <div>Country of Origin: {{ selectedBean.country }}</div>
        <div>Our Price: £{{ selectedBean.costGBP }}</div>
        <div>Colour: {{ selectedBean.colour }}</div>
      </div>
      <img class="selected-img" :src="selectedBean.image" :alt="selectedBean.name + ' Image'" />
    </div>
    <form class="search">  <!-- Realistically we should use form validation here -->
      <div class="search-input">
        <label class="coffee-brown" for="nameSearch">Name</label>
        <input type="text" id="nameSearch" placeholder="Name" v-model="searchCriteria.name" />
      </div>
      
      <div class="search-input">
        <label class="coffee-brown" for="countrySearch">Country</label>
        <input type="text" id="countrySearch" placeholder="Country" v-model="searchCriteria.country" />
      </div>
      
      <div class="search-input">
        <label class="coffee-brown" for="colourSearch">Colour</label>
        <input type="text" id="colourSearch" placeholder="Colour" v-model="searchCriteria.colour" />
      </div>
      
      <div class="search-input">
        <label class="coffee-brown" for="lowestPriceSearch">Lowest Price</label>
        <input type="number" id="lowestPriceSearch" placeholder="Lowest Price" v-model="searchCriteria.lowerPrice" />
      </div>
      
      <div class="search-input">
        <label class="coffee-brown" for="highestPriceSearch">Highest Price</label>
        <input type="number" id="highestPriceSearch" placeholder="Highest Price" v-model="searchCriteria.higherPrice" />
      </div>
      {{ searchCriteria.name }}
      <button class="submit-button" type="button" @click="search">Search</button>
      <div v-if="inputErrorState" class="error">Error fetching beans</div>
    </form>
    <div class="bean-list">
      <div class="bean-item" v-for="bean in beans.sort((a, b) => a.index - b.index)" :key="bean.index">
        <BeanListItem :bean="bean" :selectedBean="selectedBean.index === bean.index" />
      </div>
      <div v-if="beans.length === 0" class="no-beans">No beans found.</div>
    </div>
  </div>
</template>

<style scoped>

@media (min-width: 1024px) {
    .browse-beans {
        padding-top: 100px;
        margin: 20px 40px 20px 20px;
    }
}

@media (max-width: 1024px) {
    .browse-beans {
      margin: 20px 0 0 0;
    }
}

.browse-beans {
  width: 100%;
  background-color: var(--color-background-soft-transparent);
  padding: 10px;
  border-radius: 4px;
}

.selected-bean {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  width: 100%;
  margin-bottom: 20px;
}

.selected-info-block {
  margin-right: 5px;
  div {
    margin-bottom: 5px;
    text-align: center;
  }

  .selected-description {
    text-align: left;
    font-size: 1.1rem;
  }
}

.selected-name {
  text-align: center;
}

.selected-img {
  border: 2px solid var(--color-accent);
  border-radius: 20px;
  height: 200px;
  align-self: center;
  max-width: 200px;
  min-width: 40%;
  object-fit: cover;
}

.search {
  display: flex;
  flex-flow: wrap;
  gap: 10px;
  margin-bottom: 20px;
  width: 100%;
}

.search-input {
  display: flex;
  flex-direction: column;
  width: 150px;
}

.no-beans {
  height: 100%;
  text-align: center;
  text-justify: center;
  font-size: 1.5rem;
}

</style>