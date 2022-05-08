using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Map : MonoBehaviour
{
    public Vector2 minvector;
    public Vector2 maxvector;
    public List<ItemPos> itemPos;
    public Vector3[] playerPos;
}
public class ItemPos
{
    public Vector3 position;
    public Item item;
}
public enum DoorArrow
{
    Right,
    Left,
    Up,
    Down
}