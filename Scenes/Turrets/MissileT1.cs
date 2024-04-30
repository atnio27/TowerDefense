using Godot;
using System;

public class MissileT1 : Turret
{
	public override void _PhysicsProcess(float delta)
	{
		base.Turn();
	}
}
