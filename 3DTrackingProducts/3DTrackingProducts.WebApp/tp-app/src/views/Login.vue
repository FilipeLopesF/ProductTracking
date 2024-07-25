<template>
  <v-container fluid fill-height>
    <v-layout align-center justify-center>
      <v-flex xs12 sm8 md4>
        <v-card class="elevation-12">
          <v-toolbar dark color="primary">
            <v-toolbar-title>Login</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <form ref="form" @submit.prevent="login()">
              <v-text-field
                v-model="user.email"
                name="username"
                label="Username"
                type="email"
                placeholder="username"
                required
              ></v-text-field>

              <v-text-field
                v-model="user.password"
                name="password"
                label="Password"
                type="password"
                placeholder="password"
                required
              ></v-text-field>
              <v-btn type="submit" class="mt-4" color="primary" value="log in"
                >Login</v-btn
              >
            </form>
          </v-card-text>

          <v-alert v-if="showAlert" v-bind:type="alertType">
            {{ alertMessage }}
          </v-alert>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import axios from "axios";
import jwtDecode from "jwt-decode";

export default {
  name: "MyLogin",
  data() {
    return {
      user: {
        email: "",
        password: "",
      },
      store: {
        user: {
          name: "",
          nameidentifier: "",
        },
      },
      showAlert: false,
      alertType: "",
      alertMessage: "",
    };
  },

  methods: {
    login() {
      axios
        .post("https://localhost:7168/api/auth/signIn", this.user)
        .then((response) => {
          console.log(response);
          this.showAlert = true;
          this.alertType = "success";
          this.alertMessage = "Login Successful";

          let access_token = response.data.access_token;
          var decoded = jwtDecode(access_token);

          console.log(decoded);

          this.store.user.name = decoded.name;
          this.store.user.nameidentifier = decoded.nameidentifier;
          let token = access_token;
          let expiration = decoded.exp * 1000;

          if (token !== "" && expiration !== "") {
            this.$store.commit("setToken", { token, expiration });
            this.$store.dispatch("setAuthUser", this.store.user);
            setTimeout(() => {
              this.showAlert = false;
              this.$router.push({ name: "Dashboard" });
            }, 1000);
          }
        })
        .catch((error) => {
          console.log(error);
          this.showAlert = true;
          this.alertType = "error";
          this.alertMessage = "Username or Password are wrong!";

          setTimeout(() => {
            this.showAlert = false;
          }, 3000);
        });
    },
  },

};
</script>