namespace LemonsTiming24.Server.Infrastructure.SocketIO;

internal class HttpClientRequestTrace : DelegatingHandler
{
    private readonly ILogger<HttpClientRequestTrace> logger;

    public HttpClientRequestTrace(ILogger<HttpClientRequestTrace> logger)
    {
        this.logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var logPayloads = true;

        HttpResponseMessage? response = null;
        try
        {
            response = await base.SendAsync(request, cancellationToken);

        }
        catch (Exception)
        {
            // We want to log HttpClient request/response when some exception occurs, so we can reproduce what caused it.
            logPayloads = true;
            throw;
        }
        finally
        {
            // Finally, we check if we decided to log HttpClient request/response or not.
            // Only if we want to, we will have some allocations for the logger and try to read headers and contents.
            if (logPayloads)
            {
                if (this.logger is not null)
                {
                    this.logger.LogInformation("Request Headers\n{Request}", request);
                    if (request?.Content != null)
                    {
                        this.logger.LogInformation("Request Body\n{Request}",                         await request.Content.ReadAsStringAsync(cancellationToken));
                    }
                    this.logger.LogInformation("Response Headers\n{Response}", response);
                    if (response?.Content != null)
                    {
                        this.logger.LogInformation("Response Body\n{Request}", await response.Content.ReadAsStringAsync(cancellationToken));
                    }
                }
            }
        }

        return response;
    }
}
