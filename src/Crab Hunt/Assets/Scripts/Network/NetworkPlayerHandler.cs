using Services;
using UnityEngine;

namespace Network
{
    public class NetworkPlayerHandler : BaseGameHandler<NetworkPlayerHandler>
    {
        public string PlayerName { get; set; }
        public bool IsObserver { get; set; }
    }
}