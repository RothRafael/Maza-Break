using Godot;

public partial class CameraShakeComponent : Node
{
    public static CameraShakeComponent Instance { get; private set; }
    [Export] private NodePath CamPath;
    private Camera2D _camera;
    private Vector2 _originalPosition;
    [Export] private float _shakeDuration;
    [Export] private float _shakeMagnitude;
    [Export] private float _shakeElapsedTime;
    [Export] private bool _isShaking;
    [Export] private float _shakeDecay;

    public override void _Ready()
    {
        _camera = GetNode<Camera2D>(CamPath);
        Instance = this;
    }

    public void Shake(float intensity, float duration)
    {
        if (_isShaking) return;
        _shakeMagnitude = intensity;
        _shakeDuration = duration;
        _shakeElapsedTime = 0f;
        _isShaking = true;
        _originalPosition = _camera.Position;
    }
    public void Shake(float intensity, float duration, bool hasPriority)
    {
        if (hasPriority) StopShake();

        Shake(intensity, duration);
    }
    //Shake Horizontal x axis
    public void ShakeH(float intensity, float duration)
    {
        if (_isShaking) return;
        _shakeMagnitude = intensity;
        _shakeDuration = duration;
        _shakeElapsedTime = 0f;
        _isShaking = true;
        _originalPosition = _camera.Position;

        Vector2 shakeOffset = new Vector2(intensity, 0);
        _camera.Position += shakeOffset;
    }
    public void StopShake()
    {
        _isShaking = false;
        _camera.Position = _originalPosition;
    }
    public override void _Process(double delta)
    {
        if (_isShaking)
        {
            _shakeElapsedTime += (float)delta;

            if (_shakeElapsedTime >= _shakeDuration)
            {
                StopShake();
                return;
            }

            float x = (float)GD.RandRange(-_shakeMagnitude, _shakeMagnitude);
            float y = (float)GD.RandRange(-_shakeMagnitude, _shakeMagnitude);

            Vector2 shakeOffset = new Vector2(x, 0);
            _camera.Position = _originalPosition + shakeOffset;
        }
    }
}
