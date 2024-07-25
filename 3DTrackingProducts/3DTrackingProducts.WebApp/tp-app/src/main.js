import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import router from './router'
import store from './store'
import axios from 'axios'
import Highcharts from "highcharts";
import HighchartsVue from 'highcharts-vue'
import Highcharts3d from "highcharts/highcharts-3d";

Vue.config.productionTip = false

Vue.use(HighchartsVue);
Highcharts3d(Highcharts);
store.state.user = store.getters.getAuthUser;
store.state.token = store.getters.getToken;
store.state.expiration = store.getters.getExpiration;
axios.defaults.headers.common['Authorization'] = 'Bearer ' + store.getters.getToken;

router.beforeEach(function(to, from, next) {
  if ((to.path !== '/login' && to.path !== 'login') && !store.getters.isAuthenticated) {
      next({
          path: '/login'
      })
  } else if ((to.path === '/login' || to.path === 'login') && store.getters.isAuthenticated) {
      next({ path: '/' })
  } 
  else {
      next()
  }
})

new Vue({
  vuetify,
  router,
  store,
  render: h => h(App)
}).$mount('#app')
