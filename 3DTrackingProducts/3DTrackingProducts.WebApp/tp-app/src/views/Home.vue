<template>
  <v-app id="inspire">
    <v-navigation-drawer v-model="drawer" app dark color="primary">
      <v-list-item @click="goTo('Dashboard')">
        <v-list-item-content>
          <v-list-item-title class="text-h6" v-text="title"></v-list-item-title>
          <v-list-item-subtitle> {{ subtitle }}</v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>
      <v-divider></v-divider>

      <v-list>
        <v-list-group v-for="module in modules" :key="module.name" no-action color="white">
          <template v-slot:activator>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title>{{ module.name }}</v-list-item-title>
              </v-list-item-content>
            </v-list-item>
          </template>

          <v-list-item v-for="child in module.childrens" :key="child.name"
            :class="child.name === $route.name ? 'v-list-item--active' : ''" @click="goTo(child.name)">
            <v-list-item-action>
              <v-icon>{{ child.icon }}</v-icon>
            </v-list-item-action>

            <v-list-item-content>
              <v-list-item-title>{{ child.name }}</v-list-item-title>
            </v-list-item-content>

          </v-list-item>
        </v-list-group>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar app elevation="6">
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>{{ title }}</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-menu v-model="menu" :close-on-content-click="false" :nudge-width="200" offset-x>
        <template v-slot:activator="{ on, attrs }">
          <v-btn color="primary" dark v-bind="attrs" v-on="on">
            <v-icon>mdi-account</v-icon>
          </v-btn>
        </template>

        <v-card>
          <v-list>
            <v-list-item>
              <v-icon>mdi-account</v-icon>
              <v-divider class="mx-3" inset vertical></v-divider>
              <v-list-item-content>
                <v-list-item-title>{{ username }}</v-list-item-title>
                <v-list-item-subtitle>Role: Admin</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-list>

          <v-divider></v-divider>

          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn text @click="menu = false"> Cancel </v-btn>
            <v-btn color="primary" text @click="logout"> Logout </v-btn>
          </v-card-actions>
        </v-card>
      </v-menu>
    </v-app-bar>
    <v-main style="padding: 64px 0px 0px">
      <v-container>
        <v-layout>
          <v-flex>
            <router-view v-slot="{ Component }">
              <transition name="slide" mode="out-in">
                <component :is="Component"></component>
              </transition>
            </router-view>
          </v-flex>
        </v-layout>
      </v-container>
    </v-main>
  </v-app>
</template>

<script>
export default {
  name: "App",

  data: () => ({
    showUserInfo: false,
    drawer: null,
    showNavbar: false,
    title: "3D Tracking App",
    subtitle: "IPLeiria",
    modules: [
      {
        name: "Users Management",
        childrens: [
          {
            name: "Users",
            icon: "mdi-account-multiple",
          },
        ],
      },
      {
        name: "Tags Management",
        childrens: [
          {
            name: "Tags",
            icon: "mdi-tag"
          },
          {
            name: "Control Tags",
            icon: "mdi-tag"
          },
          {
            name: "Categories",
            icon: "mdi-shape"
          },
          {
            name: "Logs",
            icon: "mdi-math-log"
          },
          {
            name: "Rooms",
            icon: "mdi mdi-domain"
          }
        ],
      },
      {
        name: "Tracking Positions",
        childrens: [
          {
            name: "2DPosition",
            icon: "mdi-radar"
          },
          {
            name: "3DPosition",
            icon: "mdi-radar"
          },
        ],
      },
    ],
    username: "",
    menu: false,
    routeNames: [],
  }),

  methods: {
    logout() {
      this.$store.commit("clearUserAndToken");
      this.$router.push("/login");
    },

    goTo(routeName) {
      if (
        this.$route.name !== routeName &&
        this.routeNames.includes(routeName)
      ) {
        this.$router.push({ name: routeName });
      }
    },

    getAllRouteNames() {
      this.$router.options.routes[0].children.forEach((element) => {
        this.routeNames.push(element.name);
      });
    },

    showModal() {
      this.showUserInfo = true;
    },
  },
  mounted() {
    var user = this.$store.getters.getAuthUser;
    this.username = user.name;
    this.getAllRouteNames();
  },
};
</script>
<style lang="css">
.slide-enter-active,
.slide-leave-active {
  transition: opacity 0.5s, transform 0.5s;
}

.slide-enter-from,
.slide-leave-to {
  opacity: 0;
  transform: translateX(-30%);
}

.v-list-item {
  justify-content: flex-end !important;
}

html {
  overflow-y: auto;
}
</style>