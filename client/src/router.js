import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard } from '@bcwdev/auth0provider-client'
import { clearAppState } from './AppState'

function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage'),
    beforeEnter: clearAppState

  },
  {
    path: '/vault/:vaultId',
    name: 'Vault',
    component: loadPage('VaultPage'),
    beforeEnter: clearAppState

  },
  {
    path: '/about',
    name: 'About',
    component: loadPage('AboutPage')
  },
  {
    path: '/otherprofile/:profileId',
    name: 'OtherProfile',
    component: loadPage('OtherProfilePage'),
    beforeEnter: clearAppState

  },
  {
    path: '/profile',
    name: 'Profile',
    component: loadPage('ProfilePage'),
    beforeEnter: authGuard
  }
]

const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})

export default router
