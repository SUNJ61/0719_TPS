using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private GameObject BLDeff;

    private readonly string e_bullettag = "E_BULLET";
    private readonly string BLDeffStr = "Effects/BulletImpactFleshBigEffect";
    void Start()
    {
        BLDeff = Resources.Load<GameObject>(BLDeffStr);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag(e_bullettag))
        {
            col.gameObject.SetActive(false);

            Vector3 pos = col.contacts[0].point;
            Vector3 _Normal = col.contacts[0].normal;
            Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _Normal);

            GameObject BLD = Instantiate(BLDeff, pos, rot);
            Destroy(BLD, 1.0f);
        }
    }
}
