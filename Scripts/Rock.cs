using Godot;
using System;

public partial class Rock : Projectile
{
	public int playerID = 0;
	private double spawnedInTime = 0.0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		spawnedInTime+=delta;
	}

	public void BodyEntered(Node2D body)
	{
		if(body is Player)
		{
			var player = (Player)body;
			if(player.ID != playerID || spawnedInTime>=0.7f)
			{
				GD.Print(Mathf.Round(LinearVelocity.Length() / 50));
				player.Damage((int)Mathf.Round(LinearVelocity.Length()/50));
				QueueFree();

			}
		}
	}
}
