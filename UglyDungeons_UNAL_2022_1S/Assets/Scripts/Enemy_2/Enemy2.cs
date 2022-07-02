using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private bool moving = false;

    public Transform AttackPoint;

    public float range = 0.5f;
    public int Poder = 50;
    public LayerMask PlayerLayer;

    public void MovingEnemy(bool movingarg)
    {
        moving = movingarg;
    }

    public void AttackEnemy()
    {
        
        _animator.SetTrigger("attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, range, PlayerLayer);

        //foreach(Collider2D enemy in hitEnemies)
        //{
            //enemy.GetComponent<Enemy1_movement>().takeDamage(Poder);
        //}

    }

    void Update()
    {
        _animator.SetBool("isMoving", moving);
    }

}
