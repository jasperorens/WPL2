import { defineStore } from 'pinia'

export const useWidthStore = defineStore({
    id: 'widthStore',
    state: () => ({
        width: 0
    }),
    actions: {
        setWidth(newWidth) {
            this.width = newWidth
        }
    }
})
