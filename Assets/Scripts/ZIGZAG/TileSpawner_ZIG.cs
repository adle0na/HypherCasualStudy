using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileSpawner_ZIG : MonoBehaviour
{
    [SerializeField]
    private GameController_ZIG _gameController;
    [SerializeField]
    private GameObject         tilePrefab;
    [SerializeField]
    private Transform          currentTile;

    [SerializeField]
    private int            spawnTileCountAtStart = 100;

    private void Awake()
    {
        for(int i = 0; i < spawnTileCountAtStart; ++i)
        {
            CreateTile();
        }
    }

    private void CreateTile()
    {

        GameObject clone = Instantiate(tilePrefab);

        clone.transform.SetParent(transform);
        
        clone.GetComponent<Tile_ZIG>().Setup(this);
        
        clone.transform.GetChild(1).GetComponent<Item_ZIG>().Setup(_gameController);

        SpawnTile(clone.transform);
    }

    public void SpawnTile(Transform tile)
    {
        tile.gameObject.SetActive(true);
        
        int index           = UnityEngine.Random.Range(0, 2);
        Vector3 addPosition = index == 0 ? Vector3.right : Vector3.forward;
        tile.position       = currentTile.position + addPosition;

        currentTile = tile;

        int spawnItem = Random.Range(0, 100);
        if (spawnItem < 20)
        {
            tile.GetChild(1).gameObject.SetActive(true);
        }
    }
}
