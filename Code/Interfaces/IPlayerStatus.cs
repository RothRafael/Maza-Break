using Godot;

public interface IPlayerStatus : IDamagable
{
    int Vida { get; set; }
    int Energia { get; set; }
    int Armor { get; set; }
    

}