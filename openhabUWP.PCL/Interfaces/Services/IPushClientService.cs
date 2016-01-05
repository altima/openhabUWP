using System;

namespace openhabUWP.Interfaces.Services
{
    public interface IPushClientService
    {
        bool PushChannelAttached { get; }

        void AttachToEvents(string baseUrl, string[] topyics = null, Action<string> onDataReceived = null, Action<string> onEventReceived = null);
    }
}