using Godot;
using System;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public int players;
	[Export]
	public GpuParticles2D particles;
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
			GetTree().Paused = !GetTree().Paused;
			GetNode<PauseMenu>("Pause").PauseSwap();
		}
	}
	
	public async void PlayerDead(){
		players-=1;
		if(players<=1){
			particles.Emitting=true;
			await ToSignal(GetTree().CreateTimer(5), "timeout");
			int level = GD.RandRange(1, 2);
			GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Levels/Level" + level + ".tscn");
		}
	}
}
