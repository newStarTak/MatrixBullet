using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCtrl : MonoBehaviour
{
    [Header("Controller Grap State")]
    public bool controller_L_gun_L_grabbed = false;
    public bool controller_L_gun_R_grabbed = false;
    public bool controller_R_gun_L_grabbed = false;
    public bool controller_R_gun_R_grabbed = false;

    [Header("Bullet Setting")]
    public GameObject bullet_L;
    public GameObject bullet_R;
    public GameObject firePos_L;
    public GameObject firePos_R;
    public ParticleSystem muzzleFlash;

    [Header("SFX")]
    public AudioSource auSrc;

    void Start()
    {
        auSrc = GetComponent<AudioSource>();
    }

    void Hand_L_Grap_Gun_L_Toggle(bool grabbable_gun_L_grabbed)
    {
        if (grabbable_gun_L_grabbed)
        {
            controller_L_gun_L_grabbed = true;
        }
        else
        {
            controller_L_gun_L_grabbed = false;
        }
    }

    void Hand_L_Grap_Gun_R_Toggle(bool grabbable_gun_L_grabbed)
    {
        if (grabbable_gun_L_grabbed)
        {
            controller_L_gun_R_grabbed = true;
        }
        else
        {
            controller_L_gun_R_grabbed = false;
        }
    }

    void Hand_R_Grap_Gun_L_Toggle(bool grabbable_gun_R_grabbed)
    {
        if (grabbable_gun_R_grabbed)
        {
            controller_R_gun_L_grabbed = true;
        }
        else
        {
            controller_R_gun_L_grabbed = false;
        }
    }

    void Hand_R_Grap_Gun_R_Toggle(bool grabbable_gun_R_grabbed)
    {
        if (grabbable_gun_R_grabbed)
        {
            controller_R_gun_R_grabbed = true;
        }
        else
        {
            controller_R_gun_R_grabbed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (controller_L_gun_L_grabbed)
            {
                Instantiate(bullet_L, firePos_L.transform.position, gameObject.transform.rotation);

                auSrc.Play();

                ParticleSystem muzzleFlash_particle = Instantiate(muzzleFlash, firePos_L.transform.position, transform.rotation);
                Destroy(muzzleFlash_particle, 0.5f);
            }
            else if (controller_L_gun_R_grabbed)
            {
                Instantiate(bullet_R, firePos_L.transform.position, gameObject.transform.rotation);

                auSrc.Play();

                ParticleSystem muzzleFlash_particle = Instantiate(muzzleFlash, firePos_L.transform.position, transform.rotation);
                Destroy(muzzleFlash_particle, 0.5f);
            }
        }
        else if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (controller_R_gun_L_grabbed)
            {
                Instantiate(bullet_L, firePos_R.transform.position, gameObject.transform.rotation);

                auSrc.Play();

                ParticleSystem muzzleFlash_particle = Instantiate(muzzleFlash, firePos_R.transform.position, transform.rotation);
                Destroy(muzzleFlash_particle, 0.5f);
            }
            else if (controller_R_gun_R_grabbed)
            {
                Instantiate(bullet_R, firePos_R.transform.position, gameObject.transform.rotation);

                auSrc.Play();

                ParticleSystem muzzleFlash_particle = Instantiate(muzzleFlash, firePos_R.transform.position, transform.rotation);
                Destroy(muzzleFlash_particle, 0.5f);
            }
        }
    }
}
