using BoardGamesGrpc.GameOnlines;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesServer.Models
{
    internal class PlayMatchResponseStream
    {
        public IServerStreamWriter<GamePlay> ResponseStream { get; set; }
        public string GuidID { get; set; }
    }
}
