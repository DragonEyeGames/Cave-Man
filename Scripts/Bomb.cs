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
			explode();
		}
	}
}
