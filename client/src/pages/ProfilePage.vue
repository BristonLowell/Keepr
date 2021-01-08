<template>
  <div class="about container-fluid">
    <div class="row my-5">
      <div class="col-2 ml-5">
        <img class="rounded custom-height img-fluid" :src="profile.picture" alt="" />
      </div>
      <div class="col-6 mt-4">
        <b class="display-1">{{ user.nickname }}</b>
        <br>
        <h2><b>Vaults: {{ vaults.length }}</b></h2>
        <h2><b>Keeps: {{ keeps.length }}</b></h2>
      </div>
    </div>
    <div class="row">
      <div class="col-12 text-left display-3 ml-5 my-4">
        <div>
          <b>Vaults</b>
          <button class="btn btn-success rounded ml-4" type="button" data-toggle="modal" data-target="#vaultModal">
            +
          </button>
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
          <button class="btn btn-success rounded ml-4" type="button" data-toggle="modal" data-target="#keepModal">
            +
          </button>
        </div>
      </div>
    </div>
    <div class="row my-4">
      <KeepComponent v-for="keep in keeps" :key="keep" :keep-prop="keep" />
    </div>
  </div>
  <!-- Modal -->
  <div class="modal fade"
       id="vaultModal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            Create New Vault
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form>
            <div class="form-group">
              <label for="exampleInputEmail1">Name</label>
              <input type="name"
                     class="form-control"
                     id="exampleInputEmail1"
                     aria-describedby="emailHelp"
                     placeholder="Enter name of your new vault"
                     v-model="state.newVault.name"
              >
            </div>
            <div class="form-group">
              <label for="exampleInputPassword1">Description</label>
              <input type="description"
                     class="form-control"
                     id="exampleInputPassword1"
                     placeholder="Enter description of your new vault"
                     v-model="state.newVault.description"
              >
            </div>
            <div class="form-group">
              <label for="exampleInputPassword1">Private</label>
              <input type="private" class="form-control" id="exampleInputPassword1" placeholder="true or false" v-model="state.newVault.isPrivate">
            </div>
            <!-- <div class="form-check">
              <input type="checkbox" class="form-check-input" id="exampleCheck1">
              <label class="form-check-label" for="exampleCheck1">Check me out</label>
            </div> -->
            <button type="submit" class="btn btn-primary" data-dismiss="modal" @click.prevent="createVault">
              Submit
            </button>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">
            Close
          </button>
        </div>
      </div>
    </div>
  </div>
  <!-- Modal -->
  <div class="modal fade"
       id="keepModal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="exampleModalLabel">
            Create New Keep
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form>
            <div class="form-group">
              <label for="exampleInputEmail1">Name</label>
              <input type="name"
                     class="form-control"
                     id="exampleInputEmail1"
                     aria-describedby="emailHelp"
                     placeholder="Enter name of your new keep"
                     v-model="state.newKeep.name"
              >
            </div>
            <div class="form-group">
              <label for="exampleInputPassword1">Description</label>
              <input type="description"
                     class="form-control"
                     id="exampleInputPassword1"
                     placeholder="Enter description of your new keep"
                     v-model="state.newKeep.description"
              >
            </div>
            <div class="form-group">
              <label for="exampleInputPassword1">Image URL</label>
              <input type="img"
                     class="form-control"
                     id="exampleInputPassword1"
                     placeholder="Enter url of your image"
                     v-model="state.newKeep.img"
              >
            </div>
            <!-- <div class="form-check">
              <input type="checkbox" class="form-check-input" id="exampleCheck1">
              <label class="form-check-label" for="exampleCheck1">Check me out</label>
            </div> -->
            <button type="submit" class="btn btn-primary" data-dismiss="modal" @click.prevent="createKeep">
              Submit
            </button>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-dismiss="modal">
            Close
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { profileService } from '../services/ProfileService'
import { vaultService } from '../services/VaultService'
import { keepService } from '../services/KeepService'
export default {
  name: 'Profile',
  setup() {
    const state = reactive({
      newVault: {
        name: '',
        description: '',
        isPrivate: ''
      },
      newKeep: {
        name: '',
        description: '',
        img: '',
        shares: 0,
        keeps: 0,
        views: 0
      }
    })
    onMounted(async() => {
      await profileService.getProfile()
      await vaultService.getMyVaults(AppState.profile.id)
      await keepService.getKeepsByProfile(AppState.profile.id)
    })
    return {
      state,
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user),
      createVault() {
        vaultService.createVault(state.newVault)
      },
      createKeep() {
        keepService.createKeep(state.newKeep)
      }
    }
  }
}
</script>

<style scoped>

.custom-height{
width: 100%;
}
</style>
