using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_ZIG : MonoBehaviour
{
    [SerializeField]
    private float       falldownTime = 2f;
    private Rigidbody   rigidbody;
    private TileSpawner_ZIG _tileSpawner = null;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Setup(TileSpawner_ZIG tileSpawner)
    {
        this._tileSpawner = tileSpawner;
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag.Equals("Player"))
            StartCoroutine("FallDownAndRespawnTile");
    }

    private IEnumerator FallDownAndRespawnTile()
    {
        yield return new WaitForSeconds(0.1f);

        rigidbody.isKinematic = false;

        yield return new WaitForSeconds(falldownTime);
        
        rigidbody.isKinematic = true;

        if (_tileSpawner != null)
            _tileSpawner.SpawnTile(this.transform);
        else
            gameObject.SetActive(false);
    }
}
