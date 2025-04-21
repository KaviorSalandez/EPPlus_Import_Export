<template>
  <the-navbar></the-navbar>
  <div class="import" style="margin-top: 56px">
    <div
      class="import-container"
      style="display: flex; justify-content: space-between"
    >
      <div class="import-container-left">
        <ul>
          <li>1. {{ this.MisaEmployee["VN"].import.chooseImport }}</li>
          <li>2.{{ this.MisaEmployee["VN"].import.checkData }}</li>
          <li>3. {{ this.MisaEmployee["VN"].import.resultImport }}</li>
        </ul>
      </div>
      <div class="import-container-right">
        <router-view
          :form-data="formData"
          :idImport="idImport"
          :checkImport="checkImport"
          :success="success"
          @set-dataImport="setDataImport"
          @set-dataImport2="setDataImport2"
        ></router-view>
      </div>
    </div>
    <div
      class="footer common-display"
      style="justify-content: space-between; margin-top: 1vh"
    >
      <base-button-2
        style="margin-left: 16px; border: 1px solid #e0e0e0"
        :content="MisaEmployee['VN'].import.help"
        hoverGray="true"
      >
        <template #image>
          <img
            width="25"
            height="25"
            style="margin-right: 4px"
            src="../../public/assets/icons/questionMark2.png"
          />
        </template>
      </base-button-2>

      <div class="right">
        <base-button-2
          style="margin-right: 16px; border: 1px solid #e0e0e0"
          @click="rollBackRoute"
          :content="MisaEmployee['VN'].import.rollBack"
          hoverGray="true"
        >
          <template #image>
            <img
              width="25"
              height="25"
              src="../../public/assets/icons/rollback.png"
              style="margin-right: 5px"
            />
          </template>
        </base-button-2>

        <base-button-2
          style="margin-right: 16px; border: 1px solid #e0e0e0"
          @click="nextRoute()"
          :disabled="!success"
          :content="MisaEmployee['VN'].import.pratice"
          hoverGray="true"
        >
          <template #image>
            <img
              width="25"
              height="25"
              src="../../public/assets/icons/setting.png"
              style="margin-right: 5px"
            />
          </template>
        </base-button-2>
        <router-link to="/">
          <base-button-2
            style="margin-right: 16px; border: 1px solid #e0e0e0"
            :content="MisaEmployee['VN'].import.cancel"
            hoverGray="true"
          >
            <template #image>
              <img
                width="25"
                height="25"
                src="../../public/assets/icons/cancel.png"
                style="margin-right: 5px"
              />
            </template>
          </base-button-2>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
import TheNavbar from "../layouts/navbar/TheNavbar.vue";

export default {
  components: { TheNavbar },
  data() {
    return {
      linkFile: "",
      formData: {},
      currentFullPath: this.$route.path,
      idImport: "",
      checkImport: false,
      success: true,
    };
  },
  methods: {
    /*
Tên hàm: gán dữ liệu để truyển sang table import 
param: formData là lấy từ file import, checkImport: kiểm tra xem đã import chưa 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    setDataImport(formData, checkImport) {
      this.formData = formData;
      this.checkImport = checkImport;
    },
    /*
Tên hàm: lấy id import để import vào database
param: idImport là giá trị lấy từ redis cache
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    setDataImport2(idImport, success) {
      this.success = success;
      console.log(this.success);
      this.idImport = idImport;
    },
    /*
Tên hàm: xử lý chuyển router mỗi lần thực hiện step
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    nextRoute() {
      const currentFullPath = this.$route.path;
      if (currentFullPath == "/import/import-data") {
        this.$router.push("/import/table-data");
      } else {
        this.$router.push("/import/result-data");
      }
    },
    /*
Tên hàm:Quay trở lại khi thực hiện step 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    rollBackRoute() {
      const currentFullPath = this.$route.path;
      if (currentFullPath == "/import/result-data") {
        this.$router.push("/import/table-data");
      } else if (currentFullPath == "/import/table-data") {
        this.success = true;
        this.$router.push("/import/import-data");
      } else {
        this.$router.push("/");
      }
    },
  },
};
</script>

<style scoped>
@import url(../css/users/import-employee.css);
</style>
