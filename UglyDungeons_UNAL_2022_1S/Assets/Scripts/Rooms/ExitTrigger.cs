using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private GameObject EntryRoom;

    [HideInInspector]
    public int dash = 0;

    public Vector2 outDir;

    public Room CurrentRoom;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            other.GetComponentInParent<MainCharacter_movement>().DashExit(outDir);

            CurrentRoom.onExitRoom();
            
        }
        
    }
}