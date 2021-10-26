using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minivilles
{
    class Player
    {
        public List<Card> town;
        public Die die;
        public int coins;

        public Card BuyCard(Card chosenCard)
        {
            town.Add(chosenCard);
            coins -= chosenCard.cost;
        }
    }
}
