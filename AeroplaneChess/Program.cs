using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroplaneChess
{
    class Program
    {
        public static int[] Maps = new int[100];
        public static int[] PlayerPos = new int[2];
        public static string[] PlayerName = new string[2];
        public static bool[] Flags = new bool[2];

        static void Main(string[] args)
        {
            InitailMap();   //初始化地图

            InputName();    //输入玩家姓名
            GameShow();     //游戏头
            Console.WriteLine("{0}:玩家用A表示", PlayerName[0]);
            Console.WriteLine("{0}:玩家用B表示", PlayerName[1]);
            DrawMap();

            while (PlayerPos[0] < 99 && PlayerPos[1] < 99)
            {
                if (Flags[0] == false)
                    PlayGame(0);
                else
                    Flags[0] = false;

                if (Flags[1] == false)
                    PlayGame(1); 
                else
                    Flags[1] = false;
            }
            if (PlayerPos[0] >= 99)
                Console.WriteLine("玩家{0}胜利了！！！", PlayerName[0]);
            else
                Console.WriteLine("玩家{0}胜利了！！！", PlayerName[1]);
            Console.ReadKey();
        }

        public static void GameShow()    
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*********************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*********************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*********************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("*********0505.NET飞行棋**********");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*********************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("*********************************");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*********************************");
        }

        public static void InitailMap()
        {
            int[] luckturn = { 6, 23, 40, 55, 69, 83 };                 //幸运轮盘 ◎1
            for (int i = 0; i < luckturn.Length; i++)
            {
                Maps[luckturn[i]] = 1;
            }

            int[] landMine = { 5, 13, 17, 33, 38, 50, 64, 80, 94 };     //地雷 ☆2
            for (int i = 0; i < landMine.Length; i++)
            {
                Maps[landMine[i]] = 2;
            }

            int[] pause = { 9, 27, 60, 93 };                            //暂停 ▲3
            for (int i = 0; i < pause.Length; i++)
            {
                Maps[pause[i]] = 3;
            }

            int[] timeTunnel = { 20, 25, 45, 63, 72, 88, 90 };          //时空隧道 卍4
            for (int i = 0; i < timeTunnel.Length; i++)
            {
                Maps[timeTunnel[i]] = 4;
            }
        }

        public static void DrawMap()
        {
            Console.WriteLine("图例：幸运轮盘：◎    地雷：☆    暂停：▲    时空隧道：卍");
            #region 第一横行
            for (int i = 0; i < 30; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
            #endregion
            #region 第一竖行
            for (int i = 30; i < 35; i++) {
                for (int j = 0; j < 30; j++) {
                    if (j == 29)
                    {
                        Console.Write(DrawStringMap(i));
                    }
                    else
                        Console.Write("  ");
                }
                Console.WriteLine();
            }
            #endregion
            #region 第二横行
            for (int i = 64; i >= 35; i--)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
            #endregion
            #region 第二竖行
            for (int i = 65; i <70; i++)
            {
                Console.WriteLine(DrawStringMap(i));
            }
            #endregion
            #region 第三横行
            for (int i = 70; i < 100; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
            #endregion
        }

        public static string DrawStringMap(int i)
        {
            string str = "";

            //若玩家A玩家B的坐标相同，并且都在地图上，画<>
            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[1] == i) {
                Console.ForegroundColor = ConsoleColor.Gray;
                str = "<>";//Console.Write("<>");
            }
            else if (PlayerPos[0] == i) {
                Console.ForegroundColor = ConsoleColor.Gray;
                str = "Ａ";//Console.Write("Ａ");
            }
            else if (PlayerPos[1] == i) {
                Console.ForegroundColor = ConsoleColor.Gray;
                str = "Ｂ";//Console.Write("Ｂ");
            }
            else
            {
                switch (Maps[i])
                {
                    case 0: Console.ForegroundColor = ConsoleColor.Yellow; str = "□"; break;
                    case 1: Console.ForegroundColor = ConsoleColor.Green; str = "◎"; break;
                    case 2: Console.ForegroundColor = ConsoleColor.Red; str = "☆"; break;
                    case 3: Console.ForegroundColor = ConsoleColor.Blue; str = "▲"; break;
                    case 4: Console.ForegroundColor = ConsoleColor.DarkCyan; str = "卍"; break;
                }

            }

            return str;

        }

        public static void InputName()
        {
            GameShow();     //游戏头
            Console.WriteLine("请输入玩家A的姓名:");
            PlayerName[0] = Console.ReadLine();
            while (PlayerName[0] == "") {
                Console.WriteLine("玩家A的姓名不能为空，请重新输入:");
                PlayerName[0] = Console.ReadLine();
            }
            Console.WriteLine("请输入玩家B的姓名:");
            PlayerName[1] = Console.ReadLine();
            while (PlayerName[1] == "" || PlayerName[0] == PlayerName[1])
            {
                if(PlayerName[1] == "")
                    Console.WriteLine("玩家A的姓名不能为空，请重新输入:");
                else
                    Console.WriteLine("该游戏名已有主，请重新输入:");

                PlayerName[1] = Console.ReadLine();
            }
            Console.Clear();
        }

        public static void PlayGame(int playerNumber)
        {
            Random r = new Random();
            int rNumber = r.Next(1, 7);

            Console.WriteLine("玩家{0}按任意键开始掷骰子", PlayerName[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("玩家{0}掷出了{1}", PlayerName[playerNumber], rNumber);
            PlayerPos[playerNumber] += rNumber;
            Console.ReadKey(true);
            Console.WriteLine("玩家{0}按任意键开始行动", PlayerName[playerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("玩家{0}行动完了", PlayerName[playerNumber]);

            if (PlayerPos[playerNumber] == PlayerPos[1- playerNumber])
            {
                Console.WriteLine("玩家{0}踩到了玩家{1}，玩家{1}退6格", PlayerName[playerNumber], PlayerName[1-playerNumber], PlayerName[1- playerNumber]);
                PlayerPos[1- playerNumber] -= 6;
                Console.ReadKey();
            }
            else
            {  //踩到了关卡
               //玩家的坐标
                switch (Maps[PlayerPos[playerNumber]])
                {   //0 1 2 3 4 
                    case 0:
                        Console.WriteLine("玩家{0}踩到了方块，安全", PlayerName[playerNumber]);
                        Console.ReadKey(true); break;
                    case 1:
                        Console.WriteLine("玩家{0}踩到了幸运轮盘，请选择  1 -->交换位置  2 -->轰炸对方", PlayerName[playerNumber]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("玩家{0}选择跟玩家{1}交换位置", PlayerName[playerNumber], PlayerName[1- playerNumber]);
                                int temp = PlayerPos[playerNumber];
                                PlayerPos[playerNumber] = PlayerPos[1- playerNumber];
                                PlayerPos[1- playerNumber] = temp;
                                Console.WriteLine("交换完成！！！按任意键继续游戏");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("玩家{0}选择轰炸玩家{1},玩家{1}后退6格", PlayerName[playerNumber], PlayerName[1- playerNumber]);

                                PlayerPos[1- playerNumber] -= 6;
                                Console.WriteLine("玩家{1}后退6格,按任意键继续游戏");
                                Console.ReadKey(true);
                                break;

                            }
                            else
                            {
                                Console.WriteLine("只能输入1或者2，1 -->交换位置  2 -->轰炸对方");
                                input = Console.ReadLine();
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("玩家{0}踩到了地雷，退6格", PlayerName[playerNumber]);
                        PlayerPos[playerNumber] -= 6;
                        Console.ReadKey(true); break;
                    case 3:
                        Console.WriteLine("玩家{0}踩到了暂停，暂停一回合", PlayerName[playerNumber]);
                        Flags[playerNumber] = true;
                        Console.WriteLine("按任意键开始下一回合");
                        Console.ReadKey(true); break;
                    case 4:
                        Console.WriteLine("玩家{0}踩到了时空隧道，前进10格", PlayerName[playerNumber]);
                        PlayerPos[playerNumber] += 10;
                        Console.ReadKey(true); break;

                }

            }
            Console.Clear();
            ChangePos();
            DrawMap();

        }

        public static void ChangePos()
        {
            if (PlayerPos[0] < 0)
                PlayerPos[0] = 0;
            else if (PlayerPos[0] >= 99)
                PlayerPos[0] = 99;

            if (PlayerPos[1] < 0)
                PlayerPos[1] = 0;
            else if (PlayerPos[1] >= 99)
                PlayerPos[1] = 99;

        }
    }
}
