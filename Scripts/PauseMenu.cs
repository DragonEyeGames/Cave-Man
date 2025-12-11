using Godot;
using System;

public partial class PauseMenu : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public override void _Process(double delta)
	{
		if(!(GetNode<Button>("VBoxContainer/Restart/Restart").HasFocus() || GetNode<Button>("VBoxContainer/Connect/Continue").HasFocus() || GetNode<Button>("VBoxContainer/Menu/Menu").HasFocus()) && GetTree().Paused)
		{
			GetNode<Button>("VBoxContainer/Restart/Restart").GrabFocus();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public void PauseSwap()
	{
		GetNode<Button>("VBoxContainer/Restart/Restart").GrabFocus();
		if (!GetNode<Button>("VBoxContainer/Restart/Restart").HasFocus())
		{
			GD.Print("Selling");
			GetNode<Button>("VBoxContainer/Restart/Restart").GrabFocus();
		}
		if (GetTree().Paused)
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("reveal");
		} else
		{
			GetNode<AnimationPlayer>("AnimationPlayer").Play("hide");
		}
	}
	
	public void Restart(){
		if(GetTree().Paused)
		{
			GetTree().Paused = false;
			int level = GD.RandRange(1, 2);
			GetTree().CallDeferred("change_scene_to_file", "res://Scenes/Levels/Level" + level + ".tscn");
		}
	}
	
	public void Menu(){
		if (GetTree().Paused)
		{
			GetTree().Paused = false;
			GetTree().CallDeferred("change_scene_to_file", "res://Scenes/mainMenu.tscn");
		}
	}
	
	public void Continue(){
		if (GetTree().Paused)
		{
			GetNode<Button>("VBoxContainer/Restart/Restart").GrabFocus();
			if (GetNode<Button>("VBoxContainer/Restart/Restart").HasFocus())
			{
				GD.Print("FOCUSED");
				GetNode<Button>("VBoxContainer/Restart/Restart").ReleaseFocus();
			}
			GetTree().Paused = false;
			GetNode<AnimationPlayer>("AnimationPlayer").Play("hide");
		}
	}
	
	public void Focus(){
		GD.Print("Hey");
	}
	
}
