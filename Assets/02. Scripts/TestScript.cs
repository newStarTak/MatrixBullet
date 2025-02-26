using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestScript : MonoBehaviour
{
    public GameObject hand_L;
    public GameObject hand_R;
    public GameObject gun_L;
    public GameObject gun_R;
    public TextMeshProUGUI txt;

    void Start()
    {
        Quaternion firstRotation = Quaternion.Euler(20f, 100f, 50f);
        Quaternion secondRotation = Quaternion.Euler(0f, 100f, 0f);

        // 첫 번째 회전값의 역을 계산하고 두 번째 회전값에 곱하여 차이를 나타내는 Quaternion 값을 얻음
        Quaternion rotationDifference = secondRotation * Quaternion.Inverse(firstRotation);

        Debug.Log(rotationDifference.eulerAngles);
        Debug.Log((firstRotation * rotationDifference).eulerAngles);
        Debug.Log((secondRotation * rotationDifference).eulerAngles);

        //m_grabbedObj.transform.rotation * Quaternion.Inverse(gameObject.transform.rotation);
        Debug.Log((Quaternion.Euler(0f, 100f, 0f) * Quaternion.Inverse(Quaternion.Euler(0f, 100f, 0f))).eulerAngles);

        Debug.Log((Quaternion.Euler(0f, 100f, 0f) * Quaternion.Inverse(Quaternion.Euler(0f, 0f, 0f))).eulerAngles);

        Debug.Log((Quaternion.Euler(20f, 100f, 50f) * Quaternion.Inverse(Quaternion.Euler(0f, 100f, 0f))).eulerAngles);

        Debug.Log((Quaternion.Euler(20f, 100f, 50f) * Quaternion.Inverse(Quaternion.Euler(30f, 100f, 200f))).eulerAngles);
        Debug.Log((Quaternion.Euler(30f, 100f, 200f) * Quaternion.Inverse(Quaternion.Euler(20f, 100f, 50f))).eulerAngles);

        Debug.Log((Quaternion.Euler(20f, 100f, 50f) * (Quaternion.Euler(20f, 100f, 50f) * Quaternion.Inverse(Quaternion.Euler(30f, 100f, 200f)))).eulerAngles);
        Debug.Log((Quaternion.Euler(30f, 100f, 200f) * (Quaternion.Euler(30f, 100f, 200f) * Quaternion.Inverse(Quaternion.Euler(20f, 100f, 50f)))).eulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "hand_L " + hand_L.transform.rotation.eulerAngles.ToString() + "\n" +
                    "hand_R " + hand_R.transform.rotation.eulerAngles.ToString() + "\n" +
                    "gun_L " + gun_L.transform.rotation.eulerAngles.ToString() + "\n" +
                    "gun_R " + gun_R.transform.rotation.eulerAngles.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.transform.rotation *= Quaternion.Euler(1f, 1f, 1f);
        }
    }
}
