<template>
    <v-data-table :headers="headers" :items="categories" :items-per-page="5" class="elevation-1"
        :loading="loadingCategories === true ? true : false">

        <template v-slot:top>
            <v-toolbar flat>
                <v-toolbar-title>{{ $route.name }}</v-toolbar-title>

                <v-divider class="mx-4" inset vertical></v-divider>
                <v-spacer></v-spacer>
                <v-dialog v-model="dialog" max-width="500px">
                    <template v-slot:activator="{ on, attrs }">
                        <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
                            New Category
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
                                            <v-text-field v-model="editedCategory.name" type="text" label="Name"
                                                :rules="[v => !!v || 'Name is required']" required></v-text-field>
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
                        <v-card-title class="text-h5">{{ deleteMessage }}</v-card-title>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="primary" text @click="closeDelete">Cancel</v-btn>
                            <v-btn color="primary" text @click="deleteCategoryConfirm">OK</v-btn>
                            <v-spacer></v-spacer>
                        </v-card-actions>
                    </v-card>
                </v-dialog>
            </v-toolbar>

            <v-text-field v-model="search" label="Search" class="mx-4"></v-text-field>
        </template>
        <template v-slot:[`item.actions`]="{ item }">
            <v-icon small class="mr-2" @click="editCategory(item)"> mdi-pencil </v-icon>
            <v-icon small @click="deleteCategory(item)"> mdi-delete </v-icon>
        </template>
    </v-data-table>
</template>

<script>
import axios from 'axios';

export default {
    name: "TagCategories",

    data() {
        return {
            deleteMessage: "Are you sure you want to delete this category? Deleting the category will also delete every data associated with it. Try updating instead in order to prevent deleting unwanted data",
            dialog: false,
            dialogDelete: false,
            editedIndex: -1,
            editedCategory: {
                name: ""
            },
            defaultCategory: {
                name: ""
            },
            selectedCategory: "",
            search: "",
            categories: [],
            loadingCategories: true,
        };
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
                    text: "Name",
                    align: "start",
                    sortable: true,
                    value: "name",
                },
                {
                    text: "Actions",
                    align: "end",
                    sortable: false,
                    value: "actions"
                }
            ];
        },
        formTitle() {
            return this.editedIndex === -1 ? "New Category" : "Edit Category";
        },
    },
    methods: {
        getCategories() {
            axios
                .get("https://localhost:7168/api/categories/all")
                .then((response) => {
                    console.log(response.data)
                    this.categories = response.data
                }).catch((error) => {
                    console.log(error)
                })
        },
        createCategory() {
            axios
                .post("https://localhost:7168/api/categories", this.editedCategory)
                .then((response) => {
                    console.log(response);
                    this.getCategories();
                    this.closeDelete();
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        editCategory(category) {
            this.editedIndex = this.categories.indexOf(category);
            console.log(this.editedIndex);
            this.editedCategory = Object.assign({}, category);
            this.selectedCategory = category.id;
            this.dialog = true;
        },
        update() {
            axios
                .put("https://localhost:7168/api/categories/" + this.selectedCategory, this.editedCategory)
                .then((response) => {
                    console.log(response);
                    this.getCategories();
                    this.closeDelete();
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        deleteCategory(category) {
            console.log(category.name);
            this.editedIndex = this.categories.indexOf(category);
            this.editedCategory = Object.assign({}, category);
            this.selectedCategory = category.id;
            this.dialogDelete = true;
        },
        deleteCategoryConfirm() {
            axios
                .delete("https://localhost:7168/api/categories/" + this.selectedCategory)
                .then((response) => {
                    console.log(response);
                    this.getCategories();
                    this.closeDelete();
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        save() {
            if (this.editedIndex > -1) {
                this.update()
            } else {
                this.createCategory();
            }
            this.close();
        },
        close() {
            this.dialog = false;
            this.$nextTick(() => {
                this.editedCategory = Object.assign({}, this.defaultCategory);
                this.editedIndex = -1;
            });
        },
        closeDelete() {
            this.dialogDelete = false;
            this.$nextTick(() => {
                this.editedCategory = Object.assign({}, this.defaultCategory);
                this.editedIndex = -1;
            });
        },
    },
    watch: {
        categories() {
            if (this.categories.length > 0) {
                this.loadingCategories = false;
            }
        }
    },
    mounted() {
        this.getCategories();
    },
};
</script>