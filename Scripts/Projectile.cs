using Godot;
using System;
using System.Threading.Tasks;

public abstract partial class Projectile : RigidBody2D
{
	public int playerID = 0;
	protected double spawnedInTime = 0.0;
	protected bool velocityDamage = true;
	protected float damageMultiplier = .5f;
	protected float damage = 0;
	protected bool exploding = false;
	[Export]
	public CollisionShape2D bombShape;

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
			if (!exploding)
			{
				var player = (Player)body;
				if (player.ID != playerID || spawnedInTime >= 0.7f)
				{
					GD.Print(Mathf.Round(LinearVelocity.Length() / 50));
					if (velocityDamage)
					{
						player.Damage(Mathf.Round((float)LinearVelocity.Length() / 50.0f) * damageMultiplier);
					}
					else
					{
						player.Damage(damage);
					}
					QueueFree();

				}
			}
			else
			{
				GD.Print("Hey");
				var player = (Player)body;
				if (player.ID != playerID)
				{
					bombShape.SetDeferred("disabled", false);
					GD.Print("sdia");
				}
			}
		}
	}

	public async void PlayerExplode(Node2D body)
	{
		if(body is Player)
		{
			GD.Print("BOOM");
			var player = body as Player;
			player.Damage(damage);
			GetNode<Node2D>("Explosion").Visible = true;
			GetNode<AnimatedSprite2D>("Explosion").Play("explode");
			GetNode<Node2D>("Sprite").Visible = false;
			SetDeferred("freeze", true);
			await ToSignal(GetTree().CreateTimer(1), "timeout");
			GD.Print("BYE");
			QueueFree();
		}
	}
}
