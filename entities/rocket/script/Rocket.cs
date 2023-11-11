using Godot;
using System;

public partial class Rocket : CharacterBody2D
{

	private float _angularSpeed = Mathf.Pi;
	// private Vector2 _velocity = Vector2.Zero;
	private float _speedCap = 150;
	private float _g = 0.5f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		IsColliding();
		if (Movement(delta) != 0)
		{
			GD.Print("Fin!");
			GetTree().Quit();
		}
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

	public int CheckCollision()
	{
		KinematicCollision2D collision = MoveAndCollide(Velocity, testOnly: true);

		if (collision != null && (Math.Abs(Velocity.Length()) > 0.4 || Math.Abs(convertToDegree(Rotation)) > 15))
		{
			return 1;
		}
		return 0;
	}

	public override void _PhysicsProcess(double delta)
	{
		// IsColliding();
	}

	public int Movement(double delta)
	{

		Vector2 velocity = Velocity;
		/* --------------------------- Calculate movement --------------------------- */
		int angularDirection = Input.IsActionPressed("left") ? -1 :
							Input.IsActionPressed("right") ? 1 :
							0;
		int acceleration = Input.IsActionPressed("forward") ? 1 : 0;

		/* ----------------------------- Calculate new velocity ---------------------------- */
		velocity += (Vector2.Up.Rotated(this.Rotation) * acceleration + new Vector2(0, _g)) * (float)delta;
		this.Rotation += _angularSpeed * angularDirection * (float)delta;
		this.Position += velocity;
		Velocity = velocity;

		/* ----------------------- Check collisions with walls ---------------------- */
		if(CheckCollision() > 0){
			return 1;
		}
		
		/* ----------------------------- Move the rocket ---------------------------- */
		MoveAndSlide();
		return 0;

		// speed cap NOT USED // tzw. no cap
		// float currentSpeed = _velocity.Length();
		// if (currentSpeed > _speedCap)
		// {
		//     _velocity /= currentSpeed;
		//     _velocity *= _speedCap;
		// } 
	}

	public double convertToRadian(double degree)
	{
		return degree * Mathf.Pi / 180;
	}

	public double convertToDegree(double radian)
	{
		return radian * 180 / Mathf.Pi;
	}

}