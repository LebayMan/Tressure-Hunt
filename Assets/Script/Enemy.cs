using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Import TextMeshPro

public class Enemy : MonoBehaviour
{
    public GameObject Chest;
    public GameObject Panel;
    public bool IsDead = false;
    
    private int Dig = 100;
    [SerializeField] private TMP_Text healthText; // Reference ke TextMeshPro UI

    private void Start()
    {
        UpdateHealthText();
    }

    public void TakeDamage(int damage)
    {
        if(IsDead) return; // Prevent taking damage if already dead
        Panel.SetActive(true);
        Dig -= damage;
        UpdateHealthText();

        if (Dig <= 0)
        {
            
            Die();
        }
    }


    private void Die()
    {
        IsDead = true;
        Panel.SetActive(false);
        Chest.SetActive(true);
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = $"Dig: {Dig}";
        }
    }
}

