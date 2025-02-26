using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class DataManager : MonoBehaviour
{
    public RankingData[] data; //랭킹 데이터
    public static DataManager Instance;

    //싱글톤 + 씬 이동해도 삭제되지 않고 하나만 유지.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else if(Instance != this)
        {
            Destroy(gameObject);
        }

        //시작할 때부터 데이터 로드하기
        DataManager.Instance.LoadData();

        DontDestroyOnLoad(gameObject);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/";
        string fileName = "Ranking.json";
        Debug.LogFormat("랭킹 파일 경로 : {0}", path);
        if (File.Exists(path + fileName))
        {   
            //랭킹 파일 있으면 로드
            string str = File.ReadAllText(path + fileName);
            data = FromJson<RankingData>(str);

            //정렬
            Array.Sort(data, (a,b) => {
                return (a.score < b.score) ? -1 : 1;
            });

            /*시험 출력 
            foreach (var item in data)
            {
                Debug.LogFormat("name : {0} score : {1}", item.name, item.score);
            }*/
        }
        else
        {
            //랭킹 파일 없으면 새로 생성
            data = new RankingData[5];

            for (int i = 0; i < 5; i++)
            {
                data[i] = new RankingData();
                data[i].name = "";
                data[i].score = 0;

            }

            File.WriteAllText(path + fileName, ToJson(data));
        }

    }

    //랭킹 파일 갱신
    public void SaveData(int nowScore)
    {
        if(nowScore < data[0].score)
        { //갱신해 줄 필요 없음
        }
        else
        {
            bool swap = false;
            for (int i = 1; i < 5; i++)
            {
                if ((data[i-1].score<=nowScore) && (nowScore <= data[i].score))
                {
                    for(int j = i; j >= 1; j--)
                    {
                        int temp = data[j].score;
                        data[j].score = data[j - 1].score; data[j-1].score = temp;
                        data[j - 1].score = nowScore;
                        swap = true;
                    }
                }
            }
            if (!swap)
            {
                //위에서 바뀌지 않았다면
                //nowScore가 제일 크다는 의미. data[5] 에 nowScore 삽입하기
                for (int j = 4; j >= 1; j--)
                {
                    int temp = data[j].score;
                    data[j].score = data[j - 1].score; data[j - 1].score = temp;    
                }
                data[4].score = nowScore;
            }

            //갱신했으면 저장하기
            string path = Application.persistentDataPath + "/";
            string fileName = "Ranking.json";
            File.WriteAllText(path + fileName, ToJson(data));
        }
        
    }

    void compare(RankingData a, RankingData b)
    {

    }

    //원래 jsonUtility api로는 배열 직렬화를 못한다.
    //따라서 배열을 json으로 직렬화하려면 배열을 묶어주는 객체(wrapper)를 만들어 그것을 직렬화한다.
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
