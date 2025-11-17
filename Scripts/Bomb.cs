using Godot;
using System;

public partial class Bomb : Projectile
{
	private bool velocityDamage = true;
	private float damageMultiplier = 1.2f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		countTime(delta);
	}
}
