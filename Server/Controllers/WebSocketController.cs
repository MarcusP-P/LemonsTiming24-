using LemonsTiming24.SharedCode;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.WebSockets;

namespace LemonsTiming24.Server.Controllers;
[Route("/api/v1/[controller]")]
[ApiController]
public class WebSocketController : ControllerBase
{
    private const string websocketEndpoint = "/ws";
    private readonly ILogger<WebSocketController> logger;

    public WebSocketController(ILogger<WebSocketController> logger)
    {
        this.logger = logger;
    }

    [HttpGet]
    public WebSocketUrl Get()
    {
        var request = this.HttpContext.Request;
        return new WebSocketUrl { Url = $"ws{(request.IsHttps ? "s" : "")}://{request.Host}{websocketEndpoint}" };
    }

    [HttpGet(websocketEndpoint)]
    public async Task GetWebsocket()
    {
        if (this.HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await
                               this.HttpContext.WebSockets.AcceptWebSocketAsync();
            await Echo(webSocket);
        }
        else
        {
            this.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }

    private static async Task Echo(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        while (!result.CloseStatus.HasValue)
        {
            await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

            result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        }

        await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
    }
}
