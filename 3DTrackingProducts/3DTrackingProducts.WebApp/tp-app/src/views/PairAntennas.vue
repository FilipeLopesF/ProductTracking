<template>
    <v-container>
        <v-toolbar-title>Image</v-toolbar-title>
        <v-img :src="imageByte"></v-img>
        <v-data-table :items="pairAntennas" :headers="headers">
            <template v-slot:top>
                <v-toolbar flat>
                    <v-toolbar-title>Pair Antennas</v-toolbar-title>

                    <v-divider class="mx-4" inset vertical></v-divider>
                    <v-spacer></v-spacer>
                    <v-dialog v-model="dialog" max-width="500px">
                        <template v-slot:activator="{ on, attrs }">
                            <v-btn color="primary" dark class="mb-2" v-bind="attrs" v-on="on">
                                New Pair Antenna
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
                                                <v-text-field v-model="editedPairAntenna.antenna01IP" type="text"
                                                    label="Antenna 01 IP" required></v-text-field>
                                            </v-col>

                                            <v-col cols="12" sm="6" md="4">
                                                <v-text-field v-model="editedPairAntenna.antenna01X" type="text"
                                                    label="Antenna 01 X Position" required></v-text-field>
                                            </v-col>

                                            <v-col cols="12" sm="6" md="4">
                                                <v-text-field v-model="editedPairAntenna.antenna01Y" type="text"
                                                    label="Antenna 01 Y Position" required></v-text-field>
                                            </v-col>

                                            <v-col cols="12" sm="6" md="4">
                                                <v-text-field v-model="editedPairAntenna.antenna02IP" type="text"
                                                    label="Antenna 02 IP" required></v-text-field>
                                            </v-col>

                                            <v-col cols="12" sm="6" md="4">
                                                <v-text-field v-model="editedPairAntenna.antenna02X" type="text"
                                                    label="Antenna 02 X Position" required></v-text-field>
                                            </v-col>

                                            <v-col cols="12" sm="6" md="4">
                                                <v-text-field v-model="editedPairAntenna.antenna02Y" type="text"
                                                    label="Antenna 02 Y Position" required></v-text-field>
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
                                <v-btn color="primary" text @click="deletePairAntennaConfirm">OK</v-btn>
                                <v-spacer></v-spacer>
                            </v-card-actions>
                        </v-card>
                    </v-dialog>
                </v-toolbar>
            </template>

            <template v-slot:[`item.detectingState`]="{ item }">
                <v-chip :color="getColor(item.detectingState)">
                    {{ getStatus(item.detectingState) }}
                </v-chip>
            </template>

            <template v-slot:[`item.actions`]="{ item }">
                <v-icon small class="mr-2" @click="editPairAntenna(item)"> mdi-pencil </v-icon>
                <v-icon small @click="deletePairAntenna(item)"> mdi-delete </v-icon>
            </template>
        </v-data-table>
    </v-container>
