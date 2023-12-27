using Godot;

namespace MoonLander.entities.rocket.script;

public partial class Rocket : CharacterBody2D
{
	private readonly float _angularSpeed = Mathf.Pi;
	private const float _g = 0.5f;

	/* --------------- Called when the node enters the scene tree. -------------- */
	public override void _Ready()
	{

	}

	/* - Called every frame, delta is the elapsed time since the previous frame - */
	public override void _Process(double delta)
	{

		// Assuming you have a collision shape or area named "RocketCollision" and a TileMap named "Tilemap"

		// IsColliding();
		if (Movement(delta) != 0)
		{
			GD.Print("Fin!");
			GetTree().Quit();
		}
	}

	public void Die()
	{
		GD.Print("I DIED!");
	}

}
