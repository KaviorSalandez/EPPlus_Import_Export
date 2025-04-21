<template>
  <div class="modalDelete" id="myModalDelete2">
    <div class="modal-content2">
      <div class="k-error">
        <div class="error-register" style="padding: 0">
          <div class="common-display" style="justify-content: space-between">
            <span class="title-header">
              <slot name="header_delete"></slot>
            </span>
            <div class="header-el" @click="close_modal">
              <img
                width="23"
                height="23"
                src="../../../../public/assets/icons/delete-sign.png"
                alt="delete-sign"
              />
            </div>
          </div>
          <div class="error-body common-display-align">
            <img
              v-if="checkValidate"
              width="30"
              height="30"
              style="cursor: pointer; margin-right: 12px"
              src="../../../../public/assets/icons/exclamation.png"
              alt="help"
            />
            <img
              v-else
              width="30"
              height="30"
              style="cursor: pointer; margin-right: 12px"
              src="../../../../public/assets/icons/question-mark.png"
              alt="help"
            />
            <span><slot name="title_delete"></slot></span>
          </div>
          <div class="error-footer">
            <div class="error-footer">
              <slot name="button"></slot>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  emits: ["close-modal", "delete"],
  props: ["checkDelete", "checkValidate", "checkCancel"],
  mounted() {
    console.log("check delete ");
    console.log(this.checkDelete);
  },
  methods: {
    /*
Tên hàm: đóng modal 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 15/01/2024

*/
    close_modal() {
      this.$emit("close-modal");
    },

    /*
Tên hàm: gọi bên cha để xóa nhiều 
Người viết: Đặng Đình Quốc Khánh
Ngày viết: 15/01/2024

*/
    delete_many() {
      this.$emit("delete", this.checkDelete);
      this.$emit("close-modal");
    },
  },
};
</script>
<style scoped>
.modalDelete {
  display: flex;
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  justify-content: center;
  align-items: center;
  z-index: 15;
}
.error-body {
  margin-top: 24px;
}
.error-footer {
  display: flex;
  justify-content: flex-end;
  align-items: center;
  margin-top: 24px;
}

.modal-content2 {
  background-color: #fff;
  /* padding: 20px; */
  min-width: 400px;
  max-width: 600px;
  /* max-width: 600px; */
  /* min-height: 140px; */
  padding: 24px;
  border-radius: 8px;
  animation: toastDelete 0.8s ease-in-out;
}

@keyframes toastDelete {
  0% {
    transform: translateY(-200px);
    opacity: 0.4;
  }
  50% {
    transform: translateY(50px);
    opacity: 0.7;
  }
  100% {
    transform: translateY(0);
  }
}
</style>