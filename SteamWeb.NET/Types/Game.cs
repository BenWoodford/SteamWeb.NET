using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamWeb.SteamWeb.Types
{
    public class Game
    {
        public string Name { get; set; }
        public bool Installed { get; set; }
        public bool UpdateRunning { get; set; }
        public bool UpdatePaused { get; set; }
        public long BytesDownloaded { get; set; }
        public long BytesNeeded { get; set; }
        public long BytesPerSecond { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public string Logo { get; set; }
        public long CurrentDiskBytes { get; set; }
        public long EstimatedDiskBytes { get; set; }
        public PlayTime MinutesPlayed { get; set; }
        public DateTime LastPlayedAt { get; set; }

    }
}
