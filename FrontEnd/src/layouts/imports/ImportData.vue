<template>
  <div>
    <p>{{ this.MisaEmployee["VN"].import.row1 }}</p>
    <div class="choose" style="display: flex">
      <div
        class="path-import common-display"
        style="justify-content: flex-start; padding-left: 15px"
      >
        {{ linkFile }}
      </div>
      <input
        type="file"
        ref="fileImport"
        hidden
        name=""
        id="file"
        @change="handleFileChange"
      />
      <button type="file" @click="importExcel">
        {{ this.MisaEmployee["VN"].import.choose }}
      </button>
    </div>
    <p style="color: green; font-size: 20px">{{ checkSuccess }}</p>
    <br />
    <p>
      {{ this.MisaEmployee["VN"].import.row2 }}
      <a href="#" @click="exportExcel">{{
        this.MisaEmployee["VN"].import.here
      }}</a>
    </p>
  </div>
</template>

<script>
import refreshToken from "../../contexts/GetApiContext2.js";
import { importEmployee, exportRaw } from "../../services/EmployeeService.js";
export default {
  props: ["dataImport"],
  emits: ["set-dataImport"],
  data() {
    return {
      linkFile: "",
      checkSuccess: "",
    };
  },
  mounted() {},
  methods: {
    /*
Tên hàm: xử lý khi ấn nut chọn 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    importExcel() {
      this.$refs.fileImport.click();
    },
    /*
Tên hàm: Xử lý khi file thay đổi
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    async handleFileChange(event) {
      // Xử lý sự kiện thay đổi file ở đây
      const selectedFiles = event.target.files;
      // nếu file được chọn có dữ liệu thì set vào trong dataImport
      if (selectedFiles.length > 0) {
        const selectedFile = selectedFiles[0];
        const formData = new FormData();
        formData.append("fileImport", selectedFile);
        this.linkFile = selectedFile.name;
        this.$emit("set-dataImport", formData, true);
        this.checkSuccess = this.MisaEmployee["VN"].successImport;
        this.$store.commit("setExcelData", null);
      }
    },

    /*
Tên hàm:Export excel 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    async exportExcel(event) {
      try {
        event.preventDefault();
        // Make the GET request using Axios
        const response = await refreshToken(exportRaw);
        console.log(response);
        if (response.status === 200) {
          const blob = new Blob([response.data], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          });

          const link = document.createElement("a");
          link.href = window.URL.createObjectURL(blob);
          link.download = `Misa_Raw_Employee_${new Date().toLocaleDateString()}.xlsx`;
          link.click();
          URL.revokeObjectURL(link.href);
          this.checkSuccess = this.MisaEmployee["VN"].successExport;
        } else {
          console.error(`Error: ${response.status} - ${response.statusText}`);
        }
      } catch (error) {
        console.error("Error:", error.message);
      }
    },
  },
};
</script>

<style scoped>
@import url(../../css/users/import-employee.css);
</style>
