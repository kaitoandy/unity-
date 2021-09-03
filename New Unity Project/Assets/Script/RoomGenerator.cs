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
    private GameObject endRoom;

    [Header("位置控制")]
    public Transform generatorPoint;
    public float x;
    public float y;
    public LayerMask roomLayer;

    public List<GameObject> rooms = new List<GameObject>();

   private void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity));

            //改變point位置
            ChangePointPos();

        }


        rooms[0].GetComponent<SpriteRenderer>().color = startColor;

        endRoom = rooms[0];
        foreach (var room in rooms)
        {
            if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)
            {
                endRoom = room;
            }
        }
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ChangePointPos()
    {
        do
        {


            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, y, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -y, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-x, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(x, 0, 0);
                    break;
            }
        } while (Physics2D.OverlapCircle(generatorPoint.position, 0.2f, roomLayer));
    }

}
