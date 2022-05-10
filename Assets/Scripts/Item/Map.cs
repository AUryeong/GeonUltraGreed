using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map : MonoBehaviour
{
    public Vector2 minvector;
    public Vector2 maxvector;
    public Vector2[] playerPos;
    public List<ChestBase> ChestList;

    [HideInInspector]
    public DoorArrow[] doorArrows;
}
public enum DoorArrow
{
    Right,
    Left,
    Up,
    Down
}