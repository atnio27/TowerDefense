using Godot;
using System;

public class BlueTank : PathFollow2D
{
	float speed = 150;
	
	public override void _PhysicsProcess(float delta)
	{
		move(delta);
	}

	public void move(float delta)
	{
		Offset = Offset + speed * delta;
	}
}
