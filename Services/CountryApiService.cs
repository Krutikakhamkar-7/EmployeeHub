using Newtonsoft.Json;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class CountryApiService
    {
        private readonly HttpClient _httpClient;

        public CountryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // ==========================
        // Get All Countries
        // ==========================
        public async Task<List<Country>> GetCountries()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();

                _httpClient.DefaultRequestHeaders.Add(
                    "X-CSCAPI-KEY",
                    "VWJXSzVWb0xpUUxPMldIZ2Z3OU5iZ1B5WmJ4Y0pOQzg3YTFESUFCNA=="
                );

                var response = await _httpClient.GetStringAsync(
                    "https://api.countrystatecity.in/v1/countries"
                );

                Console.WriteLine(response);

                return JsonConvert.DeserializeObject<List<Country>>(response)
                       ?? new List<Country>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Countries API Error:");
                Console.WriteLine(ex.Message);

                return new List<Country>();
            }
        }

        // ==========================
        // Get States By Country
        // ==========================
        public async Task<List<State>> GetStates(string countryCode)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();

                _httpClient.DefaultRequestHeaders.Add(
                    "X-CSCAPI-KEY",
                    "VWJXSzVWb0xpUUxPMldIZ2Z3OU5iZ1B5WmJ4Y0pOQzg3YTFESUFCNA=="
                );

                var response = await _httpClient.GetStringAsync(
                    $"https://api.countrystatecity.in/v1/countries/{countryCode}/states"
                );

                Console.WriteLine(response);

                return JsonConvert.DeserializeObject<List<State>>(response)
                       ?? new List<State>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("States API Error:");
                Console.WriteLine(ex.Message);

                return new List<State>();
            }
        }

        // ==========================
        // Get Cities By State
        // ==========================
        public async Task<List<City>> GetCities(string countryCode, string stateCode)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Clear();

                _httpClient.DefaultRequestHeaders.Add(
                    "X-CSCAPI-KEY",
                    "VWJXSzVWb0xpUUxPMldIZ2Z3OU5iZ1B5WmJ4Y0pOQzg3YTFESUFCNA=="
                );

                var response = await _httpClient.GetStringAsync(
                    $"https://api.countrystatecity.in/v1/countries/{countryCode}/states/{stateCode}/cities"
                );

                Console.WriteLine(response);

                return JsonConvert.DeserializeObject<List<City>>(response)
                       ?? new List<City>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cities API Error:");
                Console.WriteLine(ex.Message);

                return new List<City>();
            }
        }
    }
}