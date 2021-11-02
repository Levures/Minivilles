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

        public void ApplyCardsEffect(int dieValue, Player otherPlayer)
        {
            for(int i = 0; i < town.Count; i++)
            {
                foreach(int activationValue in town[i].GetActivationValue)
                {
                    case "red":
                        if(!isMyTurn && town[i].GetActivationValue.Contains(dieValue))
                        {
                            coins += town[i].GetCardGivedCoins;
                        }
                        break;
                    case "blue":
                        if(town[i].GetActivationValue.Contains(dieValue))
                        {
                            coins += town[i].GetCardGivedCoins;
                        }
                        break;

                    case "green":
                        if(isMyTurn && town[i].GetActivationValue.Contains(dieValue))
                        {
                            coins += town[i].GetCardGivedCoins;
                        }
                        break;
                }
            }
            for (int i = 0; i < otherPlayer.GetPlayerTown.Count; i++)
            {
                if (otherPlayer.GetPlayerTown[i].GetCardColor == "red" && isMyTurn && otherPlayer.GetPlayerTown[i].GetActivationValue.Contains(dieValue))
                {
                    coins -= otherPlayer.GetPlayerTown[i].GetCardGivedCoins;
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
