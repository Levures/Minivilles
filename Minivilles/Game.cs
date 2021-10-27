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
        public bool endGame = false;
        public Display display;

        public Game game()
        {
            Console.WriteLine("Bienvenue dans Minivilles !");

            while (!endGame)
            {
                // Début du tour du joueur
                playerTurn = true;
                Console.WriteLine("Tour du joueur :");
                display.DisplayCard();
                Console.WriteLine("Appuyez sur Entrée pour lancer le dé.");
                Console.ReadLine();

                //Le joueur lance le dé
                players[0].die.Roll();
                if (playerTurn)
                {
                    
                    
                }
                else
                {

                }

                //Le joueur achète ou non une propriété
            }
            

            return game();
        }

    }
}
