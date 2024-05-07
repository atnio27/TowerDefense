using Godot;
using System;

public class GameScene : Node2D
{
	Node2D mapNode;
	bool buildMode = false;
	bool buildValid = false;
	Vector2 buildLocation;
	string buildType;
	
	public override void _Ready()
	{
		mapNode = GetNode<Node2D>("Map1");

		foreach (Node node in GetTree().GetNodesInGroup("buildButtons"))
		{
			node.Connect("pressed", this, "InitiateBuildMode", new Godot.Collections.Array { node.Name }); //
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

	public void InitiateBuildMode(string towerType)
	{
		buildType = towerType + "T1";
		buildMode = true;
		GetNode<UI>("UI").SetTowerPreview(buildType, GetGlobalMousePosition());
	}

// acabar esta mal
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
		buildValid = false;;
		GetNode<Control>("UI/TowerPreview").QueueFree();
	}

	public void verifyAndBuild()
	{
		if (buildValid==true)
		{
			// enough money?
			PackedScene newTower = (PackedScene)ResourceLoader.Load("res://Scenes/Turrets/" + buildType + ".tscn");
			Node2D newTowerInstance = (Node2D)newTower.Instance();
			newTowerInstance.Position = buildLocation;
			mapNode.GetNode<Node>("Turrets").AddChild(newTowerInstance, true);
			cancelBuildMode();
		}
	}
}
