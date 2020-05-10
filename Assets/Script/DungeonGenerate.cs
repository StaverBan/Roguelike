using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGenerate : MonoBehaviour
{
    public Room[] RoomPref;
    public Room StartRoom;

    public Room[,] spawnedRooms;

    public Vector2 DungeonScale;
    public int NumberOfRoom;

    public void Start()
    {
        spawnedRooms = new Room[(int)DungeonScale.x, (int)DungeonScale.y];

        int xSpawn = Random.Range(0, (int)DungeonScale.x);
        int ySpawn = Random.Range(0, (int)DungeonScale.y);


        spawnedRooms[xSpawn,ySpawn] = StartRoom;
        StartRoom.transform.position = new Vector2(xSpawn, ySpawn);

        if (NumberOfRoom == 0) NumberOfRoom = Random.Range(5, (int)DungeonScale.x + (int)DungeonScale.y-1);

        for(int i=0; i < NumberOfRoom; i++)
        {
            LevelOnDungeon();
            Debug.Log("Create");
        }
    }

    public void LevelOnDungeon()
    {
        HashSet<Vector2Int> vacantPlace = new HashSet<Vector2Int>();
        for (int i = 0; i < spawnedRooms.GetLength(0); i++)
        {
            for (int j = 0; j < spawnedRooms.GetLength(1); j++)
            {
                if (spawnedRooms[i, j] == null) continue;

                if (i > 0 && spawnedRooms[i - 1, j] == null) vacantPlace.Add(new Vector2Int(i - 1, j));
                if (j > 0 && spawnedRooms[i, j - 1] == null) vacantPlace.Add(new Vector2Int(i, j - 1));
                if (i < spawnedRooms.GetLength(0) - 1 && spawnedRooms[i + 1, j] == null) vacantPlace.Add(new Vector2Int(i + 1, j));
                if (j < spawnedRooms.GetLength(0) - 1 && spawnedRooms[i, j + 1] == null) vacantPlace.Add(new Vector2Int(i, j + 1));
            }
        }
        Room newRoom = Instantiate(RoomPref[Random.Range(0, RoomPref.Length)]);

        int limit = 100;

        while (limit-->0)
        {
            Vector2Int position = vacantPlace.ElementAt(Random.Range(0, vacantPlace.Count));
            newRoom.RotateRandom(); 
            if(ConnectRoom(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x, position.y);
                spawnedRooms[position.x, position.y] = newRoom;
                break;
            }

        }

      //  Destroy(newRoom.gameObject);

    }

    public bool ConnectRoom(Room room,Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighboor = new List<Vector2Int>();

        if (room.DoorU != null && p.y<maxY && spawnedRooms[p.x, p.y + 1]?.DoorD != null) neighboor.Add(Vector2Int.up);
        if (room.DoorD != null && p.y>0 && spawnedRooms[p.x, p.y - 1]?.DoorU != null) neighboor.Add(Vector2Int.down);
        if (room.DoorL != null && p.x>0 && spawnedRooms[p.x-1, p.y]?.DoorR != null) neighboor.Add(Vector2Int.left);
        if (room.DoorR != null && p.x<maxX && spawnedRooms[p.x+1, p.y]?.DoorL != null) neighboor.Add(Vector2Int.right);

        if (neighboor.Count == 0) return false;

        Vector2Int SelectedDirection = neighboor[Random.Range(0, neighboor.Count)];
        Room selectedRoom = spawnedRooms[p.x + SelectedDirection.x, p.y + SelectedDirection.y];

        if (SelectedDirection == Vector2Int.up)
        {
            room.DoorU.SetActive(true);
            selectedRoom.DoorD.SetActive(true);
        }
        else if (SelectedDirection == Vector2Int.right)
        {
            room.DoorR.SetActive(true);
            selectedRoom.DoorL.SetActive(true);
        }
        else if (SelectedDirection == Vector2Int.left)
        {
            room.DoorL.SetActive(true);
            selectedRoom.DoorR.SetActive(true);
        }
        else if (SelectedDirection == Vector2Int.down)
        {
            room.DoorD.SetActive(true);
            selectedRoom.DoorU.SetActive(true);
        }

        return true;
    }
}
