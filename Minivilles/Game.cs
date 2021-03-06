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
        bool boudage = false;
        private string[] nameOrdi = new string[3] { "Titouan", "Bernard Tapie", "Ordinateur" };
        private string nameChosen = new string("prout");


        private List<Pile> gamePiles = new List<Pile> { new Pile(new Card("Ferme", 1, "blue", new int[1] { 6 }, 1, "FME")),
                                                        new Pile(new Card("Boulangerie", 1, "green", new int[1] { 8 }, 2, "BLG")),
                                                        new Pile(new Card("Champs de blé", 2, "blue", new int[1] { 7 }, 1, "CDB")),
                                                        new Pile(new Card("Café", 2, "red", new int[1] { 9 }, 1, "CAF")),
                                                        new Pile(new Card("Superette", 2, "green", new int[3] { 2, 3, 8 }, 2, "SUP")),
                                                        new Pile(new Card("Forêt", 3, "blue", new int[3] { 10, 11, 12 }, 2, "FOR")),
                                                        new Pile(new Card("Magasin de jouet", 3, "green", new int[5] { 2, 3, 4, 5, 6 }, 1, "MDJ")),
                                                        new Pile(new Card("Restaurant", 4, "red", new int[1] { 5 }, 2, "RES")),
                                                        new Pile(new Card("Stade", 6, "blue", new int[2] { 9, 10 }, 4, "STD")),
                                                        new Pile(new Card("Banque", 7, "red", new int[1] { 6 }, 4, "BNQ")),
                                                        new Pile(new Card("Fête foraine", 8, "red", new int[4] { 1, 9, 10, 11 }, 2, "FFN")) };



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
            //Début 
            display.DisplayText("Bienvenue dans Minivilles !");
            Thread.Sleep(2000);
            display.DisplayText("", "Entrez r pour connaître les règles.", "(ou appuyer sur Entrée pour continuer)");
            Console.SetCursorPosition(130, 22);

            if (Console.ReadLine() == "r")
            {
                display.DisplayText("", "", "", true);
                display.DisplayText("", "Votre objectif sera d'obtenir 20 pièces.");
                Thread.Sleep(4000);
                display.DisplayText("", "", "", true);
                display.DisplayText("Votre objectif sera d'obtenir 20 pièces.", "Pour cela, vous allez investir dans l'immobilier.");
                Thread.Sleep(4000);
                display.DisplayText("", "", "", true);
                display.DisplayText("", "Les bâtiments constitueront votre ville.");
                Thread.Sleep(4000);
                display.DisplayText("", "", "", true);
                display.DisplayText("Selon le résultat du dé,", "ils s'activeront et rapporteront de la moula.");
                Thread.Sleep(5000);
            }

            display.DisplayText("", "", "", true);
            display.DisplayText("La partie est gagnée par celui qui obtient 20 pièces (o) !", "Bonne chance.");
            Thread.Sleep(4000);
            display.DisplayText("", "", "", true);
            Console.Clear();
            string[] question = new string[1] { "Contre qui voulez-vous jouer ?" };
            int index = display.ChooseBox(question.Concat(nameOrdi).Distinct().ToArray());
            nameChosen = nameOrdi[index];
            Console.Clear();


            display.DisplayText("", "", "", true);
            display.DisplayText("", "Voici votre plateau.");
            Thread.Sleep(3000);


            display.DisplayTown(players, 100, nameChosen);
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
                        Console.SetCursorPosition(130, 22);
                        numberOfDice = Int32.Parse(Console.ReadLine());
                        if (numberOfDice == 1 || numberOfDice == 2)
                        {
                            break;
                        }
                        else
                        {
                            display.DisplayText("", "", "Un ou deux dés, pas plus pas moins vil gredin.");
                        }
                    }
                    catch
                    {
                        display.DisplayText("", "", "Un ou deux dés, pas plus pas moins vil gredin.");
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

                // Le joueur lance le dé.
                int dieFace = 0;
                int dicesFacesTotal = 0;
                int diceSeparator = 35;

                foreach (Die entry in dices)
                {
                    dieFace = players[0].die.Roll();
                    display.DieShake(diceSeparator);
                    display.DisplayDie(dieFace, diceSeparator);
                    diceSeparator -= 10;
                    dicesFacesTotal += dieFace;
                }                                

                players[0].ApplyCardsEffect(dicesFacesTotal, players[1]);
                players[1].ApplyCardsEffect(dicesFacesTotal, players[0]);

                Thread.Sleep(500);
                display.DisplayTown(players, dicesFacesTotal, nameChosen);
                Thread.Sleep(1000);


                // Fin de partie ?
                if (players[0].coins >= 20 || players[1].coins >= 20)
                {
                    endGame = true;
                }

                if (boudage)
                {
                    display.DisplayText($"Titouan: Pfff trop de la chance, toi tu fais {dicesFacesTotal}");
                    Thread.Sleep(3000);
                    display.DisplayText("", "", "", true);
                }

                if (nameChosen == nameOrdi[1])
                {
                    display.DisplayText("Bernard Tapie: Bravo gamin, allez achète.");
                    Thread.Sleep(3000);
                    display.DisplayText("", "", "", true);
                }


                if (!endGame)
                {
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
                    
                    display.DisplayTown(players, dicesFacesTotal, nameChosen);
                    display.ChooseCard(gamePiles, false, players);

                    display.DisplayTown(players, 0, nameChosen);
                    Thread.Sleep(500);
                    display.DisplayText("", "", "", true);
                    if (cardChosen <= gamePiles.Count - 1)
                    {
                        display.DisplayText("Félicitations, " + gamePiles[cardChosen].GetCard().GetCardName + " s'ajoute à votre ville.");
                    }

                    else
                        display.DisplayText("Pas de carte pour vous.");
                    Thread.Sleep(2000);

                    players[0].isMyTurn = false;
                    Thread.Sleep(1000);

                    // Début du tour de l'ordinateur.
                    display.DisplayText("", "", "", true);
                    display.DisplayText("C'est au tour de " + nameChosen);

                    if (nameChosen == "Titouan")
                    {
                        IAChildhish(dieFace);
                    }
                    else if (nameChosen == "Ordinateur")
                    {
                        IATurn(dieFace);
                    }
                    else if (nameChosen == "Bernard Tapie")
                    {
                        IATapie(dieFace);
                    }

                    players[1].isMyTurn = true;
                    System.Threading.Thread.Sleep(1000);

                    if (endGame)
                    {
                        if (players[0].coins < players[1].coins)
                        {
                            display.DisplayText("", "", "", true);
                            display.DisplayText("", $"{nameChosen} possède {players[1].coins} pièces...", "");
                            Thread.Sleep(2000);
                            display.DisplayText("", "", "", true);
                            display.DisplayText($"{nameChosen} possède {players[1].coins} pièces...", "Vous avez perdu.", "");
                            Thread.Sleep(3000);
                            display.DisplayText("", "", "", true);

                            if (nameChosen == nameOrdi[0])
                            {
                                display.DisplayText("Vous avez perdu.", $"{nameChosen}: Haha tu sais pas jouer c'est moi chuis trop fort !", "");
                            }
                            else if (nameChosen == nameOrdi[1])
                            {
                                display.DisplayText("Vous avez perdu.", $"{nameChosen}: C'est pas grave petit, à jamais le premier, c'est comme ça.", "");
                            }
                            else if (nameChosen == nameOrdi[2])
                            {
                                display.DisplayText("Vous avez perdu.", $"{nameChosen}: Je... c'est embarrassant pour l'entièreté de la race humaine...", "");
                            }
                        }
                        else
                        {
                            display.DisplayText("", "", "", true);
                            display.DisplayText("", $"Vous possédez {players[0].coins} pièces...", "");
                            Thread.Sleep(3000);
                            display.DisplayText("", "", "", true);
                            display.DisplayText($"Vous possédez {players[0].coins} pièces...", "Vous avez gagné !", "");
                            Thread.Sleep(3000);
                            display.DisplayText("", "", "", true);

                            if (nameChosen == nameOrdi[0])
                            {
                                display.DisplayText("Vous avez gagné !", $"*{nameChosen} vous traite de boomer chauve et s'en va en pleurant*", "");
                                Thread.Sleep(3000);
                                display.DisplayText("", "", "", true);
                            }
                            else if (nameChosen == nameOrdi[1])
                            {
                                display.DisplayText("Vous avez gagné !", $"{nameChosen}: Gamin, tu sais", "Quand on fait, on fait des choses biens,");
                                Thread.Sleep(3000);
                                display.DisplayText("", "", "", true);
                                display.DisplayText("Bernard Tapie: Quand on fait, on fait des choses biens,", "on fait des choses moins biens.", "C'est le lot de tous les gens qui font des choses.");
                                Thread.Sleep(3000);
                                display.DisplayText("", "", "", true);
                                display.DisplayText("Bernard Tapie 1943-2021, RIP in peace petit anje parti tro to.");
                                Thread.Sleep(3000);
                                display.DisplayText("", "", "", true);
                            }
                            else if (nameChosen == nameOrdi[2])
                            {
                                display.DisplayText("Vous avez gagné !", $"{nameChosen}: Vous avez gagné. En même temps, je ne suis qu'un modeste processeur.", "");
                                Thread.Sleep(3000);
                                display.DisplayText("", "", "", true);
                            }

                            display.DisplayText("", "", "");
                        }
                    }
                }
            }
        }

        private void IATurn(int dieFace)
        {
            List<Card> haveEnoughCoinToBuy = new List<Card>();
            bool canBuyCard = false;

            int diceSeparator = 35;
            int dicesFacesTotal = 0;
            dices = new Die[random.Next(1, 3)];

            foreach (Die entry in dices)
            {
                dieFace = players[1].die.Roll();
                display.DieShake(diceSeparator);
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
                    if (canBuyCard)
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
                        display.DisplayText($"Ordi : Je choisis {iaChosenCard.GetCardName}.");
                        foreach (Pile pile in gamePiles.ToList())
                        {
                            if (pile.GetStack.Count == 0)
                            {
                                gamePiles.Remove(pile);
                                display.ChooseCard(gamePiles, false, players, true);
                                display.ChooseCard(gamePiles, false, players);
                                display.DisplayTown(players, 100, " ", true);
                                display.DisplayTown(players, dicesFacesTotal, nameChosen);
                            }
                        }
                    }
                    else
                    {
                        display.DisplayText("Ordi : Pas de cartes pour moi.");
                    }
                    break;
                case 3 or 4:
                    display.DisplayText("Ordi : Pas de cartes pour moi.");
                    break;
            }
            display.DisplayTown(players, 100, " ", true);
            display.DisplayTown(players, dicesFacesTotal, nameChosen);
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

        private void IAChildhish(int dieFace)
        {
            List<Card> haveEnoughCoinToBuy = new List<Card>();
            bool canBuyCard = false;
            bool premierBoudage = true;
            int diceSeparator = 35;
            int dicesFacesTotal = 0;
            dices = new Die[random.Next(1, 3)];

            if (players[0].coins >= 15 && players[0].coins > players[1].coins)
            {
                boudage = true;

                foreach (Die entry in dices)
                {
                    dieFace = players[1].die.Roll();
                    display.DieShake(diceSeparator);
                    display.DisplayDie(dieFace, diceSeparator);
                    diceSeparator -= 10;
                    dicesFacesTotal += dieFace;
                    display.DisplayText($"Pfff, voilà moi je fais juste {dieFace}... ");
                    Thread.Sleep(2000);
                }


                display.DisplayText("Titouan : Non mais là aussi tu triches chuis sûr.");
                Thread.Sleep(2000);
                display.DisplayText("", "", "", true);
                display.DisplayText("Titouan : Regarde t'as plein de pièces et moi j'ai rien là.");
                Thread.Sleep(1500);
                display.DisplayText("", "", "", true);

                players[0].ApplyCardsEffect(dicesFacesTotal, players[1]);
                players[1].ApplyCardsEffect(dicesFacesTotal, players[0]);

                if (premierBoudage)
                {
                    display.DisplayText("Titouan : M'en fous je joue plus voilà.");
                    Thread.Sleep(3000);
                    display.DisplayText("", "", "", true);
                    premierBoudage = false;
                }
                players[1].isMyTurn = false;
            }

            if (!boudage)
            {

                foreach (Die entry in dices)
                {
                    dieFace = players[1].die.Roll();
                    display.DieShake(diceSeparator);
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
                        if (canBuyCard)
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
                            display.DisplayText($"Titouan : Je vais prendre {iaChosenCard.GetCardName}.");
                            foreach (Pile pile in gamePiles.ToList())
                            {
                                if (pile.GetStack.Count == 0)
                                {
                                    gamePiles.Remove(pile);
                                    display.ChooseCard(gamePiles, false, players, true);
                                    display.ChooseCard(gamePiles, false, players);
                                    display.DisplayTown(players, 100, " ", true);
                                    display.DisplayTown(players, dicesFacesTotal, nameChosen);
                                }
                            }
                        }
                        else
                        {
                            display.DisplayText("Titouan : Pas envie, elles puent.");
                            display.DisplayTown(players, 100, " ", true);
                        }
                        break;
                    case 3 or 4:
                        display.DisplayText("Titouan : Nan. J'achète rien, t'as des jeux sur ton téléphone ?");
                        display.DisplayTown(players, 100, " ", true);
                        break;
                }
            }

            display.DisplayTown(players, 100, "", true);
            display.DisplayTown(players, dicesFacesTotal, nameChosen);
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

        private void IATapie(int dieFace)
        {
            List<Card> haveEnoughCoinToBuy = new List<Card>();
            bool canBuyCard = false;

            int diceSeparator = 35;
            int dicesFacesTotal = 0;
            dices = new Die[random.Next(1, 3)];

            foreach (Die entry in dices)
            {
                dieFace = players[1].die.Roll();
                display.DieShake(diceSeparator);
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
                if (pile.GetCard().GetCardCost <= players[1].coins || pile.GetCard().GetCardCost >= players[1].coins)
                {
                    haveEnoughCoinToBuy.Add(pile.GetCard());
                    canBuyCard = true;
                }
            }

            display.DisplayText("", "", "", true);

            if (canBuyCard)
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
                display.DisplayText($"Bernard Tapie : Je vais faire l'acquisition de {iaChosenCard.GetCardName}.");
                display.DisplayText("", "", "", true);
                
                foreach (Pile pile in gamePiles.ToList())
                {
                    if (pile.GetStack.Count == 0)
                    {
                        gamePiles.Remove(pile);
                        display.ChooseCard(gamePiles, false, players, true);
                        display.ChooseCard(gamePiles, false, players);
                        display.DisplayTown(players, 100, " ", true);
                        display.DisplayTown(players, dicesFacesTotal, nameChosen);
                        
                        
                    }
                }
            }

            display.DisplayTown(players, 100, " ", true);
            display.DisplayTown(players, dicesFacesTotal, nameChosen);
            display.DisplayText("", "", "Appuyez sur Entrée pour continuer");
            Console.ReadLine();

            diceSeparator = 35;
            foreach (Die entry in dices)
            {
                display.DisplayDie(100, diceSeparator);
                diceSeparator -= 10;
            }
            if (players[1].coins <= -1)
            {
                display.DisplayText("", "", "", true);
                display.DisplayText("", "Bernard Tapie : Non mais t'inquiète,");
                Thread.Sleep(2000);
                display.DisplayText("", "", "", true);
                display.DisplayText("Bernard Tapie : Non mais t'inquiète,", "je vais me refaire gamin.");
                Thread.Sleep(3000);
                display.DisplayText("", "", "", true);
            }
            if (players[1].coins <= -10)
            {
                display.DisplayText("", "", "", true);
                display.DisplayText("Bernard Tapie : Gamin, tu veux pas me racheter Adidas ?");
                Thread.Sleep(3000);
                display.DisplayText("", "", "", true);
            }

            players[1].isMyTurn = false;
        }
    }
}
