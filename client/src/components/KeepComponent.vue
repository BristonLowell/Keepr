<template>
  <div class="keep-component card border border-dark my-2 max-width rounded grow mx-2">
    <div class="" @click.prevent="updateViews">
      <div id="background" class="row">
        <div class="col-12 text-right">
          <button class="m-2 close" v-if="profile.id == vault.creatorId && vaults.length" @click.prevent="deleteVaultKeep">
            <i class="fas fa-trash text-danger"></i>
          </button>
        </div>
        <div class="col-12" type="button" data-toggle="modal" :data-target="'#keep' + keep.id">
          <img :src="keep.img" class="card-img cover" alt="">
          <div class="card-img-overlay row align-items-end">
            <div class="col-8 text-light">
              <h1>{{ keep.name }}</h1>
            </div>
            <div class="col-4">
              <img :src="keep.creator.picture" class="circle w-50" alt="">
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="modal fade"
       :id="'keep' + keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="myLargeModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        <div class="container-fluid">
          <div class="row align-items-center">
            <div class="col-6">
              <img :src="keep.img" class="img-fluid rounded" alt="">
            </div>
            <div class="col-6">
              <div class="row">
                <div class="col-12">
                  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                  </button>
                </div>
                <div class="col-12">
                  <div class="d-flex justify-content-center mb-4">
                    <i class="fa fa-eye mx-1" aria-hidden="true">{{ keep.views }}</i>
                    <i class="fas fa-save mx-1">{{ keep.keeps }}</i>
                    <i class="fas fa-share mx-1">{{ keep.shares }}</i>
                  </div>
                </div>
                <div class="col-12 text-center display-4 mb-4">
                  {{ keep.name }}
                </div>
                <div class="col-8 offset-2 text-left">
                  {{ keep.description }}
                </div>
                <div class="col-8 offset-2 border border-dark my-4"></div>
                <div class="col-12 d-flex align-items-end mt-5 justify-content-between">
                  <button class="text-primary border border-primary btn btn-white" data-toggle="collapse" :data-target="'#collapseExample' + keep.id">
                    ADD TO VAULT
                  </button>

                  <i class="fas fa-trash text-danger ml-4" @click.prevent="deleteKeep" data-dismiss="modal" v-if="profile.id === keep.creatorId"></i>
                  <i class="fas fa-trash text-dark ml-4" v-else></i>
                  <img :src="keep.creator.picture"
                       class="custom-height rounded ml-5"
                       alt=""
                       data-dismiss="modal"
                       @click.prevent="viewOtherProfile"
                  >
                  {{ keep.creator.name }}
                </div>
                <div class="col-12 mt-3 mb-2">
                  <div class="collapse" :id="'collapseExample' + keep.id">
                    <div class="card card-body border border-dark">
                      <form class="form-group d-flex" @submit.prevent="createVaultKeep">
                        <input type="text"
                               class="form-control"
                               placeholder="Enter Vault Name"
                               v-model="state.vaultKeep.name"
                        >
                        <button class="btn btn-primary ml-2" data-toggle="collapse" :data-target="'#collapseExample' + keep.id">
                          Add
                        </button>
                      </form>
                      <div class="row justify-content-center">
                        <div class="col-12 text-center">
                          <h5 class="border-bottom border-dark">
                            Vaults
                          </h5>
                        </div>
                        <div class="col-12 text-success" v-for="vault in vaults" :key="vault">
                          {{ vault.name }}
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
import { vaultKeepService } from '../services/VaultKeepService'
import router from '../router'
export default {
  name: 'KeepComponent',
  props: {
    // eslint-disable-next-line vue/require-default-prop
    keepProp: Object
  },
  setup(props) {
    const state = reactive({
      target: {
        name: props.keepProp.name.split(' ').join('')
      },
      vaultKeep: {
        name: ''
      },
      updatedViews: {
        views: props.keepProp.views + 1
      },
      updatedKeeps: {
        keeps: props.keepProp.keeps + 1
      }
    })
    return {
      state,
      keep: computed(() => props.keepProp),
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults),
      vault: computed(() => AppState.activeVault),
      async deleteKeep() {
        if (window.confirm('Are you sure?')) {
          await keepService.deleteAllVaultKeeps(props.keepProp.id)
          await keepService.deleteOneKeep(props.keepProp.id, AppState.profile.id)
        }
      },
      createVaultKeep() {
        vaultKeepService.createVaultKeep(props.keepProp.id, state.vaultKeep)
        this.updateKeeps()
      },
      updateViews() {
        keepService.updateViews(props.keepProp.id, state.updatedViews)
      },
      updateKeeps() {
        keepService.updateViews(props.keepProp.id, state.updatedKeeps)
      },
      viewOtherProfile() {
        router.push({ name: 'OtherProfile', params: { profileId: props.keepProp.creatorId } })
      },
      deleteVaultKeep() {
        if (window.confirm('Are you sure?')) {
          vaultKeepService.deleteVaultKeep(props.keepProp, AppState.activeVault.id)
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.cover{
  background-size: cover;
}
.custom-height{
  height: 30px;
}
.circle {
  border-radius: 90%;
}
.grow {
  transition: all 0.2s ease-in-out;
}
.grow:hover {
  transform: scale(1.05);
}

</style>
