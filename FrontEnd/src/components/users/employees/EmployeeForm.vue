<template>
  <div class="modal" id="myModal">
    <div class="modal-content">
      <div class="k-form">
        <div class="form-register">
          <div class="form-header0">
            <div class="form-header">
              <h1 class="info-employee">
                {{
                  check_edit != 0 && clone == false && checkCVT == false
                    ? this.MisaEmployee[language].formEmployee.headerUpdate
                    : this.MisaEmployee[language].formEmployee.headerCreate
                }}
              </h1>
            </div>
            <div>
              <div class="imageL2" @click="close_modal"></div>
            </div>
          </div>
          <div class="form-body">
            <div class="row">
              <div class="col6">
                <base-input-2
                  :label="this.MisaEmployee[language].formEmployee.nameCode"
                  ref="employeeCode"
                  name="employeeCode"
                  style="width: 30%; margin-right: 8px"
                  required="true"
                ></base-input-2>
                <base-input-2
                  :label="this.MisaEmployee[language].formEmployee.nameFullname"
                  ref="fullName"
                  name="fullName"
                  style="width: 70%"
                  required="true"
                ></base-input-2>
              </div>
              <div class="col6" style="margin: 0">
                <base-input-2
                  :label="this.MisaEmployee[language].formEmployee.nameDOB"
                  type="date"
                  ref="dob"
                  style="width: 50%"
                ></base-input-2>
                <div
                  class="gender-input-container"
                  style="width: 50%; margin-left: 8px"
                >
                  <label style="margin-bottom: 8px">
                    {{ this.MisaEmployee[language].formEmployee.gender }}</label
                  >
                  <div class="input-block" style="margin-bottom: 8px">
                    <div class="input-group">
                      <div>
                        <label
                          class="container label-checkbox"
                          style="margin: 0 4px"
                          ><input
                            tabindex="4"
                            type="radio"
                            name="gender"
                            id="1"
                            ref="nam"
                            v-model="gender"
                            @keydown.tab.prevent.exact="
                              fnc_keydown('nu', 'radio')
                            "
                            @keydown.shift.tab.prevent="
                              handleKeyDown($event, 'dob')
                            "
                            @keyup.enter.prevent="keyup_enter('so_dien_thoai')"
                            value="1" /><span class="checkmark"></span></label
                        ><label for="1">
                          {{
                            this.MisaEmployee[language].formEmployee.male
                          }}</label
                        >
                      </div>
                      <div>
                        <label class="container label-checkbox"
                          ><input
                            tabindex="4"
                            type="radio"
                            name="gender"
                            id="0"
                            @keydown.tab.prevent.exact="
                              fnc_keydown('khac', 'radio')
                            "
                            @keydown.shift.tab.prevent="
                              handleKeyDown($event, 'nam', 'radio')
                            "
                            @keyup.enter.prevent="keyup_enter('so_dien_thoai')"
                            ref="nu"
                            v-model="gender"
                            value="0" /><span class="checkmark"></span></label
                        ><label for="0">
                          {{
                            this.MisaEmployee[language].formEmployee.female
                          }}</label
                        >
                      </div>
                      <div>
                        <label class="container label-checkbox"
                          ><input
                            tabindex="4"
                            type="radio"
                            name="gender"
                            ref="khac"
                            id="2"
                            value="2"
                            @keydown.tab.prevent.exact="
                              fnc_keydown('department', 'component')
                            "
                            @keydown.shift.tab.prevent="
                              handleKeyDown($event, 'nu', 'radio')
                            "
                            @keyup.enter.prevent="keyup_enter('department')"
                            v-model="gender" /><span
                            class="checkmark"
                          ></span></label
                        ><label for="2">{{
                          this.MisaEmployee[language].formEmployee.other
                        }}</label>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="row">
              <div class="col6">
                <div style="width: 100%">
                  <base-combobox
                    :data-droplist="departments"
                    key-droplist="DepartmentId"
                    required="true"
                    value-droplist="DepartmentName"
                    :name="this.MisaEmployee[language].formEmployee.department"
                    ref="department"
                    @click="check_press($event, 'department_press')"
                    :object-droplist="department"
                    bottom
                  >
                    <template #icon>
                      <img
                        width="20"
                        height="20"
                        src="../../../../public/assets/icons/expand-arrow--v1.png"
                        style="padding: 10px"
                      />
                    </template>
                  </base-combobox>
                </div>
              </div>
              <div class="col6" style="margin: 0">
                <base-input-2
                  :label="this.MisaEmployee[language].formEmployee.idCardShort"
                  :title="this.MisaEmployee[language].formEmployee.idCard"
                  ref="cmnd"
                  style="width: 60%; margin-right: 8px"
                ></base-input-2>

                <base-input-2
                  :label="this.MisaEmployee[language].formEmployee.dateProvide"
                  type="date"
                  ref="dateProvide"
                  style="width: 40%"
                ></base-input-2>
              </div>
            </div>
            <div class="row">
              <div class="col6">
                <div style="width: 100%">
                  <base-combobox
                    :data-droplist="positions"
                    key-droplist="PositionId"
                    value-droplist="PositionName"
                    required="true"
                    :name="this.MisaEmployee[language].formEmployee.position"
                    ref="position"
                    :object-droplist="position"
                    @click="check_press($event, 'position_press')"
                    bottom
                  >
                    <template #icon>
                      <img
                        width="20"
                        height="20"
                        src="../../../../public/assets/icons/expand-arrow--v1.png"
                        style="padding: 10px"
                      /> </template
                  ></base-combobox>
                </div>
              </div>
              <div class="col6" style="margin: 0">
                <base-input-2
                  :label="this.MisaEmployee[language].formEmployee.placeProvide"
                  ref="placeProvide"
                  style="width: 100%"
                ></base-input-2>
              </div>
            </div>
            <div class="row">
              <div class="col" style="margin: 0">
                <base-input-2
                  :label="this.MisaEmployee[language].formEmployee.address"
                  ref="address"
                  style="width: 100%"
                ></base-input-2>
              </div>
            </div>

            <div class="row">
              <div class="col6">
                <base-input-2
                  :label="this.MisaEmployee[language].formEmployee.mobilePhone"
                  :title="
                    this.MisaEmployee[language].formEmployee.mobilePhoneSuff
                  "
                  style="width: 50%; margin-right: 8px"
                  ref="mobilePhone"
                ></base-input-2>
                <base-input-2
                  :title="
                    this.MisaEmployee[language].formEmployee.landlinePhoneSuff
                  "
                  :label="
                    this.MisaEmployee[language].formEmployee.landlinePhone
                  "
                  ref="landlinePhone"
                  style="width: 50%"
                ></base-input-2>
              </div>
              <div class="col6" style="margin: 0">
                <base-input-2
                  :label="this.MisaEmployee[language].formEmployee.email"
                  ref="email"
                  style="width: 50%"
                ></base-input-2>
              </div>
            </div>
            <div class="row">
              <div class="col6">
                <base-input-2
                  :label="this.MisaEmployee[language].formEmployee.accountBank"
                  style="width: 50%; margin-right: 8px"
                  ref="accountBank"
                ></base-input-2>
                <base-input-2
                  :label="this.MisaEmployee[language].formEmployee.nameBank"
                  ref="nameBank"
                  style="width: 50%"
                ></base-input-2>
              </div>
              <div class="col6" style="margin: 0">
                <base-input-2
                  :label="this.MisaEmployee[language].formEmployee.branch"
                  ref="branch"
                  style="width: 50%"
                ></base-input-2>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="form-footer" style="justify-content: space-between">
        <base-button-2
          style="margin-left: 32px"
          id="openModalBtn"
          ref="huy"
          @click="cancelForm"
          :content="this.MisaEmployee[language].formEmployee.cancel"
          hoverGray="true"
        ></base-button-2>

        <div class="common-display">
          <base-button-2
            ref="huy"
            @click="
              add_employees(
                check_edit != 0 && clone === false && checkCVT === false
                  ? check_edit
                  : 0,
                true
              )
            "
            :content="this.MisaEmployee[language].formEmployee.cvt"
            hoverGray="true"
          ></base-button-2>
          <base-button-2
            style="margin-right: 32px"
            ref="cat"
            @click="
              add_employees(
                check_edit != 0 && clone === false && checkCVT === false
                  ? check_edit
                  : 0
              )
            "
            :content="this.MisaEmployee[language].formEmployee.cat"
            green="true"
          ></base-button-2>
        </div>
      </div>
    </div>
  </div>

  <div class="modal" id="myModal" v-if="check_validate">
    <modal-delete @close-modal="closeModal" :check-validate="check_validate">
      <template #header_delete>
        {{ this.MisaEmployee[language].delete.titleValidate }}
      </template>

      <template #title_delete v-if="Array.isArray(error_validation)">
        <ul style="margin: 0; line-height: 23px; padding-left: 20px">
          <li v-for="(value, index) in error_validation" :key="index">
            {{ value }}
          </li>
        </ul>
      </template>

      <template #title_delete v-else-if="!Array.isArray(error_validation)">
        <ul style="margin: 0; line-height: 23px; padding-left: 20px">
          <li v-for="(value, key, index) in error_validation" :key="index">
            {{ value }}
          </li>
        </ul>
      </template>
      <template #button>
        <base-button-2
          @click="closeModal"
          :content="this.MisaEmployee[language].agree"
          green="true"
        ></base-button-2>
      </template>
    </modal-delete>
  </div>
  <div class="modal" id="myModal" v-if="check_cancel">
    <modal-delete @close-modal="closeModal" :check-cancel="check_cancel">
      <template #header_delete>
        {{ this.MisaEmployee[language].titleChange }}
      </template>

      <template #title_delete>
        {{ this.MisaEmployee[language].saveChange }}
      </template>

      <template #button>
        <base-button-2
          style="margin-right: 16px"
          @click="closeModal"
          :content="this.MisaEmployee[language].close"
          borderNone="true"
        ></base-button-2>
        <base-button-2
          style="margin-right: 16px"
          @click="closeModal(2)"
          :content="this.MisaEmployee[language].unsave"
          borderNone="true"
        ></base-button-2>
        <base-button-2
          @click="
            add_employees(
              check_edit != 0 && clone === false && checkCVT === false
                ? check_edit
                : 0
            )
          "
          :content="this.MisaEmployee[language].save"
          green="true"
        ></base-button-2>
      </template>
    </modal-delete>
  </div>

  <get-api-context
    v-if="check_toast"
    :api="createOrUpdate"
    :body="body"
    :check-toast="check_toast"
    :id="check_edit"
    @close-toast="close_toast"
    @close-modal="closeModal"
    @set-refresh="setRefresh"
    method="body"
  ></get-api-context>
