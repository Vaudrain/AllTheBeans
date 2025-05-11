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
  }),
  getters: {
    getBeans: (state) => state.list,
    getSelectedBeanIndex: (state) => state.selectedBeanIndex,
    getBeanOfTheDayIndex: (state) => state.beanOfTheDayIndex,
    getBeanOfTheDay: (state) => state.list.find((bean) => bean.index === state.beanOfTheDayIndex),
  },
  actions: {
    setBeans(beans: Bean[]) {
      this.list = beans
    },
    setSelectedBeanIndex(index: number) {
      this.selectedBeanIndex = index
    },
    setBeanOfTheDayIndex(index: number) {
      this.beanOfTheDayIndex = index
    },
  },
})