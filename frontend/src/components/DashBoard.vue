<template>
    <div>
      <div v-for="sensor in sensors" :key="sensor.id">
        <h2>{{ sensor.name }}</h2>
        <p>Ostatnia wartość: {{ sensor.lastValue }}</p>
        <p>Średnia wartość (ostatnie 100 komunikatów): {{ sensor.averageValue }}</p>
      </div>
    </div>
  </template>
  
  <script>
  import { fetchNewestDataFromApi, fetchAvgDataFromApi } from '../api/apiCalls';
  
  export default {
    data() {
      return {
        sensors: [],
      };
    },
    mounted() {
      fetchNewestDataFromApi().then(newestData => {
        this.sensors = newestData.map(sensor => ({
          id: sensor.id,
          name: sensor.name,
          lastValue: sensor.values.length > 0 ? sensor.values[0].value : null,
          averageValue: null,
        }));
  
  
        this.sensors.forEach(sensor => {
          fetchAvgDataFromApi(sensor.id).then(avgValue => {
            sensor.averageValue = avgValue;
          }).catch(error => {
            console.error(`Błąd podczas pobierania średniej wartości: ${error}`);
          });
        });
      }).catch(error => {
        console.error(`Błąd podczas pobierania najnowszych danych: ${error}`);
      });
    },
  };
  </script>
