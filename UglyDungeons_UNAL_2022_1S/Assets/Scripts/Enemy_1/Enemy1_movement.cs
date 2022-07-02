using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_movement : MonoBehaviour
{
    [SerializeField]
    private int HealthMax = 100;
    [SerializeField]
    private GameObject mainC;
    [SerializeField]
    private float speed = 1.5f;
    [SerializeField]
    private Rigidbody2D _rb;
    int currentHealth;

    private Vector2 _velocity;
    private bool facingleft = true;

    public Room CurrentRoom;

    public bool isDead => currentHealth<=0;
    public float areaAttack = 3f;

    private Enemy1 movement;
    private float timeBetweenAttacks = 2;
    private float attackTimer = 0;

    private int _score = 25;

    void Start()
    {
        currentHealth = HealthMax;
        mainC = GameObject.FindObjectOfType<MainCharacter>().gameObject;
        movement = GetComponent<Enemy1>();
    }

    void Update()
    {

        if(attackTimer > 0){
		    attackTimer -= Time.deltaTime;
	    }

        float horizontal = mainC.transform.position.x - gameObject.transform.position.x;
        float vertical = mainC.transform.position.y - gameObject.transform.position.y;

        Vector2 _dir = new Vector2(horizontal, vertical);
        _dir.Normalize();
        _velocity = speed * _dir;

        if(horizontal > 0 && facingleft)
        {         
            Flip();
        }

        if (horizontal < 0 && !facingleft)
        {
            Flip();
        }

        movement.MovingEnemy(horizontal != 0);

        if (Mathf.Sqrt(horizontal*horizontal + vertical*vertical) <= areaAttack)
        {

            if(attackTimer <= 0)
            {
			    attackTimer = timeBetweenAttacks;
			    movement.AttackEnemy();
		    }
            
        }
    }

    private void FixedUpdate()
    {

        _rb.velocity = _velocity;

    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        //GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.333f, 0.333f);
        Invoke("ReturnColorEnemy", 0.18f);

        if (currentHealth <= 0)
        {
            Invoke("Die", 0.2f);
        }
    }

    private void Die()
    {

        //GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.0f, 0.0f, 0.5f);
        Destroy(gameObject, 0.1f);
        CurrentRoom.onEnemyDie();
        if(GameEvents.OnEnemyDeathEvent != null)
        {
            GameEvents.OnEnemyDeathEvent(_score);
        }

    }

    void ReturnColorEnemy()
    {
        //GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
    }

    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingleft = !facingleft;
    }

}
