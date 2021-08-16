using UnityEngine;
/// <summary>
/// ��Z���ĤH��������:��Z������
/// </summary>
/// 

//���O:�����O
// : �᭱�N���O�n�~�Ӫ����O
public class NearEnemy : BaseEnemy
{
    #region ���
    [Header("�����ϰ쪺�첾�P�j�p")]
    public Vector2 checkattackoffset;
    public Vector3 checkattacksize;

    #endregion

    #region �ƥ�
    protected override void OnDrawGizmos()
    {
        //�����O�쥻���{��
        base.OnDrawGizmos();
        Gizmos.color = new Color(1, 0.3f, 0.1f, 0.3f);
        Gizmos.DrawCube(transform.position + 
            transform.right * checkattackoffset.x + 
            transform.up * checkattackoffset.y, 
            checkattacksize);
    }

    protected override void Update()
    {
        base.Update();

        CheckPlayerInAttackArea();
    }
    #endregion

    #region ��k
    private void CheckPlayerInAttackArea()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position + 
            transform.right * checkattackoffset.x + 
            transform.up * checkattackoffset.y, 
            checkattacksize,0, 1 << 7);

        if (hit) state = StateEnemy.attack;
        

    }
    #endregion
}
