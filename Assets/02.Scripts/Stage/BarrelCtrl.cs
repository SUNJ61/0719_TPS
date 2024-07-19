using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
�÷��̰� �Ǿ��� �� �跲 ������ �����ϰ� �ٲ�� ���� �����.
5�� �̻� �跲�� �Ѿ˿� ������ ���� ���������� �����ϱ�.
*/
public class BarrelCtrl : MonoBehaviour
{
    [SerializeField] private Texture[] textures;
    private MeshRenderer meshRenderer;
    private Rigidbody rb;
    private GameObject Effect;
    private AudioClip Expclip;

    private int HitCount = 0;
    private readonly string BarTexture = "BarrelTextures";
    private readonly string bulletStr = "BULLET";
    private readonly string EffStr = "ExpEffect";
    private readonly string ExploreSound = "Sound/grenade_exp2";
    private readonly string e_bullettag = "E_BULLET";
    void Start()
    {
        textures = Resources.LoadAll<Texture>(BarTexture); //����ȯ ����� �ϳ�, as�� ���� �ʾƵ� ��, ���⼱ as���� ������
        meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        Effect = Resources.Load(EffStr) as GameObject;
        meshRenderer.material.mainTexture = textures[Random.Range(0,textures.Length)];
        Expclip = Resources.Load(ExploreSound) as AudioClip;
    }
    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == bulletStr || col.gameObject.CompareTag(e_bullettag))
        {
            if(++HitCount == 5)
            {
                ExplosionBarrel();
            }
        }
    }

    private void ExplosionBarrel()
    {
        Vector3 hitPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        GameObject eff = Instantiate(Effect, transform.position, Quaternion.identity); //��ȭ : �̰͵� ������Ʈ Ǯ������ ����
        Destroy(eff, 2.0f); //���� ��ƼŬ ������ �ı�
        SoundManager.S_instance.PlaySound(hitPos, Expclip);
        CameraCtrl.C_instance.CameraMoveOn();

        Collider[] Cols = Physics.OverlapSphere(transform.position, 20f, 1 << 7);
        //�跲 �ڱ��ڽ� ��ġ 20�ݰ�ȿ� �ִ� �跲 ���̾�(7�� ���̾�)�� Cols�迭�� ��´�.
        foreach(Collider col in Cols) //������ ���� �跲 �ݶ��̴��� �� ������.
        {
            Rigidbody rigidbody = col.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.mass = 1.0f;//�跲 ���Ը� ������ �ٲ۴�.
                rigidbody.AddExplosionForce(1000, transform.position, 20.0f, 1200f);//����ٵ𿡼� �����ϴ� ���� �Լ�
            }//AddExplosionForce(���ķ�, ������ġ, ���Ĺݰ�, ���� �ڱ�ġ�� ��) ���� ���
            Invoke("BarrelMassChange", 3.0f);
        }//��, �ֺ� �ݰ濡 �ִ� �跲���� �� ������.
    }
    private void BarrelMassChange()
    {
        Collider[] Cols = Physics.OverlapSphere(transform.position, 20f, 1 << 7);
        //�跲 �ڱ��ڽ� ��ġ 20�ݰ�ȿ� �ִ� �跲 ���̾�(7�� ���̾�)�� Cols�迭�� ��´�.
        foreach (Collider col in Cols) //������ ���� �跲 �ݶ��̴��� �� ������.
        {
            Rigidbody rigidbody = col.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.mass = 60.0f;//�跲 ���Ը� ���̰� �ٲ۴�.
            }
        }
    }
}
