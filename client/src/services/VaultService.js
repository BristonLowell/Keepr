import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { AppState } from '../AppState'

class VaultService {
  async getMyVaults(profileId) {
    try {
      const res = await api.get('profile/' + profileId + '/vaults')
      AppState.vaults = res.data
      AppState.activeVault = {}
    } catch (error) {
      logger.error(error)
    }
  }

  async getVaultById(vaultId) {
    try {
      const res = await api.get('/vaults/' + vaultId)
      AppState.activeVault = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async createVault(newVault) {
    newVault.creatorId = AppState.profile.id
    if (newVault.isPrivate.toLowerCase() === 'true') {
      newVault.isPrivate = true
    } else {
      newVault.isPrivate = false
    }
    await api.post('/vaults', newVault)
    this.getMyVaults(newVault.creatorId)
  }

  async deleteVault(vault) {
    try {
      await api.delete('vaults/' + vault.id)
      this.getVaultById(vault.id)
    } catch (error) {
      logger.error(error)
    }
  }
}
export const vaultService = new VaultService()
