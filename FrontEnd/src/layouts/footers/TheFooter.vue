<template>
  <div class="footer common-display-align">
    <div
      class="footer-container common-display"
      style="justify-content: space-between"
    >
      <span style="color: #212121"
        >Tổng:
        <span style="font-weight: bold"
          >{{ sum }}
          {{ this.MisaEmployee[language].tableEmployee.footer.records }} &nbsp
        </span></span
      >
      <div
        class="common-display"
        style="min-width: 450px; justify-content: space-between"
      >
        <span>
          {{ this.MisaEmployee[language].tableEmployee.footer.records }} /
          {{ this.MisaEmployee[language].tableEmployee.footer.pages }}</span
        >

        <base-combobox
          style="max-width: 76px; height: 36px"
          :data-droplist="pageNumberOptions"
          key-droplist="pageId"
          value-droplist="pageNumber"
          ref="pagination"
          :object-droplist="pageDefault"
          @dropItemClick="dropItemClick"
          disabled
          top
        >
          <template #icon>
            <img
              width="20"
              height="20"
              src="../../../../public/assets/icons/expand-arrow--v1.png"
              alt="expand-arrow--v1"
              style="padding: 0px 4px 0px 0px"
            /> </template
        ></base-combobox>

        <span class="common-display">
          <span style="font-weight: bold"
            >{{ (this.pageNumber - 1) * this.pageSize + 1 }} -
            {{ (this.pageNumber - 1) * this.pageSize + this.pageSize }} &nbsp
          </span>
          {{ this.MisaEmployee[language].tableEmployee.footer.records }}
        </span>
        <div class="pagination common-display">
          <div
            v-if="pageNumber == 1"
            class="left-pagi"
            style="cursor: not-allowed; opacity: 0.5"
          ></div>
          <div
            v-if="pageNumber !== 1"
            @click="pagination"
            class="left-pagi"
            style="cursor: pointer"
          ></div>
          <!-- 10 = 50 / 1 ; 10 = 50 / 5 ; 30 = 53 / 2 = 26.5  -->

          <div
            class="right-pagi"
            v-if="pageSize >= sum / pageNumber"
            style="opacity: 0.5; cursor: not-allowed"
          ></div>
          <div
            class="right-pagi"
            v-else
            style="cursor: pointer"
            @click="pagination(1)"
          ></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import BaseCombobox from "../../components/commons/comboboxs/BaseCombobox.vue";
import BaseInput2 from "../../components/commons/inputs/BaseInput2.vue";
export default {
  components: { BaseInput2, BaseCombobox },
  props: ["pageSize", "pageNumber", "pageNumberOptions", "sum", "pageDefault"],
  emits: ["pagination", "dropItemClick"],
  data() {
    return {
      language: "VN",
    };
  },
  methods: {
    /*
tên hàm: Chuyển trang, phân trang 
Tham số: / msg tương ứng với: msg = 1 sang phải, = 0 là sang trái
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 12/12/2023
*/
    pagination(msg) {
      try {
        this.$emit("pagination", msg);
      } catch (error) {
        console.error(error.message);
      }
    },

    /*
  Tên hàm: Click vào item của pagination thì cho nó nhúng giá trị
  Tham số: event là sự kiện được gửi tới 
  Người viết: Đặng Đình Quốc Khánh
  Ngày viết: 12/6/2023
  
  */
    dropItemClick(x) {
      try {
        this.$emit("dropItemClick", x);
      } catch (error) {
        console.error(error.message);
      }
    },
  },
};
</script>

<style scoped>
@import url(../../css/users/employee-table.css);
</style>