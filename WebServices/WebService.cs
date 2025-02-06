using PublicAPI.Utiities;

namespace PublicAPI.WebServices
{
    public class WebService
    {
        public WebService() { }

        private readonly HttpClient _client;
        private readonly BaseApiUrl _baseUrl;
        public WebService(HttpClient client, BaseApiUrl baseUrl)
        {
            _client = client;
            _baseUrl = baseUrl;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }

        public async Task<string> GetMathProp(int numb)
        {
            //https://localhost:44318/api/examsSettings/index
            try
            {
                var uri = new Uri(_baseUrl.Value + "/#"+ numb);
                var response = await _client.GetAsync(uri);

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(content);

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