</template>
<script>
import axios from 'axios';
export default {
    name: "PairAntennasVue",

    props: ['id', 'imageByte'],

    data() {
        return {
            warningMessage: "Are you sure you want to delete this item?",
            pairAntennas: [],
            loadingAntennas: false,
            dialog: false,
            dialogDelete: false,
            editedIndex: -1,
            editedPairAntenna: {
                id: "",
                antenna01IP: "",
                antenna02IP: "",
                antenna01X: "",
                antenna01Y: "",
                antenna02X: "",
                antenna02Y: "",
                detectingState: -1,
                idRoom: ""
            },
            defaultPairAntenna: {
                id: "",
                antenna01IP: "",
                antenna02IP: "",
                antenna01X: "",
                antenna01Y: "",
                antenna02X: "",
                antenna02Y: "",
                detectingState: -1,
                idRoom: ""
            },
        }
    },

    methods: {
        getPairAntennas() {
            console.log("called")
            axios
                .get("https://localhost:7168/api/rooms/" + this.id + "/antennas", { timeout: 5000 })
                .then((response) => {
                    console.log(response);
                    this.pairAntennas = response.data;
                    if (this.pairAntennas.length == 0) {
                        this.loadingAntennas = false
                    }
                })
                .catch((error) => {
                    console.log(error);
                    this.loadingAntennas = false;
                });
        },

        deletePairAntenna(pairAntenna) {
            this.editedIndex = this.pairAntennas.indexOf(pairAntenna);
            this.editedPairAntenna = Object.assign({}, pairAntenna);
            this.dialogDelete = true;
        },

        deletePairAntennaConfirm() {
            axios
                .delete("https://localhost:7168/api/antennas/" + this.editedPairAntenna.id)
                .then((response) => {
                    console.log(response);
                    this.getPairAntennas()
                    this.closeDelete();
                })
                .catch((error) => {
                    console.log(error);
                });
        },

        closeDelete() {
            this.dialogDelete = false;
            this.$nextTick(() => {
                this.editedPairAntenna = Object.assign({}, this.defaultPairAntenna)
                this.editedIndex = -1;
            });
        },

        close() {
            this.dialog = false;
            this.$nextTick(() => {
                this.editedPairAntenna = Object.assign({}, this.defaultPairAntenna)
                this.editedIndex = -1;
            });
        },

        createPairAntenna() {
            this.editedPairAntenna.idRoom = this.id;
            axios
                .post("https://localhost:7168/api/antennas", this.editedPairAntenna)
                .then((response) => {
                    console.log(response);
                    this.getPairAntennas();
                    this.closeDelete();
                })
                .catch((error) => {
                    console.log(error);
                });
        },

        save() {
            if (this.editedIndex > -1) {
                this.update();
            } else {
                this.createPairAntenna();
            }
            this.close();
        },

        editPairAntenna(pairAntenna) {
            this.editedIndex = this.pairAntennas.indexOf(pairAntenna);
            console.log(this.editedIndex);
            this.editedPairAntenna = Object.assign({}, pairAntenna);
            this.dialog = true;
        },

        update() {
            axios
                .put("https://localhost:7168/api/antennas/" + this.editedPairAntenna.id, this.editedPairAntenna)
                .then((response) => {
                    console.log(response);
                    this.getPairAntennas();
                    this.closeDelete();
                })
                .catch((error) => {
                    console.log(error);
                });
        },

        getColor(detectingState) {
            switch (detectingState) {
                case -1: return 'yellow'
                case 0: return 'orange'
                case 1: return 'red'
                default: return 'green'
            }
        },

        getStatus(detectingState) {
            switch (detectingState) {
                case -1: return "Not Verified"
                case 0: return "No Control Tag detected"
                case 1: return "Detecting With Error"
                default: return "Detecting Correctly"
            }
        }
    },

    computed: {
        headers() {
            return [
                {
                    text: "Antenna 01 IP",
                    align: "start",
                    sortable: false,
                    value: "antenna01IP",
                },
                {
                    text: "Antenna 01 X",
                    align: "start",
                    sortable: false,
                    value: "antenna01X",
                },
                {
                    text: "Antenna 01 Y",
                    align: "start",
                    sortable: false,
                    value: "antenna01Y",
                },
                {
                    text: "Antenna 02 IP",
                    align: "start",
                    sortable: false,
                    value: "antenna02IP",
                },
                {
                    text: "Antenna 02 X",
                    align: "start",
                    sortable: false,
                    value: "antenna02X",
                },
                {
                    text: "Antenna 02 Y",
                    align: "start",
                    sortable: false,
                    value: "antenna02Y",
                },
                {
                    text: "Detecting State",
                    align: "start",
                    sortable: false,
                    value: "detectingState",
                },
                {
                    text: "Actions",
                    align: "end",
                    sortable: false,
                    value: "actions"
                },
            ]
        },
        formTitle() {
            return this.editedIndex === -1 ? "New Pair Antenna" : "Edit Pair Antenna";
        },
    },
    watch: {
        id() {
            this.getPairAntennas()
        }
    },
    mounted() {
        this.getPairAntennas()
    }
}
</script>