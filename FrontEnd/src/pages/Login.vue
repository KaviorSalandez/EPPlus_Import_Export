<template>
  <div class="background">
    <div class="container-login">
      <div class="login-form">
        <span class="login-form-logo"></span>
        <div class="login-form-input">
          <base-input-2
            label="login"
            :placeholder="this.MisaEmployee['VN'].login.username"
            ref="username"
            @keydown.enter="login"
            @input="handleInputUsername"
            :class="[
              {
                'wrap-input2': !showUsername,
              },
              {
                inputClass: showUsername,
              },
            ]"
            @focus="focusInput"
          >
          </base-input-2>

          <span
            class="message-error2"
            :class="[{ 'wrap-input2': showUsername }]"
            ref="usernameValid"
          ></span>
          <base-input-2
            type="password"
            label="login"
            :placeholder="this.MisaEmployee['VN'].login.password"
            :class="[
              { 'wrap-input2': !showPassword },
              {
                inputClass: showPassword,
              },
            ]"
            ref="password"
            @input="handleInputPassword"
            @keydown.enter="login"
            @focus="focusInputPassword"
          >
          </base-input-2>
          <span
            class="message-error2"
            :class="[
              { 'wrap-input2': showPassword },
              { 'wrap-input3': showValidLogin },
            ]"
            ref="passwordValid"
          ></span>
        </div>
        <div class="text-right">
          <a class="forgot-password" href="#">{{
            this.MisaEmployee["VN"].login.forgotPassword
          }}</a>
        </div>

        <div class="container-login-form-btn">
          <base-button-2
            style="width: 100%; height: 40px; font-weight: bold"
            id="openModalBtn"
            ref="login"
            @click="login"
            :content="loginTitle"
            color="blue"
          ></base-button-2>
        </div>
        <div class="container-login-method">
          <div class="method">
            <div class="line-left"></div>
            <div class="method-title">
              <span>{{ this.MisaEmployee["VN"].login.methodLogin }}</span>
            </div>
            <div class="line-left"></div>
          </div>
        </div>
        <div class="method-list common-display">
          <div class="method-item">
            <img src="../../public/assets/icons/icon-google.svg" />
          </div>
          <div class="method-item">
            <img src="../../public/assets/icons/icon-apple.svg" />
          </div>
          <div class="method-item">
            <img src="../../public/assets/icons/icon-microsoft.svg" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BaseButton2 from "../components/commons/buttons/BaseButton2.vue";
