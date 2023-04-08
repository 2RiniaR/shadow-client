using Fusion;

namespace RineaR.MadeHighlow.Network
{
    public class Client : NetworkBehaviour
    {
        [Networked] public int ClientNumber { get; set; }
    }
}