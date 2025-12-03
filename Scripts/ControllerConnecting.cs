using Godot;
using System;
using System.Collections.Generic;

public partial class ControllerConnecting : Control
{

	private List<int> connectedControllers = new List<int>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		foreach (Control child in GetNode<Control>("Container").GetChildren())
		{
			child.Visible = false;
		}
		GetNode<Control>("Container").GetChild<Control>(0).Visible = Input.GetConnectedJoypads().Count>=1;
		GetNode<Control>("Container").GetChild<Control>(1).Visible = Input.GetConnectedJoypads().Count >= 2;
		GetNode<Control>("Container").GetChild<Control>(2).Visible = Input.GetConnectedJoypads().Count >= 3;
		GetNode<Control>("Container").GetChild<Control>(3).Visible = Input.GetConnectedJoypads().Count >= 4;

		if (Input.IsActionJustPressed("A"))
		{
			GameManager.connectedControllers = Input.GetConnectedJoypads().Count;
			int level = GD.RandRange(1, 2);
			GetTree().ChangeSceneToFile("res://Scenes/Levels/Level" + level + ".tscn");
		}
	}

	public override void _Input(InputEvent @event)
	{
		GD.Print(@event.Device);
		if (!connectedControllers.Contains(@event.Device))
		{
			connectedControllers.Add(@event.Device);

		}
	}

}
