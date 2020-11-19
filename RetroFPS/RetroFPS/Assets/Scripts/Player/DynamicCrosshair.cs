using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is responsible for crosshair handling
//You can define constants spread for your weapons
//Increase or reduce recoil

public class DynamicCrosshair : MonoBehaviour
{

    static public float spread = 0;

    public const int PISTOL_SHOOTING_SPREAD = 30;
    public const int Barrel_SHOOTING_SPREAD = 130;
    public const int AK_SHOOTING_SPREAD = 30;
    public const int JUMP_SPREAD = 60;
    public const int WALK_SPREAD = 20;
    public const int RUN_SPREAD = 35;

    public GameObject crosshair;
    GameObject topPart;
    GameObject bottomPart;
    GameObject leftPart;
    GameObject rightPart;

    float initialPosition;

    void Start()
    {
        topPart = crosshair.transform.Find("Top").gameObject;
        bottomPart = crosshair.transform.Find("Bot").gameObject;
        leftPart = crosshair.transform.Find("Left").gameObject;
        rightPart = crosshair.transform.Find("Right").gameObject;

        initialPosition = topPart.GetComponent<RectTransform>().localPosition.y;
    }

    void Update()
    {
        if (spread != 0)
        {
            topPart.GetComponent<RectTransform>().localPosition = new Vector3(0, initialPosition + spread, 0);
            bottomPart.GetComponent<RectTransform>().localPosition = new Vector3(0, -(initialPosition + spread), 0);
            leftPart.GetComponent<RectTransform>().localPosition = new Vector3(-(initialPosition + spread), 0, 0);
            rightPart.GetComponent<RectTransform>().localPosition = new Vector3(initialPosition + spread, 0, 0);
            spread -= 1;
        }
    }
}