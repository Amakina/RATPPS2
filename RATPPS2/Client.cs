using RATPPS1;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RATPPS2
{
    public class Client
    {
        private HttpClient httpClient = new HttpClient();
        private string url;
        
        public Client(string url)
        {
            this.url = url;
        }

        public async Task<bool> Ping()
        {
            while (true)
            {
                try
                {
                    var response = await httpClient.GetAsync($"{url}/Ping");
                    response.EnsureSuccessStatusCode();
                    return true;

                } catch
                {
                    continue;
                }
            }
        }

        public async Task<Input> GetInputData()
        {
            try
            {
                var isReady = await Ping();
                if (!isReady) throw new HttpRequestException();

                var response = await httpClient.GetAsync($"{url}/GetInputData");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var input = JSONParser.Deserialize<Input>(responseBody);
                return input;

            } catch
            {
                return null;
            }
        }

        public async Task<bool> WriteAnswer(Output answer)
        {
            try
            {
                var isReady = await Ping();
                if (!isReady) throw new HttpRequestException();

                var output = JSONParser.Serizalize(answer);
                var data = new StringContent(output, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync($"{url}/WriteAnswer", data);
                response.EnsureSuccessStatusCode();

                return true;

            } catch
            {
                return false;
            }
        }
    }
}
