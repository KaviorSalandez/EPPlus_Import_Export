<template>
  <div class="misa-user-account-menu" style="display: block">
    <i class="icon-align"></i>
    <ul class="misaid-popup-menu">
      <li class="misaid-user-details">
        <div class="misaid-user-avatar">
          <img
            class="misaid-avatar-img"
            src="../../../../public/assets/icons/person.png"
          />
        </div>
        <div class="misaid-user-info">
          <div class="misaid-user-fullname">
            {{ account?.FullName }}
          </div>
          <div class="misaid-user-account">
            {{ account?.Email }}
          </div>
        </div>
      </li>
      <li
        class="misaid-menu-account-item misaid-menu-item extend-item has-icon"
        type="changepassword"
      >
        <i
          bg="Key"
          class="misaid-user-menu-icon"
          style="background-image: url('../../public/assets/icons/key.svg')"
        ></i
        ><label style="line-height: 42px" res-key="AccountMenu_ChangePassword">
          {{ this.MisaEmployee["VN"].accountMenu.changePassword }}</label
        >
      </li>
      <li
        class="misaid-menu-account-item misaid-menu-item extend-item has-icon"
        type="info"
      >
        <i
          bg="Account"
          class="misaid-user-menu-icon"
          style="background-image: url('../../public/assets/icons/account.svg')"
        ></i
        ><label style="line-height: 42px" res-key="AccountMenu_AccountSetting">
          {{ this.MisaEmployee["VN"].accountMenu.createAccount }}</label
        >
      </li>
      <li
        class="misaid-menu-account-item misaid-menu-item extend-item has-icon"
        type="info"
      >
        <i
          bg="Account"
          class="misaid-user-menu-icon"
          style="
            background-image: url('../../public/assets/icons/protectAccount.svg');
          "
        ></i
        ><label style="line-height: 42px" res-key="AccountMenu_AccountSetting">
          {{ this.MisaEmployee["VN"].accountMenu.security }}
        </label>
      </li>
      <li
        class="misaid-menu-account-item misaid-menu-item extend-item has-icon"
        type="info"
      >
        <i
          bg="Account"
          class="misaid-user-menu-icon"
          style="background-image: url('../../public/assets/icons/payment.svg')"
        ></i
        ><label
          style="line-height: 42px"
          res-key="AccountMenu_AccountSetting"
          >{{ this.MisaEmployee["VN"].accountMenu.licence }}</label
        >
      </li>
      <li
        class="misaid-menu-account-item misaid-menu-item extend-item has-icon"
        type="info"
      >
        <i
          bg="Account"
          class="misaid-user-menu-icon"
          style="background-image: url('../../public/assets/icons/gift.svg')"
        ></i
        ><label style="line-height: 42px" res-key="AccountMenu_AccountSetting">
          {{ this.MisaEmployee["VN"].accountMenu.gift }}
        </label>
      </li>

      <li class="misaid-menu-item-logout" @click="logout">
        <p style="margin: 0">
          <label
            bg="Logout"
            class="misaid-user-menu-icon misaid-icon-logout"
            res-key="AccountMenu_Logout_Name"
            style="
              background-image: url('../../public/assets/icons/logout.svg');
            "
            >{{ this.MisaEmployee["VN"].accountMenu.logout }}</label
          >
        </p>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  props: ["account"],
  data() {
    return {};
  },
  methods: {
    /*
Tên hàm: chuyển hướng trang khi logout 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    logout() {
      this.$store.dispatch("clearToken");
      this.$router.replace("/login");
    },
  },
};
</script>


<style scoped>
.misaid-menu-item-logout:hover {
  background-color: #f2f2f2;
  cursor: pointer;
}
.misa-user-account-menu li.misaid-menu-item:hover {
  background-color: #f2f2f2;
}
.misaid-menu-item-logout .misaid-icon-logout {
  background-position: left;
  background-repeat: no-repeat;
  background-size: 24px;
  padding: 8px 32px;
  text-align: unset;
  display: unset !important;
}
.misaid-menu-item-logout {
  border-top: 1px solid #e0e0e0;
  /* height: 44px; */
  line-height: 44px;
  text-align: center;
  margin-top: 16px;
}
.misaid-popup-menu label {
  font-weight: normal !important;
}
label {
  display: inline-block;
  margin-bottom: 0.5rem;
}
.misa-user-account-menu li.misaid-menu-item i.misaid-user-menu-icon {
  height: 40px;
  width: 40px;

  background-position: 8px center !important;
  background-repeat: no-repeat;
  background-size: 24px;
  display: block;
}
.misa-user-account-menu li.misaid-menu-item {
  height: 40px;
  line-height: 42px;
  margin: 0 16px;
  border-radius: 3px;
  color: #1f1f1f;
  cursor: pointer;
  display: flex;
}
.misaid-user-account {
  font-size: 13px;
  color: #666;
  line-height: 19px;
}
.misaid-user-fullname {
  margin-bottom: 4px;
  font-size: 20px;
  font-weight: bold;
  padding: 0 24px;
  line-height: 30px;
}
.misaid-user-info {
  text-align: center;
  margin-bottom: 16px;
  margin-top: 16px;
}
.misaid-user-avatar {
  height: 64px;
  width: 64px;
  margin: 0 auto;
  margin-top: 24px;
}
.misaid-avatar-img {
  border-radius: 50%;
  height: 64px;
  width: 64px;
  display: block;
}
.misaid-popup-menu {
  font-size: 14px;
  text-align: left;
  list-style: none;
  margin: 2px 0 0;
  padding: 0;
}

.misa-user-account-menu {
  position: absolute;
  top: calc(100% + 5px);
  right: 10px;
  left: auto;
  z-index: 5000000;
  float: left;
  cursor: default;
  color: #1f1f1f;
  background-color: #fff;
  border-radius: 4px;
  -webkit-box-shadow: 0 3px 16px rgba(0, 0, 0, 0.16);
  width: 302px;
  -webkit-background-clip: padding-box;
  -moz-background-clip: padding-box;
  -moz-animation-name: fadeIn;
  -o-animation-name: fadeIn;
  -webkit-animation-name: fadeIn;
  -webkit-animation-duration: 0.5s;
  -moz-animation-duration: 0.5s;
  -o-animation-duration: 0.5s;
  -webkit-animation-fill-mode: both;
  -moz-animation-fill-mode: both;
  -o-animation-fill-mode: both;
}
.icon-align {
  width: 10px;
  height: 10px;
  display: block;
  right: 19px;
  top: -5px;
  position: absolute;
  background-color: #fff;
  -moz-transform: rotate(45deg);
  -ms-transform: rotate(45deg);
  -o-transform: rotate(45deg);
  transform: rotate(45deg);
}
</style>