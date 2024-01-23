using Godot;
using System;
namespace MoonLander.ui.script;

public partial class ChooseMap : Control
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}


	public void _on_map_moon_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/level1/level1.tscn");
	}

	public void _on_map_mars_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/level2/level2.tscn");
	}

	public void _on_map_endor_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/level3/level3.tscn");
	}

	public void _on_back_pressed()
	{
		GetTree().ChangeSceneToFile("res://ui/menu.tscn");
	}
}
