using Godot;
using System;

public abstract partial class Projectile : RigidBody2D
{
	public int playerID = 0;
	private double spawnedInTime = 0.0;
	private bool velocityDamage = true;
	private float damageMultiplier = 1.0f;

	public void countTime(double delta)
	{
		spawnedInTime += delta;
	}

	public void count(double delta)
	{
		spawnedInTime += delta;
	}

	public void BodyEntered(Node2D body)
	{
		if(body is Player)
		{
			var player = (Player)body;
			if(player.ID != playerID || spawnedInTime>=0.7f)
			{
				GD.Print(Mathf.Round(LinearVelocity.Length() / 50));
				if(velocityDamage)
				{
					player.Damage(Mathf.Round((float)LinearVelocity.Length() / 50.0f) * damageMultiplier);
				}
				QueueFree();

			}
		}
	}
}
