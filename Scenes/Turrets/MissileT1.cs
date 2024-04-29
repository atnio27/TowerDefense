using Godot;
using System;

public class MissileT1 : TowerTracking
{
	public override void _PhysicsProcess(float delta)
	{
		base.Turn();
	}
}
