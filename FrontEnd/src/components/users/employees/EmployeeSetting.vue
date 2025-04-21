<template>
  <div data-v-9a169e19="" class="setting">
    <div data-v-9a169e19="" class="setting-content">
      <div data-v-9a169e19="" class="setting-content_header-first">
        <div
          data-v-9a169e19=""
          class="setting-content_header common-display-align"
        >
          <span data-v-9a169e19="" class="cth-right">{{
            MisaEmployee["VN"].customizeEmployee.headerCustomise
          }}</span
          ><span data-v-9a169e19="" class="cth-left"
            ><div data-v-9a169e19="" @click="onCancel" class="cancel"></div
          ></span>
        </div>
        <div data-v-9a169e19="" class="container-search customise-search">
          <base-input-2
            type="search"
            @keydown.enter="setValue"
            :placeholder="MisaEmployee['VN'].customizeEmployee.placeholder"
          >
            <template #icon>
              <img
                width="26"
                height="26"
                style="
                  position: absolute;
                  left: 7px;
                  top: 5px;
                  padding-right: 35px;
                "
                src="../../../../public/assets/icons/Layer 2.svg"
              />
            </template>
          </base-input-2>
        </div>
      </div>
      <div class="setting-content_body">
        <table
          class="unborder"
          style="width: 100%; border-collapse: separate; border-spacing: 0"
        >
          <tbody class="unborder">
            <tr
              v-for="item in searchColumn"
              :key="item?.key"
              :class="[
                hover_setting,
                {
                  active_setting: columnChoosed.has(item?.key),
                },
              ]"
              class="unborder"
            >
              <td style="border: unset; width: 0">
                <input
                  type="checkbox"
                  style="accent-color: green"
                  name=""
                  :checked="columnChoosed.has(item?.key)"
                  @click="choose_column(item?.key, 'choose', $event)"
                />
              </td>
              <!-- @input="fncCheckCustomise" -->
              <td
                style="
                  display: flex;
                  border: unset;
                  padding-left: 0;
                  justify-content: space-between;
                "
                class="common-display-align"
              >
                {{ item.text }}
                <span class="common-display-align">
                  <img
                    v-if="columnGhim.has(item?.key)"
                    src="../../../../public/assets/icons/pin.png"
                    alt=""
                    srcset=""
                    @click.stop
                    @click="choose_column(item?.key, 'ghim')"
                    style="width: 20px; height: 20px; margin-right: 5px"
                  />
                  <img
                    v-else
                    src="../../../../public/assets/icons/thumbtacks.png"
                    @click.stop="choose_column(item?.key, 'ghim')"
                    style="width: 20px; height: 20px; margin-right: 5px"
                  />
                  <img
                    style="width: 30px; height: 30px"
                    src="../../../../public/assets/icons/six-dots.png"
                    alt=""
                    srcset=""
                  />
                </span>
              </td>
            </tr>
          </tbody>
        </table>
        <div
          data-v-9a169e19=""
          class="setting-content_footer common-display-align"
        >
          <base-button-2
            style="margin-right: 10px"
            ref="huy"
            @click="onDefaultColumn"
            :content="
              this.MisaEmployee['VN'].tableEmployee.customiseColumn.takeDefault
            "
            hoverGray="true"
          ></base-button-2>

          <button
            data-v-9a169e19=""
            class="k-button borderNone green"
            @click="onSaveColumn"
          >
            {{ this.MisaEmployee["VN"].tableEmployee.customiseColumn.save }}
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import BaseButton2 from "../../commons/buttons/BaseButton2.vue";
import BaseInput2 from "../../commons/inputs/BaseInput2.vue";
export default {
  components: { BaseInput2, BaseButton2 },
  emits: ["open-setting", "choose-column", "default-column"],
  props: ["columnChoosed", "columnGhim"],
  inject: ["onSetColumnChoosed"],
  data() {
    return {
      columns: [
        {
          key: 1,
          text: this.MisaEmployee["VN"].formEmployee.nameCode,
        },
        {
          key: 2,
          text: this.MisaEmployee["VN"].formEmployee.nameFullname,
        },
        {
          key: 3,
          text: this.MisaEmployee["VN"].formEmployee.gender,
        },
        {
          key: 4,
          text: this.MisaEmployee["VN"].formEmployee.nameDOB,
        },
        {
          key: 5,
          text: this.MisaEmployee["VN"].formEmployee.idCard,
        },

        {
          key: 6,
          text: this.MisaEmployee["VN"].formEmployee.position,
        },
        {
          key: 7,
          text: this.MisaEmployee["VN"].formEmployee.department,
        },
        {
          key: 8,
          text: this.MisaEmployee["VN"].formEmployee.numberBank,
        },
        {
          key: 9,
          text: this.MisaEmployee["VN"].formEmployee.nameBank,
        },
        {
          key: 10,
          text: this.MisaEmployee["VN"].formEmployee.branchBank,
        },
      ],
      searchValue: "",
    };
  },
  computed: {
    /*
Tên hàm: tìm kiếm theo tên cột 
Người viết: Đặng Đình Quốc Khánh
*/
    searchColumn() {
      try {
        if (this.searchValue) {
          return this.columns.filter((item) => {
            let itemText = item.text.toLowerCase();
            return itemText.includes(this.searchValue.toLowerCase());
          });
        }
        return this.columns;
      } catch (error) {
        console.error(error.message);
      }
    },
  },
  methods: {
    /*
Tên hàm: set lại giá trị 
Người viết: Đặng Đình Quốc Khánh
*/
    setValue(event) {
      this.searchValue = event.target.value;
    },
    /*
Tên hàm: xử lý khi lưu 
Người viết: Đặng Đình Quốc Khánh
*/
    onSaveColumn() {
      if (this.columnChoosed.size == 0) {
        this.onSetColumnChoosed(0, this.columnGhim);
      } else if (this.columnGhim.size == 0) {
        this.onSetColumnChoosed(this.columnChoosed, 0);
      } else {
        this.onSetColumnChoosed(this.columnChoosed, this.columnGhim);
      }
      this.$emit("open-setting", "setting");
    },
    /*
Tên hàm: mặc định lại các cột khi lưu 
Người viết: Đặng Đình Quốc Khánh
*/
    onDefaultColumn() {
      this.$emit("default-column");
      this.$emit("open-setting", "setting");
      // this.$emit("choose-column", id, action, event);
      this.onSetColumnChoosed(0, 0);
    },
    /*
Tên hàm: Không customise column
Người viết: Đặng Đình Quốc Khánh
*/
    onCancel() {
      this.$emit("open-setting", "setting");
    },
    /*
Tên hàm: bôi màu cho hàng và cho tự checked khi được chọn ở customise
Người viết: Đặng Đình Quốc Khánh
Tham số: id của hàng muốn tô màu và action là loại của hành động làm gì. 
Ngày viết: 12/12/2023

*/
    choose_column(id, action, event) {
      this.$emit("choose-column", id, action, event);
    },
  },
};
</script>

