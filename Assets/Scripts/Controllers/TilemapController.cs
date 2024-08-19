using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TilemapController : MonoBehaviour{
    public static TilemapController instance;
    public const bool SCREEN_POSITION = true;
    public const bool WORLD_POSITION = false;
    [SerializeField] Tilemap tilemap;
    [SerializeField] TileData plowable;
    [SerializeField] TileData notPlowable;
    [SerializeField] List<TileData> plantedStages;
    Dictionary<TileBase, TileData> dataFromTiles;

    private void Awake(){
        instance = this;
    }

    private void Start(){
        // Get the whole data from the map tiles
        dataFromTiles = new Dictionary<TileBase, TileData>();
        TileData[] tileDataList = {plowable,  notPlowable, plantedStages[0], plantedStages[1], plantedStages[2]};

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

    public void TurnIntoPlowableTile(Vector3Int tilePosition){
        tilemap.SetTile(tilePosition, plowable.tiles[0]); // Gets first tile saved in plowable, returns dirt
    }

    public bool PlantSeed(Vector3Int tilePosition, Item seed){
        if(dataFromTiles[GetTileBase(tilePosition)] == plowable){
            tilemap.SetTile(tilePosition, SeedToCrop.instance.SeedToTileBase(seed));
            return true;
        }
        return false;
    }

    public void GrowSeed(Vector3Int tilePosition){
        if(plantedStages.Contains(GetTileData(GetTileBase(tilePosition)))){
            TileData tileDataTile = dataFromTiles[GetTileBase(tilePosition)];
            if(tileDataTile.tiles[0] == plantedStages[0].tiles[0]){
                tilemap.SetTile(tilePosition, plantedStages[1].tiles[0]);
            }
            else if(tileDataTile.tiles[0] == plantedStages[1].tiles[0]){
                tilemap.SetTile(tilePosition, plantedStages[2].tiles[0]);
            }
        }
    }

    public void HarvestCrop(Vector3Int tilePosition){
        TileData tileDataTile = dataFromTiles[GetTileBase(tilePosition)];
        if(tileDataTile.tiles[0] == plantedStages[2].tiles[0]){
            InventoryController.instance.AddItem(SeedToCrop.instance.TileBaseToSeed(GetTileBase(tilePosition)));
            tilemap.SetTile(tilePosition, plowable.tiles[0]);
        }
    }
}
