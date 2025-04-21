<template>
  <div
    class="headerSearch head common-display"
    style="justify-content: space-between"
  >
    <div class="container-search" style="width: 70%">
      <base-input-2
        style="margin-right: 20px; width: 30%"
        type="search"
        @keydown.enter="search_employee"
        :placeholder="this.MisaEmployee[language].tableEmployee.search"
      >
        <template #icon>
          <img
            width="26"
            height="26"
            src="../../../../public/assets/icons/Layer 2.svg"
            class="searchV1"
          />
        </template>
      </base-input-2>
      <div class="left" v-if="rowChoosed.size !== 0">
        <base-button-2
          :content="this.MisaEmployee[language].tableEmployee.choosed"
          hoverGray="true"
        >
          <template #contentDynamic>
            <span id="numberChoose" style="font-weight: bold">
              &nbsp;&nbsp;&nbsp;{{ rowChoosed.size }}</span
            >
          </template>
        </base-button-2>
        <base-button-2
          :content="this.MisaEmployee[language].tableEmployee.unChoosed"
          @click="unChoosed"
          hoverGray="true"
        >
        </base-button-2>
        <base-button-2
          :content="this.MisaEmployee[language].tableEmployee.deleteAll"
          @click="open_modal_employee('modal-delete', 3)"
          hoverGray="true"
        >
          <template #image>
            <img
              width="30"
              height="30"
              src="../../../../public/assets/icons/waste.png"
              alt="waste"
            />
          </template>
        </base-button-2>
      </div>
    </div>
    <div class="right" style="width: 350px" title="I Haven't done">
      <div class="container-search">
        <base-input-2
          :placeholder="this.MisaEmployee[language].phCompany"
          ref="searchCompany"
          ><template #icon>
            <img
              width="26"
              height="26"
              src="../../../../public/assets/icons/Layer 2.svg"
              alt="search--v1"
              class="searchV1"
            /> </template
        ></base-input-2>
      </div>

      <!-- <span
        class="import_excel_style active"
        style="cursor: pointer"
        @click="exportExcel"
        :title="this.MisaEmployee[language].exportExcel"
      >
        <div class="import_excel"></div>
      </span> -->

      <span
        class="import_excel_style active"
        style="padding: 3px; cursor: pointer"
        :title="this.MisaEmployee[language].exportExcel"
      >
        <img
          @click="exportExcel"
          width="28"
          height="28"
          src="../../../../public/assets/icons/export3.png"
          alt="import"
        />
      </span>
      <span
        class="import_excel_style active"
        style="cursor: pointer"
        :title="this.MisaEmployee[language].importExcel"
      >
        <router-link to="/import">
          <img
            width="22"
            height="21"
            src="../../../../public/assets/icons/import3.png"
            alt="import"
          />
        </router-link>
      </span>

      <span class="import_excel_style active" @click="openSetting('setting')">
        <img
          width="21"
          height="21"
          src="../../../../public/assets/icons/settings.png"
        />
      </span>
      <employee-setting
        style="margin-top: 20px"
        v-if="setting"
        @open-setting="openSetting"
        @choose-column="chooseColumn"
        @default-column="defaultColumn"
        :columnGhim="columnGhim"
        :columnChoosed="columnChoosed"
      ></employee-setting>
      <span
        class="imgSearchB_style active"
        style="position: relative"
        :title="this.MisaEmployee[language].reload"
      >
        <!-- <div class="imgSearchB"></div> -->
        <img
          width="21"
          height="21"
          @click="reloadPage"
          style="cursor: pointer"
          src="../../../../public/assets/icons/reload.png"
        />
      </span>
    </div>
  </div>
</template>

<script>
import BaseInput2 from "../../components/commons/inputs/BaseInput2.vue";
import {
  exportEmployee,
  exportEmployees,
} from "../../services/EmployeeService";
import refreshToken from "../../contexts/GetApiContext2";
import EmployeeSetting from "../../components/users/employees/EmployeeSetting.vue";
export default {
  components: { BaseInput2, EmployeeSetting },
  props: ["rowChoosed"],
  emits: ["search-employee", "open_modal_employee", "unChoosed", "reload"],
  data() {
    return {
      language: "VN",
      setting: false,
      columnGhim: new Set(),
      columnChoosed: new Set(),
    };
  },
  methods: {
    /*
Tên hàm: Đặt mặc định cột 
Người viết: Đặng Đình Quốc Khánh
*/
    defaultColumn() {
      this.columnGhim = new Set();
      this.columnChoosed = new Set();
    },
    /*
Tên hàm: bôi màu cho hàng và cho tự checked khi được chọn ở customise
Người viết: Đặng Đình Quốc Khánh
Tham số: id của hàng muốn tô màu và action là loại của hành động làm gì. 
Ngày viết: 12/12/2023

*/
    chooseColumn(id, action, event) {
      try {
        if (action === "choose") {
          if (event.target.checked) {
            this?.columnChoosed.add(id);
          } else {
            this?.columnChoosed.delete(id);
          }
        } else {
          let check = this.columnGhim.size > 0 && this.columnGhim.has(id);
          // nếu là false thì không có trong columnGhim
          if (!check) {
            this?.columnGhim.add(id);
          } else {
            this?.columnGhim.delete(id);
          }
        }
      } catch (error) {
        console.error(error);
      }
    },
    /*
Tên hàm: mở thành true 
Tham số: msg là dữ liệu muốn cập nhật thành true
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/6/2023

*/
    openSetting(data) {
      this[data] = !this[data];
    },
    /*
Tên hàm: kiểm tra component và đặt tham số num để biết action
Tham số:+ cmp là tên component muốn render
        + num là loại của action 
        + clone là check xem có ấn chức năng nhân bản không 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/6/2023

*/
    open_modal_employee(cmp, num, clone = false) {
      try {
        this.$emit("open_modal_employee", "modal-delete", 3);
      } catch (error) {
        console.error(error);
      }
    },
    /*
Tên hàm: đưa giá trị search sang bên cha 
Tham số: event là sự kiện được gửi tới 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023

*/
    search_employee(event) {
      try {
        this.$emit("search-employee", event.target.value);
      } catch (error) {
        console.error(error.message);
      }
    },
    /*
Tên hàm:Export excel 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    async exportExcel() {
      try {
        // nếu không có mảng id  (bodyDelete ) thì checkAction là 1;
        // let checkData;
        let checkData;
        let body, response;
        if (!this.rowChoosed || this.rowChoosed.size === 0) {
          checkData = 1;
          response = await refreshToken(exportEmployees, checkData);
        } else {
          body = [...this.rowChoosed];
          response = await refreshToken(exportEmployee, body);
        }
        if (response.status === 200) {
          const blob = new Blob([response.data], {
            type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
          });

          // tạo link download
          const link = document.createElement("a");
          link.href = window.URL.createObjectURL(blob);
          link.download = `Misa_Employee_${new Date().toLocaleDateString()}.xlsx`;
          link.click();
        } else {
          console.error(`Error: ${response.status} - ${response.statusText}`);
        }
      } catch (error) {
        console.error("Error:", error.message);
      }
    },

    /*
Tên hàm: bỏ chọn
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    unChoosed() {
      this.$emit("unChoosed");
    },
    /*
Tên hàm: load lai trang 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    reloadPage() {
      this.$emit("reload");
    },
  },
};
</script>

<style scoped>
@import url(@/css/users/employee-table.css);
</style>