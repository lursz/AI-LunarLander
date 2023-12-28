using System;
using System.Linq;
using Godot;

namespace MoonLander.entities.rocket.script;


public partial class Rocket : CharacterBody2D
{
    public bool CheckWind()
    {
        // Checks whether the wind should affect the rocket.
        // It should only not affect the rocket when it's standing on a landing pad or is slightly above it


        // check what block is just under the rocket
        KinematicCollision2D collision = MoveAndCollide(new Vector2(this.Velocity.X, 0.1f), testOnly: true);

        // if no blocks then return true
        if (collision == null)
        {
            return true;
        }

        // check if the block just under rocket is an obstacle, if not then return true
        GodotObject collided = collision.GetCollider();
        if (!collided.HasMeta("obstacle"))
        {
            return true;
        }

        // if the block just under the rocket is a landing pad, if not then return true
        string[] metadataList = (string[])collided.GetMeta("obstacle");
        if (!metadataList.Any(x => x == "landingPad"))
        {
            return true;
        }
        return false;
    }
}