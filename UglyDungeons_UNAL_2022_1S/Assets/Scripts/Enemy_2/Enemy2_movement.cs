using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_movement : MonoBehaviour
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
    public float areaAttack = 0.5f;

    private Enemy2 movement;
    private Vector2 pos2;
    private Vector2 pos1 = new Vector2(0.01f,0.01f);
    private Vector2 _dir;
    int i;
    private float timeBetweenAttacks = 2;
    private float attackTimer = 0;
    public int score;

    private int _score = 50;

    void Start()
    {
        currentHealth = HealthMax;
        mainC = GameObject.FindObjectOfType<MainCharacter>().gameObject;
        movement = GetComponent<Enemy2>();

        float horizontalEStart = gameObject.transform.position.x;
        float verticalEStart = gameObject.transform.position.y;

        pos2 = new Vector2(horizontalEStart,verticalEStart);
    }

    void Update()
    {
        if(attackTimer > 0)
        {
		    attackTimer -= Time.deltaTime;
	    }


        float horizontal = mainC.transform.position.x - gameObject.transform.position.x;
        float vertical = mainC.transform.position.y - gameObject.transform.position.y;

        float horizontalE = gameObject.transform.position.x;
        float verticalE = gameObject.transform.position.y;

        float horizontal1 = pos1.x;
        float vertical1 = pos1.y;

        float horizontal2 = pos2.x;
        float vertical2 = pos2.y;

        _dir = pos2 - pos1;

        if(Mathf.Sqrt((horizontal2-horizontalE)*(horizontal2-horizontalE) + (vertical2-verticalE)*(vertical2-verticalE)) <= 0.5)
        {
            i = -1;
        }

        if(Mathf.Sqrt((horizontal1-horizontalE)*(horizontal1-horizontalE) + (vertical1-verticalE)*(vertical1-verticalE)) <= 0.5)
        {
            i = 1;
        }

        
        _dir.Normalize();
        _velocity = i*speed * _dir;

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

    private void OnCollisionEnter2D(Collision2D other){
        i *= -1;
    }

}