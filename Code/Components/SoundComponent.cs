using Godot;
using Godot.Collections;

public partial class SoundComponent : Node
{
    [Export] private AudioStreamPlayer2D _audioStreamPlayer;
    [Export] private Array<AudioStream> _audioStream;
    [Export] private float _volumeDb = 0f;
    [Export] private Vector2 PitchRange = new Vector2(0.9f, 1.1f);
    [Export] private bool _playOnReady = false;
    [Export] private bool _loop = false;

    public override void _Ready()
    {
        if (_audioStreamPlayer == null)
        {
            GD.PrintErr("AudioStreamPlayer2D is not assigned.");
            return;
        }

        if (_audioStream == null)
        {
            GD.PrintErr("AudioStream is not assigned.");
            return;
        }

        _audioStreamPlayer.VolumeDb = _volumeDb;
    }

    public void PlaySound()
    {
        if (_audioStreamPlayer != null)
        {
            _audioStreamPlayer.Stream = _audioStream.PickRandom();

            float randomPitch = (float)GD.RandRange(PitchRange.X, PitchRange.Y);
            _audioStreamPlayer.PitchScale = randomPitch;
            _audioStreamPlayer.Play();
        }
    }
    public void PlayRandomSoundBetweenIndexes(int startIndex, int endIndex)
    {
        if (_audioStreamPlayer != null && _audioStream != null && startIndex >= 0 && endIndex < _audioStream.Count && startIndex <= endIndex)
        {
            int randomIndex = (int)GD.RandRange(startIndex, endIndex + 1);
            _audioStreamPlayer.Stream = _audioStream[randomIndex];

            float randomPitch = (float)GD.RandRange(PitchRange.X, PitchRange.Y);
            _audioStreamPlayer.PitchScale = randomPitch;
            _audioStreamPlayer.Play();
        }
        else
        {
            GD.PrintErr("Invalid index range or AudioStreamPlayer2D/AudioStream is not assigned.");
        }
    }
    public void PlaySoundIncreasingPitch()
    {
        if (_audioStreamPlayer != null)
        {
            _audioStreamPlayer.Stream = _audioStream.PickRandom();

            float randomPitch = (float)GD.RandRange(PitchRange.X, PitchRange.Y);
            _audioStreamPlayer.PitchScale = randomPitch;
            _audioStreamPlayer.Play();
            _audioStreamPlayer.PitchScale += 0.1f;
        }
    }
    public bool isPlaying()
    {
        if (_audioStreamPlayer != null)
        {
            return _audioStreamPlayer.Playing;
        }
        return false;
    }
}
