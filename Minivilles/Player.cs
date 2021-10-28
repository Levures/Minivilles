using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minivilles
{
    class Player
    {

        public List<Card> town = new List<Card>();
        public Die die = new Die();
        public int coins { get; private set; } = 3;
        private Game game = new Game();
        public bool isMyTurn = false;

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
                    case "red":
                        if(!isMyTurn && dieValue == town[i].GetActivationValue[0])
                        {
                            coins += town[i].GetCardGivedCoins;
                        }
                        break;

                    case "blue":
                        if(dieValue == town[i].GetActivationValue[0])
                        {
                            coins += town[i].GetCardGivedCoins;
                        }
                        break;

                    case "green":
                        if(isMyTurn && dieValue == town[i].GetActivationValue[0])
                        {
                            coins += town[i].GetCardGivedCoins;
                        }
                        break;

                }
            }
        }

        public List<Card> GetPlayerTown
        {
            get => town;
        }
        
        public bool GetIsMyTurn
        {
            get => isMyTurn;
        }
        
    }
}
