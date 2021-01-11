import { logger } from '../utils/Logger'
import { api } from './AxiosService'
import { AppState } from '../AppState'

class KeepService {
  async getAll() {
    try {
      const res = await api.get('keeps')
      AppState.keeps = res.data
      AppState.activeVault = {}
    } catch (error) {
      logger.error(error)
    }
  }

  async getKeepsByProfile(profileId) {
    try {
      const res = await api.get('profiles/' + profileId + '/keeps')
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
    }
  }

  async createKeep(newKeep) {
    newKeep.creatorId = AppState.profile.id
    try {
      await api.post('/keeps', newKeep)
      this.getKeepsByProfile(newKeep.creatorId)
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteKeep(keepId, profileId) {
    try {
      await api.delete('keeps/' + keepId)
      this.getKeepsByProfile(profileId)
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteOneKeep(keepId, profileId) {
    try {
      await api.delete('keeps/' + keepId)
      this.getAll()
    } catch (error) {
      logger.error(error)
    }
  }

  async updateViews(keepId, updatedData) {
    try {
      await api.put('keeps/' + keepId, updatedData)
    } catch (error) {
      logger.error(error)
    }
  }

  async deleteAllVaultKeeps(keepId) {
    try {
      await api.delete('vaultkeeps/' + keepId + '/all/keeps')
    } catch (error) {
      logger.error(error)
    }
    // await AppState.keeps.forEach(k => {
    //   api.delete('vaultkeeps/' + k.vaultKeepId)
    // })
    // this.getVaultKeeps(AppState.activeVault.id)
  }
}
export const keepService = new KeepService()
