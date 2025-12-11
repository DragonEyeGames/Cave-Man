using Godot;
using System;

public partial class Rock : Projectile
{
	private bool velocityDamage = true;
	private float damageMultiplier = 0.5f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		rarity = Rarity.Common;
		countTime(delta);
	}
}
