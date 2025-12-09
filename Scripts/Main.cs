using Godot;
using System;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public int players;
	public override void _Ready()
	{
		players=GameManager.connectedControllers;
		for (int i = 0; i < GameManager.connectedControllers; i++)
		{
			Node player = GetNode<Player>("Player").Duplicate();
			Player newPlayer = player as Player;
			GetNode("Players").AddChild(newPlayer);
			var marker = GetNode<Node2D>("Markers").GetChild(i);
			Marker2D marker2D = marker as Marker2D;
			newPlayer.GlobalPosition = marker2D.GlobalPosition;
			newPlayer.ID = i;
			GetNode<Player>("Player").QueueFree();

		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("Menu"))
		{
			int level = GD.RandRange(1, 2);
			GetTree().ChangeSceneToFile("res://Scenes/Levels/Level" + level + ".tscn");
		}
	}
	
	public async void PlayerDead(){
		players-=1;
		if(players<=1){
			await ToSignal(GetTree().CreateTimer(1), "timeout");
			await ToSignal(GetTree().CreateTimer(2.5f), "timeout");
			int level = GD.RandRange(1, 2);
			GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Levels/Level" + level + ".tscn");
		}
	}
}
