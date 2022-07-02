using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour {

	public int doorDirection;
	// 1 --> need bottom opening for a connection
	// 2 --> need top opening for a connection
	// 3 --> need left opening for a connection
	// 4 --> need right opening for a connection

	private RoomHandeler handler;
	private RoomTemplates templates;
	private int rand;
	public bool spawned = false;

	public float waitTime = 4f;

	void Start()
	{
		Destroy(gameObject, waitTime);
		templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
		handler = GameObject.FindGameObjectWithTag("Handle").GetComponent<RoomHandeler>();
		Invoke("Spawn", 0.1f); //needs a delay so collision is detected
	}

    void Spawn()
	{
		if(spawned == false)
		{
			if(doorDirection == 1){
				// Need to spawn a room with a BOTTOM door.
				rand = Random.Range(0, templates.bottomOpeningRooms.Length);
				//RoomTemplates newRoom = Instantiate(templates.bottomOpeningRooms[rand], transform.position, templates.bottomOpeningRooms[rand].transform.rotation);
				handler.Roomslist.Add(Instantiate(templates.bottomOpeningRooms[rand], transform.position, templates.bottomOpeningRooms[rand].transform.rotation));
				//handler.Roomslist.Add(newRoom);
			} else if(doorDirection == 2){
				// Need to spawn a room with a TOP door.
				rand = Random.Range(0, templates.topOpeningRooms.Length);
				handler.Roomslist.Add(Instantiate(templates.topOpeningRooms[rand], transform.position, templates.topOpeningRooms[rand].transform.rotation));
			} else if(doorDirection == 3){
				// Need to spawn a room with a LEFT door.
				rand = Random.Range(0, templates.leftOpeningRooms.Length);
				handler.Roomslist.Add(Instantiate(templates.leftOpeningRooms[rand], transform.position, templates.leftOpeningRooms[rand].transform.rotation));
			} else if(doorDirection == 4){
				// Need to spawn a room with a RIGHT door.
				rand = Random.Range(0, templates.rightOpeningRooms.Length);
				handler.Roomslist.Add(Instantiate(templates.rightOpeningRooms[rand], transform.position, templates.rightOpeningRooms[rand].transform.rotation));
			}
			spawned = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other) //in case a room spawns on another room
	{
		if(other.CompareTag("SpawnPoint"))
		{
			spawned = true;
		}
	}
}
