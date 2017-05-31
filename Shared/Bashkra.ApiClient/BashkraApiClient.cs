using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Bashkra.ApiClient.Requests;
using Bashkra.ApiClient.Responses;
using Bashkra.Shared.Enums;
using Newtonsoft.Json;

namespace Bashkra.ApiClient
{
    public class BashkraApiClient
    {
        public Version Version => new Version(1, 0, 0);

        public Language Language { get; set; }

        public string ApiKey { get; set; }

        public string AccessToken { get; set; }

        #region Info

        public async Task<CountriesApiResponse> InfoCountriesAsync()
        {
            return await TryInvoceAsync<CountriesApiResponse>(async () =>
            {
                var httpClient = GetHttpClientWidthAuthenticationHeader();
                var content = new StringContent(JsonConvert.SerializeObject(new object()), Encoding.UTF8,
                    "application/json");
                return await httpClient.PostAsync("info/countries", content);
            });
        }

        public async Task<LanguagesApiResponse> InfoLanguagesAsync()
        {
            return await TryInvoceAsync<LanguagesApiResponse>(async () =>
            {
                var httpClient = GetHttpClientWidthAuthenticationHeader();
                var content = new StringContent(JsonConvert.SerializeObject(new object()), Encoding.UTF8,
                    "application/json");
                return await httpClient.PostAsync("info/languages", content);
            });
        }

        public async Task<SkillsApiResponse> InfoSkillsAsync()
        {
            return await TryInvoceAsync<SkillsApiResponse>(async () =>
            {
                var httpClient = GetHttpClientWidthAuthenticationHeader();
                var content = new StringContent(JsonConvert.SerializeObject(new object()), Encoding.UTF8,
                    "application/json");
                return await httpClient.PostAsync("info/skills", content);
            });
        }

        public async Task<NationalitiesApiResponse> InfoNationalitiesAsync(NationalitiesArgs args)
        {
            return await TryInvoceAsync<NationalitiesApiResponse>(async () =>
            {
                var httpClient = GetHttpClientWidthAuthenticationHeader();
                var content = new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8,
                    "application/json");
                return await httpClient.PostAsync("info/nationalities", content);
            });
        }

