<template>
  <div class="body1-employee">
    <span class="title-header">{{
      this.MisaEmployee[language].tableEmployee.header
    }}</span>
    <div class="common-display">
      <base-button-2
        style="margin-right: 12px"
        id="openModalBtn"
        @click="open_modal_employee('employee-form', 0)"
        @keydown.ctrl.1="open_modal_employee('employee-form', 0)"
        :content="this.MisaEmployee[language].tableEmployee.addNew"
        green="true"
      >
        <template #image>
          <img
            width="25"
            height="25"
            style="margin-right: 4px"
            src="../../../../public/assets/icons/external-add-neu-users-neu-royyan-wijaya-2.png"
          />
        </template>
      </base-button-2>
      <img
        class="ellipsis"
        width="36"
        height="36"
        src="../../../../public/assets/icons/ellipsis.png"
      />
    </div>
  </div>
  <div class="content">
    <the-header
      :rowChoosed="rowChoosed"
      @search-employee="search_employee"
      @open_modal_employee="open_modal_employee"
      @unChoosed="unChoosed"
      @reload="reload"
    ></the-header>
    <div
      class="body-employee tableFixHead"
      role="region"
      aria-labelledby="caption"
      tabindex="0"
    >
      <base-table
        :rowSelection="{
          type: selectionType,
          click: rowSelection,
        }"
        :rowChoosed2="rowChoosed"
        @setRowChoosed="setRowChoosed"
        :actionSelection="{
          clickEdit: open_modal_employee,
          clickOption: setAction,
          closeSidebar: closeSidebar,
        }"
        ref="tableEmployee"
        :itemAction="itemActions"
        rowKey="EmployeeId"
        :labels="labels"
        :data="employees"
      >
      </base-table>
      <div class="common-display searchNotFound" v-if="checkResultSearch">
        <div class="searchNotFoundContent">
          <h1 style="text-align: center">
            {{ this.MisaEmployee[language].searchNotFound }}
          </h1>
          <p style="font-size: 20px">
            {{ this.MisaEmployee[language].searchNotKey }}
            <span style="font-weight: bold">{{ search }}</span>
            .{{ this.MisaEmployee[language].reSearch }} ?
          </p>
        </div>
      </div>
    </div>
    <the-footer
      :pageNumberOptions="pageNumberOptions"
      :pageDefault="pageDefault"
      :pageNumber="pageNumber"
      :pageSize="pageSize"
      :sum="sum"
      @pagination="pagination"
      @dropItemClick="dropItemClick"
    ></the-footer>
  </div>
  <component
    :is="modal_check"
    @close-modal="closeModal"
    @delete="delete_many"
    @find-all="findAll"
    :check-edit="check_edit"
    @default-field="default_fields"
    :clone="clone"
    :checkUpdateForm="checkUpdateForm"
    :check-delete="check_delete"
  >
    modal_check === "ModalDelete"? (<template #header_delete>
      {{ this.MisaEmployee[language].tableEmployee.modal.header }}</template
    >
    <template #title_delete>
      {{ this.MisaEmployee[language].delete.titleDelete }} </template
    >) : ""
    <template #button>
      <base-button-2
        id="openModalBtn"
        @click="delete_many(check_delete)"
        :content="this.MisaEmployee[language].agree"
        green="true"
      ></base-button-2>
    </template>
  </component>
  <!-- </v-if> -->

  <get-api-context
    v-if="check_toast"
    :api="deleteOrDeleteMany"
    :body="body"
    :check-toast="check_toast"
    :id="body_delete"
    @close-toast="close_toast"
    @set-refresh="setRefresh"
    method="get"
  ></get-api-context>
</template>

