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

	public void PlayerHit(Node2D body)
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
			explode();
			var player = body as Player;
			player.Damage(damage);
			Vector2 newExplosion = (player.GlobalPosition - GlobalPosition) / 50;
			if(Mathf.Abs(newExplosion.X) > 1)
			{
				if (newExplosion.X < 0)
				{
					newExplosion.X = -1;
				} else
				{
					newExplosion.X = 1;
				}
			}
			if (Mathf.Abs(newExplosion.Y) > 1)
			{
				if (newExplosion.Y < 0)
				{
					newExplosion.Y = -1;
				}
				else
				{
					newExplosion.Y = 1;
				}
			}
			newExplosion /= 4;
			player.explodeVelocity = newExplosion*2;
		}
	}
	
	public async void explode(){
		if(!GetNode<Node2D>("Explosion").Visible){
			GetNode<Node2D>("Explosion").Visible = true;
			GetNode<AnimatedSprite2D>("Explosion").Play("explode");
			GetNode<Node2D>("Sprite").Visible = false;
			SetDeferred("freeze", true);
			GetNode<AnimationPlayer>("AnimationPlayer").Play("explode");
			await ToSignal(GetTree().CreateTimer(1), "timeout");
			QueueFree();
		}
	}
}
