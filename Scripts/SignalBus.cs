using Godot;
using System;

public partial class SignalBus : Node
{
	[Signal]
	public delegate void PlayerDeadEventHandler();

	public override void _Ready()
	{
		GameManager.signalBus = this;
	}

	public void PlayerDied()
	{
		EmitSignal(SignalName.PlayerDead);
	}
}
