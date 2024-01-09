namespace DevexpressDemoVue.Service
{
    public interface IResponseGetaway
    {
        Task<string> GetTAsync(string url);
        //Task<HttpResponseMessage> SendTAsync<T>(T t, string url, HttpMethod method);
        Task<string> SendTAsync<T>(T t, string url, HttpMethod method);
        Task<string> SendStringAsync<T>(List<KeyValuePair<string, string>> queryParams, string url, HttpMethod method);
    }
}
