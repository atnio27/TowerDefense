using Godot;
using System;

public class GunT1 : Turret
{
	public override void _Ready()
	{
		base._Ready();
	}
	
	public override void _PhysicsProcess(float delta)
	{
		base.Turn();
	}
}



