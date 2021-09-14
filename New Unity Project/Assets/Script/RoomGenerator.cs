using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction {up , down , right , left };
    public Direction direction;

    [Header("房間資訊")]
    public GameObject roomPrefab;
    public int roomNumber;

    public Color startColor, endColor;
    public GameObject endRoom;

    [Header("位置控制")]
    public Transform generatorPoint;
    public float xOffest;
    public float yOffest;
    public LayerMask roomLayer;
    public int maxStep;

    public List<Room> rooms = new List<Room>();

    List<GameObject> farRooms = new List<GameObject>();
    List<GameObject> lessFarRooms = new List<GameObject>();
    List<GameObject> oneWayRooms = new List<GameObject>();

    public Walltype walltype;

    private void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());

            //改變point位置
            ChangePointPosition();

        }


        rooms[0].GetComponent<SpriteRenderer>().color = startColor;

        endRoom = rooms[0].gameObject;

        foreach (var room in rooms)
        {
           

            // if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            // {
            //     endRoom = room.gameObject;
            // }

            SetupRoom(room, room.transform.position);
        }

        FindEndRoom();

        endRoom.GetComponent<SpriteRenderer>().color = endColor;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ChangePointPosition()
    {
        do
        {

            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, yOffest, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -yOffest, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-xOffest, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(xOffest, 0, 0);
                    break;
            }

        } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer));
    }

    public void SetupRoom(Room newRoom,Vector3 roomPosition)
    {
        newRoom.roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffest, 0), 0.2f, roomLayer);
        newRoom.roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffest, 0), 0.2f, roomLayer);
        newRoom.roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffest, 0, 0), 0.2f, roomLayer);
        newRoom.roomLift = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffest, 0, 0), 0.2f, roomLayer);

        newRoom.UpdateRoom(xOffest,yOffest);

        switch (newRoom.doorNumber)
        {
            case 1:
                if (newRoom.roomUp)
                    Instantiate(walltype.singleUp, roomPosition, Quaternion.identity);
                if (newRoom.roomDown)
                    Instantiate(walltype.singleDown, roomPosition, Quaternion.identity);
                if (newRoom.roomRight)
                    Instantiate(walltype.singleRight, roomPosition, Quaternion.identity);
                if (newRoom.roomLift)
                    Instantiate(walltype.singleLift, roomPosition, Quaternion.identity);
                break;
            case 2:
                if (newRoom.roomLift && newRoom.roomUp)
                    Instantiate(walltype.doubleUL, roomPosition, Quaternion.identity);
                if (newRoom.roomLift && newRoom.roomDown)
                    Instantiate(walltype.doubleDL, roomPosition, Quaternion.identity);
                if (newRoom.roomRight && newRoom.roomUp)
                    Instantiate(walltype.doubleUR, roomPosition, Quaternion.identity);
                if (newRoom.roomDown && newRoom.roomUp)
                    Instantiate(walltype.doubleUD, roomPosition, Quaternion.identity);
                if (newRoom.roomLift && newRoom.roomRight)
                    Instantiate(walltype.doubleLR, roomPosition, Quaternion.identity);
                if (newRoom.roomRight && newRoom.roomDown)
                    Instantiate(walltype.doubleDR, roomPosition, Quaternion.identity);
                break;
            case 3:
                if (newRoom.roomLift && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(walltype.tripleURL, roomPosition, Quaternion.identity);
                if (newRoom.roomLift && newRoom.roomDown && newRoom.roomRight)
                    Instantiate(walltype.tripleDRL, roomPosition, Quaternion.identity);
                if (newRoom.roomDown && newRoom.roomUp && newRoom.roomRight)
                    Instantiate(walltype.tripleURD, roomPosition, Quaternion.identity);
                if (newRoom.roomLift && newRoom.roomUp && newRoom.roomDown)
                    Instantiate(walltype.tripleUDL, roomPosition, Quaternion.identity);
                break;
            case 4:
                if (newRoom.roomLift && newRoom.roomUp && newRoom.roomRight && newRoom.roomDown)
                    Instantiate(walltype.fourDoors, roomPosition, Quaternion.identity);
                break;


        }

    }

    public void FindEndRoom()
    {
        //最大值房間
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].stepToStart > maxStep)
                maxStep = rooms[i].stepToStart;
        }

        //最大值房間和次大值房間
        foreach (var room in rooms)
        {
            if (room.stepToStart == maxStep)
                farRooms.Add(room.gameObject);
            if(room.stepToStart == maxStep -1)
                lessFarRooms.Add(room.gameObject);
        }

        for (int i = 0; i < farRooms.Count; i++)
        {
            if (farRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(farRooms[i]);
        }

        for (int i = 0; i < lessFarRooms.Count; i++)
        {
            if (lessFarRooms[i].GetComponent<Room>().doorNumber == 1)
                oneWayRooms.Add(lessFarRooms[i]);
        }

        if(oneWayRooms.Count != 0)
        {
            endRoom = oneWayRooms[Random.Range(0, oneWayRooms.Count)];
        }
        else
        {
            endRoom = farRooms[Random.Range(0, farRooms.Count)];
        }
        

    }
}

[System.Serializable]
public class Walltype

{
    public GameObject singleUp, singleDown, singleLift, singleRight,
                      doubleUR, doubleUL, doubleUD, doubleLR, doubleDL, doubleDR,
                      tripleURL, tripleUDL, tripleURD, tripleDRL,
                      fourDoors;



}