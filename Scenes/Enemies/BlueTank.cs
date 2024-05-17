using Godot;
using System;
using System.Xml.Resolvers;

public class BlueTank : PathFollow2D
{
	[Signal]
	delegate void baseDamage(float damage);
	float speed = 150;
	float hp = 500;
	TextureProgress healthBar;
	Position2D impactArea;
	PackedScene projectileImpact;
	float damage = 21;
	
	public override void _Ready()
	{
		healthBar = GetNode<TextureProgress>("HealthBar");
		impactArea = GetNode<Position2D>("Impact");
		projectileImpact = (PackedScene)GD.Load("res://Scenes/SupportScenes/ProjectileImpact.tscn");

		healthBar.MaxValue = hp;
		healthBar.Value = hp;
		healthBar.SetAsToplevel(true);
	}
	
	public override void _PhysicsProcess(float delta)
	{
		if (UnitOffset == 1.0)
		{
			EmitSignal("baseDamage", damage);
			QueueFree();
		}
		move(delta);
	}

	public void move(float delta)
	{
		Offset += speed * delta;
		GetNode<TextureProgress>("HealthBar").RectPosition = Position - new Godot.Vector2(30, 30);
	}

	public void onHit(float damage)
	{
		impact();
		hp -= damage;
		healthBar.Value = hp;
		if(hp <= 0)
		{
			onDestroy();
		}
	}

	public void impact()
	{
		GD.Randomize();
		float xPos = GD.Randi() % 31;
		GD.Randomize();
		float yPos = GD.Randi() % 31;
		Vector2 impactLocation = new Vector2 (xPos, yPos);
		Node2D newImpactInstance = (Node2D)projectileImpact.Instance();
		newImpactInstance.Position = impactLocation;
		impactArea.AddChild(newImpactInstance);
	}

	public async void onDestroy()
	{
		GetNode<KinematicBody2D>("KinematicBody2D").QueueFree();
		await ToSignal(GetTree().CreateTimer(0.2f), "timeout");
		this.QueueFree();
	}
}
