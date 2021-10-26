using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minivilles
{
    public class Card
    {
        private string cardName;
        private string cardColor;
        private int cardCost;
        private int givedCoins;
        private int[] activationValue;


        public Card(string aName, int aCost, string aColor, int[] aActivationValue, int aGivedCoins)
        {
            cardName = aName;
            cardColor = aColor;
            cardCost = aCost;
            givedCoins = aGivedCoins;
            activationValue = aActivationValue;
        }

        public string GetCardName
        {
            get => cardName;
        }

        public string GetCardColor
        {
            get => cardColor;
        }

        public int GetCardCost
        {
            get => cardCost;
        }

        public int GetCardGivedCoins
        {
            get => givedCoins;
        }

        public int[] GetActivationValue
        {
            get => activationValue;
        }
    }
}