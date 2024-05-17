using Godot;
using System;

public class SceneHandler : Node
{
	public override void _Ready()
	{
		loadMainMenu();
	}

	public void loadMainMenu()
	{
		GetNode<Control>("MainMenu/M/VB/NewGame").Connect("pressed", this, "OnNewGamePressed");
		GetNode<Control>("MainMenu/M/VB/Quit").Connect("pressed", this, "OnQuitPressed");
	}
	
	public void OnNewGamePressed() 
	{
		GetNode<Control>("MainMenu").QueueFree();
		PackedScene GameScene = (PackedScene)ResourceLoader.Load("res://Scenes/MainScenes/GameScene.tscn");
		Node GameInstance = GameScene.Instance();
		GameInstance.Connect("gameFinished", this, "unloadGame");
		AddChild(GameInstance);
	}
	
	public void OnQuitPressed()
	{
		GetTree().Quit();
	}

	public void unloadGame(bool result)
	{
		GetNode<GameScene>("GameScene").QueueFree();
		PackedScene MainMenu = (PackedScene)ResourceLoader.Load("res://Scenes/UIScenes/MainMenu.tscn");
		Node MainMenuInstance = MainMenu.Instance();
		AddChild(MainMenuInstance);
		loadMainMenu();
	}
}
