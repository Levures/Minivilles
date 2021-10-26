namespace Minivilles
{
    public class Pile
    {
        public Card card { get; }
        private int nbCards = 6;

        public string CardName()
        {
            string name = card.GetCardName;
            return name;
        }

        public bool WithdrawCard()
        {
            int withdrawal = nbCards - 1;
            return nbCards>0;
        }
    }
}