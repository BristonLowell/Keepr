<template>
  <div class="home flex-grow-1 container-fluid">
    <div class="card-columns ">
      <KeepComponent v-for="keep in keeps" :key="keep" :keep-prop="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import KeepComponent from '../components/KeepComponent.vue'
import { keepService } from '../services/KeepService'
import { AppState } from '../AppState'
import { vaultService } from '../services/VaultService'
import { profileService } from '../services/ProfileService'
export default {
  components: { KeepComponent },
  name: 'Home',
  setup() {
    onMounted(async() => {
      await keepService.getAll()
      try {
        setTimeout(async() => {
          await profileService.getProfile()
          await vaultService.getMyVaults(AppState.profile.id)
        }, 3000)
      } catch (error) {

      }
    })
    return {
      keeps: computed(() => AppState.keeps),
      profile: computed(() => AppState.profile)
    }
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
@media (min-width: 34em) {
    .card-columns {
        -webkit-column-count: 2;
        -moz-column-count: 2;
        column-count: 2;
    }
}

@media (min-width: 48em) {
    .card-columns {
        -webkit-column-count: 3;
        -moz-column-count: 3;
        column-count: 3;
    }
}

@media (min-width: 62em) {
    .card-columns {
        -webkit-column-count: 4;
        -moz-column-count: 4;
        column-count: 4;
    }
}

@media (min-width: 75em) {
    .card-columns {
        -webkit-column-count: 4;
        -moz-column-count: 4;
        column-count: 4;
    }
}
</style>
