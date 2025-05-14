using Godot;
using System;

[GlobalClass]
public partial class RoomInfo : Resource
{
    [Export] public string nome;
    [Export] public PackedScene roomScene;
    [Export] public Vector2 pos;
    [Export] public RoomInfo parent;
    [Export] public RoomInfo left;
    [Export] public RoomInfo right;

}   