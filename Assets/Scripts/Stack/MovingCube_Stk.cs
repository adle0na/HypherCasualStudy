using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube_Stk : MonoBehaviour
{
    [SerializeField]
    private float   moveSpeed = 1.5f;
    private Vector3 moveDirection;

    private CubeSpawner_Stk       _cubeSpawner;
    private MoveAxis              _moveAxis;
    private PerfectController_Stk _perfectController;

    public void Setup(CubeSpawner_Stk _cubeSpawner, PerfectController_Stk _perfectController, MoveAxis _moveAxis)
    {
        this._cubeSpawner       = _cubeSpawner;
        this._perfectController = _perfectController;
        this._moveAxis          = _moveAxis;

        if (_moveAxis == MoveAxis.x)      moveDirection = Vector3.left;
        else if (_moveAxis == MoveAxis.z) moveDirection = Vector3.back;
    }

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (_moveAxis == MoveAxis.x)
        {
            if (transform.position.x <= -1.5f)    moveDirection = Vector3.right;
            else if(transform.position.x >= 1.5f) moveDirection = Vector3.left;
        }
        else if (_moveAxis == MoveAxis.z)
        {
            if (transform.position.z <= -1.5f)    moveDirection = Vector3.forward;
            else if(transform.position.z >= 1.5f) moveDirection = Vector3.back;
        }
    }

    public bool Arrangement()
    {
        moveSpeed = 0;

        float hangOver = GetHangOver();

        if (IsGameOver(hangOver)) return true;

        bool isPerfect = _perfectController.IsPerfect(hangOver);

        if (isPerfect == false)
        {
            float direction = hangOver >= 0 ? 1 : -1;
            
            if (_moveAxis == MoveAxis.x)      SplitCubeOnX(hangOver, direction);
            else if (_moveAxis == MoveAxis.z) SplitCubeOnZ(hangOver, direction);
        }
        
        _cubeSpawner.LastCube = this.transform;

        return false;
    }

    private float GetHangOver()
    {
        float amount = 0;

        if (_moveAxis == MoveAxis.x)
            amount = transform.position.x - _cubeSpawner.LastCube.transform.position.x;
        else if (_moveAxis == MoveAxis.z)
            amount = transform.position.z - _cubeSpawner.LastCube.transform.position.z;

        return amount;
    }
    
    private void SplitCubeOnX(float hangOver, float direction)
    {
        float newXPosition         = transform.position.x - (hangOver / 2);
        float newXSize             = transform.localScale.x - Mathf.Abs(hangOver);

        transform.position         = new Vector3(newXPosition, transform.position.y, transform.position.z);
        transform.localScale       = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);

        float cubeEdge             = transform.position.x + (transform.localScale.x / 2 * direction);
        float fallingBlockSize     = Mathf.Abs(hangOver);
        float fallingBlockPosition = cubeEdge + fallingBlockSize / 2 * direction;
        
        SpawnDropCube(fallingBlockPosition, fallingBlockSize);
    }
    
    private void SplitCubeOnZ(float hangOver, float direction)
    {
        float newZPosition         = transform.position.z - (hangOver / 2);
        float newZSize             = transform.localScale.z - Mathf.Abs(hangOver);

        transform.position         = new Vector3(transform.position.x, transform.position.y, newZPosition);
        transform.localScale       = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
            
        float cubeEdge             = transform.position.z + (transform.localScale.z / 2 * direction);
        float fallingBlockSize     = Mathf.Abs(hangOver);
        float fallingBlockPosition = cubeEdge + fallingBlockSize / 2 * direction;
            
            
        SpawnDropCube(fallingBlockPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockPosition, float fallingBlockSize)
    {
        GameObject clone = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (_moveAxis == MoveAxis.x)
        {
            clone.transform.position   = new Vector3(fallingBlockPosition, transform.position.y, transform.position.z);
            clone.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
        }
        else if (_moveAxis == MoveAxis.z)
        {
            clone.transform.position   = new Vector3(transform.position.x, transform.position.y, fallingBlockPosition);
            clone.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
        }

        clone.GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color;
        clone.AddComponent<Rigidbody>();
        
        Destroy(clone, 2);
    }

    private bool IsGameOver(float hangOver)
    {
        float max = _moveAxis == MoveAxis.x
            ? _cubeSpawner.LastCube.transform.localScale.x
            : _cubeSpawner.LastCube.transform.localScale.z;

        if (Mathf.Abs(hangOver) > max) return true;

        return false;
    }

    public void RecoveryCube()
    {
        float recoverySize = 0.1f;

        if (_moveAxis == MoveAxis.x)
        {
            float newXSize       = transform.localScale.x + recoverySize;
            float newXPosition   = transform.position.x + recoverySize * 0.5f;

            transform.position   = new Vector3(newXPosition, transform.position.y, transform.position.z);
            transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            float newZSize       = transform.localScale.z + recoverySize;
            float newZPosition   = transform.position.z + recoverySize * 0.5f;

            transform.position   = new Vector3(transform.position.x, transform.position.y, newZPosition);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        }
    }
    
}
