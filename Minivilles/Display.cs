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
        Card[] cards = { new Card("Champs de blé", 2, "red", new int[4], 2), new Card("Café", 2, "bleu", new int[3], 5) };

        string carteExemple1 = "+-----------------+" +
                               "|  Champs de blé  |" +
                               "+-----------------+" +
                               "|  Coût : 1$      |" +
                               "|  Revenus : 5●   |" +
                               "|                 |" +
                               "|  Cette carte    |" +
                               "|   s'active à    |" +
                               "|  tous les tours |" +
                               "+-----------------+" ;

        string carteExemple2 = "+-----------------+" +
                               "|  Champs de blé  |" +
                               "+-----------------+" +
                               "|  Coût : 1$      |" +
                               "|  Revenus : 5●   |" +
                               "|                 |" +
                               "|  Cette carte    |" +
                               "|  s'active au    |" +
                               "|  tour du joueur |" +
                               "+-----------------+";

        string carteExemple3 = "+-----------------+" +
                               "|  Champs de blé  |" +
                               "+-----------------+" +
                               "|  Coût : 1$      |" +
                               "|  Revenus : 5●   |" +
                               "|                 |" +
                               "|  Cette carte    |" +
                               "|  s'active au    |" +
                               "|   tour adverse  |" +
                               "+-----------------+";

        //carte : 10 lignes --- 17 caractères entre | et |

        public void DrawCard()
        {
            string sep = "+-----------------+";



            Console.WriteLine(sep);


        }


        public void DisplayDie()
        {

        }


        public void DisplayCards()
        {

        }

        public void DisplayPiles()
        {

        }

    }
}
