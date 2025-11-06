using Godot;
using System;

public partial class Rock : RigidBody2D
{
	public int playerID = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void BodyEntered(Node2D body)
	{
		if(body is Player)
		{
			var player = (Player)body;
			if(player.ID != playerID)
			{
				player.QueueFree();
			}
		}
	}
}
