using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collison : MonoBehaviour
{
    public DialogueSystem dialogueSystem;
    void Start()
    {
        
    }

    // Update is called once per frame

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            dialogueSystem.ShowLine();
        }
    }
}
