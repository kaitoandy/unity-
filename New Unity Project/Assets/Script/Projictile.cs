using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projictile : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 10f;

    [SerializeField] public Vector2 moveDirection;

    


    private void Update()
    {
        
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

   
}
