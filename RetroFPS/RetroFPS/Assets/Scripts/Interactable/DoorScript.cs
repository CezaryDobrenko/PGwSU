using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is responsible for door handling

public class DoorScript : MonoBehaviour {

    public bool slide;
    public float speed;
    public KeyCode openningKey;
    Vector3 endPosition;
    Vector3 startPosition;
    GameObject doors;
    bool isOpen = false;

    private void Awake()
    {
        doors = transform.Find("Doors").gameObject;
        startPosition = doors.transform.position;
        endPosition = doors.transform.position;
        endPosition.y -= 6.5f;
        endPosition.x -= 0.1f;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(Input.GetKeyDown(openningKey))
            {
                if(slide)
                {
                    StartCoroutine(SlideDoors());
                }
            }
        }
    }

    IEnumerator SlideDoors()
    {
        Vector3 current = doors.transform.position;
        Vector3 destination = isOpen ? startPosition : endPosition;
        isOpen = !isOpen;
        float t = 0f;
        while ( t < 1 )
        {
            t += Time.deltaTime * speed;
            doors.transform.position = Vector3.Lerp(current, destination, t);
            yield return null;
        } 
    }

}
