<template>
  <table>
    <thead style="background-color: #e0e0e0">
      <tr class="hover-table">
        <th class="k-checkbox2 standAlone" v-if="rowSelection">
          <input
            :type="rowSelection?.type"
            @click="setRowChoosed($event, 1)"
            style="accent-color: green"
            :checked="rowChoosedPage?.size === data?.length"
          />
        </th>
        <!-- v-if="label?.customise == 1" -->
        <th
          v-for="label in labels.filter((e) => e.customise == 0)"
          :key="label.key"
          :class="[
            label?.className,
            { standAlonePin: label?.customiseGhim == 1 },
          ]"
          :style="
            label?.customiseGhim == 1 ? { left: `${label?.distance}px` } : ''
          "
          :title="label?.title"
        >
          {{ label.text }}
        </th>
      </tr>
    </thead>
    <tbody>
      <tr
        v-for="e in data"
        :key="e?.[rowKey]"
        :class="[
          hover_setting,
          {
            active_setting: rowChoosed.has(e?.[rowKey]),
          },
        ]"
        @click.stop="closeAction"
      >
        <!-- // khi nào add standAlone, khi không được checked -->
        <td
          v-if="rowSelection"
          :class="[
            'k-checkbox2',
            'standAlone',
            { standAloneBackground: !rowChoosed.has(e?.[rowKey]) },
            { standAloneBackgroundGreen: rowChoosed.has(e?.[rowKey]) },
          ]"
        >
          <input
            v-model="rowChoosed"
            :value="e?.[rowKey]"
            :type="rowSelection.type"
            style="accent-color: green"
            @click="setRowChoosed"
          />
          <div class="action-cell" style="position: relative">
            <img
              :class="['edit', { editSidebar: actionSelection.closeSidebar }]"
              width="20"
              height="20"
              :title="this.MisaEmployee['VN'].edit"
              src="../../../../../public/assets/icons/edit-v1.png"
              @click.stop="
                actionSelection.clickEdit('employee-form', e?.[rowKey])
              "
            />
            <img
              width="18"
              height="15"
              src="../../../../../public/assets/icons/ellipsis.png"
              alt="ellipsis"
              :class="[
                'ellipsis',
                { ellipsisSidebar: actionSelection.closeSidebar },
              ]"
              @click.stop="openAction(e?.[rowKey])"
            />
            <div
              :class="[
                'grandAction',
                { grandActionSidebar: actionSelection.closeSidebar },
              ]"
            >
              <div class="action-parent-row" style="position: relative">
                <div
                  :class="[
                    {
                      'context-menu':
                        e?.[rowKey] == actionRow && checkedAction == 1,
                    },
                  ]"
                >
                  <ul
                    v-if="e?.[rowKey] == actionRow && checkedAction == 1"
                    style="position: relative"
                  >
                    <li
                      v-for="item in itemAction"
                      :key="item?.key"
                      style="padding: 10px"
                      @click.stop="
                        actionSelection.clickOption(item?.key, e?.[rowKey])
                      "
                    >
                      {{ item?.label }}
                    </li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </td>
        <td
          v-for="label in labels.filter((e) => e.customise == 0)"
          :key="e[label.key]"
          :class="[
            { standAloneBackground: !rowChoosed.has(e?.[rowKey]) },
            { standAloneBackgroundGreen: rowChoosed.has(e?.[rowKey]) },
            label?.className,
            { standAlonePin: label?.customiseGhim == 1 },
          ]"
          :style="
            label?.customiseGhim == 1 ? { left: `${label?.distance}px` } : ''
          "
        >
          <!-- <div v-if="label && label.filter" v-html="label.renderFilter"></div> -->
          <div v-html="label?.renderFilter"></div>
          {{ label.render ? label.render(e[label.field]) : e[label.field] }}
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script>
export default {
  props: [
    "labels",
    "data",
    "rowSelection",
    "rowKey",
    "actionSelection",
    "itemAction",
    "rowChoosed2",
  ],
  emits: ["setRowChoosed"],
  watch: {
    /*
Tên hàm: Gán lại giá trị mới rowchoosed
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    rowChoosed2: function (newVal, oldVal) {
      this.rowChoosed = newVal;
    },
  },
  data() {
    return {
      actionRow: 0,
      checkedAction: 0,
      rowChoosed: this.rowChoosed2,
      rowChoosedPage: new Set(),
    };
  },
  /*
Tên hàm: kiểm tra dữ liệu và hàng được chọn trước khi cập nhật 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
  beforeUpdate() {
    if (this.data && this.rowChoosed) {
      let key = this.rowKey;
      let ids = this.data.map((e) => e?.[key]);
      this.rowChoosedPage = new Set(
        [...this.rowChoosed].filter((x) => ids.includes(x))
      );
    }
  },
  methods: {
    /*
Tên hàm: Mở action row 
Người viết: Đặng Đình Quốc Khánh
Tham số: id của hàng mở action
Ngày viết: 17/2/2024
*/
    openAction(id) {
      this.actionRow = id;
      this.checkedAction = !this.checkedAction;
    },
    /*
Tên hàm: đóng action ở cái hình 3 chấm 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    closeAction() {
      this.checkedAction = false;
    },
    /*
Tên hàm: set hàng được chọn 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 19/03/2024
*/
    setRowChoosed(event, msg = 0) {
      let key = this.rowKey;
      let ids = this.data.map((e) => e?.[key]);

      if (msg === 1) {
        if (event.target.checked === true) {
          this.rowChoosed = new Set([...this.rowChoosed, ...ids]);
        } else {
          this.rowChoosed = new Set(
            [...this.rowChoosed].filter((x) => !ids.includes(x))
          );
        }
      } else {
        let id = event.target.value;
        if (event.target.checked) {
          this.rowChoosed.add(id);
        } else {
          this.rowChoosed.delete(id);
        }
      }

      this.rowChoosedPage = new Set(
        [...this.rowChoosed].filter((x) => ids.includes(x))
      );
      this.$emit("setRowChoosed", this.rowChoosed);
    },
  },
};
</script>

<style >
@import url(../../../css/commons/table.css);
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
</style>

