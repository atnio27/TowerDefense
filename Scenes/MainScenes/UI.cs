using Godot;
using System;

public class UI : CanvasLayer
{
	public void SetTowerPreview(string towerType, Vector2 mousePosition)
	{
		PackedScene dragTower = (PackedScene)ResourceLoader.Load("res://Scenes/Turrets/" + towerType + ".tscn");
		Node dragTowerInstance = dragTower.Instance();
		dragTowerInstance.Name = "DragTower";

		Control control = new Control();
		control.AddChild(dragTowerInstance, true);
		control.RectPosition = mousePosition;
		control.Name = "TowerPreview";
		AddChild(control, true);
		MoveChild(GetNode<Control>("TowerPreview"), 0);
		
		GetNode<Node2D>("TowerPreview/DragTower").Modulate = new Color("ad54ff3c");
	}

// acabar esta mal
	public void updateTowerPreview(Vector2 newPosition, Color color)
	{
		GetNode<Control>("TowerPreview").RectPosition = newPosition;
		if (GetNode<Node2D>("TowerPreview/DragTower").Modulate != color)
		{
			GetNode<Node2D>("TowerPreview/DragTower").Modulate = color;
		}
	}
}
