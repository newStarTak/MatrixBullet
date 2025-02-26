using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Data Controls")]
    public TMP_Text scoreText;
    public TMP_Text timeText;
    private int score = 0;
    private float time = 999.0f;


    public static UIManager Instance;
    public GameObject gameOverHolder;

    //ΩÃ±€≈Ê
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverHolder.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timeText.text = "time : " + string.Format("{0:N2}",time);
        scoreText.text = "score : " + score;

        if(time < 0)
        {
            time = 0.0f;
            gameOverHolder.SetActive(true);
            Time.timeScale = 0;

        }
    }
    
    public void GetScore()
    {
        //≈∏∞Ÿ «œ≥™æø ∏¬√‚ ∂ß ∏∂¥Ÿ 100¡°æø ªÛΩ¬
        score += 100;
    }

    public void BackTitleScene()
    {
        //∑©≈∑ Ω∫ƒ⁄æÓ ∞ªΩ≈
        DataManager.Instance.SaveData(score);
        SceneManager.LoadScene("TitleScene");
    }
}
