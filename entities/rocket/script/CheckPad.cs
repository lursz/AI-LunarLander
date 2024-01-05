using System;
using System.Linq;
using Godot;

namespace MoonLander.entities.rocket.script;


public partial class Rocket : CharacterBody2D
{
    public bool CheckPad()
    {
        // Checks whether there's a landing pad above the rocket.
        // Used for collisions and calculating wind.


        // check what block is just under the rocket
        KinematicCollision2D collision = MoveAndCollide(new Vector2(this.Velocity.X, 0.08f), testOnly: true);

        // if no blocks then return true
        if (collision == null)
        {
            return false;
        }

        // check if the block just under rocket is an obstacle, if not then return true
        GodotObject collided = collision.GetCollider();
        if (!collided.HasMeta("obstacle"))
        {
            return false;
        }

        // if the block just under the rocket is a landing pad, if not then return true
        string[] metadataList = (string[])collided.GetMeta("obstacle");
        if (!metadataList.Any(x => x == "landingPad"))
        {
            return false;
        }
        return true;
    }
}