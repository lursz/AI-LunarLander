using Godot;
using System;

public partial class choose_map : Control
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void _on_back_pressed()
	{
		GetTree().ChangeSceneToFile("res://ui/menu.tscn");
	}
}
