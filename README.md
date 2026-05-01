# 控制台 RPG 游戏

一个用 C# 编写的轻量级控制台角色扮演游戏。

##  功能特色
- 回合制战斗系统
- 随机探索与遭遇
- 属性与道具管理
- 自动存档/读档

##  快速开始
##  基本玩法
1. **探索阶段**：前进、使用道具、休息、查看状态
2. **战斗阶段**：攻击、防御、逃跑、使用技能
3. **属性**：生命值、法力值、武器伤害
4. **道具**：血瓶可恢复生命值

##  存档说明
进度自动保存到 `s.sav` 文件，下次启动可选"继续游戏"。

##  项目结构
- `Program.cs` - 主程序与游戏逻辑
- 使用 `WuQi`、`Get` 等类管理游戏状态
## 注意事项
由于项目完完全全是CLI构造 所以暂且不能打包成exe 也就是说 需要玩家主动下载.NET10平台 或者修改csproj到你的.NET版本
修改<TargetFrame>标签内的版本 然后在终端进入项目文件夹 运行dotnet run 我清楚这体验是不好的 但我没有办法 我会继续尝试优化的

# Console RPG Game

A lightweight console role-playing game written in C#.

## Features
- Turn-based combat system
- Random exploration and encounters
- Attribute and item management
- Automatic save/load

##  Quick Start
##  Basic Gameplay
1. **Exploration Phase**: Move forward, use items, rest, check status
2. **Combat Phase**: Attack, defend, escape, use skills
3. **Attributes**: Health, mana, weapon damage
4. **Items**: Health potions restore HP

##  Save System
Progress is automatically saved to the `s.sav` file. Choose "Continue Game" on the next launch.

##  Project Structure
- `Program.cs` - Main program and game logic
- Uses classes like `WuQi`, `Get` to manage game state
## Notes
Since the project is entirely CLI-based, it currently cannot be packaged as an executable. This means players need to:
1. Actively download the .NET 10 platform, or
2. Modify the csproj file to match your .NET version by adjusting the version inside the `<TargetFramework>` tag.

Then, open a terminal in the project folder and run `dotnet run`.

I understand this experience is not ideal, but I have no other option at the moment. I will continue to work on optimizations.
