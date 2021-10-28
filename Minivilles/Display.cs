using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minivilles
{
    class Display
    {
        int[] cursorChooseCard = new int[2] { 0, 0 };
        int[] cursorThrowDice = new int[2] { 35, 20 };
        int[] cursorDisplayTown= new int[2] { 90, 33 };

        Game game;
        Die die = new Die();

        /*"+-----------------+"   "+-----+"
          "|  Champs de blé  |"   "| BLG |"
          "+-----------------+"   "|     |"
          "|                 |"   "| +5$ |"
          "|  Coût : 1$      |"   "+-----+"
          "|  Revenus : 5●   |"
          "|                 |"
          "|  Cette carte    |"
          "|   s'active à    |"
          "|  tous les tours |"
          "|                 |"
          "+-----------------+"
        */

        //Méthode qui affiche les piles de cartes qui restent et laisse l'utilisateur choisir la carte qu'il veut acheter
        public int ChooseCard(List<Pile> pileDeck, bool canChooseCard, Player[] players)
        {
            //Elements de construction pour les cartes
            string sep =  "+-----------------+";
            string line = "|                 |";

            List<Card> cardsDeck = new();
            foreach (Pile pile in pileDeck)
                cardsDeck.Add(pile.StackCard);

            Console.SetCursorPosition(cursorChooseCard[0], cursorChooseCard[1]);

            //La boucle for répète l'action autant de fois qu'il y a de lignes dans une carte
            for (int i = 1; i < 15; i++)
            {
                //La boucle foreach répète l'action autant de fois qu'il y a de cartes
                foreach(Pile pile in pileDeck)
                {
                    Card card = pile.GetCard();
                    //Couleur de la carte correspondante
                    ConsoleColor cardColor = GetCardColor(card);
                    //Switch prenant comme variable la ligne sur laquelle le programme est executé
                    switch (i)
                    {
                        case 1:
                            WriteInColor($"|       {pile.GetStack.Count} []      |", ConsoleColor.White);
                            Console.Write(" ");
                            break;
                        case 2 or 4 or 14:
                            WriteInColor(sep, cardColor);
                            Console.Write(" ");
                            break;
                        case 3:
                            WriteInColor("|  ", cardColor);
                            WriteInColor(card.GetCardName, cardColor);
                            for (int j = 0; j < 15 - card.GetCardName.Length; j++)
                                Console.Write(" ");
                            WriteInColor("| ", cardColor);
                            break;
                        case 6 or 9 or 13:
                            WriteInColor(line, cardColor);
                            Console.Write(" ");
                            break;
                        case 5:
                            WriteInColor("|    ", cardColor);
                            int[] activationValue = card.GetActivationValue;
                            int characters = 0;
                            foreach (int value in activationValue)
                            {
                                WriteInColor("|" + value + "|", ConsoleColor.White);
                                characters += 3;
                            }
                            for (int k = 0; k <  13 - characters; k++) { Console.Write(" "); }
                            WriteInColor("| ", cardColor);
                            break;
                        case 7:
                            WriteInColor("|", cardColor);
                            Console.Write(" Coût : ");
                            WriteInColor($"{card.GetCardCost} $      ", ConsoleColor.DarkYellow);
                            WriteInColor("| ", cardColor);
                            break;
                        case 8:
                            WriteInColor("|", cardColor);
                            Console.Write(" Revenus : ");
                            WriteInColor($"{card.GetCardGivedCoins} o   ", ConsoleColor.Yellow);
                            WriteInColor("| ", cardColor);
                            break;
                        case 10:
                            WriteInColor("|", cardColor);
                            WriteInColor("  Cette carte    ", ConsoleColor.DarkGray);
                            WriteInColor("| ", cardColor);
                            break;
                        case 11:
                            switch (card.GetCardColor)
                            {
                                case "red" or "green":
                                    WriteInColor("|", cardColor);
                                    WriteInColor("  s'active au    ", ConsoleColor.DarkGray);
                                    WriteInColor("| ", cardColor);
                                    break;
                                case "blue":
                                    WriteInColor("|", cardColor);
                                    WriteInColor("   s'active à    ", ConsoleColor.DarkGray);
                                    WriteInColor("| ", cardColor);
                                    break;
                            }
                            break;
                        case 12:
                            switch (card.GetCardColor)
                            {
                                case "red":
                                    WriteInColor("|", cardColor);
                                    WriteInColor("  tour adverse   ", ConsoleColor.DarkGray);
                                    WriteInColor("| ", cardColor);
                                    break;
                                case "blue":
                                    WriteInColor("|", cardColor);
                                    WriteInColor("  tous les tours ", ConsoleColor.DarkGray);
                                    WriteInColor("| ", cardColor);
                                    break;
                                case "green":
                                    WriteInColor("|", cardColor);
                                    WriteInColor("  tour du joueur ", ConsoleColor.DarkGray);
                                    WriteInColor("| ", cardColor);
                                    break;
                            }
                            break;
                    }                    
                }
                //Ne pas choisir de carte
                switch (i)
                {
                    case 2 or 14:
                        WriteInColor("+--------+", ConsoleColor.White);
                        break;
                    case 3 or 4or 5 or 6 or 10 or 11 or 12 or 13:
                        WriteInColor("|        |", ConsoleColor.White);
                        break;
                    case 7: WriteInColor("|NE PAS  |", ConsoleColor.White); break;
                    case 8: WriteInColor("|ACHETER |", ConsoleColor.White); break;
                    case 9: WriteInColor("|DE CARTE|", ConsoleColor.White); break;


                }
                Console.WriteLine("");
            }
            if (canChooseCard)
            {
                //Variables de base pour le curseur
                int cursorPositionX = 8;
                int cursorPositionY = 14;
                string cursor = "^^^";

                //Set le curseur à la position par défaut (sous la première carte)
                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                if (players[0].coins < pileDeck[(cursorPositionX - 8) / 20].GetCard().GetCardCost)
                    WriteInColor(cursor, ConsoleColor.Red);
                else
                    WriteInColor(cursor, ConsoleColor.Green);

                //Variables pour la gestion de la selection
                bool hasChosen = false;
                int selection = new();
                int chosenCard = new();

                Console.CursorVisible = false;
                //Boucle qui s'exécute pendant que l'utilisateur choisis sa carte
                do
                {
                    //Touches de clavier tapées
                    ConsoleKeyInfo keyPresed = Console.ReadKey();
                    //Switch qui gère le changement de position du curseur en fonction de l'input clavier
                    switch (keyPresed.Key)
                    {
                        //Quand la flèche de droite est tapée et que le curseur n'est pas sur la dernière carte
                        case ConsoleKey.RightArrow:
                            if (cursorPositionX < 8 + 20 * (cardsDeck.Count - 1) + 20)
                            {
                                //Offset du curseur
                                cursorPositionX += 20;
                                //Clear du curseur précédent
                                Console.SetCursorPosition(0, cursorPositionY);
                                Console.Write(new string(' ', Console.WindowWidth));
                                //Ecriture du nouveau curseur au bon endroit
                                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                                if (players[0].coins < pileDeck[(cursorPositionX - 8) / 20].GetCard().GetCardCost)
                                    WriteInColor(cursor, ConsoleColor.Red);
                                else
                                    WriteInColor(cursor, ConsoleColor.Green);
                            }
                            //Retour à la première carte si le curseur est sur la dernière carte
                            else
                            {
                                cursorPositionX = 8;
                                Console.SetCursorPosition(0, cursorPositionY);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                                if (players[0].coins < pileDeck[(cursorPositionX - 8) / 20].GetCard().GetCardCost)
                                    WriteInColor(cursor, ConsoleColor.Red);
                                else
                                    WriteInColor(cursor, ConsoleColor.Green);
                            }
                            break;

                        //Pareil pour la gauche
                        case ConsoleKey.LeftArrow:
                            if (cursorPositionX > 8)
                            {
                                cursorPositionX -= 20;
                                Console.SetCursorPosition(0, cursorPositionY);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                                chosenCard = (cursorPositionX - 8) / 20;
                                if (players[0].coins < pileDeck[(cursorPositionX - 8) / 20].GetCard().GetCardCost)
                                    WriteInColor(cursor, ConsoleColor.Red);
                                else
                                    WriteInColor(cursor, ConsoleColor.Green);
                            }
                            else
                            {
                                cursorPositionX = 8 + 20 * (cardsDeck.Count - 1) + 20;
                                Console.SetCursorPosition(0, cursorPositionY);
                                Console.Write(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                                if (players[0].coins < pileDeck[(cursorPositionX - 8) / 20].GetCard().GetCardCost)
                                    WriteInColor(cursor, ConsoleColor.Red);
                                else
                                    WriteInColor(cursor, ConsoleColor.Green);
                            }
                            break;
                        //Touche entrée tapée
                        case ConsoleKey.Enter:
                            //calcul de l'index à partir de la position du curseur
                            chosenCard = (cursorPositionX - 8) / 20;
                            if (players[0].coins < pileDeck[chosenCard].GetCard().GetCardCost)
                                break;

                            hasChosen = true;
                            break;

                        default: break;
                    }
                }
                //Fait tourner la boucle tant que rien a été choisi
                while (!hasChosen);
                if (chosenCard <= cardsDeck.Count - 1)
                {
                    //Retourne l'index de la carte dans liste fournie en argument
                    return chosenCard;
                }
                else return 100; // Retourne 100 si le joueur ne veut pas choisir de cartes
            }
            else Console.Write(new string(' ', Console.WindowWidth));
            return 200;
        }

        //Methode non fonctionelle (en cours)
        public int ChooseBox(string[] msg, bool sep, ConsoleColor boxColor = ConsoleColor.Gray, ConsoleColor textColor = ConsoleColor.Gray)
        {
            int[] msgLength = new int[msg.Length];
            for (int i = 0; i < msg.Length; i++)            
                msgLength[i] = msg[i].Length;

            int maxMsgLength = msgLength.Max();
            string cursor = "<--";
            ConsoleColor cursorColor = ConsoleColor.Green;

            for (int i = 0; i < msg.Length; i++)
            {
                WriteInColor("+", boxColor);
                WriteInColor(new string('-', maxMsgLength + 3), boxColor);

                WriteLineInColor("+", boxColor);

                WriteInColor("| ", boxColor);
                WriteInColor(msg[i], textColor);
                Console.Write(new string(' ', maxMsgLength - msg[i].Length + 2));
                WriteLineInColor("|", boxColor);
            }
            Console.SetCursorPosition(maxMsgLength + 6, 1);
            WriteInColor(cursor, cursorColor);

            int selection = new();
            bool hasChosen = false;
            int cursorX = maxMsgLength + 6;

            //Boucle qui s'exécute pendant que l'utilisateur choisis sa carte
            do
            {
                //Touches de clavier tapées
                ConsoleKeyInfo keyPresed = Console.ReadKey();
                //Switch qui gère le changement de position du curseur en fonction de l'input clavier
                switch (keyPresed.Key)
                {
                    //Quand la flèche du bas est tapée et que le curseur n'est pas sur la dernière carte
                    case ConsoleKey.RightArrow:

                        break;

                    //Pareil pour le haut
                    case ConsoleKey.LeftArrow:
                        
                        break;

                    //Touche entrée tapée
                    case ConsoleKey.Enter:
                        //calcul de l'index à partir de la position du curseur

                        hasChosen = true;
                        break;

                    default: break;
                }
            }
            //Fait tourner la boucle tant que rien a été choisi
            while (!hasChosen);
            //Retourne l'index de la carte dans liste fournie en argument
            return selection;
        }

        private ConsoleColor GetCardColor(Card card)
        {
            ConsoleColor cardColor = new();
            switch (card.GetCardColor)
            {
                case "red":
                    cardColor = ConsoleColor.Red; break;
                case "blue":
                    cardColor = ConsoleColor.Blue; break;
                case "green":
                    cardColor = ConsoleColor.Green; break;
            }
            return cardColor;
        }

        public void DisplayCard(Card card)
        {

            string sep = "+-----------------+";
            string line = "|                 |";
            ConsoleColor cardColor = GetCardColor(card);

            WriteLineInColor(sep, cardColor);
            WriteInColor("|  ", cardColor);
            WriteInColor(card.GetCardName, cardColor);
            for (int i = 0; i < 15 - card.GetCardName.Length ; i++)            
                Console.Write(" ");
            WriteLineInColor("|", cardColor);
            WriteLineInColor(sep, cardColor);
            WriteLineInColor(line, cardColor);
            WriteInColor("|", cardColor);
            Console.Write(" Coût : ");
            WriteInColor($"{card.GetCardCost} $      ", ConsoleColor.DarkYellow);
            WriteLineInColor("|", cardColor);
            WriteInColor("|", cardColor);
            Console.Write(" Revenus : ");
            WriteInColor($"{card.GetCardGivedCoins} o   ", ConsoleColor.Yellow);
            WriteLineInColor("|", cardColor);

            WriteLineInColor(line, cardColor);
            WriteInColor("|", cardColor);
            WriteInColor("  Cette carte    ", ConsoleColor.DarkGray);
            WriteLineInColor("|", cardColor);

            switch (card.GetCardColor)
            {
                case "red":
                    WriteInColor("|", cardColor);
                    WriteInColor("  s'active au    ", ConsoleColor.DarkGray);
                    WriteLineInColor("|", cardColor);
                    WriteInColor("|", cardColor);
                    WriteInColor("  tour adverse   ", ConsoleColor.DarkGray);
                    WriteLineInColor("|", cardColor);
                    break;
                case "blue":
                    WriteInColor("|", cardColor);
                    WriteInColor("   s'active à    ", ConsoleColor.DarkGray);
                    WriteLineInColor("|", cardColor);
                    WriteInColor("|", cardColor);
                    WriteInColor("  tous les tours ", ConsoleColor.DarkGray);
                    WriteLineInColor("|", cardColor);
                    break;
                case "green":
                    WriteInColor("|", cardColor);
                    WriteInColor("  s'active au    ", ConsoleColor.DarkGray);
                    WriteLineInColor("|", cardColor);
                    WriteInColor("|", cardColor);
                    WriteInColor("  tour du joueur ", ConsoleColor.DarkGray);
                    WriteLineInColor("|", cardColor);
                    break;
            }
            WriteLineInColor(line, cardColor);
            WriteLineInColor(sep, cardColor);
        }


        public void DisplayTown(Player[] players, int dieResult)
        {
            string sep =  "+-----+";
            string line = "|     |";
            string playersSep = "  ||  ";
            Player player = players[0];
            Player IA = players[1];

            cursorDisplayTown[0] -= (player.GetPlayerTown.Count * 8 + IA.GetPlayerTown.Count * 8 + 6) / 2;

            for (int i = 0; i < 6; i++)
            {
                //Traque la carte qui évolue dans la boucle
                int cardLoopCount = 0;

                //Gestion du curseur
                Console.SetCursorPosition(cursorDisplayTown[0], cursorDisplayTown[1]);

                foreach (Card card in player.GetPlayerTown)
                {                    
                    //Couleur de la carte correspondante
                    ConsoleColor cardColor = GetCardColor(card);
                    switch (i)
                    {
                        case 0:
                            if (cardLoopCount == 0)
                            {
                                WriteInColor("Player", ConsoleColor.White);
                                WriteInColor($" {player.coins}o", ConsoleColor.Yellow);
                                string spacesToWrite = new string(' ', player.GetPlayerTown.Count * 8 - 9);
                                Console.Write(spacesToWrite);
                            }
                            break;
                        case 1 or 5:
                            WriteInColor(sep, cardColor);
                            Console.Write(" ");
                            break;
                        case 2:
                            WriteInColor("|", cardColor);
                            WriteInColor($" {card.GetCardAbreviation} ", ConsoleColor.White);
                            WriteInColor("| ", cardColor);
                            break;
                        case 3:
                            WriteInColor(line, cardColor);
                            Console.Write(" ");
                            break;
                        case 4:
                            bool canBeActivated = false;
                            foreach (int activationValue in card.GetActivationValue)
                            {
                                if (activationValue == dieResult)
                                    canBeActivated = true;
                            }
                            WriteInColor("|", cardColor);
                            if (card.GetCardColor == "blue" && canBeActivated)
                                WriteInColor($" +{card.GetCardGivedCoins}$ ", ConsoleColor.Yellow);
                            else if (card.GetCardColor == "green" && player.GetIsMyTurn && canBeActivated)
                                WriteInColor($" +{card.GetCardGivedCoins}$ ", ConsoleColor.Yellow);
                            else if (card.GetCardColor == "red" && !player.GetIsMyTurn && canBeActivated)
                                WriteInColor($" +{card.GetCardGivedCoins}$ ", ConsoleColor.Yellow);
                            else Console.Write("     ");
                            WriteInColor("| ", cardColor);
                            break;
                    }
                    cardLoopCount++;
                }
                WriteInColor(playersSep, ConsoleColor.Gray);
                cardLoopCount = 0;
                foreach (Card card in IA.GetPlayerTown)
                {
                    //Couleur de la carte correspondante
                    ConsoleColor cardColor = GetCardColor(card);
                    switch (i)
                    {
                        case 0:
                            if (cardLoopCount == 0)
                            {
                                WriteInColor("IA", ConsoleColor.White);
                                WriteInColor($" {IA.coins}o", ConsoleColor.Yellow);
                            }
                            break;
                        case 1 or 5:
                            WriteInColor(sep, cardColor);
                            Console.Write(" ");
                            break;
                        case 2:
                            WriteInColor("|", cardColor);
                            WriteInColor($" {card.GetCardAbreviation} ", ConsoleColor.White);
                            WriteInColor("| ", cardColor);
                            break;
                        case 3:
                            WriteInColor(line, cardColor);
                            Console.Write(" ");
                            break;
                        case 4:
                            bool canBeActivated = false;
                            foreach (int activationValue in card.GetActivationValue)
                            {
                                if (activationValue == dieResult)
                                    canBeActivated = true;
                            }
                            WriteInColor("|", cardColor);
                            if (card.GetCardColor == "blue" && canBeActivated)
                                WriteInColor($" +{card.GetCardGivedCoins}$ ", ConsoleColor.Yellow);
                            else if (card.GetCardColor == "green" && IA.GetIsMyTurn && canBeActivated)
                                WriteInColor($" +{card.GetCardGivedCoins}$ ", ConsoleColor.Yellow);
                            else if (card.GetCardColor == "red" && !IA.GetIsMyTurn && canBeActivated)
                                WriteInColor($" +{card.GetCardGivedCoins}$ ", ConsoleColor.Yellow);
                            else Console.Write("     ");
                            WriteInColor("| ", cardColor);
                            break;
                    }
                    cardLoopCount++;
                }
                cursorDisplayTown[1] += 1;
            }
        }


        private void WriteInColor(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private void WriteLineInColor(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void DisplayDie(int dieFace)
        {
            int[] cursorThrowDice = new int[2] { 35, 20 };

            switch (dieFace)
            {
                case 1:
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1]);
                    Console.Write("+-------+");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 1);
                    Console.Write("|       |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 2);
                    Console.Write("|   o   |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 3);
                    Console.Write("|       |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 4);
                    Console.Write("+-------+");
                    break;
                case 2:
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1]);
                    Console.Write("+-------+");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 1);
                    Console.Write("|     o |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 2);
                    Console.Write("|       |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 3);
                    Console.Write("| o     |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 4);
                    Console.Write("+-------+");
                    break;
                case 3:
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1]);
                    Console.Write("+-------+");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 1);
                    Console.Write("|     o |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 2);
                    Console.Write("|   o   |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 3);
                    Console.Write("| o     |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 4);
                    Console.Write("+-------+");
                    break;
                case 4:
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1]);
                    Console.Write("+-------+");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 1);
                    Console.Write("| o   o |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 2);
                    Console.Write("|       |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 3);
                    Console.Write("| o   o |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 4);
                    Console.Write("+-------+");
                    break;
                case 5:
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1]);
                    Console.Write("+-------+");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 1);
                    Console.Write("| o   o |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 2);
                    Console.Write("|   o   |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 3);
                    Console.Write("| o   o |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 4);
                    Console.Write("+-------+");
                    break;
                case 6:
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1]);
                    Console.Write("+-------+");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 1);
                    Console.Write("| o   o |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 2);
                    Console.Write("| o   o |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 3);
                    Console.Write("| o   o |");
                    Console.SetCursorPosition(cursorThrowDice[0], cursorThrowDice[1] + 4);
                    Console.Write("+-------+");
                    break;
            }
        }

        public void DisplayText(string textLine1, string textLine2, string textLine3)
        {
            Console.SetCursorPosition(90, 19);
            Console.Write("+---------------------------------------------------------------------+");

            Console.SetCursorPosition(90, 20);
            Console.Write($"|");
            Console.SetCursorPosition(160, 20);
            Console.Write($"|");

            Console.SetCursorPosition(90, 21);
            Console.Write($"|  {textLine1}");
            Console.SetCursorPosition(160, 21);
            Console.Write($"|");

            Console.SetCursorPosition(90, 22);
            Console.Write($"|  {textLine2}");
            Console.SetCursorPosition(160, 22);
            Console.Write($"|");

            Console.SetCursorPosition(90, 23);
            Console.Write($"|  {textLine3}");
            Console.SetCursorPosition(160, 23);
            Console.Write($"|");

            Console.SetCursorPosition(90, 24);
            Console.Write($"|");
            Console.SetCursorPosition(160, 24);
            Console.Write($"|");

            Console.SetCursorPosition(90, 25);
            Console.Write("+---------------------------------------------------------------------+");
            Console.WriteLine();
        }
    }
}
