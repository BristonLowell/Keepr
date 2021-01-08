import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { AppState } from '../AppState'

class VaultKeepService {
  async getVaultKeeps(vaultId) {
    try {
      const res = await api.get('vaults/' + vaultId + '/keeps')
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async getVaultKeepsOnProfilePage(vaultId) {
    try {
      const res = await api.get('vaults/' + vaultId + '/keeps')
      AppState.vaultKeeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async createVaultKeep(keepId, vaultKeep) {
    for (let i = 0; i < AppState.vaults.length; i++) {
      const vault = AppState.vaults[i]
      if (vault.name.toLowerCase() === vaultKeep.name.toLowerCase()) {
        vaultKeep.vaultId = vault.id
      }
    }
    vaultKeep.creatorId = AppState.profile.id
    vaultKeep.keepId = keepId
    try {
      await api.post('/vaultkeeps', vaultKeep)
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteVaultKeep(vaultKeep, vaultId) {
    try {
      await api.delete('vaultkeeps/' + vaultKeep.vaultKeepId)
      this.getVaultKeeps(vaultId)
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteAllVaultKeeps() {
    try {
      await api.delete('vaultkeeps/' + AppState.activeVault.id + '/all')
      this.getVaultKeeps(AppState.activeVault.id)
    } catch (error) {
      logger.error(error)
    }
    // await AppState.keeps.forEach(k => {
    //   api.delete('vaultkeeps/' + k.vaultKeepId)
    // })
    // this.getVaultKeeps(AppState.activeVault.id)
  }
}
export const vaultKeepService = new VaultKeepService()
