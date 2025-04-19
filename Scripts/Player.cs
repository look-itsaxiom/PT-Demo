using Godot;

public partial class Player : CharacterBody3D
{
	public const float MoveSpeed = 15.0f;
	public const float BackMoveSpeed = 7.0f;
	public const float RotationSpeed = 4.5f;
	public IInteractable interactTarget;
	public bool CanMove = true;

	public Camera3D PlayerCamera;

	[Signal] public delegate void PlayerCanMoveChangedEventHandler(bool canMove);

	private AnimationPlayer _animationPlayer;

	public static readonly string Idle = "Idle";
	public static readonly string Running = "Running_A";
	public static readonly string RunningBack = "Running_B";
	public static readonly string RotateLeft = "Running_Strafe_Left";
	public static readonly string RotateRight = "Running_Strafe_Right";

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _Ready()
	{
		// Get the AnimationPlayer node.
		_animationPlayer = GetNode<AnimationPlayer>("Visual/Knight/AnimationPlayer");
		PlayerCamera = GetNode<Camera3D>("Camera3D");
		_animationPlayer.Play(Idle);
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!CanMove)
			return;

		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

		if (inputDir.X != 0)
		{
			Rotation = new Vector3(Rotation.X, Rotation.Y - inputDir.X * RotationSpeed * (float)delta, Rotation.Z);
		}
		if (direction.Z != 0)
		{
			if (inputDir.X > 0 && inputDir.Y == 0)
				_animationPlayer.Play(RotateRight);
			else if (inputDir.X < 0 && inputDir.Y == 0)
				_animationPlayer.Play(RotateLeft);
			else if (inputDir.Y > 0)
				_animationPlayer.Play(RunningBack);
			else
				_animationPlayer.Play(Running);
			velocity.X = -direction.X * (inputDir.Y > 0 ? BackMoveSpeed : MoveSpeed);
			velocity.Z = -direction.Z * (inputDir.Y > 0 ? BackMoveSpeed : MoveSpeed);
		}
		else
		{
			_animationPlayer.Play(Idle);
			velocity.X = Mathf.MoveToward(Velocity.X, 0, MoveSpeed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, MoveSpeed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("interact"))
		{
			if (interactTarget != null)
			{
				interactTarget.Interact(this);
			}
			;
		}
		base._Process(delta);
	}

}
