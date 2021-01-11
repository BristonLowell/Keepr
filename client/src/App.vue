<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view v-slot="{Component}">
      <!-- <transition name="route" mode="out-in"> -->
      <component :is="Component" />
      <!-- </transition> -->
    </router-view>
  </main>
  <footer>
    <div class="bg-info text-white text-light text-center p-4">
      Thank you for enjoying this!
    </div>
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
export default {
  name: 'App',
  setup() {
    return {
      appState: computed(() => AppState)
    }
  }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

</style>
