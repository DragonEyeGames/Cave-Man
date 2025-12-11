using Godot;
using Godot.Collections;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public int ID = 0;
	public const float Speed = 300.0f;
	public const float JumpVelocity = -180.0f;
	private float rockVelocity = 0.0f;
	private bool toggled = false;
	private bool throwing = false;
	private bool jumping = false;
	private float health = 100.0f;
	private double jumpTime = 0.0;
	private bool dropVelocity = false;
	private AnimationPlayer animator;
	private bool dead=false;
	[Export] public Vector2 explodeVelocity = Vector2.Zero;

	[Export] public PackedScene rock;
	[Export] public PackedScene bomb;
	[Export] public PackedScene boulder;
	[Export] public PackedScene fish;

	public override void _Ready()
	{
		GetNode<Sprite2D>("Icon/Head/Hat").Texture=GD.Load<Texture2D>(GetRandomFile("res://Art/CharacterCreation/Hat/"));
		GetNode<Sprite2D>("Icon/Head/Face").Texture=GD.Load<Texture2D>(GetRandomFile("res://Art/CharacterCreation/Face/"));
		GetNode<Sprite2D>("Icon/Head/Accessory").Texture=GD.Load<Texture2D>(GetRandomFile("res://Art/CharacterCreation/Accessory/"));
		animator=GetNode<AnimationPlayer>("Icon/Controller");
		//Modulate = GetRandomColor();
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
		
		if(dead){
			velocity.Y=100;
			MoveAndSlide();
			return;
		}

		// Handle Jump.
		if (jumping) {
			velocity.Y += JumpVelocity;
			jumpTime += delta;
			if (jumpTime > 0.15)
			{
				jumpTime = 0.0;
				velocity.Y /= 2;
				jumping = false;
			}
		}
		if(dropVelocity)
		{
			velocity.Y /= 2;
			dropVelocity = false;
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
		else if (explodeVelocity==Vector2.Zero)
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		if (Mathf.Abs(velocity.X) > .5 && animator.CurrentAnimation!="walk")
		{
			animator.Play("walk");
		} else if(Mathf.Abs(velocity.X) < .5 && animator.CurrentAnimation != "idle")
		{
			animator.Play("idle");
		}

		if(velocity.X<0 && GetNode<Node2D>("Icon").Scale.X < 0)
		{
			GetNode<AnimationPlayer>("Flipper").Play("left");
		}

		if (velocity.X > 0 && GetNode<Node2D>("Icon").Scale.X > 0)
		{
			GetNode<AnimationPlayer>("Flipper").Play("right");
		}

		Velocity = velocity;
		if(explodeVelocity!=Vector2.Zero)
		{
			Velocity += explodeVelocity*Speed;
			explodeVelocity *= .9f;
			if (explodeVelocity.X < 0.1)
			{
				explodeVelocity.X = 0;
			}
			if (explodeVelocity.Y < 0.1)
			{
				explodeVelocity.Y = 0;
			}
		}
		MoveAndSlide();

		if (throwing)
		{
			GetNode<Node2D>("Arrow").Visible = true;
			GetNode<Node2D>("Arrow").Scale = new Vector2(rockVelocity / 250 * (this.GlobalPosition.DistanceTo(GetNode<Control>("ColorRect").GlobalPosition) / 200), GetNode<Node2D>("Arrow").Scale.Y);
			float rightX = Input.GetJoyAxis(ID, JoyAxis.RightX);
			float rightY = Input.GetJoyAxis(ID, JoyAxis.RightY);
			Vector2 rightDirection = new Vector2(rightX, rightY);
			GetNode<ColorRect>("ColorRect").Position = rightDirection * 200;
			if (toggled)
			{
				GetNode<ColorRect>("ColorRect").Position = new Vector2(-GetNode<ColorRect>("ColorRect").Position.X, -GetNode<ColorRect>("ColorRect").Position.Y);

			}
			GetNode<Sprite2D>("Icon/Arm").Visible=false;
			GetNode<Sprite2D>("Icon/DummyArm").Visible=true;
			GetNode<Sprite2D>("Icon/DummyArm").LookAt(GetNode<ColorRect>("ColorRect").GlobalPosition);
			GetNode<Sprite2D>("Icon/DummyArm").RotationDegrees-=90;
			GetNode<Node2D>("Arrow").LookAt(GetNode<ColorRect>("ColorRect").GlobalPosition);
			rockVelocity += (float)delta * 6;

			if (rockVelocity > 10)
			{
				rockVelocity = 10;
			}

		} else {
			GetNode<Sprite2D>("Icon/Arm").Visible=true;
			GetNode<Sprite2D>("Icon/DummyArm").Visible=false;
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
		if(dead){
			return;
		}
		GetNode<AudioStreamPlayer2D>("Whoosh").Play();
		Node2D arrow = GetNode<Node2D>("Arrow");
		arrow.Scale = new Vector2(arrow.Scale.X, .03f);

		arrow.Visible = false;

		Projectile newProjectile = null;
		int random = GD.RandRange(1, 5);
		if(random == 5)
		{
			newProjectile = bomb.Instantiate() as Projectile;
		} else if(random == 4)
		{
			newProjectile = boulder.Instantiate() as Projectile;
		} else if(random == 3)
		{
			newProjectile = fish.Instantiate() as Projectile;
		} else
		{
			newProjectile = rock.Instantiate() as Projectile;
		}


		GetParent().GetParent().AddChild(newProjectile);

		newProjectile.playerID = ID;

		newProjectile.GlobalPosition = GlobalPosition;

		Vector2 dir = (GetNode<Control>("ColorRect").GlobalPosition - GlobalPosition).Normalized();
		newProjectile.LinearVelocity = -(dir * rockVelocity * 200);


		rockVelocity = 0.0f;
	}

	public override void _Input(InputEvent @event)
	{
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
			if (@event.IsActionReleased("Jump") && jumping)
			{
				jumping = false;
				dropVelocity = true;
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

	public void Damage(float damage)
	{
		if(dead){
			return;
		}
		health-=damage;
		if(health<=0)
		{
			GetNode<AudioStreamPlayer2D>("Death").Play();
			GetNode<ProgressBar>("Health").Value = 0;
			GameManager.signalBus.PlayerDied();
			dead=true;
			animator.Play("death");
			SetProcess(false);
			SetPhysicsProcess(false);
		} else
		{
			GetNode<ProgressBar>("Health").Value = health;
		}

	}
	
	private string GetRandomFile(string folderPath)
{
	DirAccess dir = DirAccess.Open(folderPath);
	if (dir == null)
	{
		GD.PushError($"Could not open: {folderPath}");
		return null;
	}

	dir.ListDirBegin();
	Array<string> files = new Array<string>();

	string fileName = dir.GetNext();
	while (fileName != "")
	{
		if (!dir.CurrentIsDir() && fileName.EndsWith(".png")) // skip folders
			files.Add(folderPath + fileName);

		fileName = dir.GetNext();
	}

	dir.ListDirEnd();

	if (files.Count == 0)
		return null;

	// Pick random
	Random rand = new Random();
	int index = rand.Next(files.Count);

	return files[index];
}
}
