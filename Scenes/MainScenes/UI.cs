using Godot;
using System;
using System.Linq;
using System.Numerics;

public class UI : CanvasLayer
{

	TextureProgress hpBar;
	Tween hpBarTween;
	public override void _Ready()
	{
		hpBar = GetNode<TextureProgress>("HUD/InfoBar/H/HP");
		hpBarTween = GetNode<Tween>("HUD/InfoBar/H/HP/Tween");
	}
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

	private void _on_PausePlay_pressed()
	{
		// if (GetParent<GameScene>().BuildMode==true)
		// {
		// 	GetParent<GameScene>().cancelBuildMode();
		// }
		if (GetTree().Paused==true)
		{
			GetTree().Paused = false;
			GD.Print(GetParent());
		}
		else if (GetParent<GameScene>().CurrentWave == 0)
		{
			GetParent<GameScene>().CurrentWave++;
			GetParent<GameScene>().startNextWave();
		}
		else
		{
			GetTree().Paused = true;
		}
	}

	private void _on_SpeedUp_pressed()
	{
		// if (GetParent<GameScene>().BuildMode==true)
		// {
		// 	GetParent<GameScene>().cancelBuildMode();
		// }
		if (Engine.TimeScale == 2.0)
		{
			Engine.TimeScale = 1.0f;
		}
		else
		{
			Engine.TimeScale = 2.0f;
		}
	}

	public void updateHealthBar(float baseHealth)
	{
		hpBarTween.InterpolateProperty(hpBar, "value", hpBar.Value, baseHealth, 0.1f, Tween.TransitionType.Linear, Tween.EaseType.InOut);
		hpBarTween.Start();
		if (baseHealth >=60)
		{
			hpBar.TintProgress = new Color("3cc510");
		}
		else if (baseHealth <= 60 && baseHealth >= 25)
		{
			hpBar.TintProgress = new Color("e1be32");
		}
		else
		{
			hpBar.TintProgress = new Color("e11e1e");
		}
	}
}


	