</template>

<script>
import { getAllDepartment } from "../../../services/DepartmentService.js";
import { getAllPosition } from "../../../services/PositionService";
import {
  createNewCode,
  createOne,
  updateOne,
  findJoin,
} from "../../../services/EmployeeService";
import refreshToken from "../../../contexts/GetApiContext2";

export default {
  props: ["checkEdit", "clone", "checkUpdateForm"],
  inject: ["language", "search"],
  emits: ["close-modal", "find-all", "default-field", "format-money"],
  computed: {
    /*
Tên hàm: tính toán cho action kiểu sửa hay cất 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    checkEditFnc() {
      return this.check_edit != 0 && this.clone == false ? "Sửa" : "Cất";
    },
  },
  data() {
    return {
      check_validate: false,
      check_cancel: false,
      error_validation: {},
      check_validation: false,
      error_code: "",
      check_toast: false,
      status_code: 0,
      gender: 1,
      code_press: false,
      fullname_press: false,
      department_press: false,
      position_press: false,
      debit_money: "",
      value_debit: 0,
      check_edit: this.checkEdit,
      departments: [],
      department: {},
      position: {},
      positions: [],
      newCode: "",
      cancel: 0,
      checkCVT: false,
      body: {},
      typeCvt: { checkCvt: false, edit: false },
      createOrUpdate: function () {},
      followChange: false,
      employee: {},
      inputs: {},
      follow: 0,
      checkUpdateForm2: this.checkUpdateForm,
    };
  },

  /*
Tên hàm: Xử lý trước khi mounted form
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 10/01/2023

*/
  async mounted() {
    try {
      this.$refs.employeeCode.focus = true;
      this.departments = await refreshToken(getAllDepartment);
      this.positions = await refreshToken(getAllPosition);
      this.newCode = await refreshToken(createNewCode);
      this.checkUpdateForm2 = false;

      if (this.check_edit === 0 || this.check_edit === 2) {
        await this.resetInput();
      } else if (this.check_edit != 0) {
        await this.assignInput();
      }
      this.inputs = this.createInputs();
    } catch (error) {
      console.error(error);
    }
  },

  /*
Tên hàm: Xử lý cập nhật mấy trường được gán 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 10/01/2023

*/
  async beforeUpdate() {
    this.check_edit = this.checkEdit;
    if (this.checkUpdateForm && this.follow === 0) {
      this.follow += 1;
      this.checkUpdateForm2 = this.checkUpdateForm;
    }
    if (this.checkUpdateForm2 != false && this.follow === 1) {
      this.$refs.employeeCode.value = this.checkUpdateForm2;
    }
  },
  methods: {
    /*
Tên hàm:theo dõi sự thay đổi để bật modal khi có dữ liệu được thay đổi 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 10/01/2023

*/
    followData(event) {
      try {
        if (event && event.target) {
          const value = event.target.value.trim();
          if (value !== undefined && value !== "") {
            this.followChange = true;
          } else {
            this.followChange = false;
          }
        } else {
          this.followChange = false;
        }
      } catch (error) {
        console.error(error.message);
      }
    },
    /*
Tên hàm:focus ô press enter customise
Tham số: msg là tên của ref input muốn focus
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 10/01/2023

*/
    keyup_enter(msg) {
      this.$refs[msg].focus = true;
    },
    /*
Tên hàm: Đóng modal lại 
param: kiểm tra nếu msg = 2 là đóng cả thằng bên modal form
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    closeModal(msg = 1) {
      if (msg === 2) {
        this.$emit("close-modal");
      }
      this.check_validate = false;
      this.check_cancel = false;
      this.$refs.fullName.focus = true;
    },
    /*
Tên hàm: shift tab to the bottom
Tham số: msg là ref của ô input, 
        + type là loại của input đó 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 10/01/2023

*/
    handleKeyDown(event, msg, type) {
      try {
        if (type === "radio") {
          this.$refs[msg].checked = true;
        }
        this.$refs[msg].focus = true;
      } catch (error) {
        console.error(error.message);
      }
    },
    /*
Tên hàm: Tab control
Người viết: Đặng Đình Quốc Khánh
Tham số: msg là ref của ô input, 
        + type là loại của input đó 
Ngày viết: 10/01/2023

*/
    fnc_keydown(msg, type) {
      try {
        if (type === "radio") {
          this.$refs[msg].checked = true;
        }
        if (msg == "department" && type == "component") {
          this.$refs.department.data.checkFocus = true;
          return;
        }
        this.$refs[msg].focus = true;
      } catch (error) {
        console.error(error.message);
      }
    },

    /*
Tên hàm: đóng modal bên component cha
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 10/01/2023
*/
    close_modal() {
      this.cancel = 1;
      this.$emit("close-modal");
    },
    close_toast() {
      this.check_toast = false;
    },
    /*
Tên hàm: reset lại tất cả ô input 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 10/01/2023

*/
    async resetInput() {
      // this.$refs.employeeCode.value = await refreshToken(createNewCode);

      this.gender = 1;

      let inputRefs = [
        "fullName",
        "dob",
        "cmnd",
        "dateProvide",
        "placeProvide",
        "address",
        "mobilePhone",
        "landlinePhone",
        "email",
        "accountBank",
        "nameBank",
        "branch",
      ];
      inputRefs.forEach((ref) => {
        this.$refs[ref].value = "";
      });
      this.department = {};
      this.$refs.department.value = {};
      this.$refs.position.value = {};
      this.position = {};
    },
    /*
Tên hàm: hàm này sẽ kiểm tra từng thay đổi để render lại màu input
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 10/01/2023

*/
    check_press(event, name) {
      try {
        if (!event.target.value || !event.target) {
          this[name] = true;
        } else {
          this[name] = false;
        }
      } catch (error) {
        console.error(error);
      }
    },

    /*
Tên hàm: Hỏi trước khi hủy modal
created by: quốc khánh
Ngày viết: 10/01/2023
    */
    cancelForm() {
      this.inputs = this.createInputs();
      let employee = {
        ...this.inputs,
        DepartmentId: this.$refs.department.value.DepartmentId,
        PositionId: this.$refs.position.value.PositionId,
        EmployeeCode: this.$refs.employeeCode.value,
        EmployeeName: this.$refs.fullName.value,
        Gender: parseInt(this.gender),
        DOB: this.$refs.dob.value || null,
        IssueDate: this.$refs.dateProvide.value || null,
      };
      const isModified = Object.entries(employee).some(([key, value]) => {
        if (key === "DOB" || key === "IssueDate") {
          let dob = this.employee[key];
          if (dob) {
            return value != dob.slice(0, 10);
          }
        }
        return value != this.employee[key];
      });
      isModified && this.check_edit !== 0
        ? (this.check_cancel = true)
        : (this.resetInput?.(), this.close_modal?.());
    },
    /*
Tên hàm: Khởi tạo inputs
created by: quốc khánh
Ngày viết: 10/01/2023
    */
    createInputs() {
      return {
        EmployeeCode: this.$refs.employeeCode,
        EmployeeName: this.$refs.fullName,
        DOB: this.$refs.dob,
        IDNo: this.$refs.cmnd?.value,
        IssueDate: this.$refs.dateProvide,
        IssuedBy: this.$refs.placeProvide?.value,
        Address: this.$refs.address?.value,
        MobilePhone: this.$refs.mobilePhone?.value,
        LandlinePhone: this.$refs.landlinePhone?.value,
        Email: this.$refs.email?.value,
        BankAccount: this.$refs.accountBank?.value,
        BankName: this.$refs.nameBank.value,
        Branch: this.$refs.branch?.value,
        DepartmentId: this.$refs.department,
        PositionId: this.$refs.position,
      };
    },
    /*
tên hàm: gán giá trị cho các ô input
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 10/01/2023
    */
    async assignInput(followChange) {
      try {
        const employee = await refreshToken(findJoin, this.check_edit);
        try {
          let {
            EmployeeId,
            EmployeeName,
            DepartmentId,
            DepartmentName,
            PositionId,
            PositionName,
            EmployeeCode,
            DOB,
            Gender,
            IDNo,
            IssueDate,
            IssuedBy,
            Address,
            MobilePhone,
            LandlinePhone,
            Email,
            BankAccount,
            BankName,
            Branch,
          } = employee;
          this.employee = employee;
          this.department = {
            DepartmentId: DepartmentId,
            DepartmentName: DepartmentName,
          };
          this.position = {
            PositionId: PositionId,
            PositionName: PositionName,
          };
          if (this.clone) {
            this.$refs.employeeCode.value = await refreshToken(createNewCode);
          } else {
            this.$refs.employeeCode.value = EmployeeCode;
          }
          this.gender = Gender; // Giả sử gender là biến trong data của Vue component
          this.$refs.fullName.value = EmployeeName;
          this.$refs.dob.value = DOB ? this.format_date(DOB) : "";
          this.$refs.cmnd.value = IDNo;
          this.$refs.dateProvide.value = IssueDate
            ? this.format_date(IssueDate)
            : "";
          this.$refs.placeProvide.value = IssuedBy;
          this.$refs.address.value = Address;
          this.$refs.mobilePhone.value = MobilePhone;
          this.$refs.landlinePhone.value = LandlinePhone;
          this.$refs.email.value = Email;
          this.$refs.accountBank.value = BankAccount;
          this.$refs.nameBank.value = BankName;
          this.$refs.branch.value = Branch;
        } catch (error) {
          console.error(error);
        }
      } catch (error) {
        console.log(error);
      }
    },
    /*
Tên hàm: validate các trường dữ liệu
Tham số: + inputValue là giá trị ref của input
          + inputElement phần tử input 
          + message: là dòng thông báo muốn hiển thị 
          + name là tên lỗi          
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 10/01/2023

*/

    handleEmptyInput(inputValue, inputElement, errorMessage, name) {
      console.log(
        !inputValue ||
          Object.keys(inputValue).length == 0 ||
          new Date(inputValue) > new Date()
      );
      if (
        !inputValue ||
        Object.keys(inputValue).length == 0 ||
        new Date(inputValue) > new Date()
      ) {
        inputElement.borderRed = true;
        inputElement.error = errorMessage;
        this.check_validate = true;
        this.error_validation = {
          ...this.error_validation,
          [name]: errorMessage,
        };
        this.check_validation = true;
      }
    },
    /*
Tên hàm: Call api add hoặc update theo checkAction
Tham số: checkAction sẽ giúp ta xác định CRUD; = 0 là create; != 0 là update 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 10/01/2023

*/
    async add_employees(checkAction, type = false) {
      try {
        this.check_validation = false;
        this.error_validation = {};
        this.handleEmptyInput(
          this.inputs.EmployeeCode.value,
          this.inputs.EmployeeCode,
          this.MisaEmployee["VN"].employeeCode,
          "employeeCode"
        );
        this.handleEmptyInput(
          this.inputs.EmployeeName.value,
          this.inputs.EmployeeName,
          this.MisaEmployee["VN"].employeeFullname,
          "employeeFullname"
        );
        if (this.inputs.DOB.value) {
          this.handleEmptyInput(
            this.inputs.DOB.value,
            this.inputs.DOB,
            this.MisaEmployee["VN"].DateOfBirth,
            "DateOfBirth"
          );
        }
        if (this.inputs.IssueDate.value) {
          this.handleEmptyInput(
            this.inputs.IssueDate.value,
            this.inputs.IssueDate,
            this.MisaEmployee["VN"].DateOfIssue,
            "DateOfIssue"
          );
        }

        this.handleEmptyInput(
          this.inputs.DepartmentId.value,
          this.inputs.DepartmentId,
          this.MisaEmployee["VN"].employeeDepartment,
          "employeeDepartment"
        );
        this.handleEmptyInput(
          this.inputs.PositionId.value,
          this.inputs.PositionId,
          this.MisaEmployee["VN"].employeePosition,
          "employeePosition"
        );
        // thành công chạy cái này
        if (!this.check_validation) {
          this.inputs = this.createInputs();
          let employee = {
            ...this.inputs,
            DepartmentId: this.$refs.department.value.DepartmentId,
            PositionId: this.$refs.position.value.PositionId,
            EmployeeCode: this.$refs.employeeCode.value,
            EmployeeName: this.$refs.fullName.value,
            Gender: parseInt(this.gender),
            DOB: this.$refs.dob.value || null,
            IssueDate: this.$refs.dateProvide.value || null,
          };
          this.error_validation = {};
          this.check_validate = false;
          this.body = employee;

          if (checkAction === 0 || checkAction === 4) {
            this.createOrUpdate = createOne;
          } else {
            this.createOrUpdate = updateOne;
          }
          this.check_toast = true;
          this.inputs.EmployeeCode.focus = true;
          // đối với trường hợp update thì cho checkAction quay lại bằng 0
          if (type === true) {
            this.typeCvt.checkCvt = true;
            if (checkAction != 0 && checkAction != 4) {
              // vì thằng kia đã cho rồi;
              this.typeCvt.edit = true;
            }
          } else {
            this.typeCvt.checkCvt = false;
          }
        }
      } catch (error) {
        console.error(error);
      }
    },
    /*
tên hàm: load lại các hàm theo yêu cầu
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 27/10/01023
  */
    async setRefresh() {
      console.log("Hello " + this.search);
      this.$emit("find-all", this.search);
      // khi cvt vẫn thực hiện công việc cũ nhưng ko cho ẩn modal thôi
      if (this.typeCvt.checkCvt === false) {
        this.$emit("close-modal");
      } else {
        // gọi hàm reset các ô input
        this.resetInput();
        this.follow = 0;
        // gọi hàm tạo code mới
        this.$refs.employeeCode.value = await refreshToken(createNewCode);
      }
      if (this.typeCvt.checkCvt === true && this.typeCvt.edit == true) {
        this.check_edit = 0;
      }
    },
  },
};
</script>

<style scoped>
@import url(../../../css/commons/modal.css);
</style>

