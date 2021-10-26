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
          "|  Coût : 1$      |"
          "|  Revenus : 5●   |"
          "|                 |"
          "|  Cette carte    |"
          "|   s'active à    |"
          "|  tous les tours |"
          "+-----------------+"


        /arte : 10 lignes --- 19 caractères par ligne --- 16 caractères après "|  "  */

        public Card ChooseCard(Card[] cards)
        {


            return;
        }

        public void DisplayCard(/*Card card*/)
        {
            Card card = new Card("Champs de blé", 2, "green", new int[4], 2);

            string sep = "+-----------------+";
            string line = "|                 |";
            ConsoleColor cardColor = ConsoleColor.Yellow;

            switch (card.GetCardColor)
            {
                case "red":
                    cardColor = ConsoleColor.Red; break;
                case "blue":
                    cardColor = ConsoleColor.Blue; break;
                case "green":
                    cardColor = ConsoleColor.Green; break;
            }

            WriteLineInColor(sep, cardColor);
            WriteInColor("|  ", cardColor);
            WriteInColor(card.GetCardName, cardColor);
            for (int i = 0; i < 15 - card.GetCardName.Length ; i++)            
                Console.Write(" ");
            WriteLineInColor("|", cardColor);
            WriteLineInColor(sep, cardColor);
            WriteLineInColor(line, cardColor);
            WriteInColor("|", cardColor);
            Console.Write(" Coût : " + card.GetCardCost + "$       ");
            WriteLineInColor("|", cardColor);
            WriteInColor("|", cardColor);
            Console.Write(" Revenus : " + card.GetCardGivedCoins + "o    ");
            WriteLineInColor("|", cardColor);
            WriteLineInColor(line, cardColor);
            WriteInColor("|", cardColor);
            Console.Write("  Cette carte    ");
            WriteLineInColor("|", cardColor);

            switch (card.GetCardColor)
            {
                case "red":
                    WriteInColor("|", cardColor);
                    Console.Write("  s'active au    ");
                    WriteLineInColor("|", cardColor);
                    WriteInColor("|", cardColor);
                    Console.Write("  tour adverse   ");
                    WriteLineInColor("|", cardColor);
                    break;
                case "blue":
                    WriteInColor("|", cardColor);
                    Console.Write("   s'active à    ");
                    WriteLineInColor("|", cardColor);
                    WriteInColor("|", cardColor);
                    Console.Write("  tous les tours ");
                    WriteLineInColor("|", cardColor);
                    break;
                case "green":
                    WriteInColor("|", cardColor);
                    Console.Write("  s'active au    ");
                    WriteLineInColor("|", cardColor);
                    WriteInColor("|", cardColor);
                    Console.Write("  tour du joueur ");
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
