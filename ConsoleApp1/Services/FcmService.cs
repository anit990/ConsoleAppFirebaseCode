using Google.Apis.Auth.OAuth2;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using WebAppDemo.Models;

namespace WebAppDemo.Services
{
    public class FcmService
    {
        private readonly string[] _scopes = { "https://www.googleapis.com/auth/firebase.messaging" };
        private readonly string _projectId;
        private readonly IConfiguration _config;
        public FcmService(IConfiguration config, IOptions<FirebaseSettings> firebaseOptions)
        {
            _config = config;
            _projectId = firebaseOptions.Value.ProjectId;
        }
        public async Task<string> GetAccessTokenAsync()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), _config["Firebase:ServiceAccountPath"]);
            var credential = GoogleCredential.FromFile(path).CreateScoped(_scopes);
            return await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        }

        public async Task SendMessageAsync(string targetToken)
        {
            var accessToken = await GetAccessTokenAsync();

            var message = new
            {
                message = new
                {
                    token = targetToken,
                    notification = new
                    {
                        title = "Hello",
                        body = "This is an FCM HTTP v1 message.",
                        image = "https://companieslogo.com/img/orig/PNB.NS_BIG-6ba17682.png"
                    }
                }
            };

            var jsonMessage = JsonConvert.SerializeObject(message);

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var url = $"https://fcm.googleapis.com/v1/projects/{_projectId}/messages:send";
            var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var result = await response.Content.ReadAsStringAsync();

            Console.WriteLine(result);
        }
    }
}
