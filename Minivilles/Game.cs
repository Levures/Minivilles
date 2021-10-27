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
        public Player[] players = new Player[2];
        public bool playerTurn { get; private set; }
        public bool endGame = false;
        public Display display = new Display();
        Die die = new Die();

        public Game game()
        {
            Console.WriteLine("Bienvenue dans Minivilles !");

            players[0] = new Player();
            players[1] = new Player();

            while (!endGame)
            {
                // Début du tour du joueur.
                playerTurn = true;
                Console.WriteLine("Tour du joueur:");
                Console.WriteLine("Appuyez sur Entrée pour lancer le dé.");
                Console.ReadLine();

                // Le joueur lance le dé.
                int dieFace = die.Roll();
                display.DisplayDie(dieFace);
                players[0].ApplyCardsEffect(dieFace);

                //Le joueur achète ou non une propriété.
                Console.WriteLine("Souhaitez-vous acheter une carte ?");
                        //Display truc qui propose oui ou non.

                        //- Si oui, curseur sous les piles de carte.
                    //players[0].BuyCard();

                        //- Si non, la suite.


                // Début du tour de l'ordinateur.
                Console.WriteLine("Tour de l'ordinateur:");
                System.Threading.Thread.Sleep(1000);
                dieFace = players[1].die.Roll();
                display.DisplayDie(dieFace);
            }


            return game();
        }

    }
}
