<template>
  <v-app>
    <v-main>
      <router-view v-slot="{ Component }">
        <transition name="fade" mode="out-in">
          <component :is="Component"></component>
        </transition>
      </router-view>
    </v-main>
  </v-app>
</template>

<script>
export default {
  name: "MyApp",
};
</script>
<style lang="css">
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

.fade-enter-active,
.fade-leave-to {
  transition: opacity 0.5s ease-out;
}

html { overflow-y: auto }
</style>