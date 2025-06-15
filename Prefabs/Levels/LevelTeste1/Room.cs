using Godot;
using System;
using System.ComponentModel;

[GlobalClass]
public partial class Room : Resource
{
    [Description("id da sala na lista do levelManager")]
    [Export] public int n, s, l, o; // norte, sul, leste, oeste
    [Export] public string nome;
    [Export] public string descricao;
    [Export] public bool isClear = false;
    [Export] public int baseLayer;
    [Export ]public int wallLayer;
    [Export] int[] enemies = { 1, 1, 1 };


}
