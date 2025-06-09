using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameMaster.instance.AddScore(scoreValue);
            gameObject.SetActive(false); // Disable the coin instead of destroying it
        }
    }
}
