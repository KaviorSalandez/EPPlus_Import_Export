<template>
  <base-loading v-if="loading"></base-loading>
  <router-view></router-view>
</template>

<script>
import BaseLoading from "./components/commons/modals/BaseLoading.vue";
export default {
  components: { BaseLoading },
  data() {
    return {
      loading: this.$store.getters.getLoading,
    };
  },
  watch: {
    /*
Tên biến: Theo dõi store loading 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    "$store.getters.getLoading"(newVal) {
      this.loading = newVal;
    },
  },
};
</script>
