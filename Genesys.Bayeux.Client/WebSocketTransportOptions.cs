﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Genesys.Bayeux.Client
{
    public class WebSocketTransportOptions
    {
        public Func<WebSocket> WebSocketFactory { get; set; }
        public Uri Uri { get; set; }

        /// <summary>
        /// Timeout for responses to be received. Must be greater than the expected Connect timeout. (Default is 65 seconds).
        /// </summary>
        public TimeSpan? ResponseTimeout { get; set; }

        internal WebSocketTransport Build(Func<IEnumerable<JObject>, CancellationToken, Task> eventPublisher)
        {
            return new WebSocketTransport(
                WebSocketFactory ?? (() => SystemClientWebSocket.CreateClientWebSocket()),
                Uri ?? throw new Exception("Please set Uri."),
                ResponseTimeout ?? TimeSpan.FromSeconds(65),
                eventPublisher);
        }
    }
}
