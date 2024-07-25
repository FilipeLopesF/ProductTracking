<template>
    <v-container>
        <v-data-table :headers="headers" :items="rooms" item-key="name" class="elevation-1" light
            :loading="loadingRooms === true ? true : false" @click:row="presentRoom">
            <template v-slot:top>
                <v-toolbar flat>
                    <v-toolbar-title>{{ $route.name }}</v-toolbar-title>

                    <v-divider class="mx-4" inset vertical></v-divider>
                    <v-spacer></v-spacer>
                    <v-dialog v-model="dialog" max-width="500px">
                        <template v-slot:activator="{ on, attrs }">
                            <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
                                New Room
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
                                                <v-text-field v-model="editedRoom.name" type="text" label="Name"
                                                    required></v-text-field>
                                            </v-col>

                                            <v-col cols="12" sm="6" md="4">
                                                <v-text-field v-model="editedRoom.length" type="number"
                                                    label="Length" required></v-text-field>
                                            </v-col>

                                            <v-col cols="12" sm="6" md="4">
                                                <v-text-field v-model="editedRoom.width" type="number" label="Width"
                                                    required></v-text-field>
                                            </v-col>

                                            <v-col>
                                                <v-file-input ref="fileUpload" accept="image/*" label="Room image"
                                                    @change="updateFile"></v-file-input>
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
                                <v-btn color="primary" text @click="deleteRoomConfirm">OK</v-btn>
                                <v-spacer></v-spacer>
                            </v-card-actions>
                        </v-card>
                    </v-dialog>
                </v-toolbar>
            </template>

            <template v-slot:[`item.actions`]="{ item }">
                <v-icon small class="mr-2" @click="editRoom(item)"> mdi-pencil </v-icon>
                <v-icon small @click="deleteRoom(item)"> mdi-delete </v-icon>
            </template>
        </v-data-table>

        <PairAntennas :id="presentedRoom.id" :image-byte="presentedRoom.imageByte" v-if="presentedRoom.id != ''"/>

    </v-container>
</template>
<script>
import axios from "axios";
import PairAntennas from "./PairAntennas.vue";

export default {
    
    name: "RoomsView",

    components: {
        PairAntennas,
    },

    data() {
        return {
            warningMessage: "Are you sure you want to delete this item?",
            dialog: false,
            dialogDelete: false,
            rooms: [],
            editedIndex: -1,
            loadingRooms: true,
            editedRoom: {
                id: "",
                name: "",
                width: "",
                length: "",
                imageByte: "",
            },
            defaultRoom: {
                id: "",
                name: "",
                width: "",
                length: "",
                imageByte: "",  
            },
            presentedRoom: {
                id: "",
                name: "",
                roomWidth: "",
                roomLength: "",
                imageByte: ""
            },
        };
    },

    methods: {
        getRooms() {
            axios
                .get("https://localhost:7168/api/rooms/all", { timeout: 5000 })
                .then((response) => {
                    console.log(response);
                    this.rooms = response.data;
                    if(this.rooms.length == 0){
                        this.loadingRooms = false
                    }
                })
                .catch((error) => {
                    console.log(error);
                    this.loadingRooms = false;
                });
        },

        createRoom() {
            var formData = new FormData();
            for (var key in this.editedRoom) {
                formData.append(key, this.editedRoom[key]);
            }

            for (let [name, value] of formData) {
                console.log(`${name} = ${value}`)
            }

            console.log(formData)
            axios
                .post("https://localhost:7168/api/rooms", formData)
                .then((response) => {
                    console.log(response);
                    this.getRooms();
                    this.close();
                })
                .catch((error) => {
                    console.log(error);
                });
        },

        deleteRoom(room) {
            this.editedIndex = this.rooms.indexOf(room);
            this.editedRoom = Object.assign({}, room);
            this.dialogDelete = true;
        },

        deleteRoomConfirm() {
            axios
                .delete("https://localhost:7168/api/rooms/" + this.editedRoom.id)
                .then((response) => {
                    console.log(response);
                    this.getRooms()
                    this.closeDelete();
                })
                .catch((error) => {
                    console.log(error);
                });
        },

        editRoom(room) {
            this.editedIndex = this.rooms.indexOf(room);
            console.log(this.editedIndex);
            this.editedRoom = Object.assign({}, room);
            this.dialog = true;
        },

        update() {
            var formData = new FormData();
            for (var key in this.editedRoom) {
                formData.append(key, this.editedRoom[key]);
            }

            for (let [name, value] of formData) {
                console.log(`${name} = ${value}`)
            }

            console.log(formData)

            axios
                .put("https://localhost:7168/api/rooms/" + this.editedRoom.id, formData)
                .then((response) => {
                    console.log(response);
                    this.getRooms();
                    this.close();
                })
                .catch((error) => {
                    console.log(error);
                });
        },

        updateFile(file) {
            console.log(file);
            this.editedRoom.imageByte = file
        },

        closeDelete() {
            this.dialogDelete = false;
            this.$nextTick(() => {
                this.editedRoom = Object.assign({},this.defaultRoom)
                this.editedIndex = -1;
            });
        },

        close() {
            this.dialog = false;
            this.$nextTick(() => {
                this.editedRoom = Object.assign({},this.defaultRoom)
                this.editedIndex = -1;
            });
        },

        save() {
            if (this.editedIndex > -1) {
                this.update();
            } else {
                this.createRoom();
            }
            this.close();
        },

        presentRoom(item) {
            console.log(item)
            let auxItem = Object.assign({}, item)
            if (auxItem.imageByte != "") {
                auxItem.imageByte = "data:image/png;base64," + item.imageByte
            }
            this.presentedRoom = Object.assign({}, auxItem)
        },

        cleanPresentRoom(){
            this.presentedRoom = Object.assign({},this.defaultRoom)
        }
    },

    computed: {
        headers() {
            return [
                {
                    text: "Name",
                    align: "start",
                    sortable: false,
                    value: "name",
                },
                {
                    text: "Length",
                    align: "start",
                    sortable: false,
                    value: "length",
                },
                {
                    text: "Width",
                    align: "start",
                    sortable: true,
                    value: "width",
                },
                {
                    text: "Actions",
                    align: "end",
                    sortable: false,
                    value: "actions"
                },
            ];
        },

        formTitle() {
            return this.editedIndex === -1 ? "New Room" : "Edit Room";
        },
    },
    watch: {
        rooms() {
            if (this.rooms.length > 0) {
                this.loadingRooms = false;
            }
        },
    },
    mounted() {
        this.getRooms();
    },
};
</script>