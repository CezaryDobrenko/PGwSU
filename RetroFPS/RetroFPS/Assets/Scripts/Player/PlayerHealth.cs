using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//this script is core for player object
//ITS ESSENTIAL TO ALWAYS attach it to player
//you can define starthealt,maxhealth, armor

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int maxArmor;
    public AudioClip hit;
    public FlashScreen flash;
    AudioSource source;
    public Text HPtext;
    public Text Armortext;
    bool isGameOver = false;
    [SerializeField]
    float armor;
    [SerializeField]
    float health;

    void Start()
    {
        armor = 0;
        health = maxHealth;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HPtext.text = "" + health;
        Armortext.text = "" + armor;
        armor = Mathf.Clamp(armor, 0, maxArmor);
        health = Mathf.Clamp(health, -Mathf.Infinity, maxHealth);
        if (health <= 0 && !isGameOver)
        {
            isGameOver = true;
            GameManager.Instance.PlayerDeath();
        }
    }

    public void AddHealth(float value)
    {
        health += value;
    }

    public void YouWon()
    {
        GameManager.Instance.PlayerWon();
    }

    public void AddArmor(float value)
    {
        armor += value;
    }

    void EnemyHit(float damage)
    {
        if (armor > 0 && armor >= damage)
        {
            armor -= damage;
        }
        else if (armor > 0 && armor < damage)
        {
            damage -= armor;
            armor = 0;
            health -= damage;
        }
        else
        {
            health -= damage;
        }
        source.PlayOneShot(hit);
        flash.TookDamage();
    }
}