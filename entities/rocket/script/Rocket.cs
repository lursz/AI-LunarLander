using System;
using Godot;

namespace MoonLander.entities.rocket.script;

public partial class Rocket : CharacterBody2D
{
	private readonly float _angularSpeed = Mathf.Pi;
	private const float _g = 0.5f;
	GodotObject ai_controller;

	/* --------------- Called when the node enters the scene tree. -------------- */
	public override void _Ready()
	{
		// Node2D aiController = GD.Load<Node2D>("res://entities/rocket/script/AIController2D.gd");
		Node2D ai_controller = GetNode<Node2D>("AIController2D");
		// GDScript MyGDScript = GD.Load<GDScript>("res://entities/rocket/script/AIController2D.gd");
		// ai_controller = (GodotObject)MyGDScript.New();
		GD.Print(ai_controller);
		GD.Print(ai_controller.Get("move.x"));
		
		Type type = ai_controller.GetType();

        // Print properties
        GD.Print("Properties:");
        foreach (var property in type.GetProperties())
        {
            GD.Print(property.Name);
        }

        // Print methods
        GD.Print("\nMethods:");
        foreach (var method in type.GetMethods())
        {
            GD.Print(method.Name);
        }
	}

	/* - Called every frame, delta is the elapsed time since the previous frame - */
	public override void _Process(double delta)
	{
		var test_velocity = ai_controller.Get("move.x");
		GD.Print(test_velocity);
		// var velocity_y = ai_controller.Get("move.y");

		// GD.Print("Velocity: ", velocity_x);

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
