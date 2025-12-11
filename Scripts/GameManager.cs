using Godot;
using Godot.Collections;
using System;

public partial class GameManager : Node
{
	public static int connectedControllers;
	public static SignalBus signalBus;
	
	public static Array<float> playerHealths = new Array<float> { 100f, 100f, 100f, 100f };
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
