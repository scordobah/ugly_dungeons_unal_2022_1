using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public ExitTrigger ExitTriggerRight;
    public ExitTrigger ExitTriggerLeft;
    public ExitTrigger ExitTriggerUp;
    public ExitTrigger ExitTriggerDown;
    [Space(20)]
    public EntryTrigger EntryTrigger;

    [Space(20)]
    public GameObject VirtualCamera;

    [Space(20)]
    public Enemy2_movement EnemyPrefab2;
    public Enemy1_movement EnemyPrefab1;

    public int enemyAmount = 1;

    public Vector2 SpawnArea;
    public GameObject Doors;
    public GameObject DoorsOpen;
    private List<Enemy1_movement> EnemiesInRoom1 = new List<Enemy1_movement>();
    private List<Enemy2_movement> EnemiesInRoom2 = new List<Enemy2_movement>();
    private int randEnemy;
    private Enemy1_movement newEnemy1;
    private Enemy2_movement newEnemy2;
    private bool roomEmpty = true;


    void Start()
    {
        DoorsOpen.SetActive(true);
        EntryTrigger.CurrentRoom = this;

        if(ExitTriggerRight != null)
        {
            ExitTriggerRight.outDir = Vector2.right;
            ExitTriggerRight.CurrentRoom = this;
        }

        if(ExitTriggerLeft != null)
        {
            ExitTriggerLeft.outDir = Vector2.left;
            ExitTriggerLeft.CurrentRoom = this;
        }

        if(ExitTriggerUp != null)
        {
            ExitTriggerUp.outDir = Vector2.up;
            ExitTriggerUp.CurrentRoom = this;
        }

        if(ExitTriggerDown != null)
        {
            ExitTriggerDown.outDir = Vector2.down;
            ExitTriggerDown.CurrentRoom = this;
        }
    }

    
    void Update()
    {
        
    }

    public void onEnterRoom()
    {
        
        StartCoroutine(toggleExitTrigger(true));
        EntryTrigger.gameObject.SetActive(false);

        VirtualCamera.gameObject.SetActive(true);

        SpawnEnemies();
        
    }

    public void onExitRoom()
    {
        VirtualCamera.gameObject.SetActive(false);

        if(ExitTriggerRight != null)
        {
            ExitTriggerRight.gameObject.SetActive(false);
        }

        if(ExitTriggerLeft != null)
        {
            ExitTriggerLeft.gameObject.SetActive(false);
        }

        if(ExitTriggerUp != null)
        {
            ExitTriggerUp.gameObject.SetActive(false);
        }

        if(ExitTriggerDown != null)
        {
            ExitTriggerDown.gameObject.SetActive(false);
        }

        StartCoroutine(toggleEntryTrigger(true));
    }

    private IEnumerator toggleEntryTrigger(bool active)
    {

        yield return new WaitForSeconds(0.3f);
        EntryTrigger.gameObject.SetActive(active);

    }

    private IEnumerator toggleExitTrigger(bool active)
    {

        yield return new WaitForSeconds(0.3f);

        if(ExitTriggerRight != null)
        {
            ExitTriggerRight.gameObject.SetActive(true);
        }

        if(ExitTriggerLeft != null)
        {
            ExitTriggerLeft.gameObject.SetActive(true);
        }

        if(ExitTriggerUp != null)
        {
            ExitTriggerUp.gameObject.SetActive(true);
        }

        if(ExitTriggerDown != null)
        {
            ExitTriggerDown.gameObject.SetActive(true);
        }

        //Activar puertas cerradas.
        Doors.SetActive(roomEmpty);
        
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyAmount; i++)
        {

            randEnemy = Random.Range(0, 3);
            Debug.Log(randEnemy);

            if(randEnemy == 2)
            {
                Enemy2_movement newEnemy2 = Instantiate(EnemyPrefab2);
                Vector2 randomPos = Vector2.zero;

                randomPos.x = Random.Range(-SpawnArea.x,SpawnArea.x); 
                randomPos.y = Random.Range(-SpawnArea.y,SpawnArea.y); 
                newEnemy2.transform.position = transform.position + (Vector3)randomPos;

                newEnemy2.CurrentRoom = this;

                EnemiesInRoom2.Add(newEnemy2);
            }

            else 
            {
                Enemy1_movement newEnemy1 = Instantiate(EnemyPrefab1);
                Vector2 randomPos = Vector2.zero;

                randomPos.x = Random.Range(-SpawnArea.x,SpawnArea.x); 
                randomPos.y = Random.Range(-SpawnArea.y,SpawnArea.y); 
                newEnemy1.transform.position = transform.position + (Vector3)randomPos;

                newEnemy1.CurrentRoom = this;

                EnemiesInRoom1.Add(newEnemy1);
            }


        }
    }

    public void onEnemyDie()
    {
        for (int i = 0; i < EnemiesInRoom1.Count; i++)
        {

            if(EnemiesInRoom1[i].isDead == false)
            {
                return;
            }
        }

        
        for (int i = 0; i < EnemiesInRoom2.Count; i++)
        {

            if(EnemiesInRoom2[i].isDead == false)
                {
                    return;
                }
        }

        //Abrir puertas.
        enemyAmount = 0;
        roomEmpty = false;
        Doors.SetActive(false);
        DoorsOpen.SetActive(!roomEmpty);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 Size = new Vector3(SpawnArea.x*2, SpawnArea.y*2, 0.01f);
        Gizmos.DrawWireCube(transform.position, Size);
    }
    

}
