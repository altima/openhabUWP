using System;
using openhabUWP.Models;

namespace openhabUWP.Interfaces.Services
{
    public interface IPushClientService
    {
        bool PushChannelAttached { get; }

        void AttachToEvents(string baseUrl, string[] topics = null, Action<string> onDataReceived = null, Action<string> onEventReceived = null);
        void PoolForEvent(string url, Action<string> onDataReceived = null);
        void PoolForEvent(Server currentServer, Action<string> onDataReceived);
    }
}