using UnityEngine;
using System;
public class AutomaticTeleportation : MonoBehaviour
{
    [SerializeField] int hallwayLength;
    [SerializeField] Transform player;

    [SerializeField] Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        if (player.position.x - transform.position.x > 4 * hallwayLength)
        {
            transform.position=new Vector2(transform.position.x + 7*hallwayLength ,transform.position.y);
        }
        if(player.position.x - transform.position.x < -4 * hallwayLength)
        {
            transform.position=new Vector2(transform.position.x - 7*hallwayLength ,transform.position.y);
        }
        if (rb != null)
        {
            if(Math.Abs(player.position.x-transform.position.x) > 2.5f * hallwayLength)
            {
                rb.bodyType=RigidbodyType2D.Static;
            }
            else
            {
                rb.bodyType=RigidbodyType2D.Dynamic;
            }
        }
    }
}
