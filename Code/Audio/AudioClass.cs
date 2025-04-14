using System;
using System.Diagnostics;
using Godot;

public partial class AudioClass : Node
{

    public void PlayAudio(AudioStreamPlayer2D player, AudioStream audioStream, Vector2 position, float volume = 1f)
    {
        if (audioStream == null)
        {
            GD.PrintErr("Failed to load audio: null audioStream provided.");
            return;
        }

        // Generate random pitch variation
        var rng = new RandomNumberGenerator();
        float pitch = rng.RandfRange(0.9f, 1.1f);

        // Configure the player
        player.Stream = audioStream;
        player.VolumeDb = Mathf.LinearToDb(volume);
        player.PitchScale = pitch;
        player.GlobalPosition = position;

        // Play the sound
        // Debug.Print(audioStream.ResourcePath);
        player.Play();

        // Debug.Print(player.IsPlaying()+ " Audio is not playing"); 
    }
}
