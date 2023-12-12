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
           <option value="" selected>Wybierz czujnik</option>
           <option value="czujnik1">Czujnik 1</option>
           <option value="czujnik2">Czujnik 2</option>
           <option value="czujnik3">Czujnik 3</option>
           <option value="czujnik4">Czujnik 4</option>
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
       <td>{{ item.sensorType }}</td>
       <td>{{ item.sensorId }}</td>
       <td>{{ item.value }}</td>
     </tr>
  </tbody>
</table>
<button @click="fetchData">Odśwież tabele</button>
</template>

<script>
import { fetchDataFromApi } from '../api/apiCalls';
import '@fortawesome/fontawesome-free/css/all.min.css';

export default {
data() {
 return {
   items: [],
   headers: ["ID", "Data", "Typ czujnika", "Numer instancji", "Wartość"],
   sortKey: '',
   sortDirection: 1
 }
},
mounted() {
 this.fetchData()
},
methods: {
createParams() {
 var params = '?sortKey=${this.sortKey}&sortDirection=${this.sortDirection}'
 if(this.dateParam != null) {
   params += '&dateFrom=${this.dateParam}'
 }
 return params
},
async fetchData() {
  try {
     this.items = await fetchDataFromApi();
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