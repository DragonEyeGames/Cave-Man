using Godot;
using System;

public partial class mainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("Play").GrabFocus();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Play()
	{
		GetTree().ChangeSceneToFile("res://Scenes/controllerConnecting.tscn");
	}

	public void Stop()
	{
		GetTree().Quit();
	}
}
