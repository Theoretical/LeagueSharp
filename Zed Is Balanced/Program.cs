using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace ZedIsBalanced
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += e =>
            {
                if (ObjectManager.Player.ChampionName == "Zed")
                    new Zed();
            };
        }
    }
}
