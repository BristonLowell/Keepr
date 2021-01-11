<template>
  <div class="vault-page container-fluid text-center">
    <div class="text-left ml-5 row">
      <div class="col-12 mb-5">
        <div class="d-flex">
          <h1 class="display-2">
            <b>{{ vault.name }}</b>
            <h2>Keeps: {{ keeps.length }}</h2>
          </h1>
          <!-- <div class="col-12 text-right" v-if="profile.id === vault.creatorId"> -->
          <button class="btn btn-danger h-25 align-self-center mb-4 ml-5" @click.prevent="deleteVault" v-if="profile.id === vault.creatorId">
            <i class="fas fa-trash fa-2x"></i>
          </button>
          <!-- </div> -->
        </div>
      </div>
    </div>
    <div class="card-columns">
      <KeepComponent v-for="keep in keeps" :key="keep" :keep-prop="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { vaultService } from '../services/VaultService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { vaultKeepService } from '../services/VaultKeepService'
import router from '../router'
import { profileService } from '../services/ProfileService'

export default {
  name: 'VaultPage',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      await vaultService.getVaultById(route.params.vaultId)
      await vaultKeepService.getVaultKeeps(route.params.vaultId)
      await profileService.getProfile()
    })
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
      profile: computed(() => AppState.profile),
      async deleteVault() {
        if (window.confirm('Are you sure?')) {
          await vaultKeepService.deleteAllVaultKeeps()
          await vaultService.deleteVault(AppState.activeVault)
          router.push({ name: 'Profile' })
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
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
