using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private bool moving = false;

    public Transform AttackPoint;

    public float range = 2f;
    public int Poder = 50;
    public LayerMask enemyLayer;

    public void Moving(bool movingarg)
    {
        moving = movingarg;
    }

    public void Attack()
    {
        _animator.SetTrigger("attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, range, enemyLayer);

        

        foreach(Collider2D enemy in hitEnemies)
        {
            if(enemy.GetComponent<Enemy2_movement>() != null)
            {
                enemy.GetComponent<Enemy2_movement>().takeDamage(Poder);
            }

            if(enemy.GetComponent<Enemy1_movement>() != null)
            {
                enemy.GetComponent<Enemy1_movement>().takeDamage(Poder);
            }
        }

    }

    void Update()
    {
        _animator.SetBool("isMoving", moving);
    }
}
