using System.Diagnostics.CodeAnalysis;

namespace RPGCLI;

class Get
{
    public int Bag;
    private static Random random = new Random();
    public int i = random.Next(0, 101);
}

class WuQi //武器机制
{
    public int defaultWuQiShh = 5;
    public int playerImage = 100;
    public int ManaPoints = 100;
}

class ToDownItem
{
    public void fall(ref int bag, WuQi wuqi)
    {
        Random rnd = new Random();
        int i = rnd.Next(0,7);
        int i2 = rnd.Next(0,6);
        wuqi.defaultWuQiShh += i2;
        Console.WriteLine($"增伤{i2}!");
        Console.WriteLine($"你获得了{i}个血瓶!");
        bag += i;
    }
}

class Program
{
    bool whi2 = true;
    int image;
    int shh;
    WuQi wuqi = new WuQi();
    Random random = new Random(); //正常随机数使用
    bool whi = true;
    Get get = new Get();

    private void ZhanDou(ref int playerImage, ref int defaultWuQiShh)
    {
        whi = true;
        ToDownItem toDownItem = new ToDownItem();
            if (whi)
            {
                Get get1 = new Get();
                if (get1.i == 50)
                {
                    image = random.Next(200,411);
                    shh = random.Next(0,41);
                }
                else
                {
                    image = random.Next(0, 101); //血量
                    shh = random.Next(0, 21); //伤害
                }

                Console.WriteLine("开始战斗");
            }

            while (whi)
            {
                Console.WriteLine($"玩家血量:{playerImage}\n玩家伤害:{defaultWuQiShh}\n怪物血量:{image}\n怪物伤害{shh}\n");
                Console.WriteLine("1.战斗,2.跳过,3.逃跑,4.血瓶,5.技能");
                try
                {
                    int a = Convert.ToInt32(Console.ReadLine());
                    switch (a)
                    {
                        case 1:
                            image -= defaultWuQiShh; //怪物掉血机制
                            if (image <= 0) //怪物死亡机制
                            {
                                Console.WriteLine("你赢了");
                                toDownItem.fall(ref get.Bag, wuqi);
                                whi = false;
                                whi2 = true;
                            }

                            playerImage -= shh;
                            if (playerImage <= 0) //玩家死亡机制 同下
                            {
                                string s = $"伤害:{Convert.ToString(wuqi.defaultWuQiShh)}|血量:{Convert.ToString(wuqi.playerImage / 2)}|背包:{get.Bag}";
                                Console.WriteLine("你死了");
                                File.WriteAllText("s.sav", s);
                                Environment.Exit(0);
                            }

                            break;
                        case 2:
                            playerImage -= shh;
                            if (playerImage <= 0)
                            {
                                string s = $"伤害:{Convert.ToString(wuqi.defaultWuQiShh)}|血量:{Convert.ToString(wuqi.playerImage / 2)}|背包:{get.Bag}";
                                Console.WriteLine("你死了");
                                File.WriteAllText("s.sav", s);
                                Environment.Exit(0);
                            }

                            break;
                        case 3:

                            bool TryEscape()
                            {
                                int chance = random.Next(0, 100);
                                return chance < 25;
                            }

                            if (TryEscape())
                            {
                                Console.WriteLine("逃跑成功");
                                whi = false;
                            }
                            else
                            {
                                Console.WriteLine("失败");
                                playerImage -= shh;
                                if (playerImage <= 0)
                                {
                                    string s = $"伤害:{Convert.ToString(wuqi.defaultWuQiShh)}|血量:{Convert.ToString(wuqi.playerImage / 2)}|背包:{string.Join(",", get.Bag)}";
                                    Console.WriteLine("你死了");
                                    File.WriteAllText("s.sav", s);
                                    Environment.Exit(0);
                                }

                            }
                                                                                                                        
                                                                                                                        
                            break;
                            case 4:
                            Console.WriteLine(get.Bag);
                            wuqi.playerImage += 50;
                            Console.WriteLine("使用成功");
                            get.Bag--;
                            break;
                        case 5:
                            if (wuqi.ManaPoints >= 20)
                            {
                                Console.WriteLine("使用成功");
                                image -= 35;
                                if (image <= 0)
                                {
                                    Console.WriteLine("你赢了");
                                    toDownItem.fall(ref get.Bag, wuqi);
                                    whi = false;
                                    whi2 = true;
                                }
                                wuqi.ManaPoints -= 20;
                            }
                            else
                            {
                                Console.WriteLine("法力值不足");
                            }
                            break;
                        default:
                            Console.WriteLine("没有这个选项");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Environment.Exit(0);
                }
            }
    }

    [SuppressMessage("ReSharper", "FunctionNeverReturns")]
    private void main()
    { 
        if (File.Exists("s.sav")) 
        {
            Console.WriteLine("1.继续游戏,2.退出");
            try
            {
                int n = Convert.ToInt32(Console.ReadLine());
                switch (n) 
                { 
                    case 1:
                        string save = File.ReadAllText("s.sav");
                        string[] saveBefore = save.Split('|');
                        string[] saveBefore2 = saveBefore[0].Split(':');
                        wuqi.defaultWuQiShh = Convert.ToInt32(saveBefore2[1]);
                        string[] saveBefore3 = saveBefore[1].Split(':');
                        wuqi.playerImage = Convert.ToInt32(saveBefore3[1]);
                        string[] saveBefore4 = saveBefore[2].Split(':');
                        get.Bag = Convert.ToInt32(saveBefore4[1]);

                        break;
                          case 2:
                              Console.WriteLine("再见!");
                              string s = $"伤害:{Convert.ToString(wuqi.defaultWuQiShh)}|血量:{Convert.ToString(wuqi.playerImage)}|背包:{get.Bag}";
                              File.WriteAllText("s.sav", s);
                              Thread.Sleep(2000);
                              Environment.Exit(0);
                              break;
                          default:
                              Console.WriteLine("没有这个选项");
                              break;
                      }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }
        else
        {
            Console.WriteLine("1.开始游戏,2.退出");
            try
            {
                int n = Convert.ToInt32(Console.ReadLine());
                switch (n)
                {
                    case 1:
                        whi2 = true;
                        break;
                    case 2:
                        Console.WriteLine("再见!");
                        Thread.Sleep(2000);
                        break;
                    default:
                        Console.WriteLine("没有这个选项");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        while (true)
        {
            while (whi2)
            {
                Console.WriteLine("1.前进,2.道具,3.休息,4.查看,5.退出");
                try
                {
                    int n2 = Convert.ToInt32(Console.ReadLine());
                    switch (n2)
                    {
                        case 1:
                            int i = random.Next(0, 101);
                            if (i <= 30)
                            {
                                whi2 = false;
                            }
                            else
                            {
                                Console.WriteLine("无事发生");
                            }
                            break;
                        case 2:
                            if (get.Bag == 0)
                            {
                                Console.WriteLine("没有血瓶");
                            }
                            else
                            {
                                get.Bag--;
                                wuqi.playerImage += 50;
                                Console.WriteLine("使用成功");
                                int i2 = random.Next(0, 101);
                                if (i2 <= 30)
                                {
                                    whi2 = false;
                                }
                                else
                                {
                                    Console.WriteLine("无事发生");
                                }
                            }

                            break;
                        case 3:
                            Console.WriteLine("已休息");
                            if (wuqi.ManaPoints <= 100)
                            {
                                wuqi.ManaPoints = Math.Min(wuqi.ManaPoints + 35, 100);
                                int i3 = random.Next(0, 101);
                                if (i3 <= 30)
                                {
                                    whi2 = false;
                                }
                            }
                            break;
                        case 4:
                            Console.WriteLine($"伤害:{wuqi.defaultWuQiShh},血量:{wuqi.playerImage},法力值:{wuqi.ManaPoints},背包:{get.Bag}");
                            break;
                        case 5:
                            Console.WriteLine("再见!");
                            string s2 = $"伤害:{Convert.ToString(wuqi.defaultWuQiShh)}|血量:{Convert.ToString(wuqi.playerImage)}|背包:{get.Bag}";
                            File.WriteAllText("s.sav", s2);
                            Thread.Sleep(2000);
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("没有这个选项");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Environment.Exit(0);
                }
            }
            whi2 = true;
            int Explore = random.Next(0, 101);
            if (Explore > 50)
            {
                ZhanDou(ref wuqi.playerImage, ref wuqi.defaultWuQiShh);
                Thread.Sleep(1500);
            }
            else if (Explore == 50)
            {
                ZhanDou(ref wuqi.playerImage, ref wuqi.defaultWuQiShh);
            }
        }
    }

    public static void Main()
    {
        Program p = new Program();
        p.main();
    }
}