using UnityEngine;

public class LeamLerp : MonoBehaviour
{
    public float a = 0, b = 10f;
    public float result;

    public float c = 0, d = 100;

    public Vector2 v2A = Vector2.zero;
    public Vector2 v2B = Vector2.one * 100;


    private void Start()
    {
        //�{�Ѵ��� Lerp :���o���I������
        //���G = �ƾ�.����(A�I , B�I, �ʤ��� 0 - 1)
        result = Mathf.Lerp(a, b, 0.5f);

        

    }

    private void Update()
    {
        c = Mathf.Lerp(c, d, 0.5f * Time.deltaTime);
        v2A = Vector2.Lerp(v2A, v2B, 0.8f * Time.deltaTime);


    }
}
