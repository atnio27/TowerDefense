using Godot;
using System;

public class GunT2 : Turret
{
	public override void _PhysicsProcess(float delta)
	{
		base.Turn();
	}
}
