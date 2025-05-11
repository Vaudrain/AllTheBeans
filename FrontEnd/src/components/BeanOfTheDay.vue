<script setup lang="ts">
import { type Bean, useBeanStore } from '@/stores/beanStore';
import { storeToRefs } from 'pinia';
import { computed, type Ref, ref, toRaw, watch } from 'vue';

const beanStore = useBeanStore();

const { getBeanOfTheDay } = storeToRefs(beanStore);

const botd = computed<Bean>(() => getBeanOfTheDay.value || {
    name: 'Loading...',
    description: 'Loading...',
    country: 'Loading...',
    costGBP: 0,
    colour: 'Loading...',
    image: 'Loading...',
    index: 0
});

</script>

<template>
    <div class="bean-of-the-day" >
        <h1 class="coffee-brown">Bean of the Day</h1>
        <img class="botd-img" :src="botd.image" :alt="botd.name + ' Image'" />
        <h1 class="botd-name coffee-brown">{{ botd.name }}</h1>
        <div class="botd-description">
            <p>{{ botd.description }}</p>
        </div>
        <div class="botd-details">
            <div>
                <p class="coffee-brown">Country of Origin</p>
                <p>{{ botd.country }}</p>
            </div>
            <div>
                <p class="coffee-brown">Our Price</p>
                <p>£{{ botd.costGBP }}</p>
            </div>
            <div>
                <p class="coffee-brown">Colour</p>
                <p>{{ botd.colour }}</p>
            </div>
        </div>
    </div>
</template>

<style scoped>
@media (min-width: 1024px) {
    .bean-of-the-day {
        padding-top: 100px;
    }
}

.bean-of-the-day {
    text-align: center;
    margin: 20px;
}

.botd-img {
    border: 2px solid var(--color-accent);;
    border-radius: 20px;
    width: 30%;
}

.botd-name {
    font-size: 2rem;
    margin: 10px 0;
}
.botd-description {
    margin: 10px 0;
}
.botd-details {
    width: 100%;
    padding: 0 10% 0 10%;
    justify-content: space-between;
    display: flex;
    flex-flow: row;
}
</style>