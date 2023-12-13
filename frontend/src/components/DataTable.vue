<template>
   <table class="bordered-table">

  <thead>
     <tr>
        <th @click="sortBy('id')">
           ID
           <i
              :class="sortIcon('id')"
              :style="iconStyle('id')"
           ></i>
        </th>
        <th @click="sortBy('date')">
           Data
           <i
              :class="sortIcon('date')"
              :style="iconStyle('date')"
           ></i>
        </th>
        <th @click="sortBy('type')">
           Typ czujnika
           <i
              :class="sortIcon('type')"
              :style="iconStyle('type')"
           ></i>
        </th>
        <th @click="sortBy('instance')">
           Numer instancji
           <i
              :class="sortIcon('instance')"
              :style="iconStyle('instance')"
           ></i>
        </th>
        <th @click="sortBy('value')">
           Wartość
           <i
              :class="sortIcon('value')"
              :style="iconStyle('value')"
           ></i>
        </th>
     </tr>
     <tr>
       <th/>
       <th>
         <input v-model="dateParam" type="datetime-local"/>
       </th>
       <th>
         <select v-model="typeParam">
           <option value="">Wybierz czujnik</option>
           <option value="0">HeartRate</option>
           <option value="1">Temperature</option>
           <option value="2">RespirationRate</option>
           <option value="3">Pressure</option>
         </select>
       </th>
       <th>
         <input v-model="instanceParam" type="number"/>
       </th>
     </tr>
  </thead>
  <tbody>
     <tr
        v-for="item in items"
        :key="item.id"
     >
       <td>{{ item.id }}</td>
       <td>{{ new Date(item.date).toUTCString() }}</td>
       <td>{{ typesEnum[item.sensorType] }}</td>
       <td>{{ item.sensorId }}</td>
       <td>{{ item.value }}</td>
     </tr>
  </tbody>
</table>
<button @click="fetchData">Odśwież tabele</button>
<button @click="downloadCsv">Pobierz CSV</button>
<button @click="downloadJson">Pobierz JSON</button>
<button @click="generateChart">Generuj Wykres</button>
<div><canvas ref="canvasChart"></canvas></div>

</template>

<script>
import { fetchDataFromApi, downloadJsonFromApi,downloadCsvFromApi } from '../api/apiCalls';
import '@fortawesome/fontawesome-free/css/all.min.css';
import Chart from 'chart.js/auto';

export default {
data() {
 return {
   items: [],
   headers: ["ID", "Data", "Typ czujnika", "Numer instancji", "Wartość"],
   sortKey: '',
   sortDirection: 1,
   typesEnum: ["HeartRate", "Temperature", "RespirationRate", "Pressure"],
   dataChart: null
 }
},
mounted() {
 this.typeParam = ""
 this.fetchData()
},
methods: {
createParams() {
   var params = `?`
   if(this.sortKey != '') {
      params += `sortKey=${this.sortKey}&sortDirection=${this.sortDirection}`
   }
   if(this.dateParam != null) {
      params += `&dateFrom=${this.dateParam}`
   }
   if(this.typeParam != "") {
      params += `&SensorType=${this.typeParam}`
   }
   if(this.instanceParam != null) {
      console.log(this.instanceParam)
      params += `&SensorId=${this.instanceParam}`
   }
 return params
},
async fetchData() {
  try {
     this.items = await fetchDataFromApi(this.createParams());
  } catch (error) {
     console.log('Nie udało się pobrać danych: ', error);
  }
},
sortBy(key) {
  if (this.sortKey === key) {
     if(this.sortDirection === -1) {
       this.sortKey = ''
     }
     this.sortDirection = this.sortDirection * -1;
  } else {
     this.sortDirection = 1;
     this.sortKey = key;
  }
},
sortIcon(key) {
  if (this.sortKey === key) {
     return this.sortDirection && 'fas fa-chevron-down';
  }
  return;
},

iconStyle(key) {
  if (this.sortKey === key) {
     return { transform: `rotate(${this.sortDirection === 1 ? 0 : 180}deg)` };
  }
  return {};
},
async downloadCsv() {
   try {
      const csvData = await downloadCsvFromApi(this.createParams());
      this.downloadFile(csvData,'text/csv', 'Samples.csv')
   }catch (error){
      console.log("Error downloading csv", error);
   }
},
async downloadJson() {
   try {
      const jsonData = await downloadJsonFromApi(this.createParams());

      this.downloadFile(jsonData,'application/json', 'Samples.json')
   }catch (error){
      console.log("Error downloading json", error);
   }
},
downloadFile(data, mimeType, fileName) {
      const blob = new Blob([data], { type: mimeType });
      const link = document.createElement('a');

      link.href = window.URL.createObjectURL(blob);
      link.download = fileName;

      // Append the link to the body and trigger the click event
      document.body.appendChild(link);
      link.click();

      // Clean up: Remove the link after the download
      document.body.removeChild(link);
    
  },

  async generateChart() {
      try {
        const jsonData = await fetchDataFromApi(this.createParams());

        const labels = jsonData.map(item => item.Date);
        const values = jsonData.map(item => item.Value);

        if (this.dataChart) {
          this.dataChart.destroy();
        }

        const canvas = this.$refs.canvasChart;


        const ctx = canvas.getContext('2d');
        new Chart(ctx, {
          type: 'bar', 
          data: {
            labels: labels,
            datasets: [{
              label: 'Wartość czujnika',
              data: values,
              backgroundColor: 'rgba(75, 192, 192, 0.2)',
              borderColor: 'rgba(75, 192, 192, 1)', 
              borderWidth: 1 
            }]
          },
          options: {
            scales: {
              y: {
                beginAtZero: true
              }
            }
          }
        });
      } catch (error) {
        console.log('Error generating chart:', error);
      }
    },

},
};
</script>

<style>
.user-table {
display: flex;
justify-content: center;
align-items: center;
margin: 0 1rem;
}

table.bordered-table {
box-shadow: 4px 8px 12px rgba(28, 6, 49, 0.4);
border-collapse: collapse;
border-radius: 2rem;
border: 1px solid black;
overflow: hidden;
width: 100%;
text-align: left;
table-layout: fixed;
}

table.bordered-table th {
font-size: 20px;
}

table.bordered-table th,
table.bordered-table td {
padding: 0.5rem 2rem;
border: 1px solid black;
margin: 3px;
}

table.bordered-table th {
cursor: pointer;
}
th i {
transition: transform 0.3s ease-in-out;
}

.rotate-up {
transform: rotate(180deg); 
}

.rotate-down {
transform: rotate(0deg);
}

.table-user-name p {
font-weight: bold;
cursor: pointer;
color: rgb(86, 81, 81);
border-bottom: 2px solid rgba(255, 255, 255, 0);
display: inline;
transition: all 0.2s ease-in-out;
}

.table-user-name p:hover {
border-bottom: 2px solid black;
}
</style>