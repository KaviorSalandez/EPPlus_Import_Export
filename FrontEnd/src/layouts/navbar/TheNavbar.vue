<template>
  <div id="navbar-head">
    <div class="navbar">
      <div class="navbar-left common-display-align">
        <div class="image1Sidebar" onclick="openNav()"></div>
        <router-link to="/home" style="text-decoration: none">
          <img
            src="../../../../public/assets/favicon.ico"
            style="cursor: pointer"
            class="favicon"
        /></router-link>
        <span class="title-header">
          <router-link to="/home" style="text-decoration: none">
            <span style="color: black">
              {{ this.MisaEmployee["VN"].navBar.nameWeb }}
            </span></router-link
          >
        </span>

        <base-input-2
          style="width: 25%; margin-left: 10px"
          type="search"
          @keydown.enter="search_employee"
          :placeholder="this.MisaEmployee['VN'].tableEmployee.search"
          ref="searchCompany"
        >
          <template #icon>
            <img
              width="26"
              height="26"
              class="searchV1"
              src="../../../../../public/assets/icons/Layer 2.svg"
              alt="search--v1"
            />
          </template>
        </base-input-2>
        <!-- </div> -->
      </div>
      <div class="navbar-right">
        <img
          src="../../../../public/assets/icons/down_up.svg"
          style=""
          class="down-up"
        />
        <div class="notify">
          <img src="../../../../public/assets/icons/bell.png" class="bell" />
          <img
            width="18"
            class="notify-circle"
            src="../../../../public/assets/icons/2-circle.png"
          />
        </div>
        <img
          src="../../../../public/assets/icons/question.svg"
          class="icon-question"
        />
        <img
          width="40"
          @click="openAccountMenu"
          class="user-image"
          src="../../../../public/assets/icons/person.png"
          alt="user-male-circle"
        />
        <the-accountmenu
          v-if="accountMenu"
          :account="account"
        ></the-accountmenu>
      </div>
    </div>
  </div>
</template>

<script>
import TheAccountmenu from "../../components/commons/menus/TheAccountmenu.vue";

import BaseInput2 from "../../components/commons/inputs/BaseInput2.vue";
export default {
  components: { BaseInput2, TheAccountmenu },
  data() {
    return {
      accountMenu: false,
      account: null,
    };
  },
  methods: {
    /*
Tên hàm: mở account menu
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2023
*/
    openAccountMenu() {
      this.accountMenu = !this.accountMenu;
      let account = this.$store.getters.getAccount;
      this.account = JSON.parse(account);
    },
  },
};
</script>

<style scoped>
@import url(../../css/commons/navbar.css);
</style>
