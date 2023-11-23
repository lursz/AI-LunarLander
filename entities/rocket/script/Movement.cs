using System;
using Godot;

namespace MoonLander.entities.rocket.script;

public partial class Rocket : CharacterBody2D
{
    public int Movement(double delta)
    {
        Vector2 velocity = Velocity;

        // Calculate movement
        var angularDirection = Input.IsActionPressed("left") ? -1 :
            Input.IsActionPressed("right") ? 1 : 0;
        var acceleration = Input.IsActionPressed("forward") ? 1 : 0;

        // Calculate new velocity
        velocity += (Vector2.Up.Rotated(this.Rotation) * acceleration + new Vector2(0, _g)) * (float)delta;
        this.Rotation += _angularSpeed * angularDirection * (float)delta;
        this.Position += velocity;
        Velocity = velocity;

        // Check collisions with walls
        if (CheckCollision() > 0)
        {
            return 1;
        }

        // Move the rocket
        MoveAndSlide();
        return 0;
    }

    public int CheckCollision()
    {
        KinematicCollision2D collision = MoveAndCollide(Velocity, testOnly: true);
        GD.Print(collision);
        return collision != null &&
               (Math.Abs(Velocity.Length()) > 0.4 || Math.Abs(Converter.ConvertToDegree(Rotation)) > 15)
            ? 1
            : 0;
    }

    public bool IsColliding()
    {
        if (IsOnFloor())
        {
            GD.Print("Floor");
        }

        if (IsOnWall())
        {
            GD.Print("Wall");
            // Check what texture is on the wall
        }

        if (IsOnCeiling())
        {
            GD.Print("Ceiling");
        }

        return false;
    }
}