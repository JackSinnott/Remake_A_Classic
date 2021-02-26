using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHealthBars;
    public int mana;
    public int numOfManaBars;

    public Image[] hearts;
    public Image[] manaImages;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite fullMana;
    public Sprite emptyMana;

    private void Update()
    {

        if (health > numOfHealthBars)
        {
            health = numOfHealthBars;
        }
        if (mana > numOfManaBars)
        {
            mana = numOfManaBars;
        }
        if (health <= 0)
        {
            health = 0;
        }
        if (mana <= 0)
        {
            mana = 0;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < mana)
            {
                manaImages[i].sprite = fullMana;
            }
            else
            {
                manaImages[i].sprite = emptyMana;
            }
            if (i < numOfHealthBars)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
            if (i < numOfManaBars)
            {
                manaImages[i].enabled = true;
            }
            else
            {
                manaImages[i].enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            health += 1;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health -= 1;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            mana += 1;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            mana -= 1;
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

    public void heal(int heal)
    {
        health += heal;
    }

    public int getHealth()
    {
        return health;
    }

    public void useMana()
    {
        mana -= 1;
    }

    public void restoreMana(int amount)
    {
        mana += amount;
    }

    public int getMana()
    {
        return mana;
    }
}
