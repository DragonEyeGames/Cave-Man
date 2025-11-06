using Godot;
using System;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print(GameManager.connectedControllers);
		for (int i = 0; i < GameManager.connectedControllers; i++)
		{
			Node player = GetNode<Player>("Player").Duplicate();
			Player newPlayer = player as Player;
			AddChild(newPlayer);
			var marker = GetNode<Node2D>("Markers").GetChild(i);
			Marker2D marker2D = marker as Marker2D;
			newPlayer.GlobalPosition = marker2D.GlobalPosition;
			GD.Print(i);
			newPlayer.ID = i;
			GetNode<Player>("Player").QueueFree();

		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
