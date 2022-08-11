using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum MoveAxis { x = 0, z}

public class CubeSpawner_Stk : MonoBehaviour
{
    [SerializeField]
    private Transform[]   cubeSpawnPoints;
    [SerializeField]
    private Transform     movingCubePrefab;

    [field: SerializeField]
    public Transform      LastCube { set; get; }
    public MovingCube_Stk CurrentCube { set; get; } = null;

    [SerializeField]
    private float         colorWeight = 15.0f;

    private int           currentColorNubmerOfTime = 5;
    private int           maxColorNumberOfTime     = 5;

    private MoveAxis      _moveAxis = MoveAxis.x;
    
    public void SpawnCube()
    {
        Transform clone = Instantiate(movingCubePrefab);

        if (LastCube == null || LastCube.name.Equals("StartCubeTop"))
        {
            clone.position = cubeSpawnPoints[(int) _moveAxis].position;
        }
        else
        {
            float x = _moveAxis == MoveAxis.x ? cubeSpawnPoints[(int) _moveAxis].position.x : LastCube.position.x;
            float z = _moveAxis == MoveAxis.z ? cubeSpawnPoints[(int) _moveAxis].position.z : LastCube.position.z;

            float y = LastCube.position.y + movingCubePrefab.localScale.y;

            clone.position = new Vector3(x, y, z);
        }

        clone.localScale = new Vector3(LastCube.localScale.x, movingCubePrefab.localScale.y, LastCube.localScale.z);
        
        clone.GetComponent<MeshRenderer>().material.color = GetRandomColor();
        
        clone.GetComponent<MovingCube_Stk>().Setup(this, _moveAxis);

        _moveAxis = (MoveAxis) (((int) _moveAxis + 1) % cubeSpawnPoints.Length);

        CurrentCube = clone.GetComponent<MovingCube_Stk>();
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < cubeSpawnPoints.Length; ++i)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(cubeSpawnPoints[i].transform.position, movingCubePrefab.localScale);
        }
    }
    private Color GetRandomColor()
    {
        Color color = Color.white;

        if (currentColorNubmerOfTime > 0)
        {
            float colorAmount = (1.0f / 255.0f) * colorWeight;
            color = LastCube.GetComponent<MeshRenderer>().material.color;
            color = new Color(color.r - colorAmount, color.g - colorAmount, color.b - colorAmount);

            currentColorNubmerOfTime--;
        }
        else
        {
            color = new Color(Random.value, Random.value, Random.value);

            currentColorNubmerOfTime = maxColorNumberOfTime;
        }

        return color;
    }
}
