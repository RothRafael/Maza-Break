
using Godot;



public partial class PlayerController : CharacterBody2D
{
	[Export] private float speed;
	[Export] private float acceleration;
	[Export(PropertyHint.Range, "0,200")] private int CameraFollowMouse = 150;
	private Node2D _camera;
	private EnemyHitBoxScript _enemyHitBoxScript;
	private PlayerStatus _playerStatus;
	private WeaponOnHandScript _handsScript;
	private Vector2 shootAngle;
	public float moveX;
	public float moveY;
	public float stickX;
	public float stickY;

	

	[Export] private Vector2 _handsOffsset;
	private Node2D hands;

	public override void _Ready()
	{
		hands = GetNode<Node2D>("Hands");
		hands.Position = _handsOffsset;
		// Initialize the camera	
		_camera = GetNode<Node2D>("CamPivo");
		// Set the camera to follow the player
		_enemyHitBoxScript = GetNode<EnemyHitBoxScript>("EnemyHitBox");
		_playerStatus = PlayerStatus.Instance;
		_handsScript = GetNode<WeaponOnHandScript>("Hands");
		ThrowErrors();
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleMovement();
		HandleHandTracking();
		HandleMouseOffset();

		// Handle shooting
		HandleShooting();

		//Check collisions
		PlayerStatus.Instance.playerPosition = Position;	
	}


	public void HandleMovement()
	{
		Vector2 velocity = Vector2.Zero;

		// Get input direction
		moveX = Input.GetActionStrength("Right") - Input.GetActionStrength("Left");
		moveY = Input.GetActionStrength("Down") - Input.GetActionStrength("Up");

		Vector2 direction = new Vector2(moveX, moveY).Normalized();

		// Apply movement
		if (direction != Vector2.Zero)
			velocity = direction * speed;

		Velocity = velocity;
		MoveAndSlide();
	}

	public void HandleHandTracking()
	{
		Vector2 inputDirection = Vector2.Zero;

		// Get right stick input (axes 2 and 3 are common for right stick)
		float stickX = Input.GetJoyAxis(0, JoyAxis.RightX); // JOY_AXIS_2
		float stickY = Input.GetJoyAxis(0, JoyAxis.RightY); // JOY_AXIS_3

		// Apply deadzone
		float deadzone = 0.2f;
		if (Mathf.Abs(stickX) > deadzone || Mathf.Abs(stickY) > deadzone)
		{
			inputDirection = new Vector2(stickX, stickY).Normalized();
		}
		else
		{
			// Fallback to mouse direction if stick is idle
			Vector2 mousePosition = GetGlobalMousePosition();
			inputDirection = (mousePosition - GlobalPosition).Normalized();
		}

		// Rotate the hand towards the input direction
		hands.Rotation = inputDirection.Angle();
		shootAngle = inputDirection;
		PlayerStatus.Instance.lookDirection = inputDirection;
	}


	public void HandleShooting()
	{
		GunBase gun = _handsScript.CurrentGun();
		if (gun.isAutomatic)
		{
			if (Input.IsActionPressed("Shoot"))
			{
				Shoot();
			}
		}
		else if (!gun.isAutomatic)
		{
			if (Input.IsActionJustPressed("Shoot"))
			{
				Shoot();
			}
		}
	}
	private void Shoot()
	{
		GunBase gun = _handsScript.CurrentGun();
		if (gun == null) return;
		gun.Shoot(shootAngle);
	}
	public void HandleMouseOffset()
	{
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 offsetH = (mousePosition - GlobalPosition) / (GetViewportRect().Size.X / CameraFollowMouse);
		Vector2 offsetV = (mousePosition - GlobalPosition) / (GetViewportRect().Size.Y / CameraFollowMouse);

		// Set the camera position to follow the mouse
		_camera.Position = new Vector2(offsetH.X, offsetV.Y);
		// Rotate the camera to face the mouse
	}

	public void ThrowErrors()
	{
		// if (_enemyHitBoxScript == null)
		// {
		// 	GD.PrintErr("EnemyHitBoxScript is null");
		// }
		// else
		// {
		// 	GD.Print("EnemyHitBoxScript is not null");
		// }

		// if (hands == null)
		// {
		// 	GD.PrintErr("Hands is null");
		// }
		// else
		// {
		// 	GD.Print("Hands is not null");
		// }
	}
	void _on_mouse_entered()
	{
		GD.Print("Mouse entered");
	}
}