<script>
import refreshToken from "../contexts/GetApiContext2.js";
import {
  deleteOne,
  deleteMany,
  findAndFilter,
  createNewCode,
} from ".././services/EmployeeService";
import Gender from "../helpers/Enum.js";
import BaseTable from "../components/commons/tables/BaseTable.vue";
export default {
  components: { BaseTable },
  props: ["closeSidebar"],
  inject: ["language"],
  provide() {
    return {
      closeModal: this.closeModal,
      onSetColumnChoosed: this.setColumnCustomise,
      search: this.search,
    };
  },
  data() {
    return {
      employees: [],
      rowChoosed: new Set(),
      modal_check: false,
      arr_delete: [],
      check_edit: 0,
      check_toast: false,
      error_valiation: {},
      pageSize: 10,
      pageNumber: 1,
      search: "",
      dataPass: [10, 20, 30],
      checkedAction: 0,
      checkResultSearch: false,
      sum: 0,
      clone: false,
      check_delete: 0,
      checkUpdateForm: false,
      deleteOrDeleteMany: function () {},
      body_delete: 0,
      typeDelete: 0,
      selectionType: "checkbox",
      pageNumberOptions: [
        { pageId: 0, pageNumber: 10 },
        { pageId: 1, pageNumber: 20 },
        { pageId: 2, pageNumber: 30 },
      ],
      columnsCustomise: Array(10).fill(0),
      columnsGhim: Array(10).fill(0),
      pageDefault: { pageId: 0, pageNumber: 10 },
      // truyền sang bên base một hàm sự kiện để bên base bắt được bắt sự kiện cho nó
      labels: [],
      itemActions: [
        {
          key: 0,
          label: this.MisaEmployee[this.language].tableEmployee.delete,
        },
        {
          key: 1,
          label: this.MisaEmployee[this.language].tableEmployee.clone,
        },
        {
          key: 2,
          label: this.MisaEmployee[this.language].tableEmployee.stopUse,
        },
      ],
      columnWidth: [
        {
          EmployeeCode: 150,
        },
        {
          EmployeeName: 200,
        },
        {
          Gender: 100,
        },
        {
          DOB: 150,
        },
        {
          IDNo: 200,
        },
        {
          PositionName: 250,
        },
        {
          DepartmentName: 250,
        },
        {
          BankAccount: 150,
        },
        {
          BankName: 150,
        },
        {
          Branch: 250,
        },
      ],
    };
  },
  /*
Tên hàm: khởi tạo dữ liệu 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
  created() {
    this.labels = this.onSetInitial();
  },
  /*
Tên hàm: Render dữ liệu ở thời điểm tạo
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
  async mounted() {
    try {
      this.findAll();
    } catch (error) {}
  },
  computed: {},
  methods: {
    /*
Tên hàm: khởi tạo các biến của cột table 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    onSetInitial() {
      return [
        {
          text: this.MisaEmployee[this.language].tableEmployee.nameCode,
          field: "EmployeeCode",
          key: "employeeCode",
          className: "employeeCode",
          customise: this.columnsCustomise[0],
          customiseGhim: this.columnsGhim[0],
          distance: 0,
        },
        {
          text: this.MisaEmployee[this.language].tableEmployee.nameFullname,
          field: "EmployeeName",
          key: "employeeName",
          className: "employeeName",
          customise: this.columnsCustomise[1],
          customiseGhim: this.columnsGhim[1],
          distance: 0,
        },
        {
          text: this.MisaEmployee[this.language].tableEmployee.gender,
          field: "Gender",
          render: (genders) => {
            if (genders == 1) {
              return Gender.MALE;
            } else if (genders == 0) {
              return Gender.FEMALE;
            } else {
              return Gender.OTHER;
            }
          },
          key: "gender",
          className: "genderColumn",
          customise: this.columnsCustomise[2],
          customiseGhim: this.columnsGhim[2],
          distance: 0,
        },
        {
          text: this.MisaEmployee[this.language].tableEmployee.nameDOB,
          field: "DOB",
          key: "dob",
          className: "center",
          render: (dob) => {
            return dob ? this.format_date(dob, "dd-mm-yyyy") : "";
          },
          customise: this.columnsCustomise[3],
          customiseGhim: this.columnsGhim[3],
          distance: 0,
        },
        {
          text: this.MisaEmployee[this.language].tableEmployee.idCardShort,
          field: "IDNo",
          key: "idNo",
          className: "idNo",
          title: this.MisaEmployee[this.language].tableEmployee.idCard,
          customise: this.columnsCustomise[4],
          customiseGhim: this.columnsGhim[4],
          distance: 0,
        },
        {
          text: this.MisaEmployee[this.language].tableEmployee.title,
          field: "PositionName",
          key: "PositionName",
          className: "PositionName",
          customise: this.columnsCustomise[5],
          customiseGhim: this.columnsGhim[5],
          distance: 0,
        },
        {
          text: this.MisaEmployee[this.language].tableEmployee.nameDepartment,
          field: "DepartmentName",
          key: "DepartmentName",
          className: "DepartmentName",
          customise: this.columnsCustomise[6],
          customiseGhim: this.columnsGhim[6],
          distance: 0,
        },

        {
          text: this.MisaEmployee[this.language].tableEmployee.numberBank,
          field: "BankAccount",
          key: "bankAccount",
          className: "BankAccount",
          customise: this.columnsCustomise[7],
          customiseGhim: this.columnsGhim[7],
          distance: 0,
        },
        {
          text: this.MisaEmployee[this.language].tableEmployee.nameBank,
          field: "BankName",
          key: "bankName",
          className: "BankName",
          customise: this.columnsCustomise[8],
          customiseGhim: this.columnsGhim[8],
          distance: 0,
        },
        {
          text: this.MisaEmployee[this.language].tableEmployee.branchBank,
          field: "Branch",
          key: "branch",
          className: "Branch",
          customise: this.columnsCustomise[9],
          customiseGhim: this.columnsGhim[9],
          distance: 0,
        },
      ];
    },
    /*
Tên hàm: xử lý customise column 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    setColumnCustomise(itemsSetColumn, setGhim) {
      // = 0 là có set cho nó được ghim, và được hiển thị cột đo
      // = 1 là set cho nó không được ghim và không được hiển thị cột đó
      if (itemsSetColumn != 0) {
        this.columnsCustomise = Array(10).fill(1);
        itemsSetColumn.forEach((id) => {
          // Kiểm tra ID và tăng giá trị tương ứng trong columns
          this.columnsCustomise[id - 1] = 0;
        });
      }
      if (setGhim != 0) {
        // mới vào thì set các cột là ko bị ghim, cho là 0 đi
        // khi bị ghim thì set là bị ghim tức là giá trị 1
        this.columnsGhim = Array(10).fill(0);
        setGhim.forEach((id) => {
          this.columnsGhim[id - 1] = 1;
        });
      }
      if (itemsSetColumn === 0 && setGhim === 0) {
        this.columnsCustomise = Array(10).fill(0);
        this.columnsGhim = Array(10).fill(0);
        // return;
      }
      this.labels = this.onSetInitial();
      this.labels = this.labels.sort(
        (a, b) => b.customiseGhim - a.customiseGhim
      );
      // khi sắp xếp rồi thì cần set lại distance để set pin
      // C1: tạo ra một mảng chứa width của từng cột
      // sau đó khi sort xong thì mình sẽ set lại width của những cột có pin = true là = với width của những thằng trước đó cộng lại
      // index-1 ( ++ )
      this.labels = this.calculateDistance(this.labels, this.columnWidth);
    },

    /*
Tên hàm: xử lý lấy id trong checkbox được chọn 
param: new Value là giá trị id của hàng được chọn 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 17/2/2024
*/
    setRowChoosed(newValue) {
      this.rowChoosed = newValue;
    },
    /*
    tên hàm: bắt sự kiện của action trong hàng với icon elipsis
    param: key để set giá trị theo điểu kiện, id là kiểm tra kiểu delete 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 27/12/2023
    */
    setAction(key, id = 0) {
      if (key === 0) {
        this.check_delete = id;
        this.open_modal_employee("modal-delete", 2);
      } else if (key === 1) {
        this.open_modal_employee("employee-form", id, true);
      }
    },
    calculateDistance(value, columnWidth) {
      let object = value;
      let totalDistance = 60;
      for (let i = 0; i < object.length; i++) {
        let field = object[i].field;
        if (object[i].customiseGhim == 1) {
          console.log(field);
          for (let j = 0; j < columnWidth.length; j++) {
            if (columnWidth[j][field]) {
              object[i].distance = totalDistance;
              console.log(totalDistance);
              totalDistance += columnWidth[j][field];
              console.log(totalDistance);
              break;
            }
          }
        }
      }
      return object;
    },
    /*
    
tên hàm: load lại các hàm theo yêu cầu
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 27/12/2023
  */
    async set_refresh() {
      this.findAll();
      this.closeModal();
    },

    /*
Tên hàm: load lại trang
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023

*/
    reload() {
      this.pageSize = 10;
      this.pageNumber = 1;
      this.unChoosed();
      this.findAll();
    },

    /*
Tên hàm: search theo employee
param: value là gía trị search 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023

*/
    search_employee(value) {
      try {
        this.findAll(value);
      } catch (error) {
        console.error(error.message);
      }
    },
    /*
tên hàm: Chuyển trang, phân trang 
Tham số: / msg tương ứng với: msg = 1 sang phải, = 0 là sang trái
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    pagination(msg) {
      try {
        if (msg == 1) {
          this.pageNumber += 1;
        } else {
          this.pageNumber -= 1;
        }
        this.findAll();
      } catch (error) {
        console.error(error.message);
      }
    },

    /*
tên hàm: lấy ra tất cả bản ghi employee
param: search là giá trị tìm kiếm
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
    */
    async findAll(search = "") {
      try {
        if (search) {
          this.pageSize = 10;
          this.pageNumber = 1;
          this.pageDefault = { pageId: 0, pageNumber: 10 };
        }
        const employee_list = await refreshToken(
          findAndFilter,
          this.pageSize,
          this.pageNumber,
          search
        );
        this.employees = employee_list?.data?.Employees;
        this.sum = employee_list?.data?.Count;
        if (search) {
          this.search = search;
          if (this.employees.length === 0 && this.sum === 0) {
            this.checkResultSearch = true;
            return;
          } else {
            this.checkResultSearch = false;
            return;
          }
        }

        // this.setLoading = false;
        this.default_fields();
      } catch (error) {
        console.error(error);
      }
    },
    /*
Tên hàm: Đặt mặc định các trường
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    default_fields() {
      try {
        this.checkedAction = 0;
      } catch (error) {
        console.error(error);
      }
    },
    /*
Tên hàm: kiểm tra dropdown
param: msg là giá trị muốn thay đổi phủ địn 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    kDropdown(msg) {
      this[msg] = !this[msg];
    },

    /*
  Tên hàm: Click vào item của pagination thì cho nó nhúng giá trị
  Tham số: x là kích cỡ
  Người viết: Đặng Đình Quốc Khánh
  Ngày viết: 12/6/2023
  
  */
    dropItemClick(x) {
      try {
        this.pageSize = x;
        this.pageDefault = this.pageNumberOptions.find(
          (e) => e.pageNumber === x
        );
        this.findAll();
      } catch (error) {
        console.error(error.message);
      }
    },

    /*
Tên hàm: kiểm tra component và đặt tham số num để biết action
Tham số:+ cmp là tên component muốn render
        + num là loại của action 
        + clone là check xem có ấn chức năng nhân bản không 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/6/2023

*/
    async open_modal_employee(cmp, num, clone = false) {
      try {
        this.modal_check = cmp;
        this.clone = false;
        this.checkUpdateForm = false;
        if (num === 1 || num === 2) {
          this.check_edit = this.arr_delete[0];
          this.clone = false;
          // add new
        } else if (num === 0) {
          this.checkUpdateForm = await refreshToken(createNewCode);
          this.check_edit = 0;
          // clone
        } else if (clone == true) {
          this.check_edit = num;
          this.clone = true;
          // delete many
        } else if (num === 3) {
          this.check_delete = 1;
          this.check_edit = this.arr_delete;
        } else {
          this.check_edit = num;
        }
      } catch (error) {
        console.error(error);
      }
    },
    /*
Tên hàm: Đóng toast 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    close_toast() {
      this.check_toast = false;
      this.closeModal();
    },
    /*
Tên hàm: Call API xóa nhiều
Người viết: Đặng Đình Quốc Khánh
Tham số: num = 0 là xóa 1 bản ghi, num = 1 là xóa nhiều 
Ngày viết: 12/12/2023
*/
    async delete_many(num = 0) {
      try {
        this.check_toast = true;
        if (num === 1) {
          // delete many
          this.body_delete = [...this.rowChoosed];
        } else {
          this.body_delete = this.check_delete;
        }
        try {
          let api = "";
          if (num === 1) {
            // delete many
            let query = "";
            for (let index = 0; index < this.body_delete.length; index++) {
              query += "Ids=" + this.body_delete[index];
              if (index < this.body_delete.length - 1) {
                query += "&";
              }
            }
            this.body_delete = query;
            this.deleteOrDeleteMany = deleteMany;
          } else {
            // api = `https://localhost:7061/api/v1/Employees/${body_delete}`;
            this.deleteOrDeleteMany = deleteOne;
          }
        } catch (error) {
          console.error(error);
        }

        // hiển thị lại
      } catch (error) {
        console.error(error);
      }
    },
    closeModal() {
      this.modal_check = false;
    },
    /*
Tên hàm: bỏ chọn
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    unChoosed() {
      this.rowChoosed = new Set();
    },
    /*
Tên hàm: refresh lại trang 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    setRefresh() {
      this.rowChoosed = new Set();
      this.findAll();
    },
  },
};
</script>

<style scoped>
@import url(../css/users/employee-table.css);
</style>