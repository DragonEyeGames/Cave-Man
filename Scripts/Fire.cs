using Godot;
using Godot.Collections;
using System;
using static System.Net.Mime.MediaTypeNames;

public partial class Fire : CpuParticles2D
{

	[Export]
	public CollisionPolygon2D collider;

    public Array<Player> playersInArea = new Array<Player>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		fireEvents();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Emitting && playersInArea.Count > 0)
		{
			foreach(Player player in playersInArea)
			{
                player.Damage(30*(float)delta);
				player.explodeVelocity = new Vector2(0, -0.1f);
            }
        }
    }

	public async void fireEvents()
	{
		await ToSignal(GetTree().CreateTimer(GD.RandRange(1, 2)), "timeout");
		Emitting = true;
		collider.Disabled = false;
        await ToSignal(GetTree().CreateTimer(GD.RandRange(1, 2)), "timeout");
        Emitting = false;
        collider.Disabled = true;
		fireEvents();
    }

	public void PlayerHit(Node2D body)
	{
		if (body is Player)
		{
			playersInArea.Add(body as Player);
        }
	}

    public void PlayerLeft(Node2D body)
    {
        if (body is Player && playersInArea.Contains((Player)body))
        {
            playersInArea.Remove(body as Player);
        }
    }
}
