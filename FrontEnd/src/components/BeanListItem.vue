<script setup lang="ts">
import { useBeanStore, type Bean } from '@/stores/beanStore';
import { storeToRefs } from 'pinia';
import { computed } from 'vue';

defineProps<{
  bean: Bean
  selectedBean: boolean
}>()

const beanStore = useBeanStore();


const { getBasket } = storeToRefs(beanStore);

const basket = computed(() => getBasket.value || []);
</script>

<template>
    <div 
        v-on:click="beanStore.setSelectedBeanIndex(bean.index)" 
        :class="['bean-list-item', { highlighted: selectedBean }]"
    >
        <img class="image" :src="bean.image" :alt="bean.name + ' Image'" />
        <h2 class="name">{{ bean.name }}</h2>
        <p class="cost">£{{ bean.costGBP }}</p>
        <div class="shopping-actions">
            <button 
                class="add-to-basket" 
                @click.stop="beanStore.addToBasket(bean,1)"
            >
                +
            </button>
            <template v-if="basket.some(b => b.bean.index === bean.index)">
                <button 
                    class="remove-from-basket" 
                    @click.stop="beanStore.removeFromBasket(bean,1)"
                >
                    -
                </button>
                <button 
                    class="remove-all-from-basket" 
                    @click.stop="beanStore.removeFromBasket(bean,-1)"
                >
                    X
                </button>    
                <input 
                    type="number" 
                    class="basket-quantity" 
                    :value="basket.find(b => b.bean.index === bean.index)?.quantity"
                    @blur="(e) => beanStore.updateBasketItemQuantity(bean, parseInt((e.target as HTMLInputElement)?.value))"
                ></input>                
            </template>
        </div>
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

    background-color: var(--color-background-mute);
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

.shopping-actions {
    display: flex;
    flex-flow: row;
    gap: 5px;
    input {
        text-align: center;
        width: 40px;
        -appearance: none;
        appearance: textfield;
        -moz-appearance: textfield;
    }
}
</style>