<template>
  <div class="content-dropdown">
    <label style="display: block; margin-bottom: 8px" v-if="name"
      >{{ name }} &nbsp<span class="star">*</span></label
    >
    <div
      :class="[
        'container',
        { borderTop: top === '' },
        { borderRed: borderRed },
        { borderGreen: !borderRed && checkFocus && top !== '' },
        { 'container-border': checkFocus && check && bottom === '' },
        { border: bottom === '' && !borderRed },
      ]"
    >
      <input
        type="text"
        name=""
        id="country-input"
        :class="['input-box', 'common-display', { paddingTypeTop: top === '' }]"
        ref="item"
        style="margin: 0"
        :placeholder="placeholder"
        @focus="handlerFocus('checkFocus')"
        @blur="handlerFocus('checkFocus', 0)"
        @input="show_item($event, 1)"
        @keydown.arrow-down="handleArrowDown"
        @keydown.enter="
          show_item($event, 0);
          handlerFocus('checkFocus');
        "
        :disabled="disabled === ''"
      />
      <ul
        v-if="check && sum != 0"
        style="padding: 5px 0px 5px 0px"
        :class="[
          'autocomplete-list',
          { 'autocomplete-bottom': bottom === '' },
          { 'autocomplete-top': top === '' },
        ]"
        id="country-list"
      >
        <!-- class="autocomplete-list" -->
        <li
          v-for="item in data"
          :key="item[keyData]"
          @click="
            click_choose(item);
            handlerFocus('check', 0);
          "
        >
          <div
            v-if="item[keyData] === value?.[keyData]"
            class="content-dropdown-li"
            style="color: #50b83c"
          >
            <span style="font-size: 14px">{{ item[nameData] }}</span>
            <img
              v-if="item[keyData] === value?.[keyData]"
              src="../../../../public/assets/icons/Layer 2.png"
              alt=""
              srcset=""
              style="position: absolute; right: 10px"
            />
          </div>
          <div v-else class="content-dropdown-li">
            <span style="font-size: 14px">{{ item[nameData] }}</span>
          </div>
        </li>
      </ul>
      <ul
        v-if="check && sum == 0"
        style="
          padding: 5px 20px 5px 0px;
          width: 96%;
          border-radius: 4px;
          height: 200px;
        "
        :class="[
          'autocomplete-list',
          ' common-display',
          { 'autocomplete-bottom': bottom === '' },
          { 'autocomplete-top': top === '' },
        ]"
        id="country-list"
      >
        <img
          class="loading"
          src="../../../../public/assets/loading.svg"
          alt=""
          srcset=""
        />
      </ul>
      <div
        :class="['icon-drop', { 'border-left': bottom === '' }]"
        style="
          border-top-right-radius: 10px; /* Bo góc phải trên */
          border-bottom-right-radius: 10px;
        "
        @click="
          show_item($event, 0);
          handlerFocus('checkFocus');
        "
      >
        <slot name="icon"></slot>
      </div>
      <span class="message-error">{{ error }}</span>
    </div>
  </div>
