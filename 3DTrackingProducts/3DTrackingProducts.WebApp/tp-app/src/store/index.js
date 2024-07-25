import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    user: {},
    token: "",
    expiration: 0
  },
  getters: {
    getToken(state) {
      let token = localStorage.getItem('access_token')
      let expiration = localStorage.getItem('expiration_time')

      if (!token || !expiration) {
        return null;
      }
      var nowMs = Date.now();

      if (nowMs > parseInt(expiration)) {
        state.token = "";
        state.user = "";
        state.expiration = 0;
        localStorage.removeItem('access_token')
        localStorage.removeItem('expiration_time')
        localStorage.removeItem('user')
        axios.defaults.headers.common.Authorization = undefined;
        return null;
      }

      return token;
    },

    getExpiration() {
      let expiration = localStorage.getItem('expiration_time')
      if (!expiration) {
        return null;
      }

      return expiration;
    },

    getAuthUser() {
      let user = localStorage.getItem('user');
      if (!user) {
        return null;
      }

      return JSON.parse(user);
    },
    isAuthenticated(state) {
      let expiration = localStorage.getItem('expiration_time')
      var nowMs = Date.now();
      if (nowMs > parseInt(expiration)) {
        state.token = "";
        localStorage.removeItem('access_token')
        localStorage.removeItem('expiration_time')
        localStorage.removeItem('user')
        axios.defaults.headers.common.Authorization = undefined;
        return false;
      }
      else
        if (state.user && state.token) {
          return true;
        }
      return false;
    },
  },
  mutations: {
    setToken(state, { token, expiration }) {
      state.token = token;
      state.expiration = parseInt(expiration);
      localStorage.setItem('access_token', token);
      localStorage.setItem('expiration_time', expiration);

      axios.defaults.headers.common.Authorization = "Bearer " + token;
    },

    clearUserAndToken(state) {
      state.user = null;
      state.token = "";
      state.expiration = 0;
      localStorage.removeItem('user');
      localStorage.removeItem('access_token');
      localStorage.removeItem('expiration_time');
      localStorage.removeItem('projects');

      axios.defaults.headers.common.Authorization = undefined;
    },

    setAuthUser(state, user) {
      state.user = user;
      localStorage.setItem('user', JSON.stringify(user));
  },

  },
  actions: {
    setAuthUser({ commit }, data) {
      commit('setAuthUser', data);
    },
  },
  modules: {
  }
})
