using UnityEngine;

public class player : MonoBehaviour
{
#region ���
[Header("���ʳt��"),Range(0,1000)]
public float speed = 10.5f;
[Header("���D����"),Range(0,3000)]
public int hight = 100;
[Header("��q"), Range(0,100)]
public float HP = 100;
[Header("��q"), Tooltip("�Ψ��x�s�}��O�_�b�a�O�W����T �b�a�O�W true ���b�a�O�W false")]
public bool isGround;

private AudioSource aud;
private Rigidbody2D rig;
private Animator ani;




#endregion
}
