<template>
  <div>
    <v-container fluid>
      <v-row>
        <v-col cols="12" md="6">
          <form ref="form" @submit.prevent="calculatePosition()">

            <v-select v-model="epc" label="Tag EPC" :items="tags" item-text="epc" item-value="epc"
              variant="solo"></v-select>

            <v-btn type="submit" class="mt-4" color="primary">Submit</v-btn>
          </form>
        </v-col>
        <v-col>
          <highcharts :options="chartOptions"></highcharts>
        </v-col>
      </v-row>
    </v-container>
    <v-container>
      <v-alert type="error" v-text="errorMessage" v-show="visible"></v-alert>
    </v-container>
    <v-container v-if='this.tagPoint.room.name != ""'>
      <h3>Info</h3>
      <h4>Room Name: {{ this.tagPoint.room.name }}</h4>
    </v-container>
  </div>
</template>
<script>
import axios from "axios";

export default {
  data() {
    return {
      epc: "",
      chartOptions: {
        chart: {
          type: 'scatter',
          plotBackgroundImage: 'https://www.highcharts.com/samples/graphics/skies.jpg'
        },
        plotOptions:{
          scatter:{
            lineWidth: 2
          }
        },
        title: {
          text: "2D Position"
        },
        xAxis: {
          max: 10,
          min: 0
        },
        yAxis: {
          max: 10,
          min: 0
        },
        series: [{
          data: [[4, 0],[9, 1],[5, 4]], // sample data
          type: 'scatter'
        }],
      },
      tags: [],
      visible: false,
      errorMessage: "",
      alertType: "error",
      tagPoint: {
        pairAntenna: {
          antenna01IP: "",
          antenna01X: 0,
          antenna01Y: 0,
          antenna02IP: "",
          antenna02X: 0,
          antenna02Y: 0,
        },
        locX: 0,
        locY: 0,
        room: {
          imageByte: "",
          name: "",
          width: 0,
          length: 0
        }
      },
      defaultTagPoint: {
        pairAntenna: {
          antenna01IP: "",
          antenna01X: 0,
          antenna01Y: 0,
          antenna02IP: "",
          antenna02X: 0,
          antenna02Y: 0,
        },
        locX: 0,
        locY: 0,
        room: {
          imageByte: "",
          name: "",
          width: 0,
          length: 0
        }
      }
    }
  },

  methods: {
    redrawChart() {
      this.chartOptions.chart.backgroundColor = null
      this.chartOptions.chart.plotBackgroundImage = "data:image/png;base64," + this.tagPoint.room.imageByte
      this.chartOptions.xAxis.max = this.tagPoint.room.width
      this.chartOptions.yAxis.max = this.tagPoint.room.length
      this.chartOptions.series = [
        {
          name: this.tagPoint.pairAntenna.antenna01IP,
          marker: {
              symbol: 'circle'
          },
          data: [
            {
              x: this.tagPoint.pairAntenna.antenna01X,
              y: this.tagPoint.pairAntenna.antenna01Y,
              color: '#888888'
            },
            {
              x: this.tagPoint.locX, 
              y: this.tagPoint.locY,
              color: '#000000'
            }
          ]
        },
        {
          name: this.tagPoint.pairAntenna.antenna02IP,
          marker: {
              symbol: 'circle'
          },
          data: [
            {
              x: this.tagPoint.pairAntenna.antenna02X,
              y: this.tagPoint.pairAntenna.antenna02Y,
              color: '#888888'
            },
            {
              x: this.tagPoint.locX, 
              y: this.tagPoint.locY,
              color: '#000000'
            }
          ]
        } 
      ]
    },

    calculatePosition() {
      console.log(this.position)
      this.visible = false
      axios
        .get("https://localhost:7168/api/tags/" + this.epc + "/position",)
        .then((response) => {
          console.log(response.data);
          this.tagPoint = Object.assign({}, response.data)
          console.log(this.tagPoint)
          this.redrawChart();
          this.visible = false
        }).catch((error) => {
          console.log(error)
          this.visible = true
          this.errorMessage = error.response.data
          this.getLastKnownPosition()
        })
    },

    getLastKnownPosition(){
      axios.get("https://localhost:7168/api/tags/positions/" + this.epc+ "/last")
          .then((response) => {
            console.log(response.data);
            this.tagPoint = Object.assign({}, response.data)
            console.log(this.tagPoint)
            this.redrawChart();
          }).catch((error) => {
            console.log(error)
            this.tagPoint = Object.assign({}, this.defaultTagPoint)
            this.errorMessage = error.response.data
          })
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
}
</script>