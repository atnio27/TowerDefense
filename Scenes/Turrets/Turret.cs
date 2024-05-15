using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Turret : Node2D
{
	List<PathFollow2D> enemyList = new List<PathFollow2D>();
	string name;
	PathFollow2D enemy;

	public override void _Ready()
	{
		if (this.Name != "DragTower")
		{
			if (this.Name.Contains("GunT1"))
			{
				name = "GunT1";
			}
			else if (this.Name.Contains("MissileT1"))
			{
				name = "MissileT1";
			}

			CircleShape2D circleShape = (CircleShape2D)GetNode<CollisionShape2D>("Range/CollisionShape2D").Shape;
			circleShape.Radius = 0.5f * GameData.towerData[name]["range"];
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		if (enemyList.Count != 0 && this.Name != "DragTower")
		{
			selectEnemy();
			turn();
		}
		else
		{
			enemy = null;
		}
	}

	public void turn() 
	{
		GetNode<Sprite>("Turret").LookAt(enemy.Position);
	}

	public void selectEnemy()
	{
		List<float> enemyProgressList = new List<float>();
		foreach (PathFollow2D i in enemyList)
		{
			enemyProgressList.Add(i.Offset);
			float maxOffset = enemyProgressList.Max();
			int enemyIndex = enemyProgressList.IndexOf(maxOffset);
			enemy = enemyList[enemyIndex];
		}
	}
	
	private void _on_Range_body_entered(PathFollow2D body)
	{
		enemyList.Add(body.GetParent<PathFollow2D>());
	}


	private void _on_Range_body_exited(PathFollow2D body)
	{
		enemyList.Remove(body.GetParent<PathFollow2D>());
	}
}

