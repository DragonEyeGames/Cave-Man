using Godot;
using System;

public partial class Camera : Camera2D
{
	[Export]
	int margin = 5;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Node first = GetNode<Node2D>("../Players").GetChild(0);
		Node2D firstNode = first as Node2D;

		float minX = firstNode.Position.X;
		float maxX = firstNode.Position.X;
		float x = 0.0f;
		foreach(Node2D child in GetNode("../Players").GetChildren()) {
			Node2D newChild = child as Node2D;
			if(newChild.Position.X<minX)
			{
				minX = newChild.Position.X;
			}
			if(newChild.Position.X>maxX)
			{
				maxX = newChild.Position.X;
			}
		}
		x = (minX + maxX);
		x /= 2;

		float minY = firstNode.Position.Y;
		float maxY = firstNode.Position.Y;
		float y = 0.0f;
		foreach (Node2D child in GetNode("../Players").GetChildren())
		{
			Node2D newChild = child as Node2D;
			if (newChild.Position.Y < minY)
			{
				minY = newChild.Position.Y;
			}
			if (newChild.Position.Y > maxY)
			{
				maxY = newChild.Position.Y;
			}
		}
		y = (minY + maxY);
		y /= 2;

		GlobalPosition = new Vector2(x, y);

		Vector2 viewportSize = GetViewportRect().Size;
		float distance = Mathf.Abs(maxX - minX);
		float zoomX = (distance + margin) / viewportSize.X;
		float distanceY = Mathf.Abs(maxY - minY);
		float zoomY = (distanceY + margin) / viewportSize.Y;
		float biggerZoom = 0.0f;
		if (zoomX > zoomY)
		{
			biggerZoom = zoomX;
		}
		else
		{
			biggerZoom = zoomY;
		}
		Zoom = new Vector2(1 / biggerZoom, 1/biggerZoom);
	}
}
