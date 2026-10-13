using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] private AnimatedSprite2D animation {get; set;}
	[Export] private Area2D area2D {get; set;}
	[Export] private ShaderMaterial materialRed {get; set;}

	private bool _isDead = false;
	private float _velocity = 100.0f;
	private float _jumpVelocity = -300.0f;

	public override void _Ready()
	{
		AddToGroup("player");
		area2D.BodyEntered += OnArea2DBodyEntered;
	}

	private async void OnArea2DBodyEntered(Node2D body)
	{
		animation.Material = materialRed;
		_isDead = true;
		animation.Stop();

		await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
		EmitSignal(SignalName.PlayerDied);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_isDead) return;

		Velocity += GetGravity() * (float)delta;

		// Jumping
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			Velocity = new Vector2(Velocity.X, _jumpVelocity);
		}

		// Get the input direction and handle the movement/deceleration.
		float velocityX = Input.GetAxis("left", "right") * _velocity;
		if (velocityX > 0)
		{
			animation.FlipH = true;
		}
		else if (velocityX < 0)
		{
			animation.FlipH = false;
		}

		Velocity = new Vector2(velocityX, Velocity.Y);
		MoveAndSlide();

		if (!IsOnFloor())
		{
			animation.Play("jump");
		}
		else if (Velocity.X != 0)
		{
			animation.Play("run");
		}
		else
		{
			animation.Play("idle");
		}
	}

	void test()
	{
		GD.Print("platanos");
	}

	[Signal]
    public delegate void PlayerDiedEventHandler();
}
