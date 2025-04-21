<template>
  <div
    id="mySidenav"
    :class="['sidenav', { close_nav: close }, { openNav: !close }]"
  >
    <ul class="sidebar-body">
      <li
        :class="[
          'sidebar-body-ele',
          { 'common-display': close },
          { 'common-display-align': !close },
        ]"
        style="background-color: rgba(167, 196, 255, 0.5); border-radius: 4px"
      >
        <div class="image2Sidebar"></div>
        <a href="#" class="title-sidebar">Tổng quan </a>
      </li>
      <li>
        <router-link
          :class="['sidebar-body-ele', { 'common-display': close }]"
          style="display: flex"
          to="/report"
        >
          <div class="image3Sidebar"></div>

          <a class="title-sidebar"> Tiền mặt </a>
        </router-link>
      </li>
      <li>
        <router-link
          to="/"
          :class="['sidebar-body-ele', { 'common-display': close }]"
          style="display: flex"
        >
          <div class="image4Sidebar"></div>
          <a class="title-sidebar">Tiền gửi </a>
        </router-link>
      </li>
      <li
        :class="[
          'sidebar-body-ele',
          { 'common-display': close },
          { 'common-display-align': !close },
        ]"
      >
        <div class="image5Sidebar"></div>
        <a class="title-sidebar">Mua hàng </a>
      </li>
      <li
        :class="[
          'sidebar-body-ele',
          { 'common-display': close },
          { 'common-display-align': !close },
        ]"
        style="padding: 12px 12px 12px 8px"
      >
        <div class="image6Sidebar"></div>
        <a class="title-sidebar">Bán hàng </a>
      </li>
      <li
        :class="[
          'sidebar-body-ele',
          { 'common-display': close },
          { 'common-display-align': !close },
        ]"
      >
        <div class="image7Sidebar"></div>
        <a class="title-sidebar" style="width: 170px">Quản lý hóa đơn </a>
      </li>
      <li
        :class="[
          'sidebar-body-ele',
          { 'common-display': close },
          { 'common-display-align': !close },
        ]"
      >
        <div class="image8Sidebar"></div>
        <a class="title-sidebar">Kho </a>
      </li>
      <li
        :class="[
          'sidebar-body-ele',
          { 'common-display': close },
          { 'common-display-align': !close },
        ]"
        style="align-items: center"
      >
        <div class="image9Sidebar"></div>
        <a class="title-sidebar" style="width: 170px">Công cụ dụng cụ </a>
      </li>
      <li
        :class="[
          'sidebar-body-ele',
          { 'common-display': close },
          { 'common-display-align': !close },
        ]"
      >
        <div class="image10Sidebar"></div>
        <a class="title-sidebar">Tài sản cố định </a>
      </li>
      <li
        :class="[
          'sidebar-body-ele',
          { 'common-display': close },
          { 'common-display-align': !close },
        ]"
      >
        <div class="image11Sidebar"></div>
        <a class="title-sidebar">Thuế </a>
      </li>
      <li
        :class="[
          'sidebar-body-ele',
          { 'common-display': close },
          { 'common-display-align': !close },
        ]"
      >
        <div class="image12Sidebar"></div>
        <a class="title-sidebar">Giá thành </a>
      </li>
      <li
        :class="[
          'sidebar-body-ele',
          { 'common-display': close },
          { 'common-display-align': !close },
        ]"
      >
        <div class="image13Sidebar"></div>
        <a class="title-sidebar">Tổng hợp </a>
      </li>
      <li
        :class="[
          'sidebar-body-ele',
          { 'common-display': close },
          { 'common-display-align': !close },
        ]"
      >
        <div class="image14Sidebar"></div>
        <a class="title-sidebar">Ngân sách </a>
      </li>
      <li
        :class="[
          'sidebar-body-ele',
          { 'common-display': close },
          { 'common-display-align': !close },
        ]"
      >
        <div class="image15Sidebar"></div>
        <a class="title-sidebar">Báo cáo </a>
      </li>
      <li
        :class="[
          'sidebar-body-ele',
          { 'common-display': close },
          { 'common-display-align': !close },
        ]"
      >
        <div class="image16Sidebar"></div>
        <a class="title-sidebar">Phân tích tài chính </a>
      </li>
    </ul>
    <div
      class="sidebar-body sidebar-body-short shortNav"
      :class="{ 'common-display': close }"
      @click="close_nav"
    >
      <div
        :class="[
          { 'open-nav': !close },
          { imageShort1: !close },
          { imageShort2: close },
        ]"
      ></div>
      <a class="title-sidebar">Thu gọn</a>
    </div>
  </div>
</template>

<script>
export default {
  props: ["closeSidebar"],
  emits: ["closeSideFnc"],
  data() {
    return {
      close: this.closeSidebar,
    };
  },
  methods: {
    /*
Tên hàm: đóng sidebar
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 15/01/2024

*/
    close_nav() {
      this.close = !this.close;
      this.$emit("close-sideFnc");
    },
  },
};
</script>


<style>
.close_nav {
  width: 73px;
}
.close_nav .title-sidebar {
  display: none;
  border: 1px solid red;
}
.openNav {
  width: 200px;
}
.open-nav {
  margin-left: 13px;
}

