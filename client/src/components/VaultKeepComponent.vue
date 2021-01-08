<template>
  <div class="vault-keep-component">
    {{ keep.name }}
  </div>
</template>

<script>
import { computed } from 'vue'
export default {
  name: 'VaultKeepComponent',
  props: {
    // eslint-disable-next-line vue/require-default-prop
    keepProp: Object
  },
  setup(props) {
    return {
      keep: computed(() => props.keepProp)
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
