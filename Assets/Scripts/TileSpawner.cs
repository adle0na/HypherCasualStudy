using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;      // 맵에 배치되는 타일 프리팹
    [SerializeField]
    private Transform currentTile;      // 현재 타일 Transform 정보 (새로운 타일의 생성 위치 설정에 사용)

    [SerializeField]
    private int spawnTileCountAtStart = 100;

    private void Awake()
    {
        for(int i = 0; i < spawnTileCountAtStart; ++i)
        {
            CreateTile();
        }
    }

    private void CreateTile()
    {
        // TilePrefab 오브젝트 생성
        GameObject clone = Instantiate(tilePrefab);
        // 생성된 타일 오브젝트의 부모 오브젝트를 tilePrefab으로 설정
        clone.transform.SetParent(transform);

        SpawnTile(clone.transform);
    }

    public void SpawnTile(Transform tile)
    {
        // 스폰하려는 타일을 보이게 설정
        tile.gameObject.SetActive(true);
        
        // 0,1 중 임의의 수 생성
        // 0 이나오면 currentTile의 오른쪽
        // 1 이나오면 currentTile의 앞쪽에 배치
        int index           = UnityEngine.Random.Range(0, 2);
        Vector3 addPosition = index == 0 ? Vector3.right : Vector3.forward;
        tile.position       = currentTile.position + addPosition;
        
        // 마지막에 생성한 Tile을 currentTile로 지정
        currentTile = tile;
    }
}
