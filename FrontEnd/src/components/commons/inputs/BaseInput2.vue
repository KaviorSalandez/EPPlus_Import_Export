<template>
  <input
    v-if="label === 'login'"
    class="borderLogin wrap-input"
    style="width: 100%; height: 48px"
    :type="type || 'text'"
    :placeholder="placeholder"
    :value="value"
    @input="setValue"
  />
  <div v-else-if="label">
    <label :title="title" v-if="label">
      {{ label }} <span class="star" v-if="required">*</span>
    </label>
    <br />
    <input
      :class="[
        'input',
        { border: !borderRed },
        { borderRed: borderRed },
        { label: label },
      ]"
      :type="type || 'text'"
      :ref="name"
      :placeholder="placeholder"
      :value="value"
      @input="setValue"
      @focus="focusInput"
    />
    <span class="message-error">{{ error }}</span>
  </div>

  <div v-else class="container-search" style="margin: 0">
    <slot name="icon"></slot>
    <input
      class="border input"
      style="width: 100%; padding-left: 35px"
      :type="type || 'text'"
      :placeholder="placeholder"
      :value="value"
      @input="setValue"
    />
  </div>
</template>

<script scoped>
export default {
  props: [
    "label",
    "placeholder",
    "title",
    "type",
    "required",
    "name",
    "styleInput",
  ],
  data() {
    return {
      value: "",
      error: "",
      borderRed: false,
      focus: false,
    };
  },
  mounted() {},
  /*
Tên hàm: trước khi cập nhật 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
  beforeUpdate() {
    // focus nhưng nếu ấn cái khác là hết focus
    if (this.focus) {
      this.$refs[this.name].focus();
    }
  },

  methods: {
    /*
Tên hàm: set giá trị cho value 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    setValue(event) {
      this.value = event.target.value;
    },
    /*
Tên hàm: xử lý khi focus vào ô input
  // cho lỗi rỗng, border màu đỏ và focus bằng false
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    focusInput(event) {
      if (focus) {
        this.error = "";
        this.borderRed = false;
        this.focus = false;
      }
    },
  },
};
</script>

<style scoped>
@import url(../../../css/commons/input.css);
</style>
