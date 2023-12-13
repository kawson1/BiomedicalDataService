
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
}

export default { fetchDataFromApi };