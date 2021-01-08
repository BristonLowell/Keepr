<template>
  <div class="about container-fluid" v-if="vaults.length">
    <div class="row my-5 justify-content-start">
      <div class="col-2 ml-5">
        <img class="rounded custom-height img-fluid" :src="vaults[0].creator.picture" alt="" />
      </div>
      <div class="col-6 mt-4">
        <b class="display-1">{{ vaults[0].creator.email }}</b>
        <br>
        <h2><b>Vaults: {{ vaults.length }}</b></h2>
        <h2><b>Keeps: {{ keeps.length }}</b></h2>
      </div>
    </div>
    <div class="row">
      <div class="col-12 display-3 ml-5 my-4">
        <div>
          <b> Vaults</b>
        </div>
      </div>
    </div>
    <div class="row my-4">
      <VaultComponent v-for="vault in vaults" :key="vault" :vault-prop="vault" />
    </div>
    <div class="row">
      <div class="col-12 text-left display-3 ml-5">
        <div>
          <b> Keeps</b>
        </div>
      </div>
    </div>
    <div class="row my-4">
      <KeepComponent v-for="keep in keeps" :key="keep" :keep-prop="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { vaultService } from '../services/VaultService'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
export default {
  name: 'OtherProfile',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      await vaultService.getMyVaults(route.params.profileId)
      await keepService.getKeepsByProfile(route.params.profileId)
    })
    return {
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.custom-height{
width: 100%;
}
</style>
