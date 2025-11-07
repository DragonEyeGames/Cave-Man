using Godot;
using System;

public partial class Camera : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float x = 0.0f;
		foreach(Marker2D child in GetNode("../Markers").GetChildren()) {
			Node2D newChild = child as Node2D;
			x += newChild.GlobalPosition.X;
		}
		x/=GetNode("../Markers").GetChildren().Count;
		
		float y = 0.0f;
		foreach(Marker2D child in GetNode("../Markers").GetChildren()) {
			Node2D newChild = child as Node2D;
			y += newChild.GlobalPosition.Y;
		}
		y/=GetNode("../Markers").GetChildren().Count;
		GlobalPosition = new Vector2(x, y);
		
		var biggestX = 0.0f;
		foreach(Marker2D child in GetNode("../Markers").GetChildren()) {
			Node2D newChild = child as Node2D;
			if(MathF.Abs(newChild.GlobalPosition.X)>MathF.Abs(biggestX)) {
				biggestX=newChild.GlobalPosition.X;
			}
		}
		GD.Print("AHEAHE" + (biggestX-GlobalPosition.X));
		if(MathF.Abs(biggestX-GlobalPosition.X) > GetViewport().GetVisibleRect().Size.X/2) {
			Zoom = new Vector2(Zoom.X - .1f, Zoom.Y - .1f);
		}
		GD.Print(GetViewport().GetVisibleRect().Size.X + " AAH " + MathF.Abs(biggestX-GlobalPosition.X));
		x/=GetNode("../Markers").GetChildren().Count;
	}
}
