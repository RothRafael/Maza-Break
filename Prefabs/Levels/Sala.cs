using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

[GlobalClass]
public partial class Room : Resource
{
    [Export] public Room n, s, l, o; // norte, sul, leste, oeste
    [Export] public string nome;
    [Export] public string descricao;
    [Export] public int baseLayer;
    [Export ]public int wallLayer;
    [Export] int[] enemies = { 1, 1, 1 };


}
