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
              New Control Tag
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
                        v-model="editedTag.x_Tag"
                        type="number"
                        step="0.1"
                        label="Position X"
                        required
                      ></v-text-field>
                    </v-col>

                    <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedTag.y_Tag"
                        type="number"
                        step="0.1"
                        label="Position Y"
                        required
                      ></v-text-field>
                    </v-col>

                     <v-col cols="12" sm="6" md="4">
                      <v-text-field
                        v-model="editedTag.description"
                        type="text"
                        label="Description"
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
      <v-icon small class="mr-2" @click="openDialogEditTag(item)"> mdi-pencil </v-icon>
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
      editedIndex: -1,
      loadingTags: true,
      tagSelected: null,
      editedTag: {
        x_Tag: 0,
        y_Tag: 0,
        description: "",
        id: ""
      },
      defaultTag: {
        x_Tag: 0,
        y_Tag: 0,
        description: "",
        id: ""
      },
      tagResource:{
        x_Tag: 0,
        y_Tag: 0,
        description: "",
        origin: false
      }
    };
  },

  methods: {
    getControlTags() {

      axios
        .get("https://localhost:7168/api/controlTag/all")
        .then((response) => {
          console.log(response.data);
          this.tags = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    createTag() {
      this.tagResource.x_Tag = this.editedTag.x_Tag;
      this.tagResource.y_Tag = this.editedTag.y_Tag;
      this.tagResource.description = this.editedTag.description;
      axios
        .post("https://localhost:7168/api/controlTag", this.tagResource)
        .then((response) => {
          console.log(response);
          this.getControlTags();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    editTag(){
      this.tagResource.x_Tag = this.editedTag.x_Tag;
      this.tagResource.y_Tag = this.editedTag.y_Tag;
      this.tagResource.description = this.editedTag.description;
      axios
        .put(`https://localhost:7168/api/controlTag/${this.editedTag.id}`, this.tagResource)
        .then((response) => {
          console.log(response);
          this.getControlTags();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    deleteTag(tag) {
      this.editedIndex = this.tags.indexOf(tag);
      this.editedTag = Object.assign({}, tag);
      this.dialogDelete = true;
    },

    deleteTagConfirm() {
      axios
        .delete(`https://localhost:7168/api/controlTag/${this.editedTag.id}`)
        .then((response) => {
          console.log(response);
          this.getControlTags();
          this.closeDelete();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    openDialogEditTag(tag) {
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

    filterText(value, search) {
      return (
        value != null &&
        search != null &&
        typeof value === "string" &&
        value.toString().indexOf(search) !== -1
      );
    },

    save() {
      if (this.editedIndex > -1) {
        this.editTag();
      } else {
        this.createTag();
      }
      this.close();
    },
  },

  computed: {
    headers() {
      return [
        // {
        //   text: "Id",
        //   align: "start",
        //   sortable: false,
        //   value: "id",
        //   visible: false,
        // },
        {
          text: "Description",
          align: "start",
          sortable: false,
          value: "description",
        },
        {
          text: "X Position",
          align: "start",
          sortable: false,
          value: "x_Tag",
        },
        {
          text: "Y Position",
          align: "start",
          sortable: false,
          value: "y_Tag",
        },
        {
          text: "Actions",
          align: "end",
          sortable: false,
          value: "actions",
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
  },
  mounted() {
    this.getControlTags();
  },
};
</script>