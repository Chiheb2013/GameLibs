using System;

namespace Mappy.Worlds
{
    public class TeleportationEventArgs : EventArgs
    {
        string nextWorld;

        public string NextWorld { get { return nextWorld; } }

        public TeleportationEventArgs(string nextWorld)
        {
            this.nextWorld = nextWorld;
        }
    }
}
