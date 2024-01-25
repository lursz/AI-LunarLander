using Godot;

namespace MoonLander.ui.script;

public partial class Menu : Control
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void _on_play_ai_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/main/mainAI.tscn");
	}

	public void _on_play_human_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/main/main.tscn");
	}
	public void _on_choose_map_pressed()
	{
		GetTree().ChangeSceneToFile("res://ui/choose_map.tscn");
	}


	public void _on_quit_pressed()
	{
		GetTree().Quit();
	}


}