using Godot;
using System;

public partial class Boulder : Projectile
{
	private bool newDamage = true;
	private float newMultiplier = 1.5f;

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Ready()
    {
		this.velocityDamage = newDamage;
		this.damageMultiplier = newMultiplier;
    }
	public override void _Process(double delta)
	{
		countTime(delta);
	}
}
