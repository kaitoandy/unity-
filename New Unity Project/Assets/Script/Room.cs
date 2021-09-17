using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject doorUp, doorDown, doorRight, doorLift;

    public bool roomUp, roomDown, roomRight, roomLift;

    public int stepToStart;

    public Text text;

    public int doorNumber;

    private void Start()
    {
        doorUp.SetActive(roomUp);
        doorDown.SetActive(roomDown);
        doorRight.SetActive(roomRight);
        doorLift.SetActive(roomLift);
    }


    public void UpdateRoom(float xOffset, float yOffset)
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / xOffset) + Mathf.Abs(transform.position.y / yOffset));

        text.text = stepToStart.ToString();

        if (roomUp)
            doorNumber++;
        if (roomDown)
            doorNumber++;
        if (roomRight)
            doorNumber++;
        if (roomLift)
            doorNumber++;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController.instance.ChangeTarget(transform);
        }
    }
}
