using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameConsole
{
    public enum Hand
    {
        Nothing,
        OnePair,
        TwoPairs,
        ThreeKind,
        Straight,
        Flush,
        FullHouse,
        FourKind,
        StraightFlush
    }
    public struct HandValue
    {
        public int Total { get; set; }
        public int HighCard { get; set; }
    }
    class HandEvaluator : Card
    {
        private int heartsSum;
        private int dimondSum;
        private int clubSum;
        private int spadeSum;
        private Card[] cards;
        private HandValue handValue;

        public HandEvaluator(Card[] sortedHand)
        {
            heartsSum = 0;
            dimondSum = 0;
            clubSum = 0;
            spadeSum = 0;
            cards = new Card[5];
            Cards = sortedHand;
            handValue = new HandValue();
        }

        public HandValue HandValue
        {
            get { return handValue; }
            set { handValue = value; }
        }

        public Card[] Cards
        {
            get { return cards; }
            set
            {
                cards[0] = value[0];
                cards[1] = value[1];
                cards[2] = value[2];
                cards[3] = value[3];
                cards[4] = value[4];
            }
        }
        public Hand EvaluateHand()
        {
            getNumberOfSuit();
            if (StraightFlush())
                return Hand.StraightFlush;
            else if (FourOfKind())
                return Hand.FourKind;
            else if (FullHouse())
                return Hand.FullHouse;
            else if (Flush())
                return Hand.Flush;
            else if (Straight())
                return Hand.Straight;
            else if (ThreeOfKind())
                return Hand.ThreeKind;
            else if (TwoPairs())
                return Hand.TwoPairs;
            else if (OnePair())
                return Hand.OnePair;

            handValue.HighCard = (int)cards[4].MyValue;
            return Hand.Nothing;
        }
        private void getNumberOfSuit()
        {
            foreach (var element in Cards)
            {
                switch (element.MySuit)
                {
                    case Card.Suit.Hearts:
                        heartsSum++;
                        break;
                    case Card.Suit.Dimonds:
                        dimondSum++;
                        break;
                    case Card.Suit.Clubs:
                        clubSum++;
                        break;
                    case Card.Suit.Spades:
                        spadeSum++;
                        break;

                }
            }
        }
        /// <summary>
        /// 同花順
        /// </summary>
        /// <returns></returns>
        private bool StraightFlush()
        {
            if (Straight() && Flush())
            {
                handValue.Total = (int)cards[0].MyValue + (int)cards[1].MyValue + (int)cards[2].MyValue +
                          (int)cards[3].MyValue + (int)cards[4].MyValue;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 鐵支
        /// </summary>
        /// <returns></returns>
        private bool FourOfKind()
        {
            //判斷4張是否一樣
            if (cards[0].MyValue == cards[1].MyValue && cards[0].MyValue == cards[2].MyValue && cards[0].MyValue == cards[3].MyValue)
            {
                handValue.Total = (int)cards[1].MyValue * 4;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[1].MyValue == cards[2].MyValue && cards[1].MyValue == cards[3].MyValue && cards[1].MyValue == cards[4].MyValue)
            {
                handValue.Total = (int)cards[1].MyValue * 4;
                handValue.HighCard = (int)cards[0].MyValue;
                return true;
            }

            return false;
        }
        /// <summary>
        /// 葫蘆
        /// </summary>
        /// <returns></returns>
        private bool FullHouse()
        {
            //前3張一樣數值一樣加上後2張數值一樣
            //前2張一樣數值一樣加上後3張數值一樣
            if (cards[0].MyValue == cards[1].MyValue && cards[0].MyValue == cards[2].MyValue && cards[3].MyValue == cards[4].MyValue ||
                cards[0].MyValue == cards[1].MyValue && cards[2].MyValue == cards[3].MyValue && cards[2].MyValue == cards[4].MyValue)
            {
                handValue.Total = (int)(cards[0].MyValue) + (int)(cards[1].MyValue) + (int)(cards[2].MyValue) +
                    (int)(cards[3].MyValue) + (int)(cards[4].MyValue);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 同花
        /// </summary>
        /// <returns></returns>
        private bool Flush()
        {
            //如果5張牌花色一樣
            if (heartsSum == 5 || dimondSum == 5 || clubSum == 5 || spadeSum == 5)
            {
                //如果2邊都同花，那麼就看玩家手持的最大牌
                //如果2邊卡牌加總相同那麼將看玩家手上的牌加總之數值來分勝負
                handValue.Total = (int)cards[4].MyValue;
                return true;

            }
            return false;
        }
        /// <summary>
        /// 順子
        /// </summary>
        /// <returns></returns>
        private bool Straight()
        {
            //如果你有5個連續值
            if (cards[0].MyValue + 1 == cards[1].MyValue &&
                cards[1].MyValue + 1 == cards[2].MyValue &&
                cards[2].MyValue + 1 == cards[3].MyValue &&
                cards[3].MyValue + 1 == cards[4].MyValue)
            {
                //雙方的最後一張牌高者獲勝
                /*
                handValue.Total = (int)cards[0].MyValue+
                (int)cards[1].MyValue+
                (int)cards[2].MyValue+
                (int)cards[3].MyValue+
                (int)cards[4].MyValue;
                */
                handValue.Total = (int)cards[4].MyValue;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 三條
        /// </summary>
        /// <returns></returns>
        private bool ThreeOfKind()
        {
            if ((cards[0].MyValue == cards[1].MyValue && cards[0].MyValue == cards[2].MyValue) ||
                (cards[1].MyValue == cards[2].MyValue && cards[1].MyValue == cards[3].MyValue))
            {
                handValue.Total = (int)cards[2].MyValue * 3;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[2].MyValue == cards[3].MyValue && cards[2].MyValue == cards[4].MyValue)
            {
                handValue.Total = (int)cards[2].MyValue * 3;
                handValue.HighCard = (int)cards[1].MyValue;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 兩對
        /// </summary>
        /// <returns></returns>
        private bool TwoPairs()
        {
            //1,2 3,4
            //1,2 4,5
            //2,3 4,5 
            if (cards[0].MyValue == cards[1].MyValue && cards[2].MyValue == cards[3].MyValue)
            {
                handValue.Total = ((int)cards[1].MyValue * 2) + (int)cards[3].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[0].MyValue == cards[1].MyValue && cards[3].MyValue == cards[4].MyValue)
            {
                handValue.Total = ((int)cards[1].MyValue * 2) + (int)cards[3].MyValue * 2;
                handValue.HighCard = (int)cards[2].MyValue;
                return true;
            }
            else if (cards[1].MyValue == cards[2].MyValue && cards[3].MyValue == cards[4].MyValue)
            {
                handValue.Total = ((int)cards[1].MyValue * 2) + (int)cards[3].MyValue * 2;
                handValue.HighCard = (int)cards[0].MyValue;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 一對
        /// </summary>
        /// <returns></returns>
        private bool OnePair()
        {
            //1,2
            //2,3
            //3,4
            //4,5
            if (cards[0].MyValue == cards[1].MyValue)
            {
                handValue.Total = (int)cards[0].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[1].MyValue == cards[2].MyValue)
            {
                handValue.Total = (int)cards[1].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[2].MyValue == cards[3].MyValue)
            {
                handValue.Total = (int)cards[2].MyValue * 2;
                handValue.HighCard = (int)cards[4].MyValue;
                return true;
            }
            else if (cards[3].MyValue == cards[4].MyValue)
            {
                handValue.Total = (int)cards[3].MyValue * 2;
                handValue.HighCard = (int)cards[2].MyValue;
                return true;
            }
            return false;
        }
    }
}
