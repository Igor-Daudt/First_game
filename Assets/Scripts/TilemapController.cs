using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TilemapController : MonoBehaviour{
    public const bool SCREEN_POSITION = true;
    public const bool WORLD_POSITION = false;
    [SerializeField] Tilemap tilemap;
    [SerializeField] List<TileData> tileDataList;
    Dictionary<TileBase, TileData> dataFromTiles;

    private void Start(){
        // Get the whole data from the map tiles
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach(TileData tileData in tileDataList)
        {
            foreach(TileBase tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
            }
        }

    }

    public TileBase GetTileBase(Vector3Int gridPosition){
        TileBase tile = tilemap.GetTile(gridPosition);
        
        return tile;
    }
    
    public Vector3Int GetGridPosition(Vector2 mousePosition, bool MousePositionScreen = SCREEN_POSITION){
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if(MousePositionScreen == SCREEN_POSITION){
            worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        }
        else if(MousePositionScreen == WORLD_POSITION){
            worldPosition = mousePosition;
        }

        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        return gridPosition;
    }

    public TileData GetTileData(TileBase tile){
        return dataFromTiles[tile];
    }
}
