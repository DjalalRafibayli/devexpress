using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Xml;

namespace DevexpressDemoVue.Service
{
    public class ResponseGetaway : IResponseGetaway
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly IConfiguration _configuration;

        public ResponseGetaway(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<string> GetTAsync(string url)
        {
            var client = _clientFactory.CreateClient("recipeService");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(client.BaseAddress + url),
                Headers =
                {
                    { HttpRequestHeader.AcceptLanguage.ToString(), CultureInfo.CurrentCulture.Name },
                }
            };
            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SendStringAsync<T>(List<KeyValuePair<string, string>> queryParams, string url, HttpMethod method)
        {
            var client = _clientFactory.CreateClient("recipeService");

            Dictionary<string, string> values = new();
            if (queryParams is not null)
            {
                foreach (var item in queryParams)
                {
                    values.Add(item.Key, item.Value);
                }
            }

            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(client.BaseAddress + url),
                Content = new FormUrlEncodedContent(values),
                Headers =
                {
                    { HttpRequestHeader.AcceptLanguage.ToString(), CultureInfo.CurrentCulture.Name },
                }
            };
            var response = await client.SendAsync(request);

            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> SendTAsync<T>(T t, string url, HttpMethod method)
        {
            try
            {
                var client = _clientFactory.CreateClient("recipeService");
                var request = new HttpRequestMessage
                {
                    Method = method,
                    RequestUri = new Uri(client.BaseAddress + url),
                    Content = new StringContent(JsonConvert.SerializeObject(t), Encoding.UTF8, "application/json"),
                    Headers =
                {
                    { HttpRequestHeader.AcceptLanguage.ToString(), CultureInfo.CurrentCulture.Name },
                }
                };
                var response = await client.SendAsync(request);

                return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
