using Godot;
using System;

public abstract class Turret : Node2D
{
	public void Turn() 
	{
		Vector2 enemyPosition = GetGlobalMousePosition();
		GetNode<Sprite>("Turret").LookAt(enemyPosition);
	}
}

