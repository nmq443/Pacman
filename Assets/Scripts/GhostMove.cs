using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    int cur = 0;

    // default 0.3f
    [SerializeField] float speed;

    [SerializeField] GameObject gameOverPanel;

    void FixedUpdate()
    {
        // waypoint not reached yet, then move closer
        if (transform.position != waypoints[cur].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, waypoints[cur].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        } else
        {
            cur = (cur + 1) % waypoints.Length;
        }

        // animation
        Vector2 dir = waypoints[cur].position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Pacman")
        {
            // TODO: game over panel
            Destroy(collision.gameObject);
            Debug.Log("You lose!");
            gameOverPanel.SetActive(true);
        }
    }
}
