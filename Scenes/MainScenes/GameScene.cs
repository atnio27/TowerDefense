using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;


public class GameScene : Node2D
{
	Node2D mapNode;
	bool buildMode = false;
	bool buildValid = false;
	Vector2 buildLocation;
	Vector2 buildTile;
	string buildType;

	int currentWave = 0;
	int enemiesInWave = 0;

	public bool BuildMode { get => buildMode; set => buildMode = value; }
	public int CurrentWave { get => currentWave; set => currentWave = value; }


	public override void _Ready()
	{
		mapNode = GetNode<Node2D>("Map1");

		foreach (Node node in GetTree().GetNodesInGroup("buildButtons"))
		{
			node.Connect("pressed", this, "initiateBuildMode", new Godot.Collections.Array { node.Name });
		}
	}
	
	public override void _Process(float delta)
	{
		if (buildMode==true)
		{
			updateTowerPreview();
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_cancel") && buildMode==true)
		{
			cancelBuildMode();
		}
		if (@event.IsActionReleased("ui_accept") && buildMode==true)
		{
			verifyAndBuild();
		}
	}

	public async void startNextWave()
	{
		Dictionary<float,string> waveData = retrieveWaveData();
		await ToSignal(GetTree().CreateTimer(0.2f), "timeout");
		spawnEnemies(waveData);
	}

	

	public Dictionary<float,string> retrieveWaveData()
	{
		Dictionary<float,string> waveData = new Dictionary<float,string>()
		{
			{2f, "BlueTank"},
			{1f, "BlueTank"}
		};
		currentWave ++;
		enemiesInWave = waveData.Count;
		return waveData;
	}

	public async void spawnEnemies(Dictionary<float,string> waveData)
	{
		waveData = retrieveWaveData();
		foreach (KeyValuePair<float, string> kvp in waveData)
		{
			PackedScene newEnemy = (PackedScene)ResourceLoader.Load("res://Scenes/Enemies/" + kvp.Value + ".tscn");
			Node newEnemyinstance = newEnemy.Instance();
			mapNode.GetNode<Path2D>("Path").AddChild(newEnemyinstance,true);
			await ToSignal(GetTree().CreateTimer(kvp.Key), "timeout");
		}
	}

	public void initiateBuildMode(string towerType)
	{
		if (buildMode==false)
		{
			buildType = towerType + "T1";
			buildMode = true;
			GetNode<UI>("UI").SetTowerPreview(buildType, GetGlobalMousePosition());
		}
	}
	
	public void updateTowerPreview()
	{
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 currentTile = mapNode.GetNode<TileMap>("TowerExclusion").WorldToMap(mousePosition);
		Vector2 tilePosition = mapNode.GetNode<TileMap>("TowerExclusion").MapToWorld(currentTile);

		if (mapNode.GetNode<TileMap>("TowerExclusion").GetCellv(currentTile) == -1)
		{
			GetNode<UI>("UI").updateTowerPreview(tilePosition, new Color("ad54ff3c"));
			buildValid = true;
			buildLocation = tilePosition;
			buildTile = currentTile;
		}
		else
		{
			GetNode<UI>("UI").updateTowerPreview(tilePosition, new Color("adff4545"));
			buildValid = false;
		}
	}

	public void cancelBuildMode()
	{
		buildMode = false;
		buildValid = false;
		GetNode<Control>("UI/TowerPreview").QueueFree();
	}

	public void verifyAndBuild()
	{
		if (buildValid==true)
		{
			// enough money?
			// ACABAR Turret turret = GetNode<Turret>("Map1/Turrets/" + buildType);
			PackedScene newTower = (PackedScene)ResourceLoader.Load("res://Scenes/Turrets/" + buildType + ".tscn");
			Node2D newTowerInstance = (Node2D)newTower.Instance();
			newTowerInstance.Position = buildLocation;
			// ACABAR turret.Built = true;
			mapNode.GetNode<TileMap>("TowerExclusion").SetCellv(buildTile, 5);
			mapNode.GetNode<Node2D>("Turrets").AddChild(newTowerInstance, true);
			cancelBuildMode();
		}
	}
}
