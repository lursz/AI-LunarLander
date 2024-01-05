using System;
using Godot;

namespace MoonLander.entities.rocket.script;

public partial class Rocket : CharacterBody2D
{
	private readonly float _angularSpeed = Mathf.Pi;
	private const float _g = 0.5f;
	Node2D ai_controller;

	//Called when the node enters the scene tree.
	public override void _Ready()
	{
		ai_controller = GetNode<Node2D>("AIController2D");
		ai_controller.GetPropertyList();

		Vector2 testvar = (Vector2)ai_controller.Get("move");
		GD.Print(testvar);
	}

	//Called every frame, delta is the elapsed time since the previous frame
	public override void _Process(double delta)
	{
		// HUMAN MOVEMENT
		Movement(delta);
	}

}

