using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minivilles
{
    class Game
    {
        public Pile[] piles;
        public Player[] players;
        public bool playerTurn { get; private set; }
        public Display display;
    }
}
