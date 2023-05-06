using SocketIOClient.Transport.Http;
using System.Net;
using System.Reflection;

namespace LemonsTiming24.Server.Infrastructure.SocketIO;

public class DebuggingHttpClient : IHttpClient
{
    public DebuggingHttpClient(IHttpClientFactory clientFactory)
    {
        this.httpClient = clientFactory.CreateClient("SocektIO");
    }

    private readonly HttpClient httpClient;

    private static readonly HashSet<string> allowedHeaders = new()
    {
            "user-agent",
        };

    public void AddHeader(string name, string value)
    {
        if (this.httpClient.DefaultRequestHeaders.Contains(name))
        {
            _ = this.httpClient.DefaultRequestHeaders.Remove(name);
        }

        if (allowedHeaders.Contains(name.ToLowerInvariant()))
        {
            _ = this.httpClient.DefaultRequestHeaders.TryAddWithoutValidation(name, value);
        }
        else
        {
            this.httpClient.DefaultRequestHeaders.Add(name, value);
        }
    }

    public IEnumerable<string> GetHeaderValues(string name)
    {
        return this.httpClient.DefaultRequestHeaders.GetValues(name);
    }

    public void SetProxy(IWebProxy proxy)
    {
        var handler = this.httpClient.GetType().BaseType!.GetField("handler", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(this.httpClient) as HttpClientHandler;
        handler!.Proxy = proxy;
    }

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return this.httpClient.SendAsync(request, cancellationToken);
    }

    public Task<HttpResponseMessage> PostAsync(string requestUri,
        HttpContent content,
        CancellationToken cancellationToken)
    {
        return this.httpClient.PostAsync(requestUri, content, cancellationToken);
    }

    public Task<string> GetStringAsync(Uri requestUri)
    {
        return this.httpClient.GetStringAsync(requestUri);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.httpClient.Dispose();
        }
    }
}