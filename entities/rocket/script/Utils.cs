using System;
using Godot;

namespace MoonLander.entities.rocket.script;

public partial class RocketController : CharacterBody2D
{
    private Vector2 CalculateNewVelocity(
        Vector2 velocity,
        int angularDirection,
        int acceleration,
        double delta)
    {
        Vector2 newVelocity = velocity + (Vector2.Up.Rotated(this.Rotation) * acceleration + new Vector2(0, _g)) * (float)delta;
        this.Rotation += _angularSpeed * angularDirection * (float)delta;
        this.Position += velocity;
        this.Velocity = velocity;
        return newVelocity;
    }

    private void RocketLogic()
    {
        bool isAbovePad = CheckPad();
        CollisionController(isAbovePad);
        if (!isAbovePad)
        {
            // the wind will have to be randomly generated somewhere
            // float wind = 0.002f;
            // shitty wind implementation, ignore wind when already on the landing pad
            // the rocket would drift away otherwise
            // this.Velocity = new Vector2(this.Velocity.X + wind, this.Velocity.Y);
        }

        // Move the rocket
        MoveAndSlide();
    }
}