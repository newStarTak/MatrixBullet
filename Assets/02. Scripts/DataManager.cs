using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class DataManager : MonoBehaviour
{
    public RankingData[] data; //��ŷ ������
    public static DataManager Instance;

    //�̱��� + �� �̵��ص� �������� �ʰ� �ϳ��� ����.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }else if(Instance != this)
        {
            Destroy(gameObject);
        }

        //������ ������ ������ �ε��ϱ�
        DataManager.Instance.LoadData();

        DontDestroyOnLoad(gameObject);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/";
        string fileName = "Ranking.json";
        Debug.LogFormat("��ŷ ���� ��� : {0}", path);
        if (File.Exists(path + fileName))
        {   
            //��ŷ ���� ������ �ε�
            string str = File.ReadAllText(path + fileName);
            data = FromJson<RankingData>(str);

            //����
            Array.Sort(data, (a,b) => {
                return (a.score < b.score) ? -1 : 1;
            });

            /*���� ��� 
            foreach (var item in data)
            {
                Debug.LogFormat("name : {0} score : {1}", item.name, item.score);
            }*/
        }
        else
        {
            //��ŷ ���� ������ ���� ����
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

    //��ŷ ���� ����
    public void SaveData(int nowScore)
    {
        if(nowScore < data[0].score)
        { //������ �� �ʿ� ����
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
                //������ �ٲ��� �ʾҴٸ�
                //nowScore�� ���� ũ�ٴ� �ǹ�. data[5] �� nowScore �����ϱ�
                for (int j = 4; j >= 1; j--)
                {
                    int temp = data[j].score;
                    data[j].score = data[j - 1].score; data[j - 1].score = temp;    
                }
                data[4].score = nowScore;
            }

            //���������� �����ϱ�
            string path = Application.persistentDataPath + "/";
            string fileName = "Ranking.json";
            File.WriteAllText(path + fileName, ToJson(data));
        }
        
    }

    void compare(RankingData a, RankingData b)
    {

    }

    //���� jsonUtility api�δ� �迭 ����ȭ�� ���Ѵ�.
    //���� �迭�� json���� ����ȭ�Ϸ��� �迭�� �����ִ� ��ü(wrapper)�� ����� �װ��� ����ȭ�Ѵ�.
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