</template>
<script>
export default {
  props: [
    "item",
    "dataDroplist",
    "keyDroplist",
    "valueDroplist",
    "name",
    "required",
    "objectDroplist",
    "bottom",
    "top",
    "disabled",
    "placeholder",
  ],
  emits: ["call-data", "dropItemClick"],
  data() {
    return {
      data: this.dataDroplist,
      dataOrigin: this.dataDroplist,
      keyData: this.keyDroplist,
      nameData: this.valueDroplist,
      sum: 0,
      idUserChoosed: [],
      check: false,
      value: {},
      checkFocus: false,
      dataInput: "",
      focus: false,
      error: "",
      borderRed: false,
    };
  },
  /*
Tên hàm: trước khi mount dữ liệu 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
  mounted() {
    // có dữ liệu thì gửi vào hàm click là đã chọn
    if (this.objectDroplist && Object.keys(this.objectDroplist).length > 0) {
      this.click_choose(this.objectDroplist);
      this.checkValue = true;
      // không có dữ liệu thì gửi vào hàm click là rỗng
    } else if (
      this.objectDroplist &&
      Object.keys(this.objectDroplist).length == 0 &&
      Object.keys(this.value).length == 0
    ) {
      this.checkValue = false;
      this.click_choose("");
    }
  },
  /*
Tên hàm: trước khi thay đổi component
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
  beforeUpdate() {
    // có dữ liệu thì gửi vào hàm click là đã chọn
    if (
      this.objectDroplist &&
      Object.keys(this.objectDroplist).length > 0 &&
      this.value
    ) {
      if (this.objectDroplist[this.keyData] === this.value[this.keyData]) {
        return;
      } else if (Object.keys(this.value).length > 0) {
        this.click_choose(this.value);
      } else {
        this.click_choose(this.objectDroplist);
      }
      // không có dữ liệu thì gửi vào hàm click là rỗng
    } else if (
      this.value &&
      this.objectDroplist &&
      Object.keys(this.objectDroplist).length === 0 &&
      Object.keys(this.value).length === 0
    ) {
      this.checkValue = false;
      this.click_choose("");
    }
  },
  methods: {
    /*
Tên hàm: hiển thị ra các item ở dropdown 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 15/01/2024

*/
    show_item(event, check) {
      this.dataInput = event.target.value;
      this.check = !this.check;

      if (!this.check) {
        if (typeof this.dataInput == "string") {
          this.value = this.dataInput;
        }
        this.checkFocus = false;
        return;
      }
      this.data = this.dataDroplist;
      this.dataOrigin = this.data;
      this.sum = this.data ? this.data.length : 0;
      if (check === 1) {
        // lọc theo giá trị truyền đến
        this.value = this.dataInput;
        this.data = this.dataOrigin.filter((e) =>
          e[this.nameData]
            .toLowerCase()
            .includes(this.dataInput.toLowerCase().trim())
        );
        ``;
        this.check = true;
      }
    },
    /*
Tên hàm: xử lý giá trị khi được chọn
param:item giá trị được chọn 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 15/01/2024

*/
    click_choose(item) {
      this.checkFocus = false;
      if (typeof item == "string") {
        return;
      }
      this.$refs.item.value =
        Object.keys(item).length == 0 ? "" : item[this.nameData];
      this.value = Object.keys(item).length > 0 ? item : "";
      this.$emit("dropItemClick", this.value[this.nameData]);
      // nếu là nam cho dẫus tích
    },
    /*
Tên hàm: xử lý khi được focus vào ô
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 15/01/2024

*/
    handlerFocus(msg, i = 1) {
      this.borderRed = false;
      this[msg] = true;
      this.error = "";
      if (i == 0) {
        this[msg] = false;
      }
    },
  },
};
</script>

    <style scoped>
.paddingTypeTop {
  padding: 0px 0px 0px 10px;
}
.border {
  border: 1px solid #ccc;
}
.borderGreen {
  border: 1px solid #50b83c;
}
.border-left {
  border-left: 1px solid #e0e0e0;

  /* border: 1px solid black; */
}
.content-dropdown {
  background-color: #fff;
}
.common-display {
  display: flex;
  justify-content: center;
  align-items: center;
}
@-webkit-keyframes rotation {
  from {
    -webkit-transform: rotate(0deg);
  }
  to {
    -webkit-transform: rotate(359deg);
  }
}
.loading {
  width: 50px;
  height: 50px;
  -webkit-animation: rotation 2s infinite linear;
}
.container {
  width: 100%;
  height: 36px;
  position: relative;

  border-radius: 4px;
}
/* .container {
  border: 1px solid #50b83c;
} */
.container-border {
  width: 100%;
  height: 36px;
  position: relative;
  /* margin-top: 8px; */
  /* border-radius: 4px; */
}
.container-border:focus {
  border: 1px solid #50b83c;
}
.content-dropdown-li {
  /*  */
  width: calc(100% - 30px);
  padding: 15px; /* font-weight: bold; */
  display: flex;
  /* justify-content-dropdown: center; */
  align-items: center;
  cursor: pointer;
}
.input-box {
  /* height: 32px; */
  width: calc(100% - 50px);
  border-radius: 4px;
  border: none;
  outline: none;
  color: black;
  background-color: #fff;
  font-size: 14px;
}
.input-box:focus {
  border: none;
  transition: border 0.6s ease-in-out;
}
.content-dropdown-li:hover {
  background-color: rgba(80, 184, 60, 0.1);
}
.input-box::placeholder {
  color: black;
}
.autocomplete-list {
  list-style-type: none;
  position: absolute;
  margin: 0;
  max-height: 308px;
  background-color: #ffff;
  width: 100%;
  overflow-y: auto;
  box-shadow: rgba(0, 0, 0, 0.19) 0px 10px 20px, rgba(0, 0, 0, 0.23) 0px 6px 6px;
  z-index: 100;
  /* display: none; */
}
.autocomplete-bottom {
  top: 40px;
  left: 0;
}
.autocomplete-top {
  bottom: 40px;
  left: 0;
}
.autocomplete-list.show {
  display: block; /* Hiển thị danh sách khi có lớp 'show' */
  background-color: #fff;
  border: 1px solid #e0e0e0;
}

.autocomplete-list li {
  width: calc(100%);
  /* line-height: 15px; */
}
.icon-drop {
  position: absolute;
  bottom: 0;
  right: 0;

  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  background-color: #fff;
}
#link-drop {
  padding: 0 15px;
  cursor: pointer;
}
</style>