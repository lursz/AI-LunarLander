using System;
using Godot;

namespace MoonLander.entities.rocket.script;

public partial class Rocket : CharacterBody2D
{
	private readonly float _angularSpeed = Mathf.Pi;
	private const float _g = 0.5f;
	Node2D ai_controller;

	[Signal]
	public delegate void CrashSignalEventHandler();

	[Signal]
	public delegate void LandingSignalEventHandler();

	//Called when the node enters the scene tree.
	public override void _Ready()
	{
		// GD.Print(Position);
		ai_controller = GetNode<Node2D>("AIController2D");
		// ai_controller.GetPropertyList();
		// ai_controller.Set("reward", 0.0f);
		GD.Print(ai_controller.Get("reward"));

		Vector2 testvar = (Vector2)ai_controller.Get("move");
		GD.Print(testvar);
	}

	//Called every frame, delta is the elapsed time since the previous frame
	public override void _Process(double delta)
	{
		Vector2 testvar = (Vector2)ai_controller.Get("move");
		
		// HUMAN MOVEMENT
		// Movement(delta);

		// AI MOVEMENT
		MovementAI(delta, testvar);
	}

	public void _Reset()
	{
		Position = new Vector2(144, 102);
		Rotation = 0;
		Velocity = new Vector2(0, 0);
	}

}

