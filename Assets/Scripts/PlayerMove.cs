using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    public float speed;

    Rigidbody2D _rigidbody;

    // Use this for initialization
    void Start()
    {
        _rigidbody = gameObject.GetComponentInChildren<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update()
    {
        _rigidbody.velocity = (new Vector2(Input.GetAxisRaw("Horizontal") * 4 * speed, Input.GetAxisRaw("Vertical") * 4 * speed));
    }
}
