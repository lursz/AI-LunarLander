using System;
using System.Numerics;
using Godot;

namespace MoonLander.entities.rocket.script;

public partial class RocketController : CharacterBody2D
{
    Node2D raycast;
    Variant ray_results;

    private bool humanMovement = true;
    private readonly float _angularSpeed = Mathf.Pi;
    private const float _g = 0.5f;
    Node2D ai_controller;
    float distance = 0.0f;
    Godot.Vector2 goal_pos = new Godot.Vector2(0, 0);

    [Signal]
    public delegate void CrashSignalEventHandler();

    [Signal]
    public delegate void LandingSignalEventHandler();
    [Signal]
    public delegate void StayingAliveSignalEventHandler();
    [Signal]
    public delegate void LandingPadCrashSignalEventHandler();

    public override void _Ready()
    {
        // ai_controller.GetPropertyList();
        // ai_controller.Set("reward", 0.0f);
        raycast = GetNode<Node2D>("RaycastSensor2D");
        raycast.Call("activate");
        if (HasNode("../Sync") || (HasNode("../../Sync")))
        {
            humanMovement = false;
        }
    }


    public override void _Process(double delta)
    {
        ray_results = raycast.Call("get_observation");

        // calculate the position of the landing pad
        if (goal_pos == new Godot.Vector2(0, 0))
        {
            var landingPad = GetNode<TileMap>("../Landing_pad");
            var usedCells = landingPad.GetUsedCells(0);
            Calculate_goal_pos(usedCells);
        }

        distance = (float)Math.Sqrt(Math.Pow(goal_pos.X - this.Position.X, 2) + Math.Pow(goal_pos.Y - this.Position.Y, 2));

        ai_controller = GetNode<Node2D>("AIController2D");

        EmitSignal(SignalName.StayingAliveSignal, Converter.ConvertToDegrees(this.Rotation), Math.Abs(Velocity.Length()), distance);

        Godot.Vector2 move = (Godot.Vector2)ai_controller.Get("move");

        if (humanMovement)
        { Movement(delta); }
        else
        { MovementAI(delta, move); }
        updateSpeedometer();
        // updateAltimeter();
    }

    private void Calculate_goal_pos(Godot.Collections.Array<Vector2I> cells)
    {
        float cellSize = 17.0f;
        float x = cellSize * (cells[0][0] + cells[cells.Count - 1][0]) / 2;
        float y = cellSize * (cells[0][1] + cells[cells.Count - 1][1]) / 2;
        Godot.Vector2 pos = new Godot.Vector2(x, y);
        this.goal_pos = pos;
    }


    public void Reset()
    {
        Position = new Godot.Vector2(926, 168);
        Rotation = 0;
        Velocity = new Godot.Vector2(0, 0);
    }

    public void _on_play_ai_pressed()
    {
        GD.Print("AI"); 
        this.humanMovement = true;
    }
}

