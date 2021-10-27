using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minivilles
{
    class Player
    {
        public List<Card> town = new List<Card>()
        {
            new Card("Boulangerie", 1, "vert", new int [1]{2}, 1),
            new Card("Champ de blé", 1, "vert", new int [1]{1}, 1)
        };
        public Die die;
        public int coins { get; private set; } = 3;
        private Game game;

        public void BuyCard(Card chosenCard)
        {
            town.Add(chosenCard);
            coins -= chosenCard.GetCardCost;
        }

        public void ApplyCardsEffect(int dieValue)
        {
            for(int i = 0; i < town.Count; i++)
            {
                switch (town[i].GetCardColor)
                {
                    case "rouge":
                        if(!game.playerTurn && dieValue == town[i].GetActivationValue[0])
                        {
                            coins += town[i].GetCardGivedCoins;
                        }
                        break;

                    case "bleue":
                        if(dieValue == town[i].GetActivationValue[0])
                        {
                            coins += town[i].GetCardGivedCoins;
                        }
                        break;

                    case "vert":
                        if(game.playerTurn && dieValue == town[i].GetActivationValue[0])
                        {
                            coins += town[i].GetCardGivedCoins;
                        }
                        break;

                }
            }
        }
    }
}
