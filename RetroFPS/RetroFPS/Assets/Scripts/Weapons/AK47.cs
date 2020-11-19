using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//This is script for handling AK47 weapon
//If you want to create new kind of weapon always 
//add this script to weapon object and modify it

[RequireComponent(typeof(AudioSource))]
public class AK47 : MonoBehaviour
{
    public GameObject bloodSplat;
    public Sprite AK47Hand;
    public Sprite AK47Shot;
    public float AK47Damage;
    public float AK47Range;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptyGunSound;

    public Text ammoText;

    public int ammoAmount;
    public int ammoClipSize;

    public GameObject bulletHole;

    int ammoLeft;
    int ammoClipLeft;

    bool isShot;
    bool isReloading;

    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        ammoLeft = ammoAmount;
        ammoClipLeft = ammoClipSize;
    }

    void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        ammoText.text = ammoClipLeft + " / " + ammoLeft;
        if (Input.GetButtonDown("Fire1") && isReloading == false)
            isShot = true;
        if (Input.GetKeyDown(KeyCode.R) && isReloading == false && ammoClipLeft != ammoClipSize)
            Reload();
    }

    void FixedUpdate()
    {
        Vector2 bulletOffset = Random.insideUnitCircle * DynamicCrosshair.spread;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2 + bulletOffset.x, Screen.height / 2 + bulletOffset.y, 0));
        RaycastHit hit;
        if (isShot == true && ammoClipLeft > 0 && isReloading == false)
        {
            isShot = false;
            DynamicCrosshair.spread += DynamicCrosshair.AK_SHOOTING_SPREAD;
            ammoClipLeft--;
            source.PlayOneShot(shotSound);
            StartCoroutine("shot");
            if (Physics.Raycast(ray, out hit, AK47Range))
            {
                hit.collider.gameObject.SendMessage("AddDamage", AK47Damage, SendMessageOptions.DontRequireReceiver);
                if (hit.transform.CompareTag("Enemy"))
                {
                    Instantiate(bloodSplat, hit.point, Quaternion.identity);
                    var state = hit.collider.gameObject.GetComponent<EnemyStates>();
                    if (state.currentState == state.patrolState || state.currentState == state.alertState)
                        hit.collider.gameObject.SendMessage("HiddenShot", transform.parent.transform.position, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)).transform.parent = hit.collider.gameObject.transform;
                }
            }
        }
        else if (isShot == true && ammoClipLeft <= 0 && isReloading == false)
        {
            isShot = false;
            Reload();
        }
    }

    void Reload()
    {
        int bulletsToReload = ammoClipSize - ammoClipLeft;
        if (ammoLeft >= bulletsToReload)
        {
            StartCoroutine("ReloadWeapon");
            ammoLeft -= bulletsToReload;
            ammoClipLeft = ammoClipSize;
        }
        else if (ammoLeft < bulletsToReload && ammoLeft > 0)
        {
            StartCoroutine("ReloadWeapon");
            ammoClipLeft += ammoLeft;
            ammoLeft = 0;
        }
        else if (ammoLeft <= 0)
        {
            source.PlayOneShot(emptyGunSound);
        }
    }

    IEnumerator ReloadWeapon()
    {
        isReloading = true;
        source.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(2);
        isReloading = false;
    }

    IEnumerator shot()
    {
        GetComponent<SpriteRenderer>().sprite = AK47Shot;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().sprite = AK47Hand;
    }

    public void AddAmmo(int value)
    {
        ammoLeft += value;
    }

}
