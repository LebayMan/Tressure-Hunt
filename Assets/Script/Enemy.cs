using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Import TextMeshPro

public class Enemy : MonoBehaviour
{
    public GameObject Chest;
    public GameObject Panel;
    public bool IsDead = false;
    public GameMaster GameMaster; // Reference to GameMaster script
    
    private int Dig = 100;
    [SerializeField] private TMP_Text healthText; // Reference ke TextMeshPr
    public PlayerTypingChallenge PlayerAttack; // Reference to PlayerAttack script   

    public void Reset()
    {
        IsDead = false;
        Dig = 100;
        Panel.SetActive(false);
        Chest.SetActive(false);
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
        PlayerAttack.EndChallenge();
        IsDead = true;
        GameMaster.Won();
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

