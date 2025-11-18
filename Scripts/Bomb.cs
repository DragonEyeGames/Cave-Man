using Godot;
using System;

public partial class Bomb : Projectile
{
	private bool velocityBased = false;
	private float newDamage = 40.0f;
	private bool explodingProjectile = true;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Ready()
	{
		this.velocityDamage = velocityBased;
		this.damage = newDamage;
		this.exploding = explodingProjectile;
	}
	public async override void _Process(double delta)
	{
		countTime(delta);
		if(this.spawnedInTime > 5)
		{
			this.bombShape.SetDeferred("disabled", false);
			GD.Print("sdia");
			GetNode<Node2D>("Explosion").Visible = true;
			GetNode<AnimatedSprite2D>("Explosion").Play("explode");
			SetDeferred("freeze", true);
			GetNode<Node2D>("Sprite").Visible = false;
			await ToSignal(GetTree().CreateTimer(1), "timeout");
			GD.Print("BYE");
			QueueFree();
		}
	}
}
