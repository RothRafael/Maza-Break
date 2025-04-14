using Godot;
using System;

public partial class EffectsScript : Node2D
{
	[Export] AudioStream explosionSound;
	private AnimationPlayer anim;
	private AudioStreamPlayer2D audioPlayer;
	AudioClass audioManager;
	public int ExplosionDamage = 0;
	public override void _Ready()
	{
		// Initialize the AnimationPlayer or any other components here
		audioPlayer = GetNodeOrNull<AudioStreamPlayer2D>("AudioPlayer");
		audioManager = new AudioClass();
		anim = GetNode<AnimationPlayer>("AnimationPlayer");

		if (anim != null && audioPlayer != null)
		{
			Rotation = (float)GD.RandRange(0f, 360f);
			anim.SpeedScale = 2.4f;

			// Play the Sound
			audioManager?.PlayAudio(audioPlayer, explosionSound, GlobalPosition);
	        CameraShakeComponent.Instance.Shake(10, 0.5f, true);
		}

		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}
	public override void _Process(double delta)
	{
		if (!anim.IsPlaying() && audioPlayer != null && !audioPlayer.IsPlaying())
		{
			// If the animation is not playing and the audio is not playing, free the node
			QueueFree();
		}
	}
	public void OnHit(IDamagable target)
	{
		target?.TakeDamage(ExplosionDamage);
	}
	private void OnBodyEntered(Node body)
	{
		if (body is IDamagable character)
		{
			OnHit(character);
		}
	}
	public void SetExplosionDamage(int damage)
	{
		ExplosionDamage = damage;
	}
}
