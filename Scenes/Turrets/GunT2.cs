using Godot;
using System;

public class GunT2 : TowerTracking
{
	public override void _PhysicsProcess(float delta)
	{
		base.Turn();
	}
}
