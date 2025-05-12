<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import Title from './components/Title.vue';
import { type Bean, useBeanStore } from './stores/beanStore';
import { onMounted } from 'vue';

const beanStore = useBeanStore();

onMounted(async () => {
  try {
    const response = await fetch('http://localhost:5000/api/Beans');
    if (!response.ok) {
      throw new Error(`Error fetching beans: ${response.statusText}`);
    }
    beanStore.setBeans(await response.json() as Bean[]);
  } catch (error) {
    console.error(error);
  }

  try {
    const response = await fetch('http://localhost:5000/api/Beans/botd');
      if (!response.ok) {
        throw new Error(`Error fetching bean of the day: ${response.statusText}`);
      }
      beanStore.setBeanOfTheDayIndex((await response.json() as Bean).index);
  } catch (error) {
      console.error(error);
  }
});

</script>

<template>
  <header>
    <img alt="Background beans" class="background" src="@/assets/backgroundBeans.png" />
    <img alt="Beans logo" class="logo" src="@/assets/logo.png" width="125" height="125" />

    <div class="wrapper">
      <Title msg="All the Beans!" />

      <nav>
        <RouterLink to="/">Bean of the Day</RouterLink>
        <RouterLink to="/browse">Browse Beans</RouterLink>
      </nav>
    </div>
  </header>

  <RouterView />
</template>

<style scoped>
header {
  line-height: 1.5;
  max-height: 100vh;
}

.background {
  position: fixed;
  top: 0;
  left: 0;
  min-width: 100%;
  height: 100%;
  z-index: -1;
  transform: translateY(-100px);
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

nav {
  width: 100%;
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }

  nav {
    text-align: left;
    margin-left: -1rem;
    font-size: 1rem;

    padding: 1rem 0;
    margin-top: 1rem;
  }
}
</style>
