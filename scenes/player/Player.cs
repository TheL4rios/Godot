using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private float _velocity = 100.0f;
	private float _jumpVelocity = -300.0f;

	public override void _PhysicsProcess(double delta)
	{
		Velocity += GetGravity() * (float)delta;

		// Jumping
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			Velocity = new Vector2(Velocity.X, _jumpVelocity);
		}

		// Get the input direction and handle the movement/deceleration.
		float velocityX = Input.GetAxis("ui_left", "ui_right") * _velocity;

		Velocity = new Vector2(velocityX, Velocity.Y);
		MoveAndSlide();
	}

	void test()
	{
		GD.Print("platanos");
	}
}
