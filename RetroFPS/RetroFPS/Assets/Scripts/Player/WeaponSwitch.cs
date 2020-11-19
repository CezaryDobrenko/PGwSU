using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is responsible for changing weapons
//You can define binds for each weapon
//Add or remove weapon for Player

public class WeaponSwitch : MonoBehaviour {

    public List<Transform> weapons;
    public int initialWeapon;
    public bool autoFill;
    int selectedWeapon;

    private void Awake()
    {
        if(autoFill)
        {
            weapons.Clear();
            foreach (Transform weapon in transform)
                weapons.Add(weapon);
        }
    }

    void Start () {
        selectedWeapon = initialWeapon % weapons.Count;
        UpdateWeapon();
	}
	
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            selectedWeapon = (selectedWeapon + 1) % weapons.Count;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            selectedWeapon = Mathf.Abs(selectedWeapon - 1) % weapons.Count;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedWeapon = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 1)
            selectedWeapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Count > 2)
            selectedWeapon = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4) && weapons.Count > 3)
            selectedWeapon = 3;

        UpdateWeapon();
    }

    void UpdateWeapon()
    {
        for(int i = 0; i < weapons.Count; i++)
        {
            if (i == selectedWeapon)
                weapons[i].gameObject.SetActive(true);
            else
                weapons[i].gameObject.SetActive(false);
        }
    }
}
