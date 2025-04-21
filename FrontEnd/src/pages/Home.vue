<template>
  <div>
    <the-navbar></the-navbar>
    <div
      id="main"
      :class="[{ 'open-nav': !close }, { 'close-sidebar': close }]"
    >
      <the-sidebar
        class="sidebar"
        :close-sidebar="close"
        @close-sideFnc="closeSidebar"
      >
      </the-sidebar>
      <router-view :close-sidebar="close"></router-view>
    </div>
  </div>
  <!-- <dropdown></dropdown> -->
</template>

<script>
import TheNavbar from "../layouts/navbar/TheNavbar.vue";
import TheSidebar from "../layouts/sidebars/TheSidebar.vue";
export default {
  data() {
    return {
      close: false,
    };
  },
  provide() {
    return {
      language: "VN",
    };
  },
  methods: {
    /*
Tên hàm: đóng sidebar
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 15/01/2024
*/
    closeSidebar() {
      this.close = !this.close;
    },
  },

  components: { TheNavbar, TheSidebar },
};
</script>

<style scoped>
.open-nav {
  margin-left: 198px;
  height: calc(100% - 56px);

  margin-top: 56px;
}
.close-sidebar {
  margin-left: 70px;
  margin-top: 56px;
  height: calc(100% - 56px);
}
</style>