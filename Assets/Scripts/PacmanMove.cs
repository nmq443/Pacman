using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PacmanMove : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] Text pointText;
    [SerializeField] Text highestPointText;

    static int highestPoint;
    static int point;
    int winningPoints = 327;

    public static int HighestPoint
    {
        get { return highestPoint; }
    }

    public static int Point
    {
        get { return point; }
    }

    void UpdatePoint()
    {
        point++;
        highestPoint = Mathf.Max(highestPoint, point);
        highestPointText.text = $"Highest:\n{highestPoint}";
        pointText.text = $"Score:\n{point}";
    }

    public void ResetPoint()
    {
        point = 0;
    }

    Vector2 dest = Vector2.zero;

    void Start()
    {
        if (highestPoint != 0)
        {
            highestPointText.text = $"Highest:\n{highestPoint}";
        }
        dest = transform.position;
    }

    void FixedUpdate()
    {
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        if ((Vector2)transform.position == dest)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && valid(Vector2.left))
            {
                dest = (Vector2)transform.position + Vector2.left;
            } else if (Input.GetKey(KeyCode.RightArrow) && valid(Vector2.right))
            {
                dest = (Vector2)transform.position + Vector2.right;

            } else if (Input.GetKey(KeyCode.UpArrow) && valid(Vector2.up))
            {
                dest = (Vector2)transform.position + Vector2.up;

            } else if (Input.GetKey(KeyCode.DownArrow) && valid(Vector2.down))
            {
                dest = (Vector2)transform.position + Vector2.down;
            }
        }

        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    bool valid(Vector2 dir)
    {
        Vector2 curPos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(curPos + dir, curPos);
        return (hit.collider == GetComponent<Collider2D>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.StartsWith("pacdot"))
        {
            UpdatePoint();
            if (point == winningPoints)
            {
                Debug.Log("You won!");
                point = 0;
            }
        } else if (collision.tag == "Enemy")
        {
            point = 0;
            Debug.Log("non!");
        }
    }
}
