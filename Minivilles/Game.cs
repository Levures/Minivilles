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
                                                        new Pile(new Card("Boulangerie", 1, "green", new int[1] { 2 }, 2, "BLG")),
                                                        new Pile(new Card("Café", 2, "red", new int[1] { 3 }, 1, "CAF")),
                                                        new Pile(new Card("Superette", 2, "green", new int[1] { 4 }, 3, "SUP")),
                                                        new Pile(new Card("Forêt", 1, "blue", new int[1] { 5 }, 2, "FOR")),
                                                        new Pile(new Card("Restaurant", 4, "red", new int[1] { 5 }, 2, "RES")),
                                                        new Pile(new Card("Stade", 6, "blue", new int[1] { 6 }, 4, "STD")),
                                                        new Pile(new Card("Banque", 7, "red", new int[1] { 6 }, 4, "BNQ")),
                                                        new Pile(new Card("Magasin de jouet", 8, "green", new int[3] { 2, 3, 4 }, 1, "MDJ")),
                                                        new Pile(new Card("Fête foraine", 8, "red", new int[2] { 1, 2 }, 2, "FFN")) };
                                                        


public void game()
        {
            //Init
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetBufferSize(350, 63);
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
            display.DisplayText("", "Voici votre plateau.");
            Thread.Sleep(2000);
            display.DisplayTown(players, 100);
            canChooseCard = false;
            display.ChooseCard(gamePiles, canChooseCard, players);            

            Console.SetCursorPosition(0, 26);
            Console.Write(new string(' ', Console.BufferWidth));

            while (!endGame)
            {

                // Début du tour du joueur.
                display.DisplayText("", "", "", true);
                players[0].isMyTurn = true;
                Thread.Sleep(1000);
                display.DisplayText("Tour du joueur");
                Thread.Sleep(1000);
                int numberOfDice;

                while (true)
                {
                    display.DisplayText("", "Voulez-vous lancer un ou deux dés ?");
                    try
                    {
                        numberOfDice = Int32.Parse(Console.ReadLine());
                        if (numberOfDice == 1 || numberOfDice == 2)
                        {
                            break;
                        }
                        else
                        {
                            display.DisplayText("", "", "Un ou deux dés, pas plus pas moins vil gredin");
                        }
                    }
                    catch
                    {
                        display.DisplayText("", "", "Un ou deux dés, pas plus pas moins vil gredin");
                    }
                }
                display.DisplayText("", "", "", true);
                display.DisplayText("Tour du joueur");
                numberOfDice = Math.Clamp(numberOfDice, 1, 2);
                dices = new Die[numberOfDice];
                for (int i = 0; i < numberOfDice; i++)
                {
                    dices.SetValue(new Die(), i);
                }

                display.DisplayText("", "", "Appuyez sur Entrée pour lancer les dés.");
                Console.ReadLine();

                // Le joueur lance le dé.
                int dieFace = 0;
                int dicesFacesTotal = 0;
                int diceSeparator = 35;

                foreach (Die entry in dices)
                {
                    dieFace = players[0].die.Roll();
                    display.DisplayDie(dieFace, diceSeparator);
                    diceSeparator -= 10;
                    dicesFacesTotal += dieFace;
                }

                Console.WriteLine("{0}", dicesFacesTotal);
                players[0].ApplyCardsEffect(dicesFacesTotal, players[1]);
                players[1].ApplyCardsEffect(dicesFacesTotal, players[0]);

                Thread.Sleep(500);
                display.DisplayTown(players, dicesFacesTotal);
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

                diceSeparator = 35;
                foreach (Die entry in dices)
                {
                    display.DisplayDie(100, diceSeparator);
                    diceSeparator -= 10;
                }


                display.DisplayTown(players, dicesFacesTotal);
                display.ChooseCard(gamePiles, false, players);

                Thread.Sleep(500);
                display.DisplayText("", "", "", true);
                if (cardChosen <= gamePiles.Count - 1)
                    display.DisplayText("Félicitations, " + gamePiles[cardChosen].GetCard().GetCardName + " s'ajoute à votre ville.");
                else
                    display.DisplayText("Pas de carte pour vous.");
                Thread.Sleep(2000);

                players[0].isMyTurn = false;
                Thread.Sleep(1000);

                // Début du tour de l'ordinateur.
                display.DisplayText("", "", "", true);
                display.DisplayText("C'est au tour de l'ordinateur.");

                players[1].isMyTurn = true;
                System.Threading.Thread.Sleep(1000);
                
                IATurn(dieFace);
            }

            if (players[0].coins < players[1].coins)
            {
                display.DisplayText("                                ", "Bahaha t'as perdu !", "                                ");
            }
            else
            {
                display.DisplayText("                                ", "Bravo champion !", "                                ");
            }
        }

        #region Private Methods
        private void IATurn(int dieFace)
        {
            List<Card> haveEnoughCoinToBuy = new List<Card>();
            bool canBuyCard = false;

            int diceSeparator = 35;
            int dicesFacesTotal = 0;

            foreach (Die entry in dices)
            {
                dieFace = players[1].die.Roll();
                display.DisplayDie(dieFace, diceSeparator);
                diceSeparator -= 10;
                dicesFacesTotal += dieFace;
            }

            players[0].ApplyCardsEffect(dicesFacesTotal, players[1]);
            players[1].ApplyCardsEffect(dicesFacesTotal, players[0]);

            // Fin de partie ?
            if (players[0].coins >= 20 || players[1].coins >= 20)
            {
                endGame = true;
            }

            foreach (Pile pile in gamePiles)
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
                                display.DisplayTown(players, 100, true);
                                display.DisplayTown(players, dicesFacesTotal);
                            }
                        }
                    }
                    else
                    {                        
                        display.DisplayText("Ordi : Pas de cartes pour moi.");
                    }
                    break;
                case 3:
                    display.DisplayText("Ordi : Pas de cartes pour moi.");
                    break;
            }
            display.DisplayTown(players, 100, true);
            display.DisplayTown(players, dicesFacesTotal);
            display.DisplayText("", "", "Appuyez sur Entrée pour continuer");
            Console.ReadLine();

            diceSeparator = 35;
            foreach (Die entry in dices)
            {
                display.DisplayDie(100, diceSeparator);
                diceSeparator -= 10;
            }
            
            players[1].isMyTurn = false;
        }

        #endregion

    }
}
