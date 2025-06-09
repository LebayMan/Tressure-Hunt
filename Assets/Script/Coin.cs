using UnityEngine;

public class Coin : MonoBehaviour
{
    public int scoreValue = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameMaster.instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
