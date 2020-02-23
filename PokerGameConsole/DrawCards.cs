using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace PokerGameConsole
{
    class DrawCards
    {
        /// <summary>
        /// 畫出卡片的輪廓  
        /// </summary>
        /// <param name="xcoor">卡片寬度</param>33
        /// <param name="ycoor">卡片高度</param>
        public static void DrawCardOutline(int xcoor, int ycoor)
        {
            Console.ForegroundColor = ConsoleColor.White;

            int x = xcoor * 12;
            int y = ycoor;

            Console.SetCursorPosition(x, y);
            Console.Write(" __________\n");//卡片的上面的邊緣 

            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(x, y + 1 + i);

                if (i != 9)
                {
                    Console.WriteLine("|          |");//卡牌的左邊緣及右邊緣
                }
                else
                {
                    Console.WriteLine("|__________|");//卡牌底部邊緣
                }
            }
        }
        /// <summary>
        /// 讓撲克牌的花色和數值顯示在輪廓裡
        /// </summary>
        /// <param name="card"></param>
        /// <param name="xcoor"></param>
        /// <param name="ycoor"></param>
        public static void DrawCardSuitValue(Card card, int xcoor, int ycoor)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string cardSuit = "";
            int x = xcoor * 12;
            int y = ycoor;

            //從CodePage 437 裡找到 愛心、鑽石、梅花和黑桃 4種花色，
            //然後依據它們的花色為它們上色

            switch (card.MySuit)
            {
                case Card.Suit.Hearts:
                    // cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 3 })[0];
                    cardSuit = "紅 心";
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Card.Suit.Dimonds:
                    // cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 4 })[0];
                    cardSuit = "鑽 石";
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Card.Suit.Clubs:
                    //cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 5 })[0];
                    cardSuit = "梅 花";
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case Card.Suit.Spades:
                    // cardSuit = Encoding.GetEncoding(437).GetChars(new byte[] { 6 })[0];
                    cardSuit = "黑 桃";
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
            }

            //顯示花色及數值
            Console.SetCursorPosition(x + 3, y + 5);
            Console.Write(cardSuit);
            Console.SetCursorPosition(x + 4, y + 7);
            Console.Write(card.MyValue);
        }
    }
}
