<template>
  <div>
    <h1>{{ this.MisaEmployee["VN"].import.resultImport }}</h1>
    <p>
      {{ this.MisaEmployee["VN"].import.downloadImport }}
      <a href="#">{{ this.MisaEmployee["VN"].import.here }}</a>
    </p>
    <ul>
      <li style="line-height: 30px">
        {{ this.MisaEmployee["VN"].import.successImport }}
        {{ employeeImport?.CountSuccess ? employeeImport?.CountSuccess : 0 }}
      </li>
      <li>
        {{ this.MisaEmployee["VN"].import.failImport }}
        {{ employeeImport?.CountFail ? employeeImport?.CountFail : 0 }}
      </li>
    </ul>
  </div>
</template>

<script>
import refreshToken from "../../contexts/GetApiContext2.js";
import { importEmployeeDatabase } from "../../services/EmployeeService.js";
export default {
  props: ["dataImport", "idImport"],
  data() {
    return {
      employeeImport: this.$store.getters.getExcelData,
    };
  },
  async mounted() {
    await this.callAPIImport();
  },
  methods: {
    /*
Tên hàm: Goi api import vào database 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    async callAPIImport() {
      try {
        const response = await refreshToken(
          importEmployeeDatabase,
          this.idImport
        );
      } catch (error) {
        console.error("Error:", error);
      }
    },
  },
};
</script>

<style scoped>
</style>