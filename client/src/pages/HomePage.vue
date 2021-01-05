<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <KeepComponent />
  </div>
</template>

<script>
import { onMounted } from 'vue'
import KeepComponent from '../components/KeepComponent.vue'
import { keepService } from '../services/KeepService'
export default {
  components: { KeepComponent },
  name: 'Home',
  setup() {
    onMounted(async() => {
      await keepService.getAll()
    })
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
