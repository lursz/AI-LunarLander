using System;
using System.Linq;
using Godot;

namespace MoonLander.entities.rocket.script;


public partial class RocketController : CharacterBody2D
{
    public void MovementAI(double delta, Vector2 move)
    {
        Vector2 velocity = this.Velocity;

        // Calculate movement
        var angularDirection = move.X < -0.66f ? -1 :
                move.X > 0.66f ? 1 : 0;
        var acceleration = move.Y < 0f ? 1 : 0;

        Velocity = CalculateNewVelocity(velocity, angularDirection, acceleration, delta);

        RocketLogic();
    }

}