import logger from '../utils/Logger'
import { api } from './AxiosService'
class KeepService {
  async getAll() {
    try {
      const res = await api.get('keep')
      // eslint-disable-next-line no-console
      console.log(res)
    } catch (error) {
      logger.error(error)
    }
  }
}
export const keepService = new KeepService()
