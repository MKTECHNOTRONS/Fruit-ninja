using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    // Start is called before the first frame update
    int vertexCount = 0;
    bool mouseDown = false;
    LineRenderer line;
    public GameObject blast;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {

                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    line.positionCount = vertexCount + 1;
                    Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                    line.SetPosition(vertexCount, mousepos);
                    vertexCount++;

                    BoxCollider2D box = gameObject.AddComponent<BoxCollider2D>();
                    box.transform.position = line.transform.position;
                    box.size = new Vector2(0.1f, 0.1f);
                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    vertexCount = 0;
                    line.positionCount = 0;

                    BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
                    foreach (BoxCollider2D box in colliders)
                    {
                        Destroy(box);
                    }
                }
            }
        }
        //else if (Application.platform == RuntimePlatform.WindowsPlayer)
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDown = true;
            }

            if (mouseDown)
            {
                line.positionCount = vertexCount + 1;
                Vector3 mousepos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                line.SetPosition(vertexCount, mousepos);
                vertexCount++;

                BoxCollider2D box = gameObject.AddComponent<BoxCollider2D>();
                box.transform.position = line.transform.position;
                box.size = new Vector2(0.1f, 0.1f);
            }

            if (Input.GetMouseButtonUp(0))
            {
                mouseDown = false;
                vertexCount = 0;
                line.positionCount = 0;

                BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
                foreach (BoxCollider2D box in colliders)
                {
                    Destroy(box);
                }
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "bomb")
        {
             GameObject b  =   Instantiate(blast, col.transform.position, Quaternion.identity) as GameObject;
            Destroy(b.gameObject, 5f);

            Destroy(col.gameObject);
        }
    }
}
