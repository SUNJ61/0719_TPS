using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCtrl : MonoBehaviour
{
    private BoxCollider boxCol;
    private GameObject Explore;
    //private AudioSource source; //������Ŵ����� ��ü
    private AudioClip ExploreClip;

    private string ExploreSound = "Sound/grenade_exp2";
    private string TinyFlames = "TinyFlames";
    private string bullet = "BULLET";
    private string e_bullet = "E_BULLET";
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        //source = GetComponent<AudioSource>(); //������Ŵ����� ��ü
        ExploreClip = Resources.Load(ExploreSound) as AudioClip;
        Explore = Resources.Load(TinyFlames) as GameObject;
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == bullet || col.gameObject.tag == e_bullet)
        {
            Vector3 hitPos = col.transform.position;
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hitPos.normalized);
            GameObject eff = Instantiate(Explore, hitPos, rot);
            Destroy(eff, 1.0f);
            //Destroy(col.gameObject);
            col.gameObject.SetActive(false); //���� ������ �Ѿ� ��Ȱ��ȭ
            //source.PlayOneShot(ExploreClip, 1.0f); ����� �Ŵ����� ��ü
            SoundManager.S_instance.PlaySound(hitPos, ExploreClip);
        }
    }
}
