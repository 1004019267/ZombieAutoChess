using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    Button btnStart;
    Button btnRank;
    Button btnExit;
    BasekeyState key;
    void Awake()
    {
        btnStart = transform.Find("btnStart").GetComponent<Button>();
        btnRank = transform.Find("btnRank").GetComponent<Button>();
        btnExit = transform.Find("btnExit").GetComponent<Button>();
        key = GetComponent<BasekeyState>();
    }

    // Use this for initialization
    void Start()
    {
        key.registerCallBack(btnStart, btnStartOnClick);
        key.registerCallBack(btnRank, btnRankOnClick);
        key.registerCallBack(btnExit, btnExitOnClick);
    }

    private void btnStartOnClick()
    {
        Debug.Log("start");
    }
    private void btnRankOnClick()
    {
        Debug.Log("rank");
    }
    private void btnExitOnClick()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
