using UnityEngine;

public class Player : Character
{
    Rigidbody2D rb;
    Collider2D collider;
    float moveX, moveY;
    float speed = 5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        (moveX, moveY) = (Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveX, moveY) * speed;
    }
    protected override void Death()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

}
