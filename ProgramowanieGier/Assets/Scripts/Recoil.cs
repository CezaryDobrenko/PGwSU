using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{

    public static float spread = 0;
    public const int PISTOL_SHOTTING_SPREAD = 25;
    public const int PISTOL_SHOTTING_JUMP = 75;
    public const int PISTOL_SHOTTING_WALK = 35;
    public const int PISTOL_SHOTTING_RUN = 55;

    public GameObject crosshair;
    GameObject topPart;
    GameObject bottomPart;
    GameObject leftPart;
    GameObject rightPart;

    private float initialPosition;

    void Start()
    {
        topPart = crosshair.transform.FindChild("TopPart").gameObject;
        bottomPart = crosshair.transform.FindChild("BottomPart").gameObject;
        leftPart = crosshair.transform.FindChild("LeftPart").gameObject;
        rightPart = crosshair.transform.FindChild("RightPart").gameObject;

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
            spread -= 0.5f;
        }
    }


}