        public async Task<CitiesApiResponse> InfoCitiesAsync(CitiesArgs args)
        {
            return await TryInvoceAsync<CitiesApiResponse>(async () =>
            {
                var httpClient = GetHttpClientWidthAuthenticationHeader();
                var content = new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8,
                    "application/json");
                return await httpClient.PostAsync("info/cities", content);
            });
        }

        public async Task<VisaStatusesApiResponse> InfoVisaStatusesAsync()
        {
            return await TryInvoceAsync<VisaStatusesApiResponse>(async () =>
            {
                var httpClient = GetHttpClientWidthAuthenticationHeader();
                var content = new StringContent(JsonConvert.SerializeObject(new object()), Encoding.UTF8,
                    "application/json");
                return await httpClient.PostAsync("info/visa_statuses", content);
            });
        }

        public async Task<PackagesApiResponse> InfoPackagesAsync()
        {
            return await TryInvoceAsync<PackagesApiResponse>(async () =>
            {
                var httpClient = GetHttpClientWidthAuthenticationHeader();
                var content = new StringContent(JsonConvert.SerializeObject(new object()), Encoding.UTF8,
                    "application/json");
                return await httpClient.PostAsync("info/packages", content);
            });
        }

        public async Task<MaidsApiResponse> InfoMaidsAsync(MaidsArgs args)
        {
            return await TryInvoceAsync<MaidsApiResponse>(async () =>
            {
                var httpClient = GetHttpClientWidthAuthenticationHeader();
                var content = new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8,
                    "application/json");
                return await httpClient.PostAsync("info/maids", content);
            });
        }

        public async Task<AgenciesApiResponse> InfoAgenciesAsync(AgenciesArgs args)
        {
            return await TryInvoceAsync<AgenciesApiResponse>(async () =>
            {
                var httpClient = GetHttpClientWidthAuthenticationHeader();
                var content = new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8,
                    "application/json");
                return await httpClient.PostAsync("info/agencies", content);
            });
        }

        #endregion

        #region Helpers

        private static async Task<T> TryInvoceAsync<T>(Func<Task<HttpResponseMessage>> func)
            where T : ApiResponse, new()
        {
            try
            {
                var responseMessage = await func();
                if (!responseMessage.IsSuccessStatusCode)
                {
                    var msg = await responseMessage.Content.ReadAsStringAsync();

                    return
                        GenerateErroResponse<T>(
                            $"API respons error: <{responseMessage.ReasonPhrase}> when try get request from {responseMessage.RequestMessage.RequestUri} with method {responseMessage.RequestMessage.Method} with error message {msg}");
                }
                var responseContext = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseContext);
            }
            catch (Exception ex)
            {
                return GenerateErroResponse<T>(ex.Message);
            }
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(Constants.API_BASE_URL),
                Timeout = TimeSpan.FromSeconds(25)
            };

            // add language to header
            httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(Language.ToString()));

            // add api-key to header
            httpClient.DefaultRequestHeaders.Add(Constants.HEADER_ODRI_API_KEY_NAME, ApiKey);

            return httpClient;
        }

        private HttpClient GetHttpClientWidthAuthenticationHeader()
        {
            var httpClient = GetHttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            return httpClient;
        }

        private static T GenerateErroResponse<T>(string errorMesage) where T : ApiResponse, new()
        {
            var errorResult = new T();
            errorResult.Error = errorMesage;
            return errorResult;
        }

        #endregion

        #region Auth

        public async Task<SignInApiResponse> AuthSignUp(SignUpArgs args)
        {
            return await TryInvoceAsync<SignInApiResponse>(async () =>
            {
                var httpClient = GetHttpClientWidthAuthenticationHeader();
                var content = new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8, "application/json");

                return await httpClient.PostAsync("auth/signup", content);
            });
        }

        public async Task<SignInApiResponse> AuthSignIn(string email, string password)
        {
            return await TryInvoceAsync<SignInApiResponse>(async () =>
            {
                var httpClient = GetHttpClientWidthAuthenticationHeader();
                var content = new StringContent(JsonConvert.SerializeObject(new SignInArgs
                {
                    Email = email,
                    Password = password
                }), Encoding.UTF8, "application/json");

                return await httpClient.PostAsync("auth/signin", content);
            });
        }

        public async Task<SignInApiResponse> ExternalSignIn(ExternalSignInArgs args)
        {
            return await TryInvoceAsync<SignInApiResponse>(async () =>
            {
                var httpClient = GetHttpClientWidthAuthenticationHeader();
                var content = new StringContent(JsonConvert.SerializeObject(args), Encoding.UTF8, "application/json");

                return await httpClient.PostAsync("auth/external_signin", content);
            });
        }

        public async Task<ApiResponse> AuthSignOut()
        {
            return await TryInvoceAsync<ApiResponse>(async () =>
            {
                var httpClient = GetHttpClientWidthAuthenticationHeader();
                var content = new StringContent(JsonConvert.SerializeObject(new object()), Encoding.UTF8,
                    "application/json");

                return await httpClient.PostAsync("auth/signout", content);
            });
        }

        /*
        public async Task<SignUpApiResponse> AuthSignUp(string email, string password)
        {
            return await TryInvoceAsync<SignUpApiResponse>(async () =>
            {
                var httpClient = GetHttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(new SignUpArgs
                {
                    Email = email,
                    Password = password,
                    ConfirmPassword = password
                }), Encoding.UTF8, "application/json");

                return await httpClient.PostAsync("auth/signup", content);
            });
        }
        */

        #endregion
    }
}