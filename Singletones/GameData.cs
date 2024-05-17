using Godot;
using System;
using System.Collections.Generic;

public class GameData : Node
{
    public static Dictionary<string, Dictionary<string, float>> towerData = new Dictionary<string, Dictionary<string, float>>();
    public override void _Ready()
    {
        Dictionary<string, float> GunT1 = new Dictionary<string, float>(){
            {"damage", 20},
            {"rof", 0.3f},
            {"range", 350},
            {"category", 0}
        };
        towerData.Add("GunT1", GunT1);

        Dictionary<string, float> MissileT1 = new Dictionary<string, float>(){
            {"damage", 100},
            {"rof", 3},
            {"range", 550},
            {"category", 1}
        };
        towerData.Add("MissileT1", MissileT1);
    }
}
