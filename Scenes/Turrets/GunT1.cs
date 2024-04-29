using Godot;
using System;

public class GunT1 : TowerTracking
{
	public override void _PhysicsProcess(float delta)
	{
		base.Turn();
	}
}
