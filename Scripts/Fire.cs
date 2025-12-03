using Godot;
using System;
using static System.Net.Mime.MediaTypeNames;

public partial class Fire : CpuParticles2D
{

	[Export]
	public CollisionPolygon2D collider;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		fireEvents();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public async void fireEvents()
	{
		await ToSignal(GetTree().CreateTimer(GD.RandRange(10, 20)), "timeout");
		Emitting = true;
		collider.Disabled = false;
	}

	public void PlayerHit(Node2D body)
	{
		if (body is Player)
		{
			var player = (Player)body;
			player.Damage(1);
		}
	}
}
