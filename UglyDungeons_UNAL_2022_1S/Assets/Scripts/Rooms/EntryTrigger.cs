using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryTrigger : MonoBehaviour
{
    private GameObject EntryRoom;

    [HideInInspector]
    public int dash = 0;

    public Room CurrentRoom;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CurrentRoom.onEnterRoom();
        }
        
    }
}