<template>
  <div
    class="modal-content-validationToast toast-position common-display-align"
  >
    <div class="k-error">
      <div class="error-register common-display">
        <div class="error-header common-display">
          <div
            class="icon-success"
            v-if="statusCode === 201 || statusCode === 200"
          ></div>
          <div class="icon-info" v-else></div>
          <div class="body-el">
            <ul
              v-for="(index, err) in errMessage"
              :key="index"
              style="color: #50b83c; list-style: none; padding: 0"
              class="titleVali"
            >
              <li>
                {{
                  typeof index === "string" ? index : errMessage[err].join("\n")
                }}
              </li>
            </ul>
          </div>
        </div>
        <div class="common-display" style="justify-content: space-between">
          <div class="cancel" @click="close_toast"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: ["errMessage", "statusCode"],
  emits: ["check-toast"],
  methods: {
    close_toast() {
      this.$emit("check-toast");
    },
  },
};
</script>

<style >
@import url(../../../css/commons/main.css);
</style>