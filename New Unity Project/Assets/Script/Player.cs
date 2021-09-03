using UnityEngine;

public class Player : MonoBehaviour
{
    #region ���
    [Header("���ʳt��"), Range(0, 1000)]
    public float speed = 10.5f;
    [Header("��q"), Range(0, 500)]
    public float HP = 200;
    

    //�p�H��줣���
    //�}���ݩʭ��O�����Ҧ� Debug �i�H�ݨ�p�H���
    private AudioSource aud;
    private Rigidbody2D rig;
    private Animator ani;

    
    public Vector2 movement;
    

    private float hValue;

    #endregion


    private void Start()
    {
        //GetComponent <����> �x���k.�i�H���w��������

        //�@��:���o������2D���餸��
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

        
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rig.MovePosition(rig.position + movement * speed * Time.fixedDeltaTime);
   
    }
}
