using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamWeb.SteamWeb.Types
{
    public class GameResult
    {
        public bool success { get; set; }
        public Dictionary<string, Game> data { get; set; }
    }
}
