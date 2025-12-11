using Godot;
using System;

public partial class Fish : Projectile
{
	private bool newDamage = false;
	private float newMultiplier = 1.5f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Ready()
	{
		rarity = Rarity.Legendary;
		this.velocityDamage = newDamage;
		this.damage = 200.0f;
	}
	public override void _Process(double delta)
	{
		countTime(delta);
		GetNode<Node2D>("Skeleton2D/Bone2D/Bone2D4").LookAt(GetNode<Node2D>("Head").GlobalPosition);
		//GetNode<Node2D>("Skeleton2D/Bone2D/Bone2D4").RotationDegrees += 90;
	}
}
