using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnContainer
{
    private List<GameObject> _column;
    private int _lastCount;

    private Color[] _colors = { Color.green, Color.cyan, Color.blue, Color.magenta, Color.red };

    public ColumnContainer()
    {
        _column = new List<GameObject>();
    }

    public GameObject this[int x]
    {
        get { return _column[x]; }
    }

    public int count
    {
        get { return _column.Count; }
    }

    public void Add(GameObject gameObject)
    {
        _column.Add(gameObject);
        ResetColor();
    }

    public bool CheckCountChange()
    {
        bool output = _lastCount != count;
        _lastCount = count;
        return output;
    }
    
    public void ResetColor()
    {
        for (int i = 0; i < _column.Count; i++)
        {
            if(_column[i].GetComponent<Brick>().breakable)
            {
                _column[i].GetComponent<Renderer>().material.color = _colors[Mathf.Min(_colors.Length - 1, i)];
            }
        }
    }

    public void Remove(GameObject gameObject)
    {
        _column.Remove(gameObject);
    }

    public void Die()
    {
        for (int i = 0; i < _column.Count; i++)
        {
            GameObject.Destroy(_column[i]);
        }
    }
}
