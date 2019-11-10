using System.Collections.Generic;

namespace BoardGameDatabase.Models
{
    public class CreateMatch
    {
        public int GameType { get; set; }

        public List<int> UserIdList { get; set; }

        //public string GameData { get; set; }
    }
}
