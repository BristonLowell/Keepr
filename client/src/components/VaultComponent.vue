<template>
  <div class="vault-component col-sm-5 col-md-3 col-lg-3 my-2">
    <div @click.prevent="selectedVault" class=" border border-dark my-2 rounded mx-4 min-height grow">
      <div class="row align-items-center">
        <div v-for="keep in vaultKeeps" :key="keep" class="col">
          <img :src="keep.img" class="card-img cover min-height pb-1" alt="">
        </div>
        <div class="card-img-overlay col-12 w-100 d-flex justify-content-start ml-5 align-items-end">
          <h1 class="text-light">
            {{ vault.name }}
          </h1>
        </div>
      </div>
    <!-- <img :src="keep.img" class="card-img cover" alt="">
          <div class="card-img-overlay row align-items-end"> -->
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import router from '../router'
import { AppState } from '../AppState'
import { vaultKeepService } from '../services/VaultKeepService'
// import { vaultService } from '../services/VaultService'
export default {
  name: 'VaultComponent',
  props: {
    // eslint-disable-next-line vue/require-default-prop
    vaultProp: Object
  },
  setup(props) {
    onMounted(async() => {
      await vaultKeepService.getVaultKeepsOnProfilePage(props.vaultProp.id)
    })
    return {
      vault: computed(() => props.vaultProp),
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.keeps),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      selectedVault() {
        router.push({ name: 'Vault', params: { vaultId: props.vaultProp.id } })
      }
      // deleteVault() {
      //   vaultService.deleteVault(props.vaultProp)
      // }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.min-height{
  height: 35vh;
}
.grow {
  transition: all 0.2s ease-in-out;
}
.grow:hover {
  transform: scale(1.05);
  box-shadow: 10px 10px black;
}
</style>