import BaseInput2 from "../components/commons/inputs/BaseInput2.vue";
import { signin } from "../services/EmployeeService";
export default {
  data() {
    return {
      showUsername: false,
      showPassword: false,
      showValidLogin: false,
      loginTitle: this.MisaEmployee["VN"].login.login,
    };
  },
  components: { BaseInput2, BaseButton2 },
  methods: {
    /*
Tên hàm: xử lý khi focus vào ô input username
param: event là sự kiện focus 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 20/02/2024
*/
    focusInput(event) {
      if (event.type == "focus") {
        this.showUsername = false;
        this.$refs.usernameValid.textContent = "";
      }
    },
    /*
Tên hàm: xử lý khi focus vào ô password
param: event là sự kiện focus 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 20/02/2024
*/
    focusInputPassword(event) {
      if (event.type == "focus") {
        this.showPassword = false;
        this.$refs.passwordValid.textContent = "";
      }
    },
    /*
Tên hàm: xử lý khi nhập vào ô username
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 20/02/2024
*/
    handleInputUsername() {
      if (this.$refs.username.value) {
        this.showUsername = false;
        this.$refs.usernameValid.textContent = "";
      } else {
        this.showUsername = true;
        this.$refs.usernameValid.textContent =
          this.MisaEmployee["VN"].login.usernameValid;
      }
    },
    /*
Tên hàm: xử lý khi nhập vào ô password
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 20/02/2024
*/
    handleInputPassword() {
      if (this.$refs.password.value) {
        this.showPassword = false;
        this.$refs.passwordValid.textContent = "";
      } else {
        this.showPassword = true;
        this.$refs.passwordValid.textContent =
          this.MisaEmployee["VN"].login.passwordValid;
      }
    },
    /*
Tên hàm: xử lý khi người dùng login 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 20/2/2024
*/
    async login() {
      this.loginTitle = this.MisaEmployee["VN"].login.loginValidating;
      try {
        let username = this.$refs.username.value;
        let password = this.$refs.password.value;
        // nếu ko có username
        if (!username) {
          this.showUsername = true;
          this.$refs.usernameValid.textContent =
            this.MisaEmployee["VN"].login.usernameValid;
        }
        // nếu ko có password
        if (!password) {
          this.showPassword = true;
          this.$refs.passwordValid.textContent =
            this.MisaEmployee["VN"].login.passwordValid;
          // nếu độ dài mật khẩu nhỏ hơn 6 ký tự
        } else if (password.length < 6) {
          this.showValidLogin = true;
          this.$refs.passwordValid.textContent =
            this.MisaEmployee["VN"].login.passwordLength;
          // nếu thỏa mãn tất
        } else {
          const body = {
            username: username,
            password: password,
          };
          // call api sign in
          const result = await signin(body);
          const response = result.data;
          // gửi vào store để lưu giá trị từ result
          this.$store.dispatch("setToken", response.AccessToken);
          this.$store.dispatch("setRefreshToken", response.RefreshToken);
          this.$store.dispatch("setAccount", response.Account);
          this.$router.push("/home");
        }
      } catch (error) {
        this.showValidLogin = true;
        this.$refs.passwordValid.textContent = error?.data?.errors[0];
        console.error(error);
      }
      this.loginTitle = this.MisaEmployee["VN"].login.login;
    },
  },
};
</script>

<style scoped>
.inputClass {
  border: 1px solid red;
}
.background {
  background: url("../../public/assets/bg5.jpg");
  background-size: cover;
  background-position: center;
  height: 100vh; /* Đảm bảo rằng chiều cao của phần tử background là bằng với chiều cao của màn hình */
  display: flex;
  justify-content: center;
  align-items: center;
  color: white; /* Để text trở nên dễ đọc hơn */
  text-align: center; /* Canh giữa nội dung */
}
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}
.container-login {
  box-sizing: border-box;
  border-radius: 8px;
  padding: 40px 48px 40px 48px;
  width: 400px;
  min-height: 340px;
  background: #fff;
  position: relative;
  box-shadow: 0 12px 20px rgba(0, 0, 0, 0.12);
}

.wrap-input2 {
  margin-bottom: 12px;
}
.wrap-input3 {
  font-size: 15px;
  margin: 0 0 12px 0;
}
.login-form {
  width: 100%;
}
.login-form-logo {
  font-size: 60px;
  background: url(../../public/assets/icons/icon-amis-platform2.svg) center
    no-repeat;
  background-size: contain;
  display: block;
  display: -moz-box;
  display: -ms-flexbox;
  width: 196px;
  height: 36px;
  margin: 0 auto;
  margin-bottom: 40px;
}
.text-right {
  display: flex;
}
.forgot-password {
  font-size: 14px;
  color: #0073e6;
  line-height: 17px;
  margin-top: 0;
  display: block;
  text-align: right;
  text-decoration: none;
  margin-bottom: 24px;
}
.method {
  display: flex;
  align-items: center;
  text-align: center;
  margin: 24px 0 0 0;
}
.line-left {
  height: 1px;
  flex: 1;
  background-color: #9ea1a5;
}
.method-title {
  /* text-align: center; */
  padding: 5px 10px;
  background-color: #fff;
  color: #9ea1a5;
  font-size: 14px;
}
.method-item {
  margin: 0 4px;
}
.message-error2 {
  color: red;

  display: flex;
  position: relative;
  /* font-size: 13px; */
  /* width: 100%; */
  /* margin-top: 20px; */
}
</style>
