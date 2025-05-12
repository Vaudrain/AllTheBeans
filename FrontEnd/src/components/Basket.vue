<script setup lang="ts">
import { useBeanStore } from '@/stores/beanStore';
import { storeToRefs } from 'pinia';
import { computed, ref, type Ref } from 'vue';
import BeanListItem from './BeanListItem.vue';

const beanStore = useBeanStore();
const { getBasket } = storeToRefs(beanStore);
const basket = computed(() => getBasket.value || []);

const orderbuttonPressed: Ref<boolean> =  ref(false);

</script>

<template>
    <div class="basket">
        <h3 class="coffee-brown" v-if="basket.length === 0">
            Your basket is empty.
        </h3>
        <h3 class="coffee-brown" v-else>
            Basket
        </h3>
        <div class="bean-list">
            <div class="bean-item" v-for="item in basket">
                <BeanListItem :bean="item.bean" :selected-bean="false" />
            </div>
        </div>
        <button v-if="basket.length !== 0" class="submit-button clear-basket" type="button" @click="beanStore.clearBasket()">Clear Basket</button>
        <button v-if="basket.length !== 0" class="submit-button" type="button" @click="orderbuttonPressed = true">Order now!</button>
        <div v-if="orderbuttonPressed" class="order-confirmation">
            <h3 class="coffee-brown">Order Confirmation</h3>
            <p>Thank you for your order!</p>
            <p>This is where you would be directed to a payment/shipping page</p>
            <p>(That won't happen though, delivering coffee beans is <i>probably</i> beyond the scope of this task)</p>
        </div>
    </div>
</template>

<style scoped>
.basket {
    text-align: center;
    border-top: 2px solid var(--color-accent);
    margin-bottom: 40px;
    background-color: var(--color-background-soft-transparent);
    padding: 10px;
    border-radius: 4px;
    width: 100%;
}

.clear-basket {
    margin-bottom: 10px;

    &:hover {
        background-color: red;
    }
}
</style>