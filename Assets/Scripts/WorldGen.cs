using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//made by Coen van Diepen
public class WorldGen : MonoBehaviour
{
    [SerializeField]
    GameObject _ballPrefab;
    [SerializeField]
    GameObject _brickPrefab;
    [SerializeField]
    GameObject _playerPrefab;

    private ColumnContainer[] _list;
    private List<Ball> _balls;
    private PlayerMove _player;

    // Use this for initialization
    public void GenerateMap(int columns, int rows, int minMagnitude, int ballAmount, float ballSpeed, float playerSpeed)
    {
        _balls = new List<Ball>();
        _list = new ColumnContainer[columns];
        for(int i = 0; i < columns; i++)
        {
            _list[i] = new ColumnContainer();
        }
        
        for(int i = 0; i < ballAmount; i++)
        {
            _balls.Add(Instantiate(_ballPrefab).GetComponent<Ball>());
            _balls[i].speed = ballSpeed;
        }
        _player = Instantiate(_playerPrefab).GetComponent<PlayerMove>();
        _player.speed = playerSpeed;
        
        float[] rots = Arc.ArcRotations(columns, -1.0f, 0.0f, false);

        Vector2[] locs;
        for (int i = 0; i < rows; i++)
        {
            locs = Arc.ArcLocations(columns, -1.0f, 0.0f, i + minMagnitude, false);
            for(int j = 0; j < columns; j++)
            {
                _list[j].Add(Instantiate(_brickPrefab));
                _list[j][i].transform.Rotate(new Vector3(0,0,-rots[j])*360);
                _list[j][i].transform.position = locs[j];
                Brick brick = _list[j][i].GetComponent<Brick>();
                brick.myContainer = _list[j];
                brick.Reshape(columns,i + minMagnitude);
                brick.breakable = true;
            }
        }

        locs = Arc.ArcLocations(columns, -1.0f, 0.0f, rows + minMagnitude, false);
        for (int j = 0; j < columns; j++)
        {
            _list[j].Add(Instantiate(_brickPrefab));
            _list[j][rows].transform.Rotate(new Vector3(0, 0, -rots[j]) * 360);
            _list[j][rows].transform.position = locs[j];
            Brick brick = _list[j][rows].GetComponent<Brick>();
            brick.myContainer = _list[j];
            brick.Reshape(columns, rows + minMagnitude);
            brick.breakable = false;
        }
    }

    private void Update()
    {
        if(_list != null)
        {
            for (int i = 0; i < _list.Length; i++)
            {
                if (_list[i].CheckCountChange())
                {
                    _list[i].ResetColor();
                }
            }
        }
    }

    public void DestroyMap()
    {
        if (_balls != null)
        {
            for (int i = 0; i < _balls.Count; i++)
            {
                Destroy(_balls[i].gameObject);
            }
            _balls = null;
        }
        if (_player != null)
        {
            Destroy(_player.gameObject);
        }
        if(_list != null)
        {
            for (int i = 0; i < _list.Length; i++)
            {
                _list[i].Die();
            }
            _list = null;
        }
    }
}