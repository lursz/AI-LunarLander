using System;
using System.Linq;
using Godot;

namespace MoonLander.entities.rocket.script;

public partial class RocketController : CharacterBody2D
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
            case 1:
                return Collision.OBSTACLE;
            case 2:
                return Collision.PAD;
            default:
                return Collision.NONE;
        }
    }

    private void CollisionController(bool isAbovePad)
    {
        // adjust position if there's a landing pad just under the rocket
        if (isAbovePad) AdjustLandingPosition();

        Collision collisionType = GetCollisionType();

        if (collisionType == Collision.NONE) return;

        if (collisionType == Collision.OBSTACLE)
        {
            // GetTree().Quit();
        }
    }

    private void AdjustLandingPosition()
    {
        // don't adjust if it should crash
        if (Math.Abs(this.Velocity.Length()) >= 0.8 || Math.Abs(Converter.ConvertToDegrees(Rotation)) >= 30) return;

        if (this.Rotation != 0)
        {
            float newRotation = this.Rotation * (3/4);
            this.Rotation = newRotation < 1.5 ? 0 : newRotation;
        }

        // get rid of the horizontal speed on the landing pad
        if (this.Velocity.X != 0)
        {
            float newX = this.Velocity.X * (3/4);
            Vector2 newVelocity = new Vector2(Math.Abs(newX) < 0.1 ? 0 : newX, this.Velocity.Y);
            this.Velocity = newVelocity;
        }

        // this.Rotation = 0;
        // this.Velocity = new Vector2(0, 0);
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
        if (!metadataList.Any(x => x == "landingPad")){

            EmitSignal(SignalName.CrashSignal);
            return 1;
        }
        // if collided with landing pad, handle the landing
        if (Math.Abs(this.Velocity.Length()) < 0.8 && Math.Abs(Converter.ConvertToDegrees(Rotation)) < 30)
        {
            EmitSignal(SignalName.LandingSignal);
            return 2;
        }
        else
        {
            EmitSignal(SignalName.LandingPadCrashSignal);
            return 1;
        }

    }
}