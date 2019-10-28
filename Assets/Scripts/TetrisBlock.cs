using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    public float idleFallTime = .8f;
    public static int height = 20;
    public static int width = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fallTime = Input.GetKey(KeyCode.DownArrow) ? idleFallTime / 10 : idleFallTime;
        float y = transform.position.y;

        bool ValidMove()
        {
            foreach (Transform children in transform)
            {
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);

                if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= 20)
                {
                    return false;
                }
            }
            return true;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove()) {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position += new Vector3(0, -y, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(0, 1, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.C))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            if (!ValidMove())
            {
                if (transform.position.y > 0)
                {
                    transform.position += new Vector3(1, 0, 0);
                    if (!ValidMove())
                    {
                        transform.position += new Vector3(-2, 0, 0);
                    }
                }
                else
                {
                    transform.position += new Vector3(0, 1, 0);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
            {
                if (transform.position.y > 0)
                {
                    transform.position += new Vector3(1, 0, 0);
                    if (!ValidMove())
                    {
                        transform.position += new Vector3(-2, 0, 0);
                    }
                }
                else
                {
                    transform.position += new Vector3(0, 1, 0);
                }
            }
        }
        if (Time.time - previousTime > fallTime)
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(0, 1, 0);
            }
            previousTime = Time.time;
        } 
    }
}
