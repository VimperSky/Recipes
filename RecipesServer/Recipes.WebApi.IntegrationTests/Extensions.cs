using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Recipes.WebApi.IntegrationTests.TestDbProviders;

namespace Recipes.WebApi.IntegrationTests
{
    public static class Extensions
    {
        public static void SetAuthToken(this HttpClient httpClient, bool useSecondToken = false)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                useSecondToken ? TestUserDbProvider.Token2 : TestUserDbProvider.Token1);
        }

        /// <summary>
        ///     Copied from HttpClientJsonExtensions PostAsJsonAsync and modified to Patch
        /// </summary>
        /// <param name="client"></param>
        /// <param name="requestUri"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this HttpClient client, string? requestUri,
            TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            var content = JsonContent.Create(value, null, options);
            return client.PatchAsync(requestUri, content, cancellationToken);
        }
    }
}