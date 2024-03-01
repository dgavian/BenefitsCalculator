using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Models;
using Newtonsoft.Json;
using Xunit;

namespace ApiTests;

internal static class ShouldExtensions
{
    public static void ShouldReturn(this HttpResponseMessage response, HttpStatusCode expectedStatusCode)
    {
        AssertCommonResponseParts(response, expectedStatusCode);
    }
    
    public static async Task ShouldReturn<T>(this HttpResponseMessage response, HttpStatusCode expectedStatusCode, T expectedContent)
    {
        response.ShouldReturn(expectedStatusCode);
        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
        var apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(await response.Content.ReadAsStringAsync());
        Assert.True(apiResponse.Success);
        Assert.Equal(JsonConvert.SerializeObject(expectedContent), JsonConvert.SerializeObject(apiResponse.Data));
    }

    private static void AssertCommonResponseParts(this HttpResponseMessage response, HttpStatusCode expectedStatusCode)
    {
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }
}

