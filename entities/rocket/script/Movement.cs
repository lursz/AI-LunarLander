using System;
using System.Linq;
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
        // So far some shitty method (if CheckCollisions returns 1 then you die,
        // if it returns -1 then you're on the landing pad)
        int x = CheckCollision();
        if (x > 0)
        {
            return 1;
        }
        else if (x < 0)
        {
            // slowly allign the rocket vertically when on landing pad
            if (this.Rotation != 0)
            {
                float halfRotation = this.Rotation / 2;
                this.Rotation = halfRotation < 1.5 ? 0 : halfRotation;
            }

            // get rid of the horizontal speed on the landing pad
            if (Math.Abs(Velocity.X) > 0)
            {
                float newX = Velocity.X / 2;
                Vector2 newVelocity = new Vector2(newX < 0.1 ? 0 : newX, Velocity.Y);
                Velocity = newVelocity;
                GD.Print(Velocity);
            }
        }
        // Check if just collided with a landing pad or standing on one
        bool skipWind = false;
        KinematicCollision2D collision = MoveAndCollide(new Vector2(Velocity.X, 0.1f), testOnly: true);
        if (collision != null)
        {
            GodotObject collided = collision.GetCollider();
            if (collided.HasMeta("obstacle"))
            {
                string[] metadataList = (string[])collided.GetMeta("obstacle");
                if (metadataList.Any(x => x == "landingPad"))
                {
                    skipWind = true;
                }
            }
        }

        if (!skipWind)
        {
            // the wind will have to be randomly generated somewhere
            float wind = 0.002f;
            // shitty wind implementation, ignore wind when already on the landing pad
            // the rocket would drift away otherwise
            Velocity = new Vector2(Velocity.X + wind, Velocity.Y);
        }

        // Move the rocket
        MoveAndSlide();
        return 0;
    }


    public int CheckCollision()
    {
        KinematicCollision2D collision = MoveAndCollide(Velocity, testOnly: true);
        if (collision == null)
        {
            return 0;
        }
        GodotObject collided = collision.GetCollider();
        if (collided.HasMeta("obstacle"))
        {
            // this will call the method from an object if it has one
            // collided.Call("TestMethod", "gowno");
            string[] metadataList = (string[])collided.GetMeta("obstacle");
            if (!metadataList.Any(x => x == "landingPad"))
            {
                return 1;
            }



            if (Math.Abs(Velocity.Length()) < 0.4 && Math.Abs(Converter.ConvertToDegree(Rotation)) < 10)
            {
                GD.Print("You're a stellar pilot!");
                return -1;
            }
            else
            {
                GD.Print("You're a shitty pilot!");
                return 1;
            }
        }

        return 0;

        // return collision != null &&
        //        (Math.Abs(Velocity.Length()) > 0.4 || Math.Abs(Converter.ConvertToDegree(Rotation)) > 15)
        //     ? 1
        //     : 0;
    }

    // public bool IsColliding()
    // {
    //     if (IsOnFloor())
    //     {
    //         GD.Print("Floor");
    //     }

    //     if (IsOnWall())
    //     {
    //         GD.Print("Wall");
    //         // Check what texture is on the wall
    //     }

    //     if (IsOnCeiling())
    //     {
    //         GD.Print("Ceiling");
    //     }

    //     return false;
    // }
}