import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/Home.vue'
import LoginView from '../views/Login.vue'
import DashboardView from '../views/Dashboard.vue'
import UsersView from '../views/Users.vue'
import TagsView from '../views/Tags.vue'
import _2DPositionView from '../views/Tag2DPosition.vue'
import _3DPositionView from '../views/Tag3DPosition.vue'
import CategoriesView from '../views/Categories.vue'
import LogsView from '../views/Logs.vue'
import RoomsView from '../views/Rooms.vue'
import ControlTagsView from '../views/ControlTags.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Home',
    component: HomeView,
    children: [
      {
        path: '',
        name: 'Dashboard',
        component: DashboardView
      },
      {
        path: '/Users',
        name: 'Users',
        component: UsersView
      },
      {
        path: '/Tags',
        name: 'Tags',
        component: TagsView
      },
      {
        path: '/2DPosition',
        name: '2DPosition',
        component: _2DPositionView
      },
      {
        path: '/3DPosition',
        name: '3DPosition',
        component: _3DPositionView
      },
      {
        path: '/Categories',
        name: 'Categories',
        component: CategoriesView
      },
      {
        path: '/Logs',
        name: 'Logs',
        component: LogsView

      },
      {
        path: '/Rooms',
        name: 'Rooms',
        component: RoomsView
      },
      {
        path: '/ControlTags',
        name: 'Control Tags',
        component: ControlTagsView
      }
    ]
  },
  {
    path: '/Login',
    name: 'Login',
    component: LoginView
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
