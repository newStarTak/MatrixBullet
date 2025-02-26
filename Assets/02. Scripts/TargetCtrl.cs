using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCtrl : MonoBehaviour
{
    private GameMgr gameMgr;

    [Header("Target Type")]
    public bool isStaticType;
    public bool isDynamic;
    public bool isBezier_LtoR;
    public bool isBezier_RtoL;
    public bool isBezier_MtoL;
    public bool isBezier_MtoR;
    public float spawn_delay;

    private int dynamic_type;
    private int whereIsFirst;

    [Header("Target Features")]
    public float speed_target = 5f;
    public float lifeTime_target = 100f;

    [Header("Particle System")]
    public ParticleSystem target_hit;

    [Header("SFX")]
    public AudioClip target_hit_sfx;
    public AudioClip target_lost_sfx;

    private int curIndex = 0;
    private int count = 99;

    // Start is called before the first frame update
    void Start()
    {
        gameMgr = GameObject.FindGameObjectWithTag("GAMEMGR").GetComponent<GameMgr>();

        Destroy(gameObject, lifeTime_target);

        if (isStaticType || isDynamic)
        {
            float newX = Random.Range(-3.8f, 9.8f);
            float newY = Random.Range(0.6f, 3.1f);
            float newZ = Random.Range(7f, 16f);

            gameObject.transform.position = new Vector3(newX, newY, newZ);

            if(isDynamic)
            {
                dynamic_type = Random.Range(0, 2);
                whereIsFirst = Random.Range(0, 2);
            }
        }
        else if(isBezier_LtoR)
        {
            gameObject.transform.position = gameMgr.tr_L.position - new Vector3(0, spawn_delay, 0);
        }
        else if(isBezier_RtoL)
        {
            gameObject.transform.position = gameMgr.tr_R.position - new Vector3(0, spawn_delay, 0);
        }
        else if(isBezier_MtoL || isBezier_MtoR)
        {
            gameObject.transform.position = gameMgr.tr_M.position - new Vector3(0, spawn_delay, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isStaticType)
        {
            // none
        }
        else if(isDynamic)
        {
            if(dynamic_type == 0)
            {                
                if(whereIsFirst == 0)
                {
                    transform.Translate(Vector3.right * speed_target * Time.deltaTime);

                    if(transform.position.x >= 9.8f)
                    {
                        whereIsFirst = 1;
                    }
                }
                else if (whereIsFirst == 1)
                {
                    transform.Translate(Vector3.left * speed_target * Time.deltaTime);

                    if (transform.position.x <= -3.8f)
                    {
                        whereIsFirst = 0;
                    }
                }
            }
            else if(dynamic_type == 1)
            {
                if (whereIsFirst == 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y * 1.03f, transform.position.z);

                    if (transform.position.y >= 3.1f)
                    {
                        whereIsFirst = 1;
                    }
                }
                else if (whereIsFirst == 1)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y * 0.97f, transform.position.z);

                    if (transform.position.y <= 0.6f)
                    {
                        whereIsFirst = 0;
                    }
                }
            }
        }
        else if(isBezier_LtoR)
        {
            Vector3 targetPosition = gameMgr.curvePoints_LtoR[curIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed_target * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                if(curIndex < count)
                {
                    curIndex++;
                }
                else if(curIndex == count)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (isBezier_RtoL)
        {
            Vector3 targetPosition = gameMgr.curvePoints_RtoL[curIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                if (curIndex < count)
                {
                    curIndex++;
                }
                else if (curIndex == count)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (isBezier_MtoL)
        {
            Vector3 targetPosition = gameMgr.curvePoints_MtoL[curIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                if (curIndex < count)
                {
                    curIndex++;
                }
                else if (curIndex == count)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (isBezier_MtoR)
        {
            Vector3 targetPosition = gameMgr.curvePoints_MtoR[curIndex];
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);

            if (transform.position == targetPosition)
            {
                if (curIndex < count)
                {
                    curIndex++;
                }
                else if (curIndex == count)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (gameObject.CompareTag("TARGET_L") && coll.gameObject.CompareTag("BULLET_L"))
        {
            UIManager.Instance.GetScore();

            GameObject audioSourceGO = new GameObject("TargetAudioSource");
            audioSourceGO.transform.position = transform.position;
            AudioSource audioSource = audioSourceGO.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
            audioSource.PlayOneShot(target_hit_sfx, 2f);
            Destroy(audioSourceGO, 1f);

            Destroy(gameObject);
            Destroy(coll.gameObject);

            ParticleSystem target_hit_particle = Instantiate(target_hit, transform.position, transform.rotation);
            Destroy(target_hit_particle, 1.0f);
        }
        else if (gameObject.CompareTag("TARGET_R") && coll.gameObject.CompareTag("BULLET_R"))
        {
            UIManager.Instance.GetScore();

            GameObject audioSourceGO = new GameObject("TargetAudioSource");
            audioSourceGO.transform.position = transform.position;
            AudioSource audioSource = audioSourceGO.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
            audioSource.PlayOneShot(target_hit_sfx, 2f);
            Destroy(audioSourceGO, 1f);

            Destroy(gameObject);
            Destroy(coll.gameObject);

            ParticleSystem target_hit_particle = Instantiate(target_hit, transform.position, transform.rotation);
            Destroy(target_hit_particle, 1.0f);
        }
    }
}