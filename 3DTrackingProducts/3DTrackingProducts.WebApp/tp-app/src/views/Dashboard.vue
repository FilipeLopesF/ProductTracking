<template>
      <v-row align="center"
      justify="center">
        <v-col>
          <v-card shaped dark color="primary" elevation="20">
            <v-card-title>Users</v-card-title>
            <v-card-subtitle>Number in Database:</v-card-subtitle>
            <v-card-text><p class="text-h4 text--white">20</p> </v-card-text>
          </v-card>
        </v-col>
        <v-col>
          <v-card shaped dark elevation="20">
            <v-card-title>Tags</v-card-title>
            <v-card-subtitle>Number in Database:</v-card-subtitle>
            <v-card-text><p class="text-h4 text--white">20</p> </v-card-text>
          </v-card>
        </v-col>
      </v-row>
</template>
<script>
export default {
  name: "MyDashboard",

  components: {},

  data: () => ({
    //
  }),
};
</script>