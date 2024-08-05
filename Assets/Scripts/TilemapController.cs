using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour{

    Tilemap tilemap;
    
    public TilemapController(Tilemap tilemap)
    {
        this.tilemap = tilemap;
    }

    public TileBase GetTileBase(Vector2 mousePosition){
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        TileBase tile = tilemap.GetTile(gridPosition);

        Debug.Log("Tile in position =" + gridPosition + " is " + tile);

        return null;
    }
}
