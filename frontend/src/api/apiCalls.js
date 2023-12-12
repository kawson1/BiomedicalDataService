
export const fetchDataFromApi = async () => {
    try {
      const apiUrl = "http://localhost:5000/api/Sample";

      const response = await fetch(apiUrl);
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