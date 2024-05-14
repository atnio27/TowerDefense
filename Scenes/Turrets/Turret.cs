using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Turret : Node2D
{
	// ACABAR
	List<PathFollow2D> enemyList = new List<PathFollow2D>();
	bool built = false;

	public bool Built { get => built; set => built = value; }

	public override void _Ready()
	{
		if(built == true)
		{
			GetNode<CircleShape2D>("Range/CollisionShape2D").Radius = 0.5f * GameData.towerData[this.Name]["name"];
		}
	}

	//

	public void Turn() 
	{
		Vector2 enemyPosition = GetGlobalMousePosition();
		GetNode<Sprite>("Turret").LookAt(enemyPosition);
	}
	
	private void _on_Range_body_entered(PathFollow2D body)
	{
		enemyList.Append(body.GetParent<PathFollow2D>());
		GD.Print(enemyList);
	}


	private void _on_Range_body_exited(PathFollow2D body)
	{
		enemyList.Remove(body.GetParent<PathFollow2D>());
		GD.Print(enemyList);
	}
}

