using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class LevelManagerScript : Node
{
    private RoomTree roomTree;
    [Export] public Array<RoomInfo> rooms = new Array<RoomInfo>();
    private HashSet<Vector2> usedPositions = new HashSet<Vector2>();

    private int x = 0;
    private int y = 0;
    private int distancia = 200;
    public override void _Ready()
    {
        ArrangeRooms();
    }
    public void ArrangeRooms()
    {
        foreach (RoomInfo room in rooms)
        {
            GD.Print(room.nome);
            room.pos = getFreeRoomPosition(room);

            TileMapLayer newRoom = room.roomScene.Instantiate<TileMapLayer>();
            AddChild(newRoom);

            newRoom.GlobalPosition = room.pos;
            GD.Print(newRoom.GlobalPosition);
        }
    }
    public Vector2 getFreeRoomPosition(RoomInfo room)
    {
        Vector2 position = new Vector2();

        while (true)
        {
            position = new Vector2(x, y);
            if (!isRoomFilled(position))
            {
                break;
            }
            x += distancia;
            if (x >= 1000)
            {
                x = 0;
                y += 500;
            }
        }
        return position;
    }
    public bool isRoomFilled(Vector2 position)
    {
        foreach (RoomInfo room in rooms)
        {
            if (room.pos == position)
            {
                return true;
            }
        }
        return false;
    }
}

public class RoomTree
{
    public RoomInfo Root { get; set; }

    public RoomTree(RoomInfo root)
    {
        Root = root;
    }

    public void AddRoom(Room room, Room parent)
    {

    }
}

