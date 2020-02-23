using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGameConsole
{
    class DealCards : DeckOfCards
    {
        private Card[] playerHand;
        private Card[] computerHand;
        private Card[] sortedPlayerHand;
        private Card[] sortedComputerHand;

        public DealCards()
        {
            playerHand = new Card[5];
            sortedPlayerHand = new Card[5];
            computerHand = new Card[5];
            sortedComputerHand = new Card[5];
        }

        public void Deal()
        {
            setUpDeck();//生成卡牌並洗牌
            getHand();
            sortCards();
            displayCards();
            evaluateHands();
        }

        public void getHand()
        {
            for (int i = 0; i < 5; i++)
            {
                playerHand[i] = getDeck[i];
            }
            for (int i = 5; i < 10; i++)
            {
                computerHand[i - 5] = getDeck[i];
            }
        }

        public void sortCards()
        {
            var queryPlayer = from hand in playerHand//在playerHand(玩家的手牌)裡的hand(手牌)
                              orderby hand.MyValue//每個hand依照MyValue做排列
                              select hand;
            var queryComputer = from hand in computerHand
                                orderby hand.MyValue
                                select hand;
            var index = 0;
            foreach (var element in queryPlayer.ToList())
            {
                sortedPlayerHand[index] = element;
                index++;
            }
            index = 0;
            foreach (var element in queryComputer.ToList())
            {
                sortedComputerHand[index] = element;
                index++;
            }
        }

        public void displayCards()
        {
            Console.Clear();
            int x = 0;//讓光標位置從左至右
            int y = 1;//讓光標位置從上至下

            //顯示玩家的手牌
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("玩家的手牌");
            for (int i = 0; i < 5; i++)
            {
                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(sortedPlayerHand[i], x, y);
                x++;
            }
            //準備畫電腦手牌
            y = 15;//將光標往下移
            x = 0;//x歸零
            Console.SetCursorPosition(x, 14);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("電腦的手牌");
            for (int i = 5; i < 10; i++)
            {
                DrawCards.DrawCardOutline(x, y);
                DrawCards.DrawCardSuitValue(sortedComputerHand[i - 5], x, y);
                x++;//向右移動
            }
        }

        public void evaluateHands()
        {
            HandEvaluator playerHandEvaluator = new HandEvaluator(sortedPlayerHand);//將玩家手牌丟進去處理
            HandEvaluator computerHandEvaluator = new HandEvaluator(sortedComputerHand);//將電腦手牌丟進去處理

            //得到玩家及電腦的手牌
            Hand playerHand = playerHandEvaluator.EvaluateHand();
            Hand computerHand = computerHandEvaluator.EvaluateHand();

            //顯示雙方手牌
            Console.WriteLine("\n\n\n\n\n玩家的手牌:" + playerHand);
            Console.WriteLine("\n電腦的手牌:" + computerHand);

            //比牌
            if (playerHand > computerHand)
            {
                Console.WriteLine("玩家勝!");
            }
            else if (playerHand < computerHand)
            {
                Console.WriteLine("電腦勝!");
            }
            else
            {
                //同牌型比較
                if (playerHandEvaluator.HandValue.Total > computerHandEvaluator.HandValue.Total)
                    Console.WriteLine("玩家勝!");
                else if (playerHandEvaluator.HandValue.Total < computerHandEvaluator.HandValue.Total)
                    Console.WriteLine("電腦勝!");
                //同牌型同數值比較
                else if (playerHandEvaluator.HandValue.HighCard > computerHandEvaluator.HandValue.HighCard)
                    Console.WriteLine("玩家勝!");
                else if (playerHandEvaluator.HandValue.HighCard < computerHandEvaluator.HandValue.HighCard)
                    Console.WriteLine("電腦勝!");
                else
                    Console.WriteLine("平手!");

            }


        }
    }
}
