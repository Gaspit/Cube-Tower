using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform _cubeToPlace;
    [SerializeField] private GameObject _allCubes;
    [SerializeField] private GameObject _cubeToCreate;
    [SerializeField] private Rigidbody _allCubesRB;
    [SerializeField] float cubeChangePlaceSpeed = 0.8f;
    
    private CubePos _nowCube = new CubePos(0, 1, 0);
    private List<Vector3> _allCubesPositions = new List<Vector3>
    {
        new Vector3(0, 0, 0),
        new Vector3(1, 0, 0),
        new Vector3(-1, 0, 0),
        new Vector3(0, 1, 0),
        new Vector3(0, 0, 1),
        new Vector3(0, 0, -1),
        new Vector3(1, 0, 1),
        new Vector3(-1, 0, -1),
        new Vector3(-1, 0, 1),
        new Vector3(1, 0, -1)
    };

    private void Start()
    {
        
        StartCoroutine(ShowCubePlace());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            
#if !UNITY_EDITOR
            if(Input.GetTouch(0.phase != TouchPhase.Began))
            {
                return;
            }
#endif

            GameObject newCube = Instantiate(_cubeToCreate, _cubeToPlace);
            
            newCube.transform.SetParent(_allCubes.transform);
            _nowCube.setVector(_cubeToPlace.position);
            _allCubesPositions.Add(_nowCube.getVector());

            _allCubesRB.isKinematic = true;
            _allCubesRB.isKinematic = false;
            
            SpawnPositions();
        }
    }

    IEnumerator ShowCubePlace()
    {
        while (true)
        {
            SpawnPositions();
            yield return new WaitForSeconds(cubeChangePlaceSpeed);
        }
    }

    private void SpawnPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        if (IsPositionEmpty(new Vector3(_nowCube.x + 1, _nowCube.y, _nowCube.z))
            && _nowCube.x + 1 != _cubeToPlace.position.x)
        {
            positions.Add(new Vector3(_nowCube.x + 1, _nowCube.y, _nowCube.z));
        }
        
        if (IsPositionEmpty(new Vector3(_nowCube.x - 1, _nowCube.y, _nowCube.z))
            && _nowCube.x - 1 != _cubeToPlace.position.x)
        {
            positions.Add(new Vector3(_nowCube.x - 1, _nowCube.y, _nowCube.z));
        }
        
        if (IsPositionEmpty(new Vector3(_nowCube.x, _nowCube.y + 1, _nowCube.z))
            && _nowCube.y + 1 != _cubeToPlace.position.y)
        {
            positions.Add(new Vector3(_nowCube.x, _nowCube.y + 1, _nowCube.z));
        }
        
        if (IsPositionEmpty(new Vector3(_nowCube.x, _nowCube.y - 1, _nowCube.z))
            && _nowCube.y - 1 != _cubeToPlace.position.y)
        {
            positions.Add(new Vector3(_nowCube.x, _nowCube.y - 1, _nowCube.z));
        }
        
        if (IsPositionEmpty(new Vector3(_nowCube.x, _nowCube.y, _nowCube.z + 1))
            && _nowCube.z + 1 != _cubeToPlace.position.z)
        {
            positions.Add(new Vector3(_nowCube.x, _nowCube.y, _nowCube.z + 1));
        }
        
        if (IsPositionEmpty(new Vector3(_nowCube.x, _nowCube.y, _nowCube.z - 1))
            && _nowCube.z - 1 != _cubeToPlace.position.z)
        {
            positions.Add(new Vector3(_nowCube.x, _nowCube.y, _nowCube.z - 1));
        }

        _cubeToPlace.position = positions[Random.Range(0, positions.Count)];
    }

    private bool IsPositionEmpty(Vector3 targetPos)
    {
        if (targetPos.y == 0)
        {
            return false;
        }

        foreach (Vector3 pos in _allCubesPositions)
        {
            if (pos.x == targetPos.x && pos.y == targetPos.y && pos.z == targetPos.z)
            {
                return false;
            }
        }

        return true;
    }
}

struct CubePos
{
    public int x, y, z;

    public CubePos(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3 getVector()
    {
        return new Vector3(x, y, z);
    }
    
    public void setVector(Vector3 pos)
    {
        x = (int)pos.x;
        y = (int)pos.y;
        z = (int)pos.z;
    }
}