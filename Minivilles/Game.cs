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
        private Random random = new Random();
        private Pile pile = new Pile(new Card("boulangerie", 1, "red", new int[1] { 2 }, 4, "blg"));

        public Game game()
        {
            //Début
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
                int dieFace = players[0].die.Roll();
                display.DisplayDie(dieFace);
                players[0].ApplyCardsEffect(dieFace);

                //Le joueur achète ou non une propriété.
                Console.WriteLine("Souhaitez-vous acheter une carte ?");

                //Display truc qui propose oui ou non.
                string playerChoice = Console.ReadLine();

                if(playerChoice == "o")
                {
                    Console.WriteLine("Je choisis une carte");
                    //display.ChooseCard();
                }

                        //- Si oui, curseur sous les piles de carte.
                    //players[0].BuyCard();

                        //- Si non, la suite.


                // Début du tour de l'ordinateur.
                Console.WriteLine("Tour de l'ordinateur:");
                System.Threading.Thread.Sleep(1000);
                dieFace = players[1].die.Roll();
                display.DisplayDie(dieFace);

                int iaChoice;
                iaChoice = random.Next(2);
                List<Card> possibility = new List<Card>();

                List<Card> haveEnoughCoinToBuy = new List<Card>();

                foreach(Card entry in possibility)
                {
                    if(entry.GetCardCost >= players[1].coins)
                    {
                        haveEnoughCoinToBuy.Add(entry);
                    }
                }

                

                switch (iaChoice)
                {
                    case 0:
                        Card iaChosenCard = haveEnoughCoinToBuy[random.Next(haveEnoughCoinToBuy.Count + 1)];
                        players[1].BuyCard(iaChosenCard);
                        Console.WriteLine("L'IA a choisi : {0}", iaChosenCard.GetCardName);
                        break;
                    case 1:
                        Console.WriteLine("Ordi : Pas de carte pour moi");
                        break;
                }                
                
            }


            return game();
        }

    }
}
