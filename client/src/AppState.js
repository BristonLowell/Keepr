import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  profile: {},
  keeps: [],
  vaults: [],
  activeVault: {},
  vaultKeeps: []
})

export function clearAppState() {
  AppState.keeps = {}
  AppState.vaults = {}
  AppState.vaultKeeps = {}
}
