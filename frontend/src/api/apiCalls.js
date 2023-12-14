
export const fetchDataFromApi = async (params) => {
    try {
      const apiUrl = "http://localhost:5000/Sample/Get";

      const response = await fetch(apiUrl+params);
      const items = await response.json();
      console.log(items);
      return items;
    }
    catch (error) {
    console.log('Nie udalo sie dobic do API');
    throw Error;
    }
};

export const downloadCsvFromApi = async(params) => {
  try {
    const apiUrl = "http://localhost:5000/Sample/GetCsv";
    const response = await fetch(apiUrl + params);
    const csvData = await response.text();
    //console.log("csvdata:", csvData)
    
    
    return csvData;
  }catch(error){
    console.log("Error downloading csv", error)
    throw error;
  }
};

export const downloadJsonFromApi = async (params) => {
  try {
    const apiUrl = "http://localhost:5000/Sample/GetJson";

    const response = await fetch(apiUrl + params);
    const jsonData = await response.json();
    //console.log(jsonData);


    return jsonData;
  } catch (error) {
    console.log('Error downloading json', error);
    throw error;
  }
};

export const fetchNewestDataFromApi = async (params) => {
  try {
    const apiUrl = "http://localhost:5000/Sample/GetNewest";

    const response = await fetch(apiUrl + params);
    const newestData = await response.json();



    return newestData;
  } catch (error) {
    console.log('Error getting newest data', error);
    throw error;
  }
};

export const fetchAvgDataFromApi = async (params) => {
  try {
    const apiUrl = "http://localhost:5000/Sample/GetAvg";

    const response = await fetch(apiUrl + params);
    const avgData = await response.json();

    return avgData;
  } catch (error) {
    console.log('Error getting average data', error);
    throw error;
  }
};

export default { fetchDataFromApi };