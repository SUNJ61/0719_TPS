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
        //collision구조체 안에 contacts라는 배열이 있는데 여기엔 감지된 위치가 저장되어 있다.
        Vector3 _Normal = col.contacts[0].normal; // 오브젝트가 감지된 방향을 얻는다.
        Quaternion rot = Quaternion.FromToRotation(-transform.forward, _Normal);
        //뒷 방향에서 오브젝트가 날라가던 방향으로 이펙트를 출력
        GameObject blood = Instantiate(bloodEffect, pos, rot);
        Destroy(blood, 1.0f);
    }
    void Die()
    {
        GetComponent<EnemyAI>().state = EnemyAI.State.DIE; //에너미 상태를 관리하는 스크립트를 불러와 상태 변경
    }
}
