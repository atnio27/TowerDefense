using Godot;
using System;
using System.Linq;
using System.Numerics;

public class UI : CanvasLayer
{
	public void SetTowerPreview(string towerType, Godot.Vector2 mousePosition)
	{
		PackedScene dragTower = (PackedScene)ResourceLoader.Load("res://Scenes/Turrets/" + towerType + ".tscn");
		Node dragTowerInstance = dragTower.Instance();
		dragTowerInstance.Name = "DragTower";

		Sprite rangeTexture = new Sprite
		{
			Position = new Godot.Vector2(32, 32)
		};
		float scaling = GameData.towerData[towerType]["range"] / 600.0f;
		rangeTexture.Scale = new Godot.Vector2(scaling, scaling);
		Texture texture = (Texture)ResourceLoader.Load("res://Assets/UI/range_overlay.png");
		rangeTexture.Texture = texture;
		rangeTexture.Modulate = new Color("ad54ff3c");


		Control control = new Control();
		control.AddChild(dragTowerInstance, true);
		control.AddChild(rangeTexture, true);
		control.RectPosition = mousePosition;
		control.Name = "TowerPreview";
		AddChild(control, true);
		MoveChild(GetNode<Control>("TowerPreview"), 0);
		
		GetNode<Node2D>("TowerPreview/DragTower").Modulate = new Color("ad54ff3c");
	}
	
	public void updateTowerPreview(Godot.Vector2 newPosition, Color color)
	{
		GetNode<Control>("TowerPreview").RectPosition = newPosition;
		if (GetNode<Node2D>("TowerPreview/DragTower").Modulate != color)
		{
			GetNode<Node2D>("TowerPreview/DragTower").Modulate = color;
			GetNode<Node2D>("TowerPreview/Sprite").Modulate = color;
		}
	}
}
