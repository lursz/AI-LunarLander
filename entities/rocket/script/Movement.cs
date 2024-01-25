using System;
using System.Linq;
using Godot;

namespace MoonLander.entities.rocket.script;


public partial class RocketController : CharacterBody2D
{
    public void Movement(double delta)
    {
        Vector2 velocity = this.Velocity;

        // Calculate movement
        var angularDirection = Input.IsActionPressed("left") ? -0.6 :
            Input.IsActionPressed("right") ? 0.6 : 0;
        var acceleration = Input.IsActionPressed("forward") ? 1 : 0;

        Velocity = CalculateNewVelocity(velocity, (float)angularDirection, acceleration, delta);

        RocketLogic();
    }



}