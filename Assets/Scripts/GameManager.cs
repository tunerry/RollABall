using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public GameObject coin;
    public Animator animator;
    public Text txt_time;
    public Text txt_rank;
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
            animator.SetTrigger("end");
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

    public void Retry()
    {
        animator.SetTrigger("retry");
        SceneManager.LoadScene(0);
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
        scoreList = new List<string>(strs);
        scoreList.Add(txt_time.text);
        List<float> intscore = new List<float>();
        foreach(string str in scoreList)
            intscore.Add(Convert.ToSingle(str));
        intscore.Sort();
        if (intscore.Count > 10)
            intscore.Remove(intscore[intscore.Count - 1]);
        int num = 1;
        txt_rank.text = "";
        foreach (float str in intscore)
        {
            if (num % 10 != num)
                txt_rank.text += "      " + num.ToString() + "                                  " + str.ToString() + "\n";
            else
                txt_rank.text += "      " + num.ToString() + "                                   " + str.ToString() + "\n";
            num += 1;
        }
        scoreList.Clear();
        foreach (float ff in intscore)
            scoreList.Add(ff.ToString());
        strs = scoreList.ToArray();
        File.WriteAllLines(curPath, strs);
    }
}
