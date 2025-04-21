<template>
  <div>
    <div style="display: flex">
      <h4 style="margin: 10px 200px 10px 0px; font-weight: normal">
        {{ countSuccess }} / {{ sum }}
        {{ this.MisaEmployee["VN"].import.validImport }}
      </h4>
      <h4 style="margin: 10px; font-weight: normal">
        {{ countFail }} /
        {{ sum }}
        {{ this.MisaEmployee["VN"].import.invalidImport }}
      </h4>
    </div>
    <div class="scrollable-table">
      <table>
        <thead>
          <tr>
            <th class="filter">
              {{ this.MisaEmployee["VN"].formEmployee.nameCode }}
            </th>
            <th>{{ this.MisaEmployee["VN"].formEmployee.nameFullname }}</th>
            <th>{{ this.MisaEmployee["VN"].formEmployee.gender }}</th>

            <th>{{ this.MisaEmployee["VN"].formEmployee.nameDOB }}</th>
            <th>{{ this.MisaEmployee["VN"].formEmployee.nameDepartment }}</th>
            <th>{{ this.MisaEmployee["VN"].formEmployee.position }}</th>

            <th>{{ this.MisaEmployee["VN"].formEmployee.numberBank }}</th>
            <th>{{ this.MisaEmployee["VN"].formEmployee.nameBank }}</th>
            <th>{{ this.MisaEmployee["VN"].import.status }}</th>
          </tr>
        </thead>
        <tbody v-if="setLoading === false">
          <tr v-for="employee in employeeImport" :key="employee.EmployeeId">
            <td>{{ employee.EmployeeCode }}</td>
            <td>{{ employee.EmployeeName }}</td>
            <td>
              {{
                employee.Gender == 0
                  ? "Nam"
                  : employee.Gender == 1
                  ? "Nữ"
                  : "Khác"
              }}
            </td>
            <td>
              {{
                employee?.DOB ? format_date(employee?.DOB, "dd-mm-yyyy") : ""
              }}
            </td>
            <td>{{ employee.PositionName }}</td>
            <td>{{ employee.DepartmentName }}</td>
            <td>{{ employee.BankAccount }}</td>
            <td>{{ employee.BankName }}</td>
            <td v-if="employee?.Errors?.length > 0">
              <li
                style="list-style-type: decimal; color: red"
                v-for="(error, index) in employee?.Errors"
                :key="index"
              >
                {{ error }} <br />
              </li>
            </td>
            <td v-else>
              <li style="list-style: order">
                {{ this.MisaEmployee["VN"].import.valid }} <br />
              </li>
            </td>
          </tr>
        </tbody>
      </table>
      <div
        class="tbody-loading common-display"
        style="height: 50vh; width: 100%"
        v-if="setLoading === true"
      >
        <img
          class="loading"
          src="../../../../public/assets/loading.svg"
          alt=""
          srcset=""
        />
      </div>
    </div>
  </div>
  <div class="modal" id="myModal" v-if="check_cancel">
    <modal-delete
      @close-modal="rollStep1"
      :check-cancel="check_cancel"
      check-validate="true"
    >
      <template #header_delete>
        {{ this.MisaEmployee["VN"].dataValid }}
      </template>

      <template #title_delete>
        {{ error }}
      </template>

      <template #button>
        <base-button-2
          style="margin-right: 16px"
          @click="rollStep1"
          :content="this.MisaEmployee['VN'].close"
          borderNone="true"
        ></base-button-2>
      </template>
    </modal-delete>
  </div>
</template>
<script>
import { importEmployee } from "../../services/EmployeeService.js";
import store from "../../store/store.js";
import ModalDelete from "../../components/commons/modals/ModalDelete.vue";
export default {
  inject: ["language"],
  components: { ModalDelete },
  props: ["dataImport", "dataCalled", "check", "formData", "checkImport"],
  emits: ["set-dataImport2"],
  mounted() {},
  data() {
    return {
      formData: this?.formData,
      countSuccess: 0,
      countFail: 0,
      employeeImport: [],
      sum: 0,
      setLoading: true,
      check_cancel: false,
      error: "",
      filterCheck: false,
      departments: [],
    };
  },
  async mounted() {
    await this.callAPIImport();
  },
  methods: {
    /*
Tên hàm: Lọc dữ liệu chức danh
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    checkPressFilter() {
      this.filterCheck = !this.filterCheck;
    },
    /*
Tên hàm: Gọi api import
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    async callAPIImport() {
      try {
        let accessToken = store.getters.getToken;
        let excelData = this.$store.getters.getExcelData;
        // nếu chưa có file nào được import
        if (!this.checkImport) {
          this.check_cancel = true;
          this.error = this.MisaEmployee["VN"].import.requiredImport;
          return;
        }
        // nếu có file được chọn rồi
        if (!excelData) {
          await importEmployee(accessToken, this.formData)
            .then((response) => {
              const { CountSuccess, CountFail, EmployeeImportDtos, IdImport } =
                response.data;
              this.countFail = CountFail;
              this.countSuccess = CountSuccess;
              this.employeeImport = EmployeeImportDtos;

              this.$store.commit("setExcelData", response.data);
              let success = CountSuccess === 0 ? false : true;
              this.$emit("set-dataImport2", IdImport, success);
              this.sum = this.countSuccess + this.countFail;
              this.setLoading = false;
            })
            .catch((err) => {
              console.log(err);
              this.check_cancel = true;
              this.error = err?.data?.errors[0]?.ErrorMessage
                ? err?.data?.errors[0]?.ErrorMessage
                : err?.data?.errors[0];
            });
          // nếu file đấy có sắn rồi thì lấy ra luôn
        } else {
          this.employeeImport = excelData?.EmployeeImportDtos;

          this.countSuccess = excelData?.CountSuccess;
          this.countFail = excelData?.CountFail;
          this.sum = this.countSuccess + this.countFail;
          this.setLoading = false;
        }
      } catch (error) {
        console.error("Error:", error);
      }
    },
    /*
Tên hàm: Quay lại bước 1 và tắt modal
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    rollStep1() {
      this.check_cancel = false;
      this.$router.push("/import/import-data");
    },
  },
};
</script>
<style scoped>
.contentFilter {
  position: relative;
  padding: 16px;
}
.footerFilter {
  background: #f5f5f5;
  position: absolute;
  bottom: 0;
  height: 56px;
  width: 100%;
  display: flex;
  justify-content: flex-end;
  align-items: center;
}
.headerFilter {
  margin-bottom: 16px;
}
.scrollable-table {
  overflow-x: auto;
  height: 81vh; /* Set an appropriate height */
}
.filterTest {
  width: 312px;
  min-height: 200px;
  background-color: #ffffff;
  position: absolute;
  top: 50px;
  z-index: 3;
}
.filter {
  position: relative;
}
table {
  border-collapse: collapse;
  width: 100%;
}

th,
td {
  border: 1px solid #dddddd;
  text-align: left;
  padding: 8px;
}
thead {
  position: sticky;
  top: -1px;
  z-index: 1;
  background-color: #f2f2f2;
  height: 48px;
}

th {
  background-color: #f2f2f2;
}

.loading {
  width: 50px;
  height: 50px;
  -webkit-animation: rotation 2s infinite linear;
}
@-webkit-keyframes rotation {
  from {
    -webkit-transform: rotate(0deg);
  }
  to {
    -webkit-transform: rotate(359deg);
  }
}
.tbody-loading {
  height: 50vh;
  width: 100%;
}
</style>