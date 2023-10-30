using Godot;

public partial class Rocket : RigidBody2D
{

	private float _angularSpeed = Mathf.Pi;
	private Vector2 _velocity = Vector2.Zero;
	private float _speedCap = 150;
	private float _g = 0.5f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		Movement(delta);

	}

	private void Movement(double delta)
	{
		/* --------------------------- Calculate movement --------------------------- */
		int angularDirection = Input.IsActionPressed("left") ? -1 :
							Input.IsActionPressed("right") ? 1 :
							0;
		int acceleration = Input.IsActionPressed("forward") ? 1 : 0;

		/* ----------------------------- Update position ---------------------------- */
		_velocity += (Vector2.Up.Rotated(this.Rotation) * acceleration + new Vector2(0, _g)) * (float)delta;
		this.Rotation += _angularSpeed * angularDirection * (float)delta;
		this.Position += _velocity;
		GD.Print(Position, _velocity.Length());

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

}
