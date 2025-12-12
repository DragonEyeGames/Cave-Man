using Godot;
using System;

public partial class PlayerOrganizer : Control
{
	[Export]
	public int playerID = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Label>("Player1").Text="Player " + (playerID+1);
		GD.Print("A" + (GameManager.connectedControllers));
		GD.Print("B" + playerID+1);
		if((GameManager.connectedControllers)<playerID+1){
			Visible=false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GetNode<ProgressBar>("Health").Value=GameManager.playerHealths[playerID];
	}
}
