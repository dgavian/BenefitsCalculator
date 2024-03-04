using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Models;
using Newtonsoft.Json;
using Xunit;

namespace ApiTests;

internal static class ShouldExtensions
{
    // There was no need that I could see for this method to be async or to exist as a pass-through to a private method.
    public static void ShouldReturn(this HttpResponseMessage response, HttpStatusCode expectedStatusCode)
    {
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }
    
    public static async Task ShouldReturn<T>(this HttpResponseMessage response, HttpStatusCode expectedStatusCode, T expectedContent)
    {
        response.ShouldReturn(expectedStatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(await response.Content.ReadAsStringAsync());
        Assert.True(apiResponse.Success);
        Assert.Equal(JsonConvert.SerializeObject(expectedContent), JsonConvert.SerializeObject(apiResponse.Data));
    }
}

