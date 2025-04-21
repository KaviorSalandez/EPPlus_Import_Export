<template>
  <div class="filter">
    <div class="headerFilter">
      {{ this.MisaEmployee["VN"].filterCondition }}
    </div>
    <slot name="combobox"></slot>
  </div>
</template>
