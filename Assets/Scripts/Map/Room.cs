using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform[] exits;
    public RoomType roomType;
}
public class RoomArea
{
    public RoomType type;
    public Bounds bounds;

    public RoomArea(RoomType roomType, Bounds bounds)
    {
        type = roomType;
        this.bounds = bounds;
    }
}

