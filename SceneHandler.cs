using Godot;
using System;

public class SceneHandler : Node
{
	public override void _Ready()
	{
		GetNode<Control>("MainMenu/M/VB/NewGame").Connect("pressed", this, "OnNewGamePressed");
		GetNode<Control>("MainMenu/M/VB/Quit").Connect("pressed", this, "OnQuitPressed");
	}
	
	public void OnNewGamePressed() 
	{
		GetNode<Control>("MainMenu").QueueFree();
		PackedScene GameScene = (PackedScene)ResourceLoader.Load("res://Scenes/MainScenes/GameScene.tscn");
		Node GameInstance = GameScene.Instance();
		AddChild(GameInstance);
	}
	
	public void OnQuitPressed()
	{
		GetTree().Quit();
	}
}
