using System;
using System.Linq;
using Godot;

namespace MoonLander.entities.rocket.script;

public partial class Rocket : CharacterBody2D
{
    enum Collision
    {
        PAD,
        NONE,
        OBSTACLE
    }

    private Collision GetCollisionType()
    {
        switch (CheckCollision())
        {
            case -1:
                return Collision.OBSTACLE;
            case 1:
                return Collision.PAD;
            default:
                return Collision.NONE;
        }
    }

    private void CollisionController()
    {
        Collision collisionType = GetCollisionType();

        if (collisionType == Collision.NONE) return;

        if (collisionType == Collision.OBSTACLE)
        {
            GD.Print("Fin!");
            GetTree().Quit();
        }

        AdjustLandingPosition();
    }

    private void AdjustLandingPosition()
    {
        // if (this.Rotation != 0)
        // {
        //     float halfRotation = this.Rotation / 2;
        //     this.Rotation = halfRotation < 1.5 ? 0 : halfRotation;
        // }

        // // get rid of the horizontal speed on the landing pad
        // if (this.Velocity.X != 0)
        // {
        //     float newX = this.Velocity.X / 2;
        //     Vector2 newVelocity = new Vector2(newX < 0.05 ? 0 : newX, this.Velocity.Y);
        //     this.Velocity = newVelocity;
        // }

        this.Rotation = 0;
        this.Velocity = new Vector2(0, 0);
    }



    public int CheckCollision()
    {
        KinematicCollision2D collision = MoveAndCollide(this.Velocity, testOnly: true);
        if (collision == null) return 0;

        GodotObject collided = collision.GetCollider();
        // check what did the rocket collide with, if obstacle then handle it
        if (!collided.HasMeta("obstacle")) return 0;
        string[] metadataList = (string[])collided.GetMeta("obstacle");

        // check if collided with landing pad, if no then crash the rocket
        if (!metadataList.Any(x => x == "landingPad")) return 1;


        // if collided with landing pad, handle the landing
        if (Math.Abs(this.Velocity.Length()) < 0.4 && Math.Abs(Converter.ConvertToDegree(Rotation)) < 10)
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
}