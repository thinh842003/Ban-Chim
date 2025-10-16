using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float FlyingSpeed;
    public float minYSpeed;
    public float maxYSpeed;

    public GameObject deadVFX;

    Rigidbody2D temp_rb;
    bool temp_moveLeftOnStart;

    bool temp_isDead;

    private void Awake()
    {
        temp_rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        RandomMoveDirection();
    }

    private void Update()
    {
        temp_rb.velocity = temp_moveLeftOnStart ? new Vector2(-FlyingSpeed, Random.Range(minYSpeed, maxYSpeed))
                                                : new Vector2(FlyingSpeed, Random.Range(minYSpeed, maxYSpeed));

        Flip();
    }

    public void RandomMoveDirection()
    {
        temp_moveLeftOnStart = transform.position.x > 0 ? true : false;
    }

    void Flip()
    {
        if (temp_moveLeftOnStart)
        {
            if (transform.localScale.x < 0) return;

            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            if (transform.localScale.x > 0) return;

            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    public void BirdDie()
    {
        temp_isDead = true;

        if (GameController.Ins.isGameover)
            return;

        GameController.Ins.BirdKilled++;

        Destroy(gameObject);

        if(deadVFX)
        {
            Instantiate(deadVFX, transform.position, Quaternion.identity);
        }

        GameGUIController.Ins.UpdateCountingKilled(GameController.Ins.BirdKilled);
    }
}
