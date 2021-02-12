using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHealthBars;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    bool m_dead;

    private void Update()
    {
        if (health > numOfHealthBars)
        {
            health = numOfHealthBars;
        }
        if (health <= 0)
        {
            health = 0;
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
            if (i < numOfHealthBars)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
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

    public bool isDead()
    {
        if(health <= 0)
        {
            return m_dead = true;
        }
        else
        {
            return m_dead = false;
        }

    }

}