.sidenav {
  height: calc(100% - 56px);
  margin-top: 56px;
  position: fixed;
  z-index: 1;
  bottom: 0;
  left: 0;
  background-color: #041434;
  overflow-x: hidden;
  transition: 0.3s ease-in-out;
}
.sidebar-header,
h2 {
  display: flex;
  align-items: center;
  font-size: 24px;
  padding: 12px;
  justify-content: space-around;
  color: white;
  margin-left: 12px;
}
.sidebar-body {
  padding: 0 12px;
  overflow: auto;
  max-height: 89%;
}
.sidebar-body::-webkit-scrollbar {
  display: none;
}
.sidenav a,
.sidebar-body-ele,
.sidebar-body-short {
  cursor: pointer;

  box-sizing: border-box;
  height: 44px;
  padding: 12px;
  border-radius: 4px;
  text-decoration: none;
  color: #ffffff;
  transition: 0.3s;
  position: relative;
  display: inline-block;
}
.shortNav {
  width: 100%;
  border-top: 1px solid #eeeeee;
  box-sizing: border-box;
  margin-top: 8px;
  position: absolute;
  bottom: 0;
}
.sidebar-body-ele:hover {
  background-color: rgba(167, 196, 255, 0.5);
  border-radius: 4px;
}

.sidenav .closebtn {
  position: absolute;
  top: 0;
  right: 25px;
  font-size: 36px;
  margin-left: 50px;
}

#main {
  transition: margin-left 0.5s;
  padding: 16px 15px 16px 16px;
  background-color: #eeeff1;
  height: 100vh;
}

@media screen and (max-height: 450px) {
  .sidenav {
    padding-top: 15px;
  }
  .sidenav a {
    font-size: 18px;
  }
}

.sidebar-body-ele,
.sidebar-body-short {
  height: 44px;
  display: flex;
  align-items: center;
  margin-bottom: 4px;
}
.header-employee,
.right-header,
.body1-employee {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin: 0px 0 16px 0px;
  background-color: #eeeff1;
}
.add_person {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -930px -202px;
  width: 20px;
  height: 20px;
}
.imageShort1 {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -36px -361px;
  width: 13px;
  height: 13px;
}
.imageShort2 {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -84px -361px;
  width: 8px;
  height: 14px;
}

.image1Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -1157px -32px;
  width: 20px;
  height: 20px;
  margin-top: 5px;
  margin-left: 5px;
  cursor: pointer;
  /* margin: 12px; */
}
.image2Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -33px -1630px;
  width: 16px;
  height: 20px;
}
.image3Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -78px -1631px;
  width: 16px;
  height: 18px;
}
.image4Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -121px -1633px;
  width: 16px;
  height: 18px;
}
.image5Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -165px -1632px;
  width: 16px;
  height: 18px;
}
.imageL1 {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -89px -145px;
  width: 24px;
  height: 24px;
  margin-right: 8px;
  cursor: pointer;
}
.imageL2 {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -147px -147px;
  width: 21px;
  height: 21px;
  cursor: pointer;
}

.form-header0 {
  display: flex;
  justify-content: space-between;
}

.right,
.left {
  display: flex;
  align-items: center;
  justify-content: space-evenly;
  background-color: #ffffff;
}

.import_excel_style {
  padding: 7px;
  background-color: #ffffff;
  border: 1px solid #e0e0e0;
  border-radius: 4px;
  margin-right: 4px;
  height: 36px;
  box-sizing: border-box;
}
.import_excel {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -1265px -146px;
  width: 23px;
  height: 22px;
}
.imgSearchB_style {
  padding: 5px;
  width: 36px;
  background-color: #ffffff;
  border: 1px solid #e0e0e0;
  border-radius: 4px;
  height: 36px;
  box-sizing: border-box;
}

.imgSearchB {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -1098px -90px;
  width: 20px;
  height: 20px;
}
.left-pagi {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -36px -361px;
  width: 8px;
  height: 14px;
  margin-right: 40px;
}
.right-pagi {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -84px -361px;
  width: 8px;
  height: 14px;
}
.icon-delete {
  background: url("../public/assets/Sprites.64af8f61.svg") -832px -462px;
  width: 36px;
  height: 36px;

  margin-right: 16px;
}
.cancel {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -147px -147px;
  width: 18px;
  height: 18px;
  cursor: pointer;
}

/* thêm thuộc tính */

.image6Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -206px -1631px;
  width: 19px;
  height: 20px;
}
.image7Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -254px -1632px;
  width: 17px;
  height: 20px;
}
.image8Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -294px -1632px;
  width: 18px;
  height: 20px;
}
.image9Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -339px -1633px;
  width: 20px;
  height: 20px;
}
.image10Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -382px -1632px;
  width: 16px;
  height: 20px;
}
.image11Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -427px -1634px;
  width: 16px;
  height: 20px;
}
.image12Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -470px -1633px;
  width: 18px;
  height: 20px;
}
.image13Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -514px -1632px;
  width: 16px;
  height: 20px;
}
.image14Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -552px -1636px;
  width: 16px;
  height: 20px;
}
.image15Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -207px -1667px;
  width: 16px;
  height: 20px;
}
.image16Sidebar {
  background: url("../public/assets/Sprites.64af8f61.svg") no-repeat -382px -1664px;
  width: 16px;
  height: 20px;
}
</style>