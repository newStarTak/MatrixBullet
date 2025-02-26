using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RankingData 
{
    public string name;
    public int score;
    
    public void printData()
    {
        Debug.Log(name);
        Debug.Log(score);
    }
}
