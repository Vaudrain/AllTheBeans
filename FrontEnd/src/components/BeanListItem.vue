<script setup lang="ts">
import { useBeanStore, type Bean } from '@/stores/beanStore';

defineProps<{
  bean: Bean
  selectedBean: boolean
}>()

const beanStore = useBeanStore();
</script>

<template>
    <div 
        v-on:click="beanStore.setSelectedBeanIndex(bean.index)" 
        :class="['bean-list-item', { highlighted: selectedBean }]"
    >
        <img class="image" :src="bean.image" :alt="bean.name + ' Image'" />
        <h2 class="name">{{ bean.name }}</h2>
        <p class="cost">£{{ bean.costGBP }}</p>
        <div>SHOPPING TODO</div>
    </div>
</template>

<style scoped>
.bean-list-item {
    display: flex;
    flex-flow: row;
    gap: 5px;
    align-items: center;
    margin: 10px;
    padding: 0 5px;
    cursor: pointer;

    background-color: var(--color-background-soft);
    border-radius: 4px;
}

.image {
    width: 40px;
    height: auto;
    height: 20px;
    object-fit: cover;
    border-radius: 10px;
}

.cost {
    margin-left: auto;
}

.highlighted {
    background-color: var(--color-accent);
}

</style>