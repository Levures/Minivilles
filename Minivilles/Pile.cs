namespace Minivilles
{
    public class Pile
    {
        public Card card { get; }
        public int nbCards { get; }

        public int WithdrawCard()
        {
            int withdrawal = nbCards - 1;
            return withdrawal;
        }
    }
}