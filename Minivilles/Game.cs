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
        public bool endGame = false;
        public Display display = new Display();
        private Random random = new Random();
        public bool canChooseCard;

        private List<Pile> gamePiles = new List<Pile> { new Pile(new Card("Champs de blé", 2, "blue", new int[1] { 1 }, 1, "CDB")),
                                                        new Pile(new Card("Ferme", 1, "blue", new int[1] { 1 }, 1, "FME")),
                                                        new Pile(new Card("Boulangerie", 1, "green", new int[1] { 2 }, 1, "BLG")),
                                                        new Pile(new Card("Café", 1, "red", new int[1] { 3 }, 4, "CAF")),
                                                        new Pile(new Card("Superette", 2, "green", new int[1] { 4 }, 3, "SUP")),
                                                        new Pile(new Card("Forêt", 1, "blue", new int[1] { 5 }, 2, "FOR")),
                                                        new Pile(new Card("Restaurant", 4, "red", new int[1] { 5 }, 2, "RES")),
                                                        new Pile(new Card("Stade", 6, "blue", new int[1] { 6 }, 4, "STD"))};


        public void game()
        {
            //Init
            Console.SetWindowSize(180, 40);            

            foreach (Pile pile in gamePiles)
            {
                pile.InitializeStack();
            }


            //Début
            Console.WriteLine("Bienvenue dans Minivilles !");
            Console.Clear();

            players[0] = new Player();
            players[1] = new Player();

            players[0].town.Add(gamePiles[0].WithdrawCard());
            players[1].town.Add(gamePiles[0].WithdrawCard());
            players[0].town.Add(gamePiles[2].WithdrawCard());
            players[1].town.Add(gamePiles[2].WithdrawCard());


            display.DisplayDie(5);
            display.DisplayTown(players, 1);
            canChooseCard = true;
            players[0].town.Add(gamePiles[display.ChooseCard(gamePiles, canChooseCard, players)].WithdrawCard());
            canChooseCard = false;
            display.ChooseCard(gamePiles, canChooseCard, players);
            Console.ReadLine();


            while (!endGame)
            {

                // Début du tour du joueur.
                players[0].isMyTurn = true;
                Console.WriteLine("Tour du joueur:");
                Console.WriteLine("Appuyez sur Entrée pour lancer le dé.");
                Console.ReadLine();

                // Le joueur lance le dé.
                int dieFace = players[0].die.Roll();
                display.DisplayDie(dieFace);
                players[0].ApplyCardsEffect(dieFace);
                players[1].ApplyCardsEffect(dieFace);
                Console.WriteLine("Le joueur a {0} pièce(s).", players[0].coins.ToString());

                // Fin de partie ?
                if (players[0].coins >= 20 || players[1].coins >= 20)
                {
                    endGame = true;
                }

                //Le joueur achète ou non une propriété.
                Console.WriteLine("Souhaitez-vous acheter une carte ?");

                //Display truc qui propose oui ou non.
                string playerChoice = Console.ReadLine();

                if(playerChoice == "o")
                {
                    Console.WriteLine("Je choisis une carte.");
                    //display.ChooseCard();
                }

                //- Si oui, curseur sous les piles de carte.
                //players[0].BuyCard();

                //- Si non, la suite.

                players[0].isMyTurn = false;
                // Début du tour de l'ordinateur.
                Console.WriteLine("Tour de l'ordinateur:");
                players[1].isMyTurn = true;
                System.Threading.Thread.Sleep(1000);
                dieFace = players[1].die.Roll();
                display.DisplayDie(dieFace);
                players[0].ApplyCardsEffect(dieFace);
                players[1].ApplyCardsEffect(dieFace);
                Console.WriteLine("L'ordinateur a {0} pièce(s).", players[1].coins.ToString());
                IATurn();
            }

            if (players[0].coins < players[1].coins)
            {
                Console.WriteLine("Bahaha t'as perdu !");
            }
            else
            {
                Console.WriteLine("Bravo champion !");
            }
        }

        #region Private Methods
        private void IATurn()
        {
            int iaChoice;
            iaChoice = random.Next(2);
            List<Card> possibility = new List<Card>();

            List<Card> haveEnoughCoinToBuy = new List<Card>();
            bool canBuyCard = false;

            foreach (Card entry in possibility)
            {
                if (entry.GetCardCost >= players[1].coins)
                {
                    haveEnoughCoinToBuy.Add(entry);
                    canBuyCard = true;
                }
            }



            switch (iaChoice)
            {
                case 0:
                    if(canBuyCard)
                    {
                        Card iaChosenCard = haveEnoughCoinToBuy[random.Next(haveEnoughCoinToBuy.Count)];
                        players[1].BuyCard(iaChosenCard);
                        Console.WriteLine("L'IA a choisi : {0}", iaChosenCard.GetCardName);
                    }
                    else
                    {
                        Console.WriteLine("Ordi : Pas de carte pour moi");
                    }
                    break;
                case 1:
                    Console.WriteLine("Ordi : Pas de carte pour moi");
                    break;
            }
            // Fin de partie ?
            if (players[0].coins >= 20 || players[1].coins >= 20)
            {
                endGame = true;
            }
            players[1].isMyTurn = false;
        }

        #endregion

    }
}
