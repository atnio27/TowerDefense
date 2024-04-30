using Godot;
using System;

public class SceneHandler : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Control>("MainMenu/M/VB/NewGame").Connect("pressed", this, "OnNewGamePressed");
		GetNode<Control>("MainMenu/M/VB/Quit").Connect("pressed", this, "OnQuitPressed");
	}
	
	public void OnNewGamePressed() 
	{
		
	}
	
	public void OnQuitPressed()
	{
		GetTree().Quit();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
