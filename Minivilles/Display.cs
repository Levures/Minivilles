using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minivilles
{
    class Display
    {
        Game game;
        Die die = new Die();

        /*"+-----------------+"
          "|  Champs de blé  |"
          "+-----------------+"
          "|                 |"
          "|  Coût : 1$      |"
          "|  Revenus : 5●   |"
          "|                 |"
          "|  Cette carte    |"
          "|   s'active à    |"
          "|  tous les tours |"
          "|                 |"
          "+-----------------+"
        */

        //Méthode qui affiche les piles de cartes qui restent et laisse l'utilisateur choisir la carte qu'il veut acheter
        public int ChooseCard(List<Pile> pileDeck)
        {
            //Elements de construction pour les cartes
            string sep = "+-----------------+";
            string line = "|                 |";

            List<Card> cardsDeck = new();
            foreach (Pile pile in pileDeck)
                cardsDeck.Add(pile.StackCard);

            //La boucle for répète l'action autant de fois qu'il y a de lignes dans une carte
            for (int i = 1; i < 14; i++)
            {
                //La boucle foreach répète l'action autant de fois qu'il y a de cartes
                foreach(Card card in cardsDeck)
                {
                    //Couleur de la carte correspondante
                    ConsoleColor cardColor = GetCardColor(card);
                    //Switch prenant comme variable la ligne sur laquelle le programme est executé
                    switch (i)
                    {
                        case 1 or 3 or 13:
                            WriteInColor(sep, cardColor);
                            Console.Write(" ");
                            break;
                        case 2:
                            WriteInColor("|  ", cardColor);
                            WriteInColor(card.GetCardName, cardColor);
                            for (int j = 0; j < 15 - card.GetCardName.Length; j++)
                                Console.Write(" ");
                            WriteInColor("| ", cardColor);
                            break;
                        case 5 or 8 or 12:
                            WriteInColor(line, cardColor);
                            Console.Write(" ");
                            break;
                        case 4:
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
                        case 6:
                            WriteInColor("|", cardColor);
                            Console.Write(" Coût : ");
                            WriteInColor($"{card.GetCardCost} $      ", ConsoleColor.DarkYellow);
                            WriteInColor("| ", cardColor);
                            break;
                        case 7:
                            WriteInColor("|", cardColor);
                            Console.Write(" Revenus : ");
                            WriteInColor($"{card.GetCardGivedCoins} o   ", ConsoleColor.Yellow);
                            WriteInColor("| ", cardColor);
                            break;
                        case 9:
                            WriteInColor("|", cardColor);
                            WriteInColor("  Cette carte    ", ConsoleColor.DarkGray);
                            WriteInColor("| ", cardColor);
                            break;
                        case 10:
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
                        case 11:
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
                    case 1 or 13:
                        WriteInColor(sep, ConsoleColor.White);
                        break;
                    case 2 or 3 or 4 or 5 or 6 or 7 or 8 or 9 or 10 or 11 or 12:
                        WriteInColor(line, ConsoleColor.White);
                        break;
                }
                Console.WriteLine("");
            }
            //Variables de base pour le curseur
            int cursorPositionX = 8;
            int cursorPositionY = 13;
            string cursor = "^^^";

            //Set le curseur à la position par défaut (sous la première carte)
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.Write(cursor);

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
                        if (cursorPositionX < 8 + 20 * (cardsDeck.Count - 1))
                        {                 
                            //Offset du curseur
                            cursorPositionX += 20;
                            //Clear du curseur précédent
                            Console.SetCursorPosition(0, cursorPositionY);
                            Console.Write(new string(' ', Console.WindowWidth));
                            //Ecriture du nouveau curseur au bon endroit
                            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                            Console.Write(cursor);
                        }
                        //Retour à la première carte si le curseur est sur la dernière carte
                        else
                        {
                            cursorPositionX = 8;
                            Console.SetCursorPosition(0, cursorPositionY);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                            Console.Write(cursor);
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
                            Console.Write(cursor);
                        }
                        else
                        {
                            cursorPositionX = 8 + 20 * (cardsDeck.Count - 1);
                            Console.SetCursorPosition(0, cursorPositionY);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                            Console.Write(cursor);

                        }
                        break;
                    //Touche entrée tapée
                    case ConsoleKey.Enter:
                        //calcul de l'index à partir de la position du curseur
                        chosenCard = (cursorPositionX - 8) / 20;
                        hasChosen = true;
                        break;

                    default: break;
                }
            }
            //Fait tourner la boucle tant que rien a été choisi
            while (!hasChosen);
            //Retourne l'index de la carte dans liste fournie en argument
            return chosenCard;
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

<<<<<<< Updated upstream
        //public void DisplayTown(Player playerHand)
        //{
        //    string handSep = " +-----+ ";
        //    string handLine = " |     | ";
        //    foreach (Card hand in Player.GetPlayerTown())
        //    {
                
        //    }
        //}
=======
        public void DisplayTown(Player playerHand)
        {
            string handSep = " +-----+ ";
            string handLine = " |     | ";
        }
>>>>>>> Stashed changes

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
            string face1 = "+-------+\n" +
                            "|       |\n" +
                            "|   o   |\n" +
                            "|       |\n" +
                            "+-------+";
            string face2 = "+-------+\n" +
                            "|     o |\n" +
                            "|       |\n" +
                            "| o     |\n" +
                            "+-------+";
            string face3 = "+-------+\n" +
                            "|     o |\n" +
                            "|   o   |\n" +
                            "| o     |\n" +
                            "+-------+";
            string face4 = "+-------+\n" +
                            "| o   o |\n" +
                            "|       |\n" +
                            "| o   o |\n" +
                            "+-------+";
            string face5 = "+-------+\n" +
                            "| o   o |\n" +
                            "|   o   |\n" +
                            "| o   o |\n" +
                            "+-------+";
            string face6 = "+-------+\n" +
                            "| o   o |\n" +
                            "| o   o |\n" +
                            "| o   o |\n" +
                            "+-------+";

            switch (dieFace)
            {
                case 1:
                    Console.WriteLine(face1);
                    break;
                case 2:
                    Console.WriteLine(face2);
                    break;
                case 3:
                    Console.WriteLine(face3);
                    break;
                case 4:
                    Console.WriteLine(face4);
                    break;
                case 5:
                    Console.WriteLine(face5);
                    break;
                case 6:
                    Console.WriteLine(face6);
                    break;
            }
        }

    }
}
