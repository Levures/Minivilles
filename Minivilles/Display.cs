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
        Card[] cards = { new Card("Champs de blé", 2, "red", new int[1] { 2 }, 2), new Card("Café", 2, "bleu", new int[1] { 3 }, 5) };

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
        public int ChooseCard(List<Card> cardsDeck)
        {
            //Elements de construction pour les cartes
            string sep = "+-----------------+";
            string line = "|                 |";

            //La boucle for répète l'action autant de fois qu'il y a de lignes dans une carte
            for (int i = 1; i < 14; i++)
            {
                // La boucle foreach répète l'action autant de fois qu'il y a de cartes
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

        public void DisplayDie()
        {

        }


        public void DisplayPiles()
        {

        }

    }
}
