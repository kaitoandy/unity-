using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projictile : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 10f;

    [SerializeField] public Vector2 moveDirection;

    


    private void Update()
    {
        attack();
    }

    private void OnEnable()
    {
        StartCoroutine(MoveDirectly());
    }


    IEnumerator MoveDirectly()
    {
        while (gameObject.activeSelf)
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void attack()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            moveDirection.x = 1;  
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
       

        if (Input.GetKeyDown(KeyCode.I))
        {
            moveDirection.y = 1;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
       

        if (Input.GetKeyDown(KeyCode.K))
        {
            moveDirection.y = -1;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
       

        if (Input.GetKeyDown(KeyCode.J))
        {
            moveDirection.x = -1;
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
       


    }
}
