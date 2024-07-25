<template>
    <v-data-table :headers="headers" :items="logs" :items-per-page="5" class="elevation-1"
        :loading="loadingLogs === true ? true : false">
        <template v-slot:top>
            <v-toolbar flat>
                <v-toolbar-title>{{ $route.name }}</v-toolbar-title>
                <v-dialog v-model="dialogDelete" max-width="500px">
                    <v-card>
                        <v-card-title class="text-h5">{{ deleteMessage }}</v-card-title>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="primary" text @click="closeDelete">Cancel</v-btn>
                            <v-btn color="primary" text @click="deleteLogConfirm">OK</v-btn>
                            <v-btn color="primary" text @click="deleteAllLogsFromTagConfirm">Delete All Logs With this
                                Tag</v-btn>
                            <v-spacer></v-spacer>
                        </v-card-actions>
                    </v-card>
                </v-dialog>
            </v-toolbar>
        </template>

        <template v-slot:[`item.actions`]="{ item }">
            <v-icon small @click="deleteLog(item)"> mdi-delete </v-icon>
        </template>
    </v-data-table>
</template>

<script>
import axios from 'axios';

export default {
    name: "TagLogs",

    data() {
        return {
            deleteMessage: "Are you sure you want to delete this log?",
            dialog: false,
            dialogDelete: false,
            search: "",
            logs: [],
            loadingLogs: true,
            selectedLog: {
                id: "",
                tagEPC: "",
            }
        };
    },
    computed: {
        headers() {
            return [
                {
                    text: "RSSI",
                    align: "start",
                    sortable: false,
                    value: "rssi",
                },
                {
                    text: "IP",
                    align: "start",
                    sortable: true,
                    value: "ipAddress",
                },
                {
                    text: "Angle",
                    align: "start",
                    sortable: true,
                    value: "angle",
                },
                {
                    text: "TAG EPC",
                    align: "start",
                    sortable: true,
                    value: "tagEPC",
                },
                {
                    text: "Timestamp",
                    align: "start",
                    sortable: true,
                    value: "timestamp",
                },
                {
                    text: "Actions",
                    align: "end",
                    sortable: false,
                    value: "actions"
                }
            ];
        },
    },
    methods: {
        getLogs() {
            axios
                .get("https://localhost:7168/api/logs/all")
                .then((response) => {
                    console.log(response.data)
                    this.logs = response.data
                }).catch((error) => {
                    console.log(error)
                })
        },
        deleteLog(log) {
            console.log(log.id);
            this.selectedLog = Object.assign({}, log);
            this.dialogDelete = true;
        },
        deleteLogConfirm() {
            axios
                .delete("https://localhost:7168/api/logs/" + this.selectedLog.id)
                .then((response) => {
                    console.log(response);
                    this.getLogs();
                    this.closeDelete();
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        deleteAllLogsFromTagConfirm() {
            axios
                .delete("https://localhost:7168/api/tags/" + this.selectedLog.tagEPC + "/logs")
                .then((response) => {
                    console.log(response);
                    this.getLogs();
                    this.closeDelete();
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        closeDelete() {
            this.dialogDelete = false;
        },
    },
    watch: {
        logs() {
            if (this.logs.length > 0) {
                this.loadingLogs = false;
            }
        }
    },
    mounted() {
        this.getLogs();
    },
};
</script>