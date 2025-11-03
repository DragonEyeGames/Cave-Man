using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	private float rockVelocity = 0.0f;

	[Export] public PackedScene rock;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();

		if (Input.IsActionPressed("Throw"))
		{
			GetNode<Node2D>("Arrow").Visible = true;
			GetNode<Node2D>("Arrow").LookAt(GetNode<ColorRect>("ColorRect").GlobalPosition);
			GetNode<ColorRect>("ColorRect").Position = Input.GetVector("Left", "Right", "Up", "Down")*200;
			rockVelocity += (float)delta * 6;

			if (rockVelocity > 10)
			{
				rockVelocity = 10;
			}

		}
		else if (Input.IsActionJustReleased("Throw"))
		{
			Node2D arrow = GetNode<Node2D>("Arrow");
			arrow.Scale = new Vector2(arrow.Scale.X, .03f);

			arrow.Visible = false;


			RigidBody2D newRock = rock.Instantiate() as RigidBody2D;


			AddChild(newRock);


			newRock.Position = new Vector2(0, 0);


			Vector2 dir = (GetNode<Control>("ColorRect").GlobalPosition - GlobalPosition).Normalized();
			newRock.LinearVelocity = -(dir * rockVelocity * 200);


			rockVelocity = 0.0f;

		}
		shakePlayer();
	}

	public void shakePlayer()
	{
		Node2D icon = GetNode<Node2D>("Icon");
		icon.Position = new Vector2((float)GD.RandRange(-rockVelocity, rockVelocity), (float)GD.RandRange(-rockVelocity, rockVelocity));
	}
}
