using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TitleManager : MonoBehaviour
{
    public GameObject mainMenuHolder;
    public GameObject rankingMenuHolder;
    public GameObject peopleMenuHolder;
    public GameObject gameSelectMenuHolder;

    public TMP_Text[] RankingList;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu();
    }

    public void endlessGameStart()
    {
        SceneManager.LoadScene("sc_endless");
    }

    public void timeAttackGameStart()
    {
        SceneManager.LoadScene("sc_timeAttack");
    }


    public void mainMenu()
    {
        mainMenuHolder.SetActive(true);
        rankingMenuHolder.SetActive(false);
        peopleMenuHolder.SetActive(false);
        gameSelectMenuHolder.SetActive(false);




    }
    public void rankingMenu() 
    {
        mainMenuHolder.SetActive(false);
        rankingMenuHolder.SetActive(true);
        peopleMenuHolder.SetActive(false);
        gameSelectMenuHolder.SetActive(false);

        
        
        for(int i = 0; i < DataManager.Instance.data.Length; i++)
        {
            RankingList[i].text = DataManager.Instance.data[i].score.ToString();
        }
        //DataManager.Instance.data;
    }

    public void peopleMenu()
    {
        mainMenuHolder.SetActive(false);
        rankingMenuHolder.SetActive(false);
        peopleMenuHolder.SetActive(true);
        gameSelectMenuHolder.SetActive(false);
    }

    public void gameSelectMenu()
    {
        mainMenuHolder.SetActive(false);
        rankingMenuHolder.SetActive(false);
        peopleMenuHolder.SetActive(false);
        gameSelectMenuHolder.SetActive(true);
    }
}
