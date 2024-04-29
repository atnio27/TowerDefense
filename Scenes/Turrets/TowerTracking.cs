using Godot;
using System;

public abstract class TowerTracking : Node2D
{
	public void Turn() 
	{
		Vector2 enemyPosition = GetGlobalMousePosition();
		GetNode<Sprite>("Turret").LookAt(enemyPosition);
	}
}

