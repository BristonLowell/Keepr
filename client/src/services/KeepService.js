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
      const res = await api.get('profile/' + profileId + '/keeps')
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

  async updateViews(keepId, updatedData) {
    try {
      await api.put('keeps/' + keepId, updatedData)
    } catch (error) {
      logger.error(error)
    }
  }
}
export const keepService = new KeepService()
