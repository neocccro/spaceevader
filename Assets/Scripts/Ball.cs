using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    public float speed;

    Rigidbody2D _rigidbody;

    private float _birthTime;

	// Use this for initialization
	void Start()
    {
        _birthTime = Time.time;

        _rigidbody = gameObject.GetComponent<Rigidbody2D>();

        _rigidbody.AddForce(new Vector2(Random.Range(0f, 2f) - 1, Random.Range(0f, 2f) - 1));
	}
	
	// Update is called once per frame
	void Update()
    {
        if(true)
        {

        }
        _rigidbody.velocity = _rigidbody.velocity.normalized * 4 * speed;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && _birthTime + 1 < Time.time)
        {
            FindObjectOfType<Menu>().GameOver();
        }
    }
}
