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
        public Display display = new Display();
        List<Card> cardsTest = new List<Card> { new Card("Boulangerie", 4, "red", new int[1] { 1 }, 3), 
            new Card("Tour de pise", 2, "blue", new int[2] { 4, 5 }, 1),
            new Card("Champs de blé", 2, "green", new int[3] { 6, 7, 8 }, 4),
            new Card("Champs de blé", 2, "green", new int[3] { 6, 7, 8 }, 4),
            new Card("Champs de blé", 2, "green", new int[3] { 6, 7, 8 }, 4),
            new Card("Champs de blé", 2, "green", new int[3] { 6, 7, 8 }, 4),
            new Card("Champs de blé", 2, "green", new int[3] { 6, 7, 8 }, 4),
            new Card("Villa", 4, "blue", new int[2] { 3, 1 }, 5)
        };

        public void Play()
        {
            Console.WindowWidth = 170;
            Console.WindowHeight = 50;

            int result = display.ChooseCard(cardsTest);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
