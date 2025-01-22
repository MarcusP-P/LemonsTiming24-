using LemonsTiming24.SharedCode;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.WebSockets;

namespace LemonsTiming24.Server.Controllers;
[Route("/api/v1/[controller]")]
[ApiController]
public class WebSocketController(ILogger<WebSocketController> logger) : ControllerBase
{
    private const string webSocketEndpoint = "/ws";
    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Will probably use later.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1823:Avoid unused private fields", Justification = "<Pending>")]
    private readonly ILogger<WebSocketController> logger = logger;

    [HttpGet]
    public WebSocketUrl Get()
    {
        var request = this.HttpContext.Request;
        var webSocketUriBuilder = new UriBuilder
        {
            Scheme = $"ws{(request.IsHttps ? "s" : "")}",
            Host = request.Host.Host,
            Port = request.Host.Port ?? (request.IsHttps ? 433 : 80),
            Path = webSocketEndpoint,
        };

        return new WebSocketUrl { Url = webSocketUriBuilder.Uri };
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route(webSocketEndpoint)]
    public async Task GetWebSocket()
    {
        if (this.HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await this.HttpContext.WebSockets.AcceptWebSocketAsync().ConfigureAwait(false);
            await Echo(webSocket).ConfigureAwait(false);
        }
        else
        {
            this.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }

    private static async Task Echo(WebSocket webSocket)
    {
        _ = webSocket ?? throw new ArgumentNullException(nameof(webSocket));

        var buffer = new byte[1024 * 4];
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None)
            .ConfigureAwait(false);
        while (!result.CloseStatus.HasValue)
        {
            await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None).ConfigureAwait(false);

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None).ConfigureAwait(false);

        }

        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None).ConfigureAwait(false);
    }
}
