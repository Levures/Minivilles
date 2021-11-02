using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
        public Die[] dices;

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
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.CursorVisible = false;
            foreach (Pile pile in gamePiles)
            {
                pile.InitializeStack();
            }

            players[0] = new Player();
            players[1] = new Player();

            players[0].town.Add(gamePiles[0].WithdrawCard());
            players[1].town.Add(gamePiles[0].WithdrawCard());
            players[0].town.Add(gamePiles[2].WithdrawCard());
            players[1].town.Add(gamePiles[2].WithdrawCard());

            //Début
            display.DisplayText("Bienvenue dans Minivilles !");
            Thread.Sleep(2000);
            display.DisplayText("Bienvenue dans Minivilles !", "Voici votre plateau");
            Thread.Sleep(2000);
            display.DisplayTown(players, 6);
            canChooseCard = false;
            display.ChooseCard(gamePiles, canChooseCard, players);

            Console.WriteLine("Avec combien de dés voulez-vous jouer ?");
            int numberOfDice = Int32.Parse(Console.ReadLine());
            dices = new Die[numberOfDice];
            for(int i = 0; i < numberOfDice; i++)
            {
                dices.SetValue(new Die(), i);
            }

            while (!endGame)
            {

                // Début du tour du joueur.
                display.DisplayText("", "", "", true);
                players[0].isMyTurn = true;
                Thread.Sleep(1000);
                display.DisplayText("Tour du joueur");
                Thread.Sleep(1000);
                display.DisplayText("Tour du joueur", "Appuyez sur Entrée pour lancer le dé.");
                Console.ReadLine();

                // Le joueur lance le dé.
                int dieFace = 0;
                int diceSeparator = 35;

                foreach (Die entry in dices)
                {
                    dieFace = players[0].die.Roll();
                    display.DisplayDie(dieFace, diceSeparator);
                    diceSeparator -= 10;
                }

                dieFace = players[0].die.Roll();
                display.DisplayDie(dieFace, 35);
                players[0].ApplyCardsEffect(dieFace, players[1]);
                players[1].ApplyCardsEffect(dieFace, players[0]);
                Thread.Sleep(500);
                display.DisplayTown(players, dieFace);
                Thread.Sleep(1000);
                

                // Fin de partie ?
                if (players[0].coins >= 20 || players[1].coins >= 20)
                {
                    endGame = true;
                }

                //Le joueur achète ou non une propriété.
                display.DisplayText("", "", "", true);
                display.DisplayText("Choisissez une carte, ou passez votre tour.");
                int cardChosen = display.ChooseCard(gamePiles, true, players);
                if (cardChosen != 100)
                    players[0].BuyCard(gamePiles[cardChosen].WithdrawCard());
                foreach (Pile pile in gamePiles.ToList())
                {
                    if (pile.GetStack.Count == 0)
                    {
                        gamePiles.Remove(pile);
                        display.ChooseCard(gamePiles, false, players, true);
                    }
                }
                display.DisplayDie(100, 35);


                display.DisplayTown(players, dieFace);
                display.ChooseCard(gamePiles, false, players);

                Thread.Sleep(500);
                display.DisplayText("", "", "", true);
                if (cardChosen <= gamePiles.Count - 1)
                    display.DisplayText("Félicitations, " + gamePiles[cardChosen].GetCard().GetCardName + " s'ajoute à votre ville");
                else
                    display.DisplayText("Pas de cartes pour vous");
                Thread.Sleep(2000);

                players[0].isMyTurn = false;
                Thread.Sleep(1000);
                // Début du tour de l'ordinateur.
                display.DisplayText("", "", "", true);
                display.DisplayText("C'est au tour de l'ordinateur.");

                players[1].isMyTurn = true;
                System.Threading.Thread.Sleep(1000);
                dieFace = players[1].die.Roll();
                display.DisplayDie(dieFace, 35);
                players[0].ApplyCardsEffect(dieFace, players[1]);
                players[1].ApplyCardsEffect(dieFace, players[0]);
                Thread.Sleep(1000);
                IATurn(dieFace);
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
        private void IATurn(int dieFace)
        {
            int iaChoice;
            iaChoice = random.Next(2);

            List<Card> haveEnoughCoinToBuy = new List<Card>();
            bool canBuyCard = false;

            foreach(Pile pile in gamePiles)
            {

                if (pile.GetCard().GetCardCost <= players[1].coins)
                {
                    haveEnoughCoinToBuy.Add(pile.GetCard());
                    canBuyCard = true;
                }

            }



            display.DisplayText("", "", "", true);

            switch (random.Next(0, 5))
            {
                case 0 or 1 or 2:
                    if(canBuyCard)
                    {
                        Card iaChosenCard = haveEnoughCoinToBuy[random.Next(haveEnoughCoinToBuy.Count)];
                        int indexCounter = -1;
                        int cardIndex = 0;
                        foreach (Pile pile in gamePiles)
                        {
                            indexCounter++;
                            if (pile.GetCard() == iaChosenCard && haveEnoughCoinToBuy.Contains(gamePiles[indexCounter].GetCard()))
                            {
                                cardIndex = indexCounter;
                            }
                        }

                        players[1].BuyCard(gamePiles[cardIndex].WithdrawCard());
                        display.DisplayText("Ordi : Je choisis " + iaChosenCard.GetCardName);
                        foreach (Pile pile in gamePiles.ToList())
                        {
                            if (pile.GetStack.Count == 0)
                            {
                                gamePiles.Remove(pile);
                                display.ChooseCard(gamePiles, false, players, true);
                                display.ChooseCard(gamePiles, false, players);
                                display.DisplayTown(players, 10, true);
                                display.DisplayTown(players, dieFace);

                            }
                        }
                    }
                    else
                    {                        
                        display.DisplayText("Ordi : Pas de cartes pour moi");
                    }
                    break;
                case 3:
                    display.DisplayText("Ordi : Pas de cartes pour moi");
                    break;
            }
            display.DisplayTown(players, 10, true);
            display.DisplayTown(players, dieFace);
            Thread.Sleep(1500);
            display.DisplayDie(100, 35);
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
