using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    [Header("Game Mode")]
    public bool isTimeAttack;
    public bool isEndless;

    [Header("Materials")]
    public Material mat_gun_L;
    public Material mat_gun_R;
    public Material mat_grenade_L;
    public Material mat_grenade_R;
    public Material mat_obj_L;
    public Material mat_obj_R;
    public Light light_L;
    public Light light_R;
    public Light light_M;

    [Header("Targets")]
    public GameObject Target_L;
    public GameObject Target_R;
    public GameObject PlusTarget_L;
    public GameObject PlusTarget_R;

    [Header("Bezier Curve - Points")]
    public Transform tr_L;
    public Transform tr_R;
    public Transform tr_M;
    public Transform tr_control_LtoR;
    public Transform tr_control_RtoL;
    public Transform tr_control_MtoL;
    public Transform tr_control_MtoR;

    [Header("Bezier Curve - Calculation List")]
    public Vector3[] curvePoints_LtoR;
    public Vector3[] curvePoints_RtoL;
    public Vector3[] curvePoints_MtoL;
    public Vector3[] curvePoints_MtoR;
    private int count = 99;

    IEnumerator InitializeCoroutine()
    {
        while(true)
        {
            int SingleOrGroup = Random.Range(0, 2);
            int targetType = Random.Range(0, 3);

            SpawnTarget(SingleOrGroup, targetType);

            yield return new WaitForSeconds(4.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mat_gun_L.SetColor("_Color", new Color(0.99f, 0.2f, 0.2f));
        mat_obj_L.SetColor("_Color", new Color(0.99f, 0.2f, 0.2f));
        mat_grenade_L.SetColor("_Color", new Color(0.99f, 0.2f, 0.2f));

        mat_gun_R.SetColor("_Color", new Color(0.2f, 0.2f, 0.99f));
        mat_obj_R.SetColor("_Color", new Color(0.2f, 0.2f, 0.99f));
        mat_grenade_R.SetColor("_Color", new Color(0.2f, 0.2f, 0.99f));

        curvePoints_LtoR = CalculateCurvePoints(count, tr_L, tr_control_LtoR, tr_R, curvePoints_LtoR);
        curvePoints_RtoL = CalculateCurvePoints(count, tr_R, tr_control_RtoL, tr_L, curvePoints_RtoL);
        curvePoints_MtoL = CalculateCurvePoints(count, tr_M, tr_control_MtoL, tr_L, curvePoints_MtoL);
        curvePoints_MtoR = CalculateCurvePoints(count, tr_M, tr_control_MtoR, tr_R, curvePoints_MtoR);

        StartCoroutine(InitializeCoroutine());
    }

    void SpawnTarget(int SingleOrGroup, int targetType)
    {
        if(SingleOrGroup == 0)
        {
            GameObject spawned_target;
            int LorR = Random.Range(0, 2);

            if(LorR == 0)
            {
                spawned_target = Instantiate(Target_L, gameObject.transform.position, Target_L.transform.rotation);
            }
            else
            {
                spawned_target = Instantiate(Target_R, gameObject.transform.position, Target_R.transform.rotation);
            }

            if(targetType == 0)
            {
                spawned_target.GetComponent<TargetCtrl>().isStaticType = true;
            }
            else if(targetType == 1)
            {
                spawned_target.GetComponent<TargetCtrl>().isDynamic = true;
            }
            else if (targetType == 2)
            {
                int bezierType = Random.Range(0, 4);

                if(bezierType == 0)
                {
                    spawned_target.GetComponent<TargetCtrl>().isBezier_LtoR = true;
                }
                else if (bezierType == 1)
                {
                    spawned_target.GetComponent<TargetCtrl>().isBezier_RtoL = true;
                }
                else if (bezierType == 2)
                {
                    spawned_target.GetComponent<TargetCtrl>().isBezier_MtoL = true;
                }
                else if (bezierType == 3)
                {
                    spawned_target.GetComponent<TargetCtrl>().isBezier_MtoR = true;
                }
            }
        }
        else if(SingleOrGroup == 1)
        {
            GameObject spawned_target;
            int LorR = Random.Range(0, 2);

            if (targetType == 0)
            {
                int spawnCount = Random.Range(3, 6);

                while(spawnCount-- > 0)
                {
                    SpawnTarget(0, 0);
                }
            }
            else if (targetType == 1)
            {
                if(LorR == 0)
                {
                    spawned_target = Instantiate(PlusTarget_L, gameObject.transform.position, PlusTarget_L.transform.rotation);
                }
                else
                {
                    spawned_target = Instantiate(PlusTarget_R, gameObject.transform.position, PlusTarget_R.transform.rotation);
                }

                float newX = Random.Range(-3.8f, 9.8f);
                float newY = Random.Range(1.7f, 2f);
                float newZ = Random.Range(7f, 16f);

                spawned_target.transform.position = new Vector3(newX, newY, newZ);
            }
            else if (targetType == 2)
            {
                GameObject spawned_target_local1;
                int LorR_local = Random.Range(0, 2);
                if(LorR_local == 0)
                {
                    spawned_target_local1 = Instantiate(Target_L, gameObject.transform.position, Target_L.transform.rotation);
                }
                else
                {
                    spawned_target_local1 = Instantiate(Target_R, gameObject.transform.position, Target_R.transform.rotation);
                }

                GameObject spawned_target_local2;
                LorR_local = Random.Range(0, 2);
                if (LorR_local == 0)
                {
                    spawned_target_local2 = Instantiate(Target_L, gameObject.transform.position, Target_L.transform.rotation);
                }
                else
                {
                    spawned_target_local2 = Instantiate(Target_R, gameObject.transform.position, Target_R.transform.rotation);
                }

                GameObject spawned_target_local3;
                LorR_local = Random.Range(0, 2);
                if (LorR_local == 0)
                {
                    spawned_target_local3 = Instantiate(Target_L, gameObject.transform.position, Target_L.transform.rotation);
                }
                else
                {
                    spawned_target_local3 = Instantiate(Target_R, gameObject.transform.position, Target_R.transform.rotation);
                }

                GameObject spawned_target_local4;
                LorR_local = Random.Range(0, 2);
                if (LorR_local == 0)
                {
                    spawned_target_local4 = Instantiate(Target_L, gameObject.transform.position, Target_L.transform.rotation);
                }
                else
                {
                    spawned_target_local4 = Instantiate(Target_R, gameObject.transform.position, Target_R.transform.rotation);
                }

                GameObject spawned_target_local5;
                LorR_local = Random.Range(0, 2);
                if (LorR_local == 0)
                {
                    spawned_target_local5 = Instantiate(Target_L, gameObject.transform.position, Target_L.transform.rotation);
                }
                else
                {
                    spawned_target_local5 = Instantiate(Target_R, gameObject.transform.position, Target_R.transform.rotation);
                }

                GameObject spawned_target_local6;
                LorR_local = Random.Range(0, 2);
                if (LorR_local == 0)
                {
                    spawned_target_local6 = Instantiate(Target_L, gameObject.transform.position, Target_L.transform.rotation);
                }
                else
                {
                    spawned_target_local6 = Instantiate(Target_R, gameObject.transform.position, Target_R.transform.rotation);
                }

                int bezierPattern = Random.Range(0, 4);

                if (bezierPattern == 0)
                {
                    spawned_target_local1.GetComponent<TargetCtrl>().isBezier_MtoL = true;
                    spawned_target_local2.GetComponent<TargetCtrl>().isBezier_MtoR = true;
                    spawned_target_local3.GetComponent<TargetCtrl>().isBezier_LtoR = true;
                    spawned_target_local3.GetComponent<TargetCtrl>().spawn_delay = 15f;
                    spawned_target_local4.GetComponent<TargetCtrl>().isBezier_RtoL = true;
                    spawned_target_local4.GetComponent<TargetCtrl>().spawn_delay = 15f;

                    Destroy(spawned_target_local5);
                    Destroy(spawned_target_local6);
                }
                else if (bezierPattern == 1)
                {
                    spawned_target_local1.GetComponent<TargetCtrl>().isBezier_LtoR = true;
                    spawned_target_local2.GetComponent<TargetCtrl>().isBezier_LtoR = true;
                    spawned_target_local2.GetComponent<TargetCtrl>().spawn_delay = 7f;
                    spawned_target_local3.GetComponent<TargetCtrl>().isBezier_LtoR = true;
                    spawned_target_local3.GetComponent<TargetCtrl>().spawn_delay = 12f;

                    Destroy(spawned_target_local4);
                    Destroy(spawned_target_local5);
                    Destroy(spawned_target_local6);
                }
                else if (bezierPattern == 2)
                {
                    spawned_target_local1.GetComponent<TargetCtrl>().isBezier_RtoL = true;
                    spawned_target_local2.GetComponent<TargetCtrl>().isBezier_RtoL = true;
                    spawned_target_local2.GetComponent<TargetCtrl>().spawn_delay = 7f;
                    spawned_target_local3.GetComponent<TargetCtrl>().isBezier_RtoL = true;
                    spawned_target_local3.GetComponent<TargetCtrl>().spawn_delay = 12f;

                    Destroy(spawned_target_local4);
                    Destroy(spawned_target_local5);
                    Destroy(spawned_target_local6);
                }
                else if (bezierPattern == 3)
                {
                    spawned_target_local1.GetComponent<TargetCtrl>().isBezier_MtoL = true;
                    spawned_target_local2.GetComponent<TargetCtrl>().isBezier_MtoR = true;
                    spawned_target_local2.GetComponent<TargetCtrl>().spawn_delay = 4.5f;
                    spawned_target_local3.GetComponent<TargetCtrl>().isBezier_MtoL = true;
                    spawned_target_local3.GetComponent<TargetCtrl>().spawn_delay = 7f;
                    spawned_target_local4.GetComponent<TargetCtrl>().isBezier_MtoR = true;
                    spawned_target_local4.GetComponent<TargetCtrl>().spawn_delay = 9.5f;
                    spawned_target_local5.GetComponent<TargetCtrl>().isBezier_MtoL = true;
                    spawned_target_local5.GetComponent<TargetCtrl>().spawn_delay = 12f;
                    spawned_target_local6.GetComponent<TargetCtrl>().isBezier_MtoR = true;
                    spawned_target_local6.GetComponent<TargetCtrl>().spawn_delay = 14.5f;
                }
            }
        }
    }
    private Vector3[] CalculateCurvePoints(int count_temp, Transform point_start, Transform point_control, Transform point_end, Vector3[] curvePoints_temp)
    {
        Vector3 pA = point_start.position;
        Vector3 pB = point_control.position;
        Vector3 pC = point_end.position;

        curvePoints_temp = new Vector3[count_temp + 1];
        float unit = 1.0f / count_temp;

        int i = 0; float t = 0f;
        for (; i < count_temp + 1; i++, t += unit)
        {
            float u = (1 - t);
            float t2 = t * t;
            float u2 = u * u;

            curvePoints_temp[i] =
                pA * u2 +
                pB * (t * u * 2) +
                pC * t2;
        }

        return curvePoints_temp;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            float newR_L = Random.Range(0.5f, 1f);
            float newG_L = Random.Range(0f, 1f);
            float newB_L = Random.Range(0f, 1f);

            float newR_R = Random.Range(0f, 1f);
            float newG_R = Random.Range(0f, 1f);
            float newB_R = Random.Range(0.5f, 1f);

            mat_gun_L.SetColor("_Color", new Color(newR_L, newG_L, newB_L));
            mat_obj_L.SetColor("_Color", new Color(newR_L, newG_L, newB_L));
            mat_grenade_L.SetColor("_Color", new Color(newR_L, newG_L, newB_L));

            mat_gun_R.SetColor("_Color", new Color(newR_R, newG_R, newB_R));
            mat_obj_R.SetColor("_Color", new Color(newR_R, newG_R, newB_R));
            mat_grenade_R.SetColor("_Color", new Color(newR_R, newG_R, newB_R));

            light_L.color = new Color(newR_L, newG_L, newB_L);
            light_R.color = new Color(newR_R, newG_R, newB_R);
            light_M.color = new Color((newR_L + newR_R) / 2, (newG_L + newG_R) / 2, (newB_L + newB_R) / 2);
        }
    }
}
