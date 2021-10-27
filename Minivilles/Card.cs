using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minivilles
{
    public class Card
    {
        private string CardName;
        private string CardColor;
        private int CardCost;
        private int GivedCoins;
        private int[] ActivationValue;
        private string NameAbreviated;


        public Card(string aName, int aCost, string aColor, int[] aActivationValue, int aGivedCoins, string aNameAbreviated)
        {
            CardName = aName;
            CardColor = aColor;
            CardCost = aCost;
            GivedCoins = aGivedCoins;
            ActivationValue = aActivationValue;
            NameAbreviated = aNameAbreviated;
        }

        public string GetCardAbreviation
        {
            get => NameAbreviated;
        }

        public string GetCardName
        {
            get => CardName;
        }

        public string GetCardColor
        {
            get => CardColor;
        }

        public int GetCardCost
        {
            get => CardCost;
        }

        public int GetCardGivedCoins
        {
            get => GivedCoins;
        }

        public int[] GetActivationValue
        {
            get => ActivationValue;
        }
    }
}