<style scoped>
.common-display-align {
  display: flex;
  align-items: center;
}
.searchV2[data-v-d761b180] {
  position: absolute;
  left: 7px;
  top: 5px;
  padding-right: 35px;
}
.setting[data-v-9a169e19] {
  position: absolute;
  top: 35px;
  right: 0;
  z-index: 10;
  width: 312px;
  height: 500px;
  background-color: #ffffff;
  border-radius: 4px;
  /* border: 1px solid #e0e0e0; */
}
.cth-right[data-v-9a169e19] {
  font-size: 20px;
  font-weight: bold;
}
.setting-content[data-v-9a169e19] {
  position: relative;
  /* border: 1px solid black; */
  height: 500px;
}

.setting-content_header-first[data-v-9a169e19] {
  padding: 16px;
  /* height: 500px; */
}

.setting-content_header[data-v-9a169e19] {
  justify-content: space-between;
}

.customise-search[data-v-9a169e19] {
  margin: 20px 0 0 0;
}
.container-search {
  position: relative;
  display: flex;
  width: 100%;
  position: relative;
  margin-right: 8px;
}

.icon-search-customise[data-v-9a169e19] {
  position: absolute;
  left: 10px;
  top: 4px;
}
.search-customise-column[data-v-9a169e19] {
  padding-left: 40px;
  margin-top: 0;
  width: 100%;
}

.unborder[data-v-9a169e19] {
  border: unset;
}
table[data-v-9a169e19] {
  border-collapse: collapse;
  /* width: 100%; */
}
table[data-v-9a169e19] {
  /* margin-bottom: 20px; */
  border-color: #e0e0e0;
  border: 1px solid red;
  width: 100%;
}
table {
  display: table;
  border-collapse: separate;
  box-sizing: border-box;
  text-indent: initial;
  border-spacing: 2px;
  border-color: gray;
}

.unborder[data-v-9a169e19] {
  border: unset;
}

.setting-content_footer[data-v-9a169e19] {
  border: 1px solid #e0e0e0;
  background-color: #f5f5f5;
  padding: 8px 0;
  position: absolute;
  width: 100%;
  justify-content: flex-end;
  bottom: 0;
}
.active_setting {
  background-color: #f1ffef;

  z-index: 10;
}
.active_setting {
  z-index: 10;
}
.setting-content_body {
  width: 100%;
  height: 330px; /* Chiều cao 100% của viewport */
  overflow: auto;
}
</style>