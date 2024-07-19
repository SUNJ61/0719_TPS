using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    private Transform FirePos;
    private AudioSource Source;
    private AudioClip fireClip;
    private Player player;
    [SerializeField]private ParticleSystem MuzzleFlash; //직접 넣음.

    private float fireTime;
    private string firepos = "P_FirePos";
    private string fireClipStr = "Sound/p_ak_1";
    void Start()
    {
        FirePos = GameObject.Find(firepos).transform.GetComponent<Transform>();
        Source = GetComponent<AudioSource>();
        player = GetComponent<Player>();
        fireClip = Resources.Load(fireClipStr) as AudioClip;
        fireTime = Time.time;
        MuzzleFlash.Stop();
    }
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    Fire();
        //}

        if (Input.GetMouseButtonDown(0))
        {
            if (!player.isRun)
                OneFire();
            MuzzleFlash.Play();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            MuzzleFlash.Stop();
        }
    }

    private void Fire()
    {
        if (Time.time - fireTime > 0.2f)
        {
            //Instantiate(Bullet, FirePos.position, FirePos.rotation); //오브젝트 풀링이 아닐 때
            var _bullet = ObjectPoolingManager.poolingManager.GetBulletPool();
            if (_bullet != null)
            {
                _bullet.transform.position = FirePos.position;
                _bullet.transform.rotation = FirePos.rotation;
                _bullet.SetActive(true);
            }
            Source.PlayOneShot(fireClip, 0.2f);
            fireTime = Time.time;
        }
    }
    private void OneFire()
    {
        var _bullet = ObjectPoolingManager.poolingManager.GetBulletPool();
        if (_bullet != null)
        {
            _bullet.transform.position = FirePos.position;
            _bullet.transform.rotation = FirePos.rotation;
            _bullet.SetActive(true);
        }
        Source.PlayOneShot(fireClip, 0.2f);
    }
}
