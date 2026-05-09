using System.Diagnostics.CodeAnalysis;

namespace RPGCLI;

class Get
{
    public int Bag;
    public int Image;
    public int ManaBag;
    public int Count;
    private static Random random = new Random();
    public int i = random.Next(0, 101);
}

class WuQi //武器机制
{
    public int defaultWuQiShh = 5;
    public int playerImage = 100;
    public int ManaPoints = 100;
}

class Shop
{
    public Shop()
    {
        Console.WriteLine("血瓶:10元\n增伤药水:30元\n法力值药水:55元\n4.退出");
        try
        {
            Get get = new Get();
            int n = Convert.ToInt32(Console.ReadLine());
            bool i = true;
            while (i)
            {
                switch (n)
                {
                    case 1:
                        if (get.Count >= 10)
                        {
                            get.Count -= 10;
                            get.Bag++;
                            Console.WriteLine("购买成功");
                        }
                        else
                        {
                            Console.WriteLine("购买失败 金币不足");
                            return;
                        }

                        break;
                    case 2:
                        if (get.Count >= 30)
                        {
                            get.Count -= 30;
                            get.Image++;
                            Console.WriteLine("购买成功");
                        }
                        else
                        {
                            Console.WriteLine("购买失败 金币不足");
	            return;
                        }

                        break;
                    case 3:
                        if (get.Count >= 55)
                        {
                            get.Count -= 55;
                            get.ManaBag++;
                            Console.WriteLine("购买成功");
                        }
                        else
                        {
                            Console.WriteLine("购买失败 金币不足");
 	            return;
                        }

                        break;
                    case 4:
                        i = false;
                        break;
                    default:
                        Console.WriteLine("没有这个选项");
                        break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

class ToDownItem
{
    public void fall(ref int bag, WuQi wuqi)
    {
        Get get = new Get();
        double d = Convert.ToDouble(wuqi.playerImage);
        Random rnd = new Random();
        int i = rnd.Next(0,(int)(get.Bag*0.1));
        int i2 = rnd.Next(0,(int)(wuqi.defaultWuQiShh*0.1));
        int i3 = rnd.Next(0, (int)(d*0.25));
        wuqi.defaultWuQiShh += i2;
        Console.WriteLine($"增伤{i2}!");
        Console.WriteLine($"你获得了{i}个血瓶!");
        Console.WriteLine($"你获得了{i3}个金币!");
        get.Count += i3;
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

    private void ZhanDou(ref int playerImage, ref int defaultWuQiShh,int explore)
    {
        whi = true;
        ToDownItem toDownItem = new ToDownItem();
            if (whi)
            {
                if (explore == 50)
                {
                    image = random.Next(200,wuqi.playerImage+501);
                    shh = random.Next(0,wuqi.defaultWuQiShh+36);
                }
                else
                {
                    image = random.Next(0, wuqi.playerImage+1); //血量
                    shh = random.Next(0, wuqi.defaultWuQiShh+6); //伤害
                }

                Console.WriteLine("开始战斗");
            }

            while (whi)
            {
                Console.WriteLine($"玩家血量:{playerImage}\n玩家伤害:{defaultWuQiShh}\n法力值:{wuqi.ManaPoints}\n怪物血量:{image}\n怪物伤害{shh}\n");
                Console.WriteLine("1.战斗,2.跳过,3.逃跑,4.血瓶,5.技能,6.回法,7.增伤");
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
                                break;
                            }
                            playerImage -= shh;
                            if (playerImage <= 0) //玩家死亡机制 同下
                            {
                                string s = $"伤害:{Convert.ToString(wuqi.defaultWuQiShh)}|血量:50|背包:{get.Bag}|法力值:{wuqi.ManaPoints}|金币:{get.Count}|增伤药水:{get.Image}|回法药水:{get.ManaBag}";
                                Console.WriteLine("你死了");
                                File.WriteAllText("s.sav", s);
                                Environment.Exit(0);
                            }

                            break;
                        case 2:
                            playerImage -= shh;
                            if (playerImage <= 0)
                            {
                                string s = $"伤害:{Convert.ToString(wuqi.defaultWuQiShh)}|血量:50|背包:{get.Bag}|法力值:{wuqi.ManaPoints}|金币:{get.Count}|增伤药水:{get.Image}|回法药水:{get.ManaBag}";
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
                                    string s = $"伤害:{Convert.ToString(wuqi.defaultWuQiShh)}|血量:50|背包:{get.Bag}|法力值:{wuqi.ManaPoints}|金币:{get.Count}|增伤药水:{get.Image}|回法药水:{get.ManaBag}";
                                    Console.WriteLine("你死了");
                                    File.WriteAllText("s.sav", s);
                                    Environment.Exit(0);
                                }

                            }
                            break;
                            case 4:
	                        if(get.Bag == 0) 
                            {
                              Console.WriteLine("没有血瓶");
                            }
	                        else
                            {
                              Console.WriteLine(get.Bag);
                              wuqi.playerImage += 50;
                              Console.WriteLine("使用成功");
                              get.Bag--;
                            }
                            break;
                        case 5:
                            if (wuqi.ManaPoints >= 20)
                            {
                                Console.WriteLine("使用成功");
                                image -= wuqi.defaultWuQiShh+30;
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
                        case 6:
                            if (get.ManaBag > 0)
                            {
                                get.ManaBag--;
                                wuqi.ManaPoints = Math.Min(wuqi.ManaPoints + 35,100);
                                Console.WriteLine("使用成功");
                            }
                            else
                            {
                                Console.WriteLine("使用失败 物品不足");
                            }

                            break;
                        case 7:
                            if (get.Image > 0)
                            {
                                get.Image--;
                                wuqi.playerImage += 50;
                                Console.WriteLine("使用成功");
                            }
                            else
                            {
                                Console.WriteLine("使用失败 物品不足");
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
                        string[] saveBefore5 = saveBefore[3].Split(':');
                        string[] saveBefore6 = saveBefore[4].Split(':');
                        string[] saveBefore7 = saveBefore[5].Split(':');
                        string[] saveBefore8 = saveBefore[6].Split(':');
                        get.Bag = Convert.ToInt32(saveBefore4[1]);
                        wuqi.ManaPoints = Convert.ToInt32(saveBefore5[1]);
                        get.Count =  Convert.ToInt32(saveBefore6[1]);
                        get.Image = Convert.ToInt32(saveBefore7[1]);
                        get.ManaBag = Convert.ToInt32(saveBefore8[1]);

                        break;
                          case 2:
                              Console.WriteLine("再见!");
                              string s = $"伤害:{Convert.ToString(wuqi.defaultWuQiShh)}|血量:{Convert.ToString(wuqi.playerImage)}|背包:{get.Bag}|法力值:{wuqi.ManaPoints}|金币:{get.Count}|增伤药水:{get.Image}|回法药水:{get.ManaBag}";
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
                Console.WriteLine("1.前进,2.血瓶,3.休息,4.查看,5,增伤,6.商店,7.退出");
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
                            Console.WriteLine($"伤害:{wuqi.defaultWuQiShh},血量:{wuqi.playerImage},法力值:{wuqi.ManaPoints},背包:{get.Bag},金币:{get.Count},增伤药水:{get.Image},回法药水:{get.ManaBag}");
                            break;
                        case 5:
                            
                            if (get.Image > 0)
                            {
                                get.Image--;
                                wuqi.defaultWuQiShh += 3;
                                Console.WriteLine("使用成功");
                            }
                            else
                            {
                                Console.WriteLine("使用失败 物品不足");
                            }

                            break;
                        case 6:
                            new Shop();
                            break;
                        case 7:
                            Console.WriteLine("再见!");
                            string s2 = $"伤害:{Convert.ToString(wuqi.defaultWuQiShh)}|血量:{Convert.ToString(wuqi.playerImage)}|背包:{get.Bag}|法力值:{wuqi.ManaPoints}|金币:{get.Count}|增伤药水:{get.Image}|回法药水:{get.ManaBag}";
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
                }
            }
            whi2 = true;
            int Explore = random.Next(0, 101);
            if (Explore > 50)
            {
                ZhanDou(ref wuqi.playerImage, ref wuqi.defaultWuQiShh,Explore);
                Thread.Sleep(1500);
            }
            else if (Explore == 50)
            {
                ZhanDou(ref wuqi.playerImage, ref wuqi.defaultWuQiShh,Explore);
            }
        }
    }

    public static void Main()
    {
        Program p = new Program();
        p.main();
    }
}