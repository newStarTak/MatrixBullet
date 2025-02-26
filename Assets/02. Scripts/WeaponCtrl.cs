using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCtrl : MonoBehaviour
{
    private Rigidbody rb;

    [Header("VFX")]
    public ParticleSystem grenade_boom_vfx;

    [Header("SFX")]
    public AudioSource ausrc;
    public AudioClip gun_respawn_sfx;
    public AudioClip grenade_boom_sfx;
    public AudioClip grenade_respawn_sfx;

    // Start is called before the first frame update
    void Start()
    {
        ausrc = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator GrenadeBoom()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject[] targets;

        if(gameObject.CompareTag("GRENADE_L"))
        {
            targets = GameObject.FindGameObjectsWithTag("TARGET_L");
        }
        else
        {
            targets = GameObject.FindGameObjectsWithTag("TARGET_R");
        }

        foreach(GameObject target in targets)
        {
            Destroy(target);
        }

        ausrc.PlayOneShot(grenade_boom_sfx);
        ParticleSystem grenade_boom_particle = Instantiate(grenade_boom_vfx, transform.position, transform.rotation);
        Destroy(grenade_boom_particle, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= 0.2)
        {
            rb.velocity = new Vector3(0, 0, 0);

            if(gameObject.CompareTag("GUN_L"))
            {
                ausrc.PlayOneShot(gun_respawn_sfx);
                transform.position = new Vector3(2.8f, 1.5f, 1.6f);
            }
            else if (gameObject.CompareTag("GUN_R"))
            {
                ausrc.PlayOneShot(gun_respawn_sfx);
                transform.position = new Vector3(3.2f, 1.5f, 1.6f);
            }
            else if (gameObject.CompareTag("GRENADE_L"))
            {
                ausrc.PlayOneShot(grenade_respawn_sfx);
                transform.position = new Vector3(2.95f, 1.5f, 1.6f);
            }
            else if (gameObject.CompareTag("GRENADE_R"))
            {
                ausrc.PlayOneShot(grenade_respawn_sfx);
                transform.position = new Vector3(3.05f, 1.5f, 1.6f);
            }
        }
    }
}
