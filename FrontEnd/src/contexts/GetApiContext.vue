

<template>
  <div class="modal" id="myModal" body="body" v-if="setStatus">
    <the-toast
      :err-message="error_validation"
      :status-code="status_code"
      v-if="setStatus"
      @check-toast="close_toast"
    ></the-toast>
  </div>
</template>

<script>
import TheToast from "../components/commons/toasts/TheToast.vue";
import { handleRefreshToken } from "../services/EmployeeService";
import store from "../store/store";

export default {
  props: ["api", "body", "checkToast", "id", "method"],
  emits: ["close-toast", "set-refresh"],
  inject: ["closeModal"],
  components: { TheToast },
  /*
Tên hàm: kiểm tra để gọi api nào trước
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 15/01/2024

*/
  mounted() {
    // kiểm tra khi giá trị toast bên kia truyền sang là true
    if (this.checkToastAssign === true && this.method === "body") {
      this.callAPIBody();
    } else if (this.checkToastAssign === true && this.method === "get") {
      this.getAPI();
    }
  },
  /*
Tên hàm: kiểm tra để gọi api nào trước
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 15/01/2024

*/
  beforeUpdate() {
    // kiểm tra khi giá trị toast bên kia truyền sang là true
    if (this.checkToastAssign === true && this.method === "body") {
      this.callAPIBody();
    } else if (this.checkToastAssign === true && this.method === "get") {
      this.getAPI();
    }
  },
  data() {
    return {
      status_code: 0,
      error_validation: [],
      setStatus: false,
      checkToastAssign: this.checkToast,
      closeModalAssign: this.closeModal,
      param: this.id,
      language: "VN",
    };
  },
  methods: {
    /*
Tên hàm: Đóng toast lại 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2023
*/
    close_toast() {
      this.$emit("close-toast");
    },
    /*
Tên hàm: Call api add hoặc update theo checkAction
Tham số: checkAction sẽ giúp ta xác định CRUD; = 0 là create; != 0 là update 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 15/01/2024

*/
    async callAPIBody() {
      try {
        let accessToken = store.getters.getToken;
        let token = {
          headers: {
            Authorization: "Bearer " + accessToken,
          },
        };
        store.dispatch("setLoading", true);
        this.api(token, this.body, this.param !== 0 ? this.param : "")
          .then((res) => {
            let status = res.status;
            this.status_code = status;

            switch (status) {
              case 201:
                this.error_validation = {
                  success: this.clone
                    ? this.MisaEmployee["VN"].successClone
                    : this.MisaEmployee["VN"].success,
                };
                break;
              case 200:
                this.error_validation = {
                  success: this.MisaEmployee["VN"].successUpdate,
                };
                break;
              case 204:
                this.error_validation = {
                  success: this.MisaEmployee["VN"].success,
                };
                break;
            }
            store.dispatch("setLoading", false);
            this.setStatus = true;
            this.checkToastAssign = false;

            setTimeout(() => {
              this.setStatus = false;
              this.$emit("set-refresh");
              this.$emit("close-toast");
            }, 2000);
          })
          .catch((err) => {
            store.dispatch("setLoading", false);
            let { status, errors } = err;
            this.status_code = status;
            console.log(this.status_code);
            switch (status) {
              case 400:
                this.error_validation = Object.values(errors);
                break;
              case 401:
                this.error_validation = {
                  fail: this.MisaEmployee[this.language].errors.response401,
                };
                const body = {
                  AccessToken: accessToken,
                  RefreshToken: this.$store.getters.getRefreshToken, // Lấy refresh token từ store
                };
                try {
                  // Gọi service refresh token
                  const refreshResponse = handleRefreshToken(body);
                  // Cập nhật token mới vào store
                  store.dispatch("setToken", refreshResponse.data.AccessToken);
                  store.dispatch(
                    "setRefreshToken",
                    refreshResponse.data.RefreshToken
                  );

                  // Gọi lại API ban đầu với token mới (đệ quy)
                  return this.callAPIBody();
                } catch (refreshError) {
                  // Xóa token và chuyển hướng về trang login
                  this.error_validation = {
                    fail: this.MisaEmployee[this.language].errors.response401,
                  };
                  setTimeout(() => {
                    this.$store.dispatch("clearToken");
                    this.$router.replace("/login");
                  }, 4000);
                }
                break;
              case 403:
                this.error_validation = {
                  fail: this.MisaEmployee[this.language].errors.response403,
                };
                break;
              case 404:
                this.error_validation = {
                  fail:
                    this.MisaEmployee[this.language].errors.response404 +
                    "Nhân Viên",
                };
                break;
              default:
                this.error_validation = [
                  this.MisaEmployee[this.language].errors.default,
                ];
                break;
            }
            this.setStatus = true;
            this.checkToastAssign = false;

            setTimeout(() => {
              this.setStatus = false;
              this.$emit("close-toast");
            }, 2000);
          });
      } catch (error) {
        console.error(error);
      }
    },
    /*
Tên hàm: Call api theo id truyền đến 
checkAction sẽ giúp ta xác định CRUD; = 0 là create; != 0 là update 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 15/01/2024

*/
    async getAPI() {
      try {
        let accessToken = store.getters.getToken;
        let token = {
          headers: {
            Authorization: "Bearer " + accessToken,
          },
        };
        store.dispatch("setLoading", true);
        this.api(token, this.param !== 0 ? this.param : "")
          .then((res) => {
            console.log(res);
            let status = res.status;
            this.status_code = status;

            switch (status) {
              case 200:
                this.error_validation = {
                  success: this.MisaEmployee["VN"].successDelete,
                };
                break;
            }
            store.dispatch("setLoading", false);
            this.setStatus = true;
            this.checkToastAssign = false;

            setTimeout(() => {
              this.setStatus = false;
              this.$emit("close-toast");
              this.$emit("set-refresh");
            }, 2000);
          })
          .catch((err) => {
            store.dispatch("setLoading", false);
            let { status, errors } = err;
            this.status_code = status;
            switch (status) {
              case 400:
                this.error_validation = Object.values(errors);
                break;
              case 401:
                this.error_validation = {
                  fail: this.MisaEmployee[this.language].errors.response401,
                };
                const body = {
                  AccessToken: accessToken,
                  RefreshToken: this.$store.getters.getRefreshToken, // Lấy refresh token từ store
                };
                try {
                  // Gọi service refresh token
                  const refreshResponse = handleRefreshToken(body);
                  // Cập nhật token mới vào store
                  store.dispatch("setToken", refreshResponse.data.AccessToken);
                  store.dispatch(
                    "setRefreshToken",
                    refreshResponse.data.RefreshToken
                  );

                  // Gọi lại API ban đầu với token mới (đệ quy)
                  return this.callAPIBody();
                } catch (refreshError) {
                  // Xóa token và chuyển hướng về trang login
                  this.error_validation = {
                    fail: this.MisaEmployee[this.language].errors.response401,
                  };
                  setTimeout(() => {
                    this.$store.dispatch("clearToken");
                    this.$router.replace("/login");
                  }, 4000);
                }
              case 403:
                this.error_validation = {
                  fail: this.MisaEmployee[this.language].errors.response403,
                };
                break;
              case 404:
                this.error_validation = {
                  fail: this.MisaEmployee[this.language].errors.response404,
                };
                break;
              default:
                this.error_validation = [
                  this.MisaEmployee[this.language].errors.default,
                ];
                break;
            }
            this.setStatus = true;
            this.checkToastAssign = false;

            setTimeout(() => {
              this.setStatus = false;
              this.$emit("close-toast");
            }, 2000);

            console.log(this.error_validation);
          });
      } catch (error) {
        console.error(error);
      }
    },
  },
};
</script>

<style>
@import url(../css/commons/modal.css);
</style>