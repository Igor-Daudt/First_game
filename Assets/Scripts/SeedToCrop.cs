using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeedToCrop : MonoBehaviour
{
    public static SeedToCrop instance;
    [SerializeField] private Item carrotCrop;
    [SerializeField] private TileBase carrotFirstCropStage;
    [SerializeField] private TileBase carrotLastCropStage;

    void Awake(){
        instance = this;
    } 

    public Item seedToCrop(Item seed){
        if(seed.Name == "CarrotSeed"){
            return seed;
        }
        return null;
    }

    public TileBase SeedToTileBase(Item seed){
        if(seed.Name == "CarrotSeed"){
            return carrotFirstCropStage;
        }
        return null;
    }

    public Item TileBaseToSeed(TileBase tileBase){
        if(tileBase == carrotLastCropStage){
            return carrotCrop;
        }
        return null;
    }
}
