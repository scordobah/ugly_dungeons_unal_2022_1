using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter_movement : MonoBehaviour
{

    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private Rigidbody2D _rb;
    [SerializeField]
    private Camera _cam;

    private Vector2 _velocity;
    private bool facingleft = true;

    private MainCharacter movement;
    public ExitTrigger EntryHandler;

    private Vector2 dashDir;
    private bool isDashing;

    [SerializeField]
    private float speedDash = 6f;

    [SerializeField]
    private float dashDuration = 1f;

    private float dashTime;

    void Start()
    {
        movement = GetComponent<MainCharacter>();
    }

    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        
        Vector2 _dir = new Vector2(horizontal, vertical);
        _dir.Normalize();

        if(isDashing == false)
        {
            _velocity = speed * _dir;
        }

        else
        {
            if(dashTime>0)
            {
                dashTime -= Time.deltaTime;

                if(dashTime<=0)
                {
                    isDashing = false;
                }
            }

            _velocity = speedDash * dashDir.normalized;
        }
        

        if(horizontal > 0 && facingleft)
        {         
            Flip();
        }

        if (horizontal < 0 && !facingleft)
        {
            Flip();
        }

        movement.Moving(horizontal != 0);

        if (Input.GetKeyDown("space"))
        {
            movement.Attack();
        }

    }

    private void FixedUpdate()
    {

        _rb.velocity = _velocity;

    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingleft = !facingleft;
    }

    public void DashExit(Vector2 direction)
    {
        dashDir = direction;
        isDashing = true;

        dashTime = dashDuration;
    }

}
