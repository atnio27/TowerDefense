using Godot;
using System;

public class BlueTank : PathFollow2D
{
	float speed = 300;
	public override void _Ready()
	{
		
	}

 // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(float delta)
	{
		move(delta);
	}

	public void move(float delta)
	{
		Offset = Offset + speed * delta;
	}
}
