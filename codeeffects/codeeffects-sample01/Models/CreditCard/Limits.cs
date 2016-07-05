using System;
using System.Collections.Generic;

namespace codeeffects_sample01.Models.CreditCard
{
    public class Limits
    {
        public Dictionary<CardType, Tuple<int, int>> CardLimits =
    new Dictionary<CardType, Tuple<int, int>>();

        public Limits()
        {
            CardLimits.Add(CardType.Visa, new Tuple<int, int>(30, 2000));
            CardLimits.Add(CardType.MasterCard, new Tuple<int, int>(100, 5000));
            CardLimits.Add(CardType.Discovery, new Tuple<int, int>(200, 500));
        }
    }
}