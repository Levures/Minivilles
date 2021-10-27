using System;
using System.Collections.Generic;

namespace Minivilles
{
    public class Pile
    {
        public Stack<Card> Stack = new Stack<Card>();
        public Card StackCard { get; }

        public Pile(Card aCard)
        {
            StackCard = aCard;
        }

        public Stack<Card> GetStack
        {
            get => Stack;
        }

        public Card GetCard()
        {
            return StackCard;
        }

        public void InitializeStack()
        {
            for (int i = 0; i < 6; i++)
            {
                Stack.Push(StackCard);
            }
        }

        public Card WithdrawCard()
        {
            if (Stack.TryPop(out Card card))
            {
                return card;
            }
            else return null;
            
        }
    }
}