using EPPMS.Portal.Exceptions;
using System.Net.Http.Json;
using System.Text.Json;

namespace EPPMS.Portal.Services.ApiClients;

public abstract class BaseApiClient
{
    protected readonly HttpClient HttpClient;

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    protected BaseApiClient(HttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);

        HttpClient = httpClient;
    }

    #region GET

    protected async Task<TResponse?> GetAsync<TResponse>(
        string requestUri,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response =
            await HttpClient.GetAsync(requestUri, cancellationToken);

        await EnsureSuccessStatusCodeAsync(response);

        return await response.Content.ReadFromJsonAsync<TResponse>(
            JsonSerializerOptions,
            cancellationToken);
    }

    #endregion

    #region POST

    protected async Task PostAsync<TRequest>(
        string requestUri,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response =
            await HttpClient.PostAsJsonAsync(
                requestUri,
                request,
                JsonSerializerOptions,
                cancellationToken);

        await EnsureSuccessStatusCodeAsync(response);
    }

    protected async Task<TResponse?> PostAsync<TRequest, TResponse>(
        string requestUri,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response =
            await HttpClient.PostAsJsonAsync(
                requestUri,
                request,
                JsonSerializerOptions,
                cancellationToken);

        await EnsureSuccessStatusCodeAsync(response);

        return await response.Content.ReadFromJsonAsync<TResponse>(
            JsonSerializerOptions,
            cancellationToken);
    }

    #endregion

    #region PUT

    protected async Task PutAsync<TRequest>(
        string requestUri,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response =
            await HttpClient.PutAsJsonAsync(
                requestUri,
                request,
                JsonSerializerOptions,
                cancellationToken);

        await EnsureSuccessStatusCodeAsync(response);
    }

    protected async Task<TResponse?> PutAsync<TRequest, TResponse>(
        string requestUri,
        TRequest request,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response =
            await HttpClient.PutAsJsonAsync(
                requestUri,
                request,
                JsonSerializerOptions,
                cancellationToken);

        await EnsureSuccessStatusCodeAsync(response);

        return await response.Content.ReadFromJsonAsync<TResponse>(
            JsonSerializerOptions,
            cancellationToken);
    }

    #endregion

    #region DELETE

    protected async Task DeleteAsync(
        string requestUri,
        CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response =
            await HttpClient.DeleteAsync(
                requestUri,
                cancellationToken);

        await EnsureSuccessStatusCodeAsync(response);
    }

    #endregion

    #region Helpers

    protected static async Task EnsureSuccessStatusCodeAsync(
      HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        string message = await response.Content.ReadAsStringAsync();

        throw new ApiException(
            (int)response.StatusCode,
            message);
    }

    #endregion
}