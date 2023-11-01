using UnityEngine;

public class TileCell : MonoBehaviour
{
    public Vector2Int coordinates {  get; set; }

    public Tile tile { get; set; }

    public bool emty => tile == null;
    public bool occupied => tile != null;



}
