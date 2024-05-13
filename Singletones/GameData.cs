using Godot;
using System;
using System.Collections.Generic;

public class GameData : Node
{
    public static Dictionary<string, Dictionary<string, int>> towerData = new Dictionary<string, Dictionary<string, int>>();
    public override void _Ready()
    {
        Dictionary<string, int> GunT1 = new Dictionary<string, int>(){
            {"damage", 20},
            {"rof", 1},
            {"range", 350}
        };
        towerData.Add("GunT1", GunT1);

        Dictionary<string, int> MissileT1 = new Dictionary<string, int>(){
            {"damage", 100},
            {"rof", 1},
            {"range", 550}
        };
        towerData.Add("MissileT1", MissileT1);
    }
}
