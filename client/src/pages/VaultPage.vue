<template>
  <div class="vault-page container-fluid text-center">
    <div class="text-left ml-5 row">
      <div class="col-12">
        <div class="d-flex">
          <h3 class="display-2">
            {{ vault.name }}
          </h3>
          <!-- <div class="col-12 text-right" v-if="profile.id === vault.creatorId"> -->
          <button class="btn btn-danger h-25 align-self-center mt-4 ml-5" @click.prevent="deleteVault">
            <i class="fas fa-trash fa-2x"></i>
          </button>
          <!-- </div> -->
        </div>
      </div>
    </div>
    <div class="row justify-content-center">
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

export default {
  name: 'VaultPage',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      await vaultService.getVaultById(route.params.vaultId)
      await vaultKeepService.getVaultKeeps(route.params.vaultId)
    })
    return {
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.keeps),
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

</style>
