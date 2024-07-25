<template>
  <v-container fluid>
    <v-row>
      <v-col cols="12" md="6">
        <form ref="form" @submit.prevent="getPosition()">
          <v-combobox
            v-model="tagSelected"
            :items="tags"
            item-value="epc"
            item-text="epc"
            :return-object="true"
            label="Tag"
            clearable
          ></v-combobox>
          <v-btn type="submit" class="mt-4" color="primary">Submit</v-btn>
        </form>
      </v-col>
      <v-col cols="12" md="6">
        <highcharts :options="chartOptions"></highcharts>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import axios from "axios";
export default {
  data() {
    return {
      tags: [],
      tagX: 0,
      tagY: 0,
      tagZ: 0,
      controlTagA_x: 0,
      controlTagA_y: 0,
      controlTagA_z: 0,
      controlTagB_x: 0,
      controlTagB_y: 0,
      controlTagB_z: 0,
      tagSelected: {
        epc:""
      },
      chartOptions: {
        chart: {
          renderTo: "container",
          margin: 100,
          type: "scatter3d",
          animation: false,
          options3d: {
            enabled: true,
            alpha: 10,
            beta: 65,
            depth: 500,
            viewDistance: 5,
            fitToPlot: false,
            frame: {
              bottom: { size: 1, color: "rgba(0,0,0,0.02)" },
              back: { size: 1, color: "rgba(0,0,0,0.04)" },
              side: { size: 1, color: "rgba(0,0,0,0.06)" },
            },
          },
        },
        title: {
          text: "3D Tracking Position",
        },
        subtitle: {
          text: "Click and drag the plot area to rotate in space",
        },
        plotOptions: {
          scatter: {
            width: 40,
            height: 40,
            depth: 40,
          },
        },
        yAxis: {
          min: 0,
          max: 20,
        },
        xAxis: {
          min: 0,
          max: 10,
          gridLineWidth: 1,
        },
        zAxis: {
          min: 0,
          max: 10,
          showFirstLabel: false,
        },
        legend: {
          enabled: false,
        },
        series: [
          {
            name: "Data",
            colorByPoint: false,
            accessibility: {
              exposeAsGroupOnly: false,
            },
            lineWidth: 2,
            data: [
              //y,z,x
              [4.636072051047093, 5.87265, 6.34375],
              //[4, 0, 4],
              //[4, 0, 8],
            ],
          },
        ],
      },
    };
  },
  methods: {
    redrawChart() {
      this.chartOptions.series[0].data = [[3,3,3],[this.tagY, this.tagZ, this.tagX],[3,3,6]];
    },

    getPosition() {
      axios
        .get(`https://localhost:7168/api/tag3D/position/${this.tagSelected.epc}`)
        .then((response) => {
          console.log(response);
          this.tagX = response.data.tagX;
          this.tagY = response.data.tagY;
          this.tagZ = response.data.tagZ;

          this.redrawChart();
        })
        .catch((error) => {
          console.log(error);
        });
    },

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
  },
  mounted() {
    this.getTags();
  },
};
</script>