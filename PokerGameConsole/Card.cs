using System.Collections;
using System.Collections.Generic;


namespace PokerGameConsole
{
    class Card 
    {
        public enum Suit//撲克牌花色
        {
            Hearts,
            Spades,
            Dimonds,
            Clubs
        }
        public enum Value//撲克牌數值
        {
            Two = 2, Three, Four, Five, Six, Seven,
            Eight, Nine, Ten, Jack, Queen, King, Ace
        }
     
        public Suit MySuit { get; set; }//此張卡的花色
        public Value MyValue { get; set; }//此張卡的數值

       
    }
}
