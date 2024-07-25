<template>
  <v-data-table
    :headers="headers"
    :items="users"
    item-key="name"
    class="elevation-1"
    :search="search"
    :custom-filter="filterText"
    light
    :loading="loadingUsers === true ? true : false"
  >
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title>{{$route.name}}</v-toolbar-title>

        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
              New User
            </v-btn>
          </template>
          <form ref="form" @submit.prevent="save">
            <v-card>
              <v-card-title>
                <span class="text-h5">{{ formTitle }}</span>
              </v-card-title>

              <v-card-text>
                <v-container>
                  <v-row>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedUser.firstName"
                        type="text"
                        label="First Name"
                        :rules="[v => !!v || 'First Name is required']"
                        required
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedUser.lastName"
                        type="text"
                        label="Last Name"
                        :rules="[v => !!v || 'Last Name is required']"
                        required
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedUser.email"
                        type="email"
                        label="Email"
                        :rules="[v => !!v || 'Email is required', v => /.+@.+/.test(v) || 'E-mail must be valid']"
                        required
                      ></v-text-field>
                    </v-col>
                  </v-row>
                </v-container>
              </v-card-text>

              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="primary" text @click="close"> Cancel </v-btn>
                <v-btn color="primary" text type="submit"> Save </v-btn>
              </v-card-actions>
            </v-card>
          </form>
        </v-dialog>

        <v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5"
              >Are you sure you want to delete this user?</v-card-title
            >
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="closeDelete">Cancel</v-btn>
              <v-btn color="primary" text @click="deleteUserConfirm">OK</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>

      <v-text-field v-model="search" label="Search" class="mx-4"></v-text-field>
    </template>

    <template v-slot:[`item.actions`]="{ item }">
      <v-icon small class="mr-2" @click="editUser(item)"> mdi-pencil </v-icon>
      <v-icon small @click="deleteUser(item)"> mdi-delete </v-icon>
    </template>
  </v-data-table>
</template>
<script>
import axios from "axios";
export default {
  name: "MyUsers",

  data() {
    return {
      dialog: false,
      dialogDelete: false,
      search: "",
      users: [],
      editedIndex: -1,
      loadingUsers: true,
      editedUser: {
        firstName: "",
        lastName: "",
        email: "",
        password: "",
      },
      defaultUser: {
        firstName: "",
        lastName: "",
        email: "",
        password: "",
      },
      selectedUserEmail: "",
    };
  },

  methods: {
    getUsers() {
      axios
        .get("https://localhost:7168/api/users/all", { timeout: 5000 })
        .then((response) => {
          console.log(response);
          this.users = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    filterText(value, search) {
      return (
        value != null &&
        search != null &&
        typeof value === "string" &&
        value.toString().indexOf(search) !== -1
      );
    },

    createUser() {
      axios
        .post("https://localhost:7168/api/auth/signUp", this.editedUser)
        .then((response) => {
          console.log(response);
          this.getUsers();
          this.closeDelete();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    update() {
      axios
        .put("https://localhost:7168/api/users/" + this.user, this.editUser)
        .then((response) => {
          console.log(response);
          this.getUsers();
          this.closeDelete();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    deleteUser(user) {
      console.log(user.email);
      this.editedIndex = this.users.indexOf(user);
      this.editedUser = Object.assign({}, user);
      this.selectedUserEmail = user.email;
      this.dialogDelete = true;
    },

    deleteUserConfirm() {
      axios
        .delete("https://localhost:7168/api/users/" + this.selectedUserEmail)
        .then((response) => {
          console.log(response);
          this.getUsers();
          this.closeDelete();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    editUser(user) {
      this.editedIndex = this.users.indexOf(user);
      console.log(this.editedIndex);
      this.editedUser = Object.assign({}, user);
      this.dialog = true;
    },

    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedUser = Object.assign({}, this.defaultUser);
        this.editedIndex = -1;
      });
    },

    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedUser = Object.assign({}, this.defaultUser);
        this.editedIndex = -1;
      });
    },

    save() {
      if (this.editedIndex > -1) {
          this.editUser();
      } else {
          this.createUser();
      }
      this.close();
    },
  },

  computed: {
    headers() {
      return [
        {
          text: "Id",
          align: "start",
          sortable: false,
          value: "id",
        },
        {
          text: "First Name",
          align: "start",
          sortable: true,
          value: "firstName",
        },
        {
          text: "Last Name",
          align: "start",
          sortable: false,
          value: "lastName",
        },
        {
          text: "Email",
          align: "start",
          sortable: false,
          value: "email",
        },
        {
          text: "Actions",
          align: "end",
          value: "actions",
          sortable: false,
        },

        //   {
        //     text: 'Role',
        //     align: 'start',
        //     sortable: false,
        //     value: 'role',
        //   },
      ];
    },

    formTitle() {
      return this.editedIndex === -1 ? "New User" : "Edit User";
    },
  },
  watch: {
    users() {
      if (this.users.length > 0) {
        this.loadingUsers = false;
      }
    },
    // dialog(val) {
    //   val || this.close();
    // },
    // dialogDelete(val) {
    //   val || this.closeDelete();
    // },
  },
  mounted() {
    this.getUsers();
  },
};
</script>