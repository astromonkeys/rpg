using System;
using System.Collections.Generic;
using System.Text;

namespace rpg
{
    class GameState
    {
        public string name { get; set; }
        public int id { get; set; }

        public GameState(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }
}
