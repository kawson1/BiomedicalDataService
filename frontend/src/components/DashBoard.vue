<template>
    <div>
      <select v-model="typeParam">
           <option value="0">HeartRate</option>
           <option value="1">Temperature</option>
           <option value="2">RespirationRate</option>
           <option value="3">Pressure</option>
      </select>
        <p>Ostatnia wartość: {{ lastValue }}</p>
        <p>Średnia wartość (ostatnie 100 komunikatów): {{ avgValue }}</p>
      </div>
  </template>
  
  <script>
  import { fetchNewestDataFromApi, fetchAvgDataFromApi } from '../api/apiCalls';
  
  export default {
    data() {
      return {
        avgValue: 0,
        lastValue: 0
      };
    },
    mounted() {
      this.typeParam = "0";
      this.fetchAvg();
      this.fetchNewest();
    },
    methods: {
      async fetchAvg() {
          try {
        this.avgValue = await fetchAvgDataFromApi(`?sensorType=${this.typeParam}`);
      } catch (error) {
        console.log('Nie udało się pobrać danych: ', error);
      }
        sleep(300).then(() => { this.fetchAvg() });
      },
      async fetchNewest() {
          try {
        this.lastValue = await fetchNewestDataFromApi(`?sensorType=${this.typeParam}`);
      } catch (error) {
        console.log('Nie udało się pobrać danych: ', error);
      }
        sleep(300).then(() => { this.fetchNewest() });
        
      }
    },
  };

  function sleep(ms) {
      return new Promise(resolve => setTimeout(resolve, ms));
    }
  </script>
