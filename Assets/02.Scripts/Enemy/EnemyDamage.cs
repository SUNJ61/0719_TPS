using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private GameObject bloodEffect;

    private readonly string bulletTag = "BULLET";
    private readonly string BLDeffTag = "Effects/BulletImpactFleshBigEffect";

    private float Hp = 100.0f;
    void Start()
    {
        bloodEffect = Resources.Load<GameObject>(BLDeffTag);
        Hp = Mathf.Clamp(Hp, 0f, 100f);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.collider.CompareTag(bulletTag))
        {
            col.gameObject.SetActive(false);
            ShowBLD_Effect(col);
            Hp -= col.gameObject.GetComponent<Bullet>().Damage;
            if (Hp <= 0)
                Die();
        }
    }

    private void ShowBLD_Effect(Collision col)
    {
        Vector3 pos = col.contacts[0].point;
        //collision����ü �ȿ� contacts��� �迭�� �ִµ� ���⿣ ������ ��ġ�� ����Ǿ� �ִ�.
        Vector3 _Normal = col.contacts[0].normal; // ������Ʈ�� ������ ������ ��´�.
        Quaternion rot = Quaternion.FromToRotation(-transform.forward, _Normal);
        //�� ���⿡�� ������Ʈ�� ���󰡴� �������� ����Ʈ�� ���
        GameObject blood = Instantiate(bloodEffect, pos, rot);
        Destroy(blood, 1.0f);
    }
    void Die()
    {
        GetComponent<EnemyAI>().state = EnemyAI.State.DIE; //���ʹ� ���¸� �����ϴ� ��ũ��Ʈ�� �ҷ��� ���� ����
    }
}
