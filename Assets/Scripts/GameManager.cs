using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public GameObject coin;
    public Animator animator;
    public int count;
    public bool isBegin;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitCoin();
        count = 0;
        isBegin = false;
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

    public void Begin()
    {
        animator.SetTrigger("out");
        isBegin = true;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
