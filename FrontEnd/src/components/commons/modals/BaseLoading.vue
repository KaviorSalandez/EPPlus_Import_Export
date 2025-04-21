<template>
  <div class="modalDelete" id="myModalDelete2">
    <div class="tbody-loading common-display">
      <img
        class="loading"
        src="../../../../public/assets/loading.svg"
        alt=""
        srcset=""
      />
    </div>
  </div>
</template>
<style scoped>
.loading {
  width: 50px;
  height: 50px;
  z-index: 1000;
  -webkit-animation: rotation 2s infinite linear;
}
@-webkit-keyframes rotation {
  from {
    -webkit-transform: rotate(0deg);
  }
  to {
    -webkit-transform: rotate(359deg);
  }
}
.tbody-loading {
  height: 50vh;
  width: 100%;
}
.modalDelete {
  display: flex;
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.2);
  justify-content: center;
  align-items: center;
  z-index: 2000;
}
</style>