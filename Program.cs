/*飞行棋规则：
如果玩家A踩到了玩家B，玩家B退6格；
踩到了地雷，退6格
踩到了时空隧道，进10格
踩到了幸运轮盘1、换位置；2、轰炸对方使得对方退六格
踩到了暂停，暂停一回合
踩到了方块，什么也不用干*/

using EXP10GameBuilder;
using System;
using System.Text;

namespace GameBuilder
{
    class Program()
    {
        public static int[] Maps = new int[100];//用静态字段模拟全局变量，来储存为地图
        //可以用0代表方块，1代表幸运轮盘，2代表地雷，3代表暂停，4代表时空隧道

        //声明一个静态数组来存储玩家A与玩家B的坐标
        public static int[] PlayerPos = new int[2];

        //声明一个静态数组来存储玩家A和B的名字
        public static string[] PlayerNames= new string[2];

        //声明一个布尔类型的变量来作为玩家的标记，其用于使玩家暂停
        public static bool[] Flags = new bool[2];//Flag[0]与Flags[1]默认都是false

        static void Main()
        {
            //FileName SUQ = new FileName();
            //SUQ._age = 114514;//创建FileName类对象

            GameShow();
            #region 输入玩家姓名
            //输入玩家的姓名
            Console.WriteLine("请输入玩家A的姓名");
            PlayerNames[0] = Console.ReadLine();
            while (PlayerNames[0]=="")
            {
                Console.WriteLine("玩家A的姓名不能为空，请重新输入");
                PlayerNames[0] = Console.ReadLine();
            }
            Console.WriteLine("请输入玩家B的姓名");
            PlayerNames[1] = Console.ReadLine();
            while (PlayerNames[1] == "" || PlayerNames[1] == PlayerNames[0])
            {
                if (PlayerNames[1]=="")
                {
                    Console.WriteLine("玩家B的姓名不能为空，请重新输入");
                    PlayerNames[1] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("玩家B的姓名不能与玩家A的姓名相同，请重新输入");
                    PlayerNames[1] = Console.ReadLine();
                } 
            }
            #endregion
            //在玩家姓名输入以后，我们要清屏
            Console.Clear();//清屏
            GameShow();
            Console.WriteLine("{0}的士兵用A表示", PlayerNames[0]);
            Console.WriteLine("{0}的士兵用B表示", PlayerNames[1]);
          
            //画地图之前，首先要初始化地图
            InitailMap();
            DrawMap();

            //当玩家A和玩家B没有一个到达终点时，则不停地玩游戏
            while (PlayerPos[0] < 99 && PlayerPos[1]<99)
            {
                if (Flags[0] == false)
                {
                    PlayGame(0);
                }
                else
                {
                    Flags[0] = false;
                }
                if (PlayerPos[0] >= 99)
                {
                    PlayerPos[0] = 99;
                    Console.WriteLine("玩家{0}是胜利者！！！", PlayerNames[0]);
                    break;
                }

                if (Flags[1]==false)
                {
                    PlayGame(1);
                }
                else
                {
                    Flags[1] = false;
                }
             
                if (PlayerPos[1]>=99)
                {
                    PlayerPos[1] = 99;
                    Console.WriteLine("玩家{0}是胜利者！！！", PlayerNames[1]);
                    break;
                }
                Console.ReadKey();

            }//while


            Console.ReadKey();
        }

        public static void GameShow()//建立游戏开始菜单
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("********************************");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("********************************");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("********************************");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("********飞行棋试做版************");
            Console.ForegroundColor = ConsoleColor.Cyan;//青色
            Console.WriteLine("********************************");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;//暗紫色
            Console.WriteLine("********************************");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// 初始化地图
        /// </summary>
        public static void InitailMap()
        {
            int[] luckyturn = { 6,23,40,55,69,83};//幸运轮盘◎
            for (int i = 0; i < luckyturn.Length; i++)
            {
                Maps[luckyturn[i]] = 1;
            }
            int[] landMine = { 5,13,17,33,38,50,64,80,94};//地雷☆
            for(int i=0;i<landMine.Length;i++)
            {
                int index = landMine[i];
                Maps[index] = 2;
            }
            int[] pause = { 9,27,60,93};//暂停▲
            for(int i=0;i<pause.Length;i++)
            {
                int index = pause[i];
                Maps[index] = 3;
            }
            int[] timeTunnel = { 20,25,45,63,72,88,90};//时空隧道
            for(int i=0;i<timeTunnel.Length;i++)
            {
                int index = timeTunnel[i];
                Maps[index] = 4;
            }
        }

        /// <summary>
        /// 绘制地图
        /// </summary>
        public static void DrawMap()//两玩家坐标相同画<>，不同时则画A或者B
        {
            Console.WriteLine("图例：幸运轮盘◎\t地雷★\t暂停▲\t时空隧道卐\t");
            #region 第一横行
            for (int i=0;i<30;i++)
            {
                Console.Write(DrawStringMap(i));
            }
            #endregion
            Console.WriteLine();//画完第一横行后换行

            #region 第一竖行
            for (int i=30;i<35;i++)
            {
                for(int j=0;j<29;j++)
                {
                    Console.Write("  ");
                }
                #region 画符号
                Console.Write(DrawStringMap(i));
                #endregion 
                Console.WriteLine();
            }
            #endregion

            for(int i = 64; i >= 35; i--)
            {
                Console.Write(DrawStringMap(i));
            }
            Console.WriteLine();
            for(int i=65;i<70;i++)
            {
                Console.Write(DrawStringMap(i));
                Console.WriteLine();
            }
            for (int i = 70; i <=99; i++)
            {
                Console.Write(DrawStringMap(i));
            }
            //画完最后一行，应该换行
            Console.WriteLine();
        }

        /// <summary>
        /// 返回一个代表地图图形的字符串
        /// </summary>
        /// <param name="i">地图的坐标值</param>
        /// <returns>返回的地形的类型</returns>
        public static string DrawStringMap(int i)//使方法返回字符串，而非直接Wirte输出
        {
            string str = "";
            //如果玩家A与玩家B的坐标相同，并且都在地图上，则画一个尖括号
            if (PlayerPos[0] == PlayerPos[1] && PlayerPos[1] == i)//确保玩家A和B都在第一横行上
            {
               str="<>";
            }
            else if (PlayerPos[0] == i)
            {
                //Shift+空格为全角
                str="A";
            }
            else if (PlayerPos[1] == i)
            {
                str="B";
            }
            else
            {
                switch (Maps[i])
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        str="■";
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Red;
                        str="◎";
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Green;
                        str="★";
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        str="▲";
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        str="卐";
                        break;
                }
            }
            return str;
        }
        /// <summary>
        /// 玩游戏
        /// </summary>
        public static void PlayGame(int PlayerNumber)
        {
            Console.WriteLine("{0}按任意键开始掷骰子", PlayerNames[PlayerNumber]);
            Console.ReadKey(true);//隐藏输入的文字,这是一个重载命令
            Random ss = new Random();
            int Play1 = ss.Next(1, 7);//掷骰子，值为1到6
            Console.WriteLine("{0}掷骰子掷出了{1}", PlayerNames[PlayerNumber], Play1);
            PlayerPos[PlayerNumber] += Play1;
            ChangePos();
            Console.ReadKey(true);
            Console.WriteLine("{0}按任意键开始行动", PlayerNames[PlayerNumber]);
            Console.ReadKey(true);
            Console.WriteLine("{0}行动完了", PlayerNames[PlayerNumber]);
            //玩家A有可能踩到玩家B，方块，幸运轮盘，地雷，暂停，时空隧道
            if (PlayerPos[PlayerNumber] == PlayerPos[1- PlayerNumber])
            {
                Console.WriteLine("玩家{0}踩到了玩家{1},玩家{2}退后6格", PlayerNames[PlayerNumber], PlayerNames[1- PlayerNumber], PlayerNames[1- PlayerNumber]);
                PlayerPos[1- PlayerNumber] -= 6;
                ChangePos();
                Console.ReadKey(true);
            }
            else//踩到了关卡
            {
                switch (Maps[PlayerPos[PlayerNumber]])
                {
                    case 0:
                        Console.WriteLine("玩家{0}踩到了一个方块，什么也没有发生", PlayerNames[PlayerNumber]);
                        Console.ReadKey(true);
                        break;
                    case 1:
                        Console.WriteLine("玩家{0}踩到了幸运轮盘，请选择1--与玩家{1}交换位置；2--轰炸玩家{2}，使其后退六格", PlayerNames[PlayerNumber], PlayerNames[1- PlayerNumber], PlayerNames[1- PlayerNumber]);
                        string input = Console.ReadLine();
                        while (true)
                        {
                            if (input == "1")
                            {
                                Console.WriteLine("玩家{0}选择与玩家{1}交换位置", PlayerNames[PlayerNumber], PlayerNames[1- PlayerNumber]);
                                Console.ReadKey(true);
                                PlayerPos[PlayerNumber] = PlayerPos[1- PlayerNumber] - PlayerPos[PlayerNumber];
                                PlayerPos[1- PlayerNumber] = PlayerPos[1- PlayerNumber] - PlayerPos[PlayerNumber];
                                PlayerPos[PlayerNumber] = PlayerPos[1- PlayerNumber] + PlayerPos[PlayerNumber];
                                Console.WriteLine("交换成功！！！按任意键继续游戏");
                                Console.ReadKey(true);
                                break;
                            }
                            else if (input == "2")
                            {
                                Console.WriteLine("玩家{0}选择轰炸玩家{1}", PlayerNames[PlayerNumber], PlayerNames[1- PlayerNumber]);
                                Console.ReadKey(true);
                                PlayerPos[1- PlayerNumber] -= 6;
                                ChangePos();
                                Console.WriteLine("玩家{0}后退了6格", PlayerNames[1- PlayerNumber]);
                                Console.ReadKey(true);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("请重新输入1或者2");
                                input = Console.ReadLine();
                            }
                        }
                        Console.ReadKey(true);
                        break;
                    case 2:
                        Console.WriteLine("玩家{0}踩中了地雷，后退6格", PlayerNames[PlayerNumber]);
                        Console.ReadKey(true);
                        PlayerPos[PlayerNumber] -= 6;
                        ChangePos();
                        break;
                    case 3:
                        Console.WriteLine("玩家{0}踩到了暂停，暂停一回合", PlayerNames[PlayerNumber]);
                        Flags[PlayerNumber] = true;
                        Console.ReadKey(true);
                        break;
                    case 4:
                        Console.WriteLine("玩家{0}踩到了时空隧道，前进10格", PlayerNames[PlayerNumber]);
                        Console.ReadKey(true);
                        PlayerPos[PlayerNumber] += 10;
                        ChangePos();
                        break;
                }//switch
            }//else
            Console.Clear();
            DrawMap();//A玩家回合结束后更新屏幕内容

        }

        /// <summary>
        /// 使得A与B玩家均无法走出边界
        /// </summary>
        public static void ChangePos()
        {
            if (PlayerPos[0]<0)
            { PlayerPos[0] = 0;}
            if (PlayerPos[0] >= 99)
            { PlayerPos[0] = 99; }
            if (PlayerPos[1] < 0)
            { PlayerPos[1] = 0; }
            if (PlayerPos[1] >= 99)
            { PlayerPos[1] = 99; }
        }

    }
}
