using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public int ID = 0;
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	private float rockVelocity = 0.0f;
	private bool toggled = false;
	private bool throwing = false;
	private bool jumping = false;

	[Export] public PackedScene rock;

	public override void _Ready()
	{
		Modulate = GetRandomColor();
		GetNode<RichTextLabel>("RichTextLabel").Text = "Player " + ID+1;
	}

	public override void _PhysicsProcess(double delta)
	{
		GetNode<RichTextLabel>("RichTextLabel").Text = "Player " + (ID + 1);
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (jumping) {
			jumping = false;
			velocity.Y = JumpVelocity;
		}
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float leftX = Input.GetJoyAxis(ID, JoyAxis.LeftX);
		if (MathF.Abs(leftX) < .2f) {
			leftX = 0.0f;
		}
		float leftY = Input.GetJoyAxis(ID, JoyAxis.LeftY);
		if (MathF.Abs(leftY) < .2f)
		{
			leftY = 0.0f;
		}
		Vector2 direction = new Vector2(leftX, leftY);
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

		if (throwing)
		{
			GetNode<Node2D>("Arrow").Visible = true;
			GetNode<Node2D>("Arrow").Scale = new Vector2(rockVelocity / 250 * (this.GlobalPosition.DistanceTo(GetNode<Control>("ColorRect").GlobalPosition) / 200), GetNode<Node2D>("Arrow").Scale.Y);
			GD.Print(GetNode<Node2D>("Arrow").Scale);
			GD.Print((rockVelocity / 250) * (this.GlobalPosition.DistanceTo(GetNode<Node2D>("Arrow").GlobalPosition) / 200));
			float rightX = Input.GetJoyAxis(ID, JoyAxis.RightX);
			if (MathF.Abs(rightX) < .2f)
			{
				rightX = 0.0f;
			}
			float rightY = Input.GetJoyAxis(ID, JoyAxis.RightY);
			if (MathF.Abs(rightY) < .2f)
			{
				rightY = 0.0f;
			}
			Vector2 rightDirection = new Vector2(rightX, rightY);
			GetNode<ColorRect>("ColorRect").Position = rightDirection * 200;
			if (toggled)
			{
				GetNode<ColorRect>("ColorRect").Position = new Vector2(-GetNode<ColorRect>("ColorRect").Position.X, -GetNode<ColorRect>("ColorRect").Position.Y);

			}
			GetNode<Node2D>("Arrow").LookAt(GetNode<ColorRect>("ColorRect").GlobalPosition);
			rockVelocity += (float)delta * 6;

			if (rockVelocity > 10)
			{
				rockVelocity = 10;
			}

		}
		shakePlayer();
	}

	public void shakePlayer()
	{
		Node2D icon = GetNode<Node2D>("Icon");
		icon.Position = new Vector2((float)GD.RandRange(-rockVelocity, rockVelocity), (float)GD.RandRange(-rockVelocity, rockVelocity));
	}
	
	private void throwObject()
	{
		Node2D arrow = GetNode<Node2D>("Arrow");
		arrow.Scale = new Vector2(arrow.Scale.X, .03f);

		arrow.Visible = false;


		Rock newRock = rock.Instantiate() as Rock;


		GetParent().AddChild(newRock);

		newRock.playerID = ID;

		newRock.GlobalPosition = GlobalPosition;

		Vector2 dir = (GetNode<Control>("ColorRect").GlobalPosition - GlobalPosition).Normalized();
		newRock.LinearVelocity = -(dir * rockVelocity * 200);


		rockVelocity = 0.0f;
	}

	public override void _Input(InputEvent @event)
	{
		GD.Print(@event.Device);
		if(@event.Device==ID)
		{
			if (@event.IsActionPressed("ToggleDirection"))
			{
				toggled = !toggled;
			}

			if (@event.IsActionPressed("Throw"))
			{
				throwing = true;

			}

			if(@event.IsActionReleased("Throw") && throwing==true)
			{
				throwing = false;
				throwObject();
			}

			if (@event.IsActionPressed("Jump") && IsOnFloor() && jumping==false)
			{
				jumping = true;
			}
		}
	}

	public Color GetRandomColor()
	{
		// Generate random float values between 0.0 and 1.0 for R, G, B, and A
		float r = GD.Randf();
		float g = GD.Randf();
		float b = GD.Randf();
		float a = 1.0f; // You can also randomize alpha if needed: GD.Randf()

		return new Color(r, g, b, a);
	}
}
