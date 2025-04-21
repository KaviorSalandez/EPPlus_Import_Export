
<template>
  <button
    :class="[
      { 'k-button': green },
      { 'k-buttonGray': borderNone },
      { 'k-buttonBlue': color === 'blue' },
      { hoverGray: hoverGray },
      { hoverBlue: color === 'blue' },
      'common-display',
      { buttonHover: green },
      { green: green },
      { borderNone: borderNone },
    ]"
  >
    <slot name="image"></slot>
    {{ content }}
    <slot name="contentDynamic"></slot>
  </button>
</template>

<script>
export default {
  props: ["content", "green", "color", "src", "border-none", "hover-gray"],
};
</script>
<style scoped>
@import url(../../../css/commons/button.css);
</style>