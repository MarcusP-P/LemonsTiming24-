namespace LemonsTiming24.Server.Infrastructure.SocketIO;

internal sealed partial class HttpClientRequestTrace : DelegatingHandler
{
    private readonly ILogger<HttpClientRequestTrace> logger;

    public HttpClientRequestTrace(ILogger<HttpClientRequestTrace> logger)
    {
        this.logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage response;
        this.LogRequest(request.ToString());
        if (request.Content != null)
        {
            this.LogRequestBody(await request.Content.ReadAsStringAsync(cancellationToken));
        }

        try
        {
            response = await base.SendAsync(request, cancellationToken);

            this.LogResult(response.ToString());
            if (response.Content != null)
            {
                this.LogResultBody(await response.Content.ReadAsStringAsync(cancellationToken));
            }
        }
        catch (Exception ex)
        {
            this.LogException(ex);
            throw;
        }

        return response;
    }

    // Log merssages
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Debug,
        Message = "Request Headers:\n{Request}")]
    public partial void LogRequest(string request);

    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Debug,
        Message = "Request Body:\n{RequestBody}")]
    public partial void LogRequestBody(string requestBody);

    [LoggerMessage(
        EventId = 3,
        Level = LogLevel.Debug,
        Message = "Response Headers:\n{Response}")]
    public partial void LogResult(string response);

    [LoggerMessage(
        EventId = 4,
        Level = LogLevel.Debug,
        Message = "Response Body:\n{ResponseBody}")]
    public partial void LogResultBody(string responseBody);

    [LoggerMessage(
        EventId = 5,
        Level = LogLevel.Warning,
        Message = "Exception while returning")]
    public partial void LogException(Exception ex);
}
