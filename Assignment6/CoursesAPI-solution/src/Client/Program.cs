namespace Client {
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using IdentityModel.Client;

    public class Program {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync() {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");

            // create token client instance
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");

            // request token (alice)
            var tokenResponse_alice = await GetTokenResponse(tokenClient, "alice");
            // request token (bob)
            var tokenResponse_bob = await GetTokenResponse(tokenClient, "bob");

            // create http client instance and set bearer token (alice)
            var client_alice = new HttpClient();
            client_alice.SetBearerToken(tokenResponse_alice.AccessToken);
            // create http client instance and set bearer token (bob)
            var client_bob = new HttpClient();
            client_bob.SetBearerToken(tokenResponse_bob.AccessToken);

            // call api (alice)
            await CallAPI(client_alice, "Alice");
            // call api (bob)
            await CallAPI(client_bob, "Bob");
        }

        private static async Task CallAPI(HttpClient client, string username) {
            var message = $"{username} is calling the API!";
            Console.WriteLine(message);
            Console.WriteLine(new String('-', message.Length));

            // get courses
            Console.WriteLine("GET /api/courses:");
            var response = await client.GetAsync("http://localhost:5001/api/courses");
            GetResponseMessage(response);

            // get course
            Console.WriteLine("GET /api/courses/1:");
            response = await client.GetAsync("http://localhost:5001/api/courses/1");
            GetResponseMessage(response);

            // post courses
            Console.WriteLine("POST /api/courses:");
            response = await client.PostAsync("http://localhost:5001/api/courses", null);
            GetResponseMessage(response);

            Console.Write(username.Equals("Alice") ? "\n\n" : "");
        }

        private static async Task<TokenResponse> GetTokenResponse(TokenClient tokenClient, string username) {
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(
                    username, "password", "api1");
            if (tokenResponse.IsError) {
                Console.WriteLine(tokenResponse.Error);
                System.Environment.Exit(-1);
            }

            return tokenResponse;
        }

        private static void GetResponseMessage(HttpResponseMessage response) {
            if (!response.IsSuccessStatusCode) {
                Console.WriteLine(response.StatusCode);
            } else {
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
