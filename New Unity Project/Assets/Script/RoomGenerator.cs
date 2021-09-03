using UnityEngine;
using System.Collections.Generic;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction {up , down , right , left };
    public Direction direction;

    [Header("房間資訊")]
    public GameObject roomPrefab;
    public int roomNumber;

    public Color startColor, endColor;

    [Header("位置控制")]
    public Transform generatorPoint;
    public float x;
    public float y;
    public LayerMask roomLayer;

    public List<GameObject> rooms = new List<GameObject>();

   void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(roomPrefab, generatorPoint.position, Quaternion.identity));

            //改變point位置
            ChangePointPos();

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
