using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerGameConsole
{
    class DeckOfCards : Card
    {
        const int Num_Of_Cards = 52;//總共52張牌
        private Card[] deck;//牌堆的牌

        public DeckOfCards()
        {
            deck = new Card[Num_Of_Cards];//新增52個空牌位
        }

        public Card[] getDeck { get { return deck; } }//將從29行new出來的Card，存放至getDeck這個Card陣列
        /// <summary>
        /// 生成52張牌:4種花色各有13張牌
        /// </summary>
        public void setUpDeck()
        {
            int i = 0;
            foreach (Suit s in Enum.GetValues(typeof(Suit)))//在Suit裡的第s個物件
            {
                foreach (Value v in Enum.GetValues(typeof(Value)))//在Value裡第v個物件
                {
                    deck[i] = new Card { MySuit = s, MyValue = v};//設定deck的第i個裡的Card裡面的MySuit及MyValue分別等於當前第s個及第v個物件
                    i++;
                }

            }
            ShuffleCards();
        }
        /// <summary>
        /// 洗牌
        /// </summary>
        public void ShuffleCards()
        {
            Random rand = new Random();
            Card temp;

            //洗牌1000次
            for (int shuffleTimes = 0; shuffleTimes < 1000; shuffleTimes++)
            {
                for (int i = 0; i < Num_Of_Cards; i++)
                {
                    //交換牌
                    int secondCardIndex = rand.Next(13);
                    temp = deck[i];
                    deck[i] = deck[secondCardIndex];
                    deck[secondCardIndex] = temp;
                }
            }
        }
    }
}
