using Godot;
using System;

public class ProjectileImpact : AnimatedSprite
{
	public override void _Ready()
	{
		Playing = true;
	}
	private void _on_ProjectileImpact_animation_finished()
	{
		QueueFree();
	}
}
