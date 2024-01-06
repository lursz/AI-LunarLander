using System;
using Godot;

namespace MoonLander.entities.rocket.script;

public partial class RocketController : CharacterBody2D
{
	private readonly float _angularSpeed = Mathf.Pi;
	private const float _g = 0.5f;
	Node2D ai_controller;
	float distance = 0.0f;

	[Signal]
	public delegate void CrashSignalEventHandler();

	[Signal]
	public delegate void LandingSignalEventHandler();
	[Signal]
	public delegate void StayingAliveSignalEventHandler();

	public override void _Ready()
	{
		// GD.Print(Position);
		// ai_controller.GetPropertyList();
		// ai_controller.Set("reward", 0.0f);
		// GD.Print(ai_controller.Get("reward"));

		// Vector2 testvar = (Vector2)ai_controller.Get("move");
		// GD.Print(testvar);
	}

	
	public override void _Process(double delta)
	{
		// if (Position.X == 0.0f && Position.Y == 0.0f)
		// {
		// 	return;
		// }
		var landingPad = GetNode<TileMap>("/root/Node/Landing_pad");
		var usedCells = landingPad.GetUsedCells(0);

		this.distance = Calculate_distance(usedCells);

		ai_controller = GetNode<Node2D>("AIController2D");

		EmitSignal(SignalName.StayingAliveSignal, Converter.ConvertToDegrees(this.Rotation), Math.Abs(Velocity.Length()), distance);	

		Vector2 move = (Vector2)ai_controller.Get("move");
		// GD.Print(testvar);
		// GD.Print(reward);
		// HUMAN MOVEMENT
		// Movement(delta);

		// AI MOVEMENT
		MovementAI(delta, move);
	}

	private float Calculate_distance(Godot.Collections.Array<Vector2I> cells)
	{	
		float cellSize = 17.0f;
		float x = cellSize * (cells[0][0] + cells[cells.Count - 1][0]) / 2;
		float y = cellSize * (cells[0][1] + cells[cells.Count - 1][1]) / 2;

		float distance = (float)Math.Sqrt(Math.Pow(x - this.Position.X, 2) + Math.Pow(y - this.Position.Y, 2));
		// if (distance > 730.0){
		// 	GD.Print(cells, Position);
		// }
		return distance;
	}


	public void Reset()
	{
		Position = new Vector2(144, 102);
		Rotation = 0;
		Velocity = new Vector2(0, 0);
	}
}

