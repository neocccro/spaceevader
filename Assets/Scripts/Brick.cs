using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public ColumnContainer myContainer;

    public bool breakable = true;

	void Start ()
    {
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && breakable)
        {
            myContainer.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !breakable)
        {
            FindObjectOfType<Menu>().Win();
        }
    }

    public void Reshape(int columns, float magnitude)
    {
        Vector2[] inner = Arc.ArcLocations(columns, -1.0f, 0.0f, magnitude, false);
        Vector2[] outer = Arc.ArcLocations(columns, -1.0f, 0.0f, magnitude + 1, false);

        PolygonCollider2D polygonCollider2d = GetComponent<PolygonCollider2D>();
        
        Vector3[] Vertices =
        {
            new Vector3(0, 0, 0),//left inner
            new Vector3(0, 1, 0),//left outer
            new Vector3(outer[1].x - inner[0].x, outer[1].y - inner[0].y, 0),//right outer
            new Vector3(inner[1].x - inner[0].x, inner[1].y - inner[0].y, 0)//right inner
       };

        int[] Triangles =
        {
          0, 1, 2,
          0, 2, 3
        };

        Mesh mesh = new Mesh();
        mesh.vertices = Vertices;
        mesh.triangles = Triangles;
        mesh.RecalculateNormals();

        gameObject.GetComponent<MeshFilter>().mesh = mesh;

        Vector2[] path =
        {
            new Vector2(0, 0),//left inner
            new Vector2(0, 1),//left outer
            outer[1] - inner[0],//right outer
            inner[1] - inner[0]//right inner
        };

        polygonCollider2d.enabled = false;
        polygonCollider2d.pathCount = 1;
        polygonCollider2d.SetPath(0, path);

        //polygonCollider2d.offset = new Vector2(-0.5f, -0.5f);
        polygonCollider2d.enabled = true;

    }
}
