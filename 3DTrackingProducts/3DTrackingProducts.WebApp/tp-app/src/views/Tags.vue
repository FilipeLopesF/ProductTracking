<template>
  <v-data-table
    :headers="headers"
    :items="tags"
    item-key="name"
    class="elevation-1"
    :search="search"
    :custom-filter="filterText"
    light
    :loading="loadingTags === true ? true : false"
  >
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title>{{ $route.name }}</v-toolbar-title>

        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
              New Tag
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
                        v-model="editedTag.EPC"
                        type="text"
                        label="EPC"
                        required
                      ></v-text-field>
                    </v-col>

                    <v-col>
                      <v-combobox
                        v-model="categorySelected"
                        :items="categories"
                        item-value="id"
                        item-text="name"
                        :return-object="true"
                        label="Category"
                        clearable
                      ></v-combobox>
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
            <v-card-title class="text-h5">{{ warningMessage }}</v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="closeDelete">Cancel</v-btn>
              <v-btn color="primary" text @click="deleteTagConfirm">OK</v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>

      <v-text-field v-model="search" label="Search" class="mx-4"></v-text-field>
    </template>

    <template v-slot:[`item.actions`]="{ item }">
      <v-icon small class="mr-2" @click="editTag(item)"> mdi-pencil </v-icon>
      <v-icon small @click="deleteTag(item)"> mdi-delete </v-icon>
    </template>
  </v-data-table>
</template>
<script>
import axios from "axios";
export default {
  name: "MyTag",

  data() {
    return {
      warningMessage: "Are you sure you want to delete this item?",
      dialog: false,
      dialogDelete: false,
      search: "",
      tags: [],
      categories: [],
      editedIndex: -1,
      loadingTags: true,
      categorySelected: null,
      editedTag: {
        EPC: "",
        CategoryId: "",
      },
      defaultTag: {
        EPC: "",
        CategoryId: "",
      },
    };
  },

  methods: {
    getTags() {
      axios
        .get("https://localhost:7168/api/tags/all", { timeout: 5000 })
        .then((response) => {
          console.log(response);
          this.tags = response.data;
        })
        .catch((error) => {
          console.log(error);
          this.loadingTags = false;
        });
    },

    getCategories() {
      axios
        .get("https://localhost:7168/api/categories/all")
        .then((response) => {
          console.log(response.data);
          this.categories = response.data;
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

    createTag() {
      this.editedTag.CategoryId = this.categorySelected.id;
      axios
        .post("https://localhost:7168/api/tags", this.editedTag)
        .then((response) => {
          console.log(response);
          this.getTags();
          this.closeDelete();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    deleteTag(tag) {
      this.editedIndex = this.tags.indexOf(tag);
      this.editTag = Object.assign({}, tag);
      this.dialogDelete = true;
    },

    deleteTagConfirm() {
      axios
        .delete("https://localhost:7168/api/tags/" + this.editTag.epc)
        .then((response) => {
          console.log(response);
          this.getTags();
          this.closeDelete();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    editTag(tag) {
      this.editedIndex = this.tags.indexOf(tag);
        console.log(this.editedIndex);
        this.editedTag = Object.assign({}, tag);
        this.dialog = true;
    },

    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedTag = Object.assign({}, this.defaultTag);
        this.editedIndex = -1;
      });
    },

    close() {
      this.dialog = false;
      this.$nextTick(() => {
        this.editedTag = Object.assign({}, this.defaultTag);
        this.editedIndex = -1;
      });
    },

    save() {
      if (this.editedIndex > -1) {
        console.log("");
      } else {
        this.createTag();
      }
      this.close();
    },
  },

  computed: {
    headers() {
      return [
        {
          text: "EPC",
          align: "start",
          sortable: false,
          value: "epc",
        },
        {
          text: "Category",
          align: "start",
          sortable: true,
          value: "category",
        },
        //Alterar o get da category para trazer a descrição da category e não o id
        {
          text: "Description",
          align: "start",
          sortable: false,
          value: "description",
        },
        {
          text: "Actions",
          align: "end",
          sortable: false,
          value:"actions"
        },
      ];
    },

    formTitle() {
      return this.editedIndex === -1 ? "New Tag" : "Edit Tag";
    },
  },
  watch: {
    tags() {
      if (this.tags.length > 0) {
        this.loadingTags = false;
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
    this.getTags();
    this.getCategories();
  },
};
</script>