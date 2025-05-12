import { defineStore } from 'pinia'

export interface Bean {
  index: number,
  name: string,
  description: string,
  image: string,
  colour: string,
  country: string,
  costGBP: number
}

export const useBeanStore = defineStore('beans', {
  state: () => ({
    list: [] as Bean[],
    selectedBeanIndex: -1,
    beanOfTheDayIndex: -1,
    basket: [] as {bean: Bean, quantity: number}[],
  }),
  getters: {
    getBeans: (state) => state.list,
    getSelectedBeanIndex: (state) => state.selectedBeanIndex,
    getSelectedBean: (state) => state.list.find((bean) => bean.index === state.selectedBeanIndex),
    getBeanOfTheDayIndex: (state) => state.beanOfTheDayIndex,
    getBeanOfTheDay: (state) => state.list.find((bean) => bean.index === state.beanOfTheDayIndex),
    getBasket: (state) => state.basket,
  },
  actions: {
    setBeans(beans: Bean[]) {
      this.list = beans
    },
    setSelectedBeanIndex(index: number) {
      this.selectedBeanIndex = index;
    },
    setBeanOfTheDayIndex(index: number) {
      this.beanOfTheDayIndex = index
    },
    addToBasket(bean: Bean, quantity: number) {
      const existingItem = this.basket.find(item => item.bean.index === bean.index);
      if (existingItem) {
        existingItem.quantity += quantity;
      } else {
        this.basket.push({ bean, quantity });
      }
    },
    removeFromBasket(bean: Bean, quantity: number) {
      const existingItem = this.basket.find(item => item.bean.index === bean.index);
      if (existingItem) {
        if (quantity === -1)  {
          this.basket = this.basket.filter(item => item.bean.index !== bean.index);
        } else {
          existingItem.quantity -= quantity;
          if (existingItem.quantity <= 0) {
            this.basket = this.basket.filter(item => item.bean.index !== bean.index);
          }
        }
      }
    },
    updateBasketItemQuantity(bean: Bean, quantity: number) {  
      const existingItem = this.basket.find(item => item.bean.index === bean.index);
      if (existingItem) {
        existingItem.quantity = quantity;
        if (existingItem.quantity <= 0) {
          this.basket = this.basket.filter(item => item.bean.index !== bean.index);
        }
      }
    },
    clearBasket() {
      this.basket = []
    },
  },
})