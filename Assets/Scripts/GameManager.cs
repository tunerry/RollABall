using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public GameObject coin;
    public Animator animator;
    public Text txt_time;
    
    public int count;
    public bool isBegin;
    public bool isEnd;

    private float begin_time;
    private List<string> scoreList = new List<string>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitCoin();
        count = 0;
        isBegin = false;
        isEnd = false;
    }

    private void Update()
    {
        if (isEnd)
        {
            isEnd = false;
            Rank();
        }

        if (!isBegin)
            return;
        txt_time.text = Math.Round(Time.time - begin_time, 2).ToString();
        if (count >= 12)
        {
            count = 0;
            isEnd = true;
            isBegin = false;
        }
    }

    public void Begin()
    {
        animator.SetTrigger("out");
        isBegin = true;
        begin_time = Time.time;
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void InitCoin()
    {
        Vector3 origin = new Vector3(-6f, 0.5f, 0f);
        bool x = true;
        bool z = true;
        for (int i = 0; i < 12; ++i)
        {
            Instantiate(coin, origin, coin.transform.rotation);
            if (x)
            {
                if (origin.x != 6f)
                {
                    origin.x += 2f;
                }
                else
                {
                    x = false;
                    origin.x -= 2f;
                }
            }
            else
            {
                origin.x -= 2f;
            }
            if (z)
            {
                if (origin.z != 6f)
                {
                    origin.z += 2f;
                }
                else
                {
                    z = false;
                    origin.z -= 2f;
                }
            }
            else
            {
                if (origin.z != -6f)
                {
                    origin.z -= 2f;
                }
                else
                {
                    z = true;
                    origin.z += 2f;
                }
            }
        }
    }

    private void Rank()
    {
        string curPath = Environment.CurrentDirectory + @"\rank.txt";
        if (!File.Exists(curPath))
            File.Create(curPath);
        string[] strs = File.ReadAllLines(curPath);
        scoreList = new List<string>(strs)
        {
            txt_time.text
        };
        scoreList.Sort();
        if (scoreList.Count > 10)
        {
            scoreList.Remove(scoreList[scoreList.Count - 1]);
        }
        foreach (string str in scoreList)
            Debug.Log(str);
        strs = scoreList.ToArray();
        File.WriteAllLines(curPath, strs);
    }
}
