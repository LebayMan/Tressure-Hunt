using UnityEngine;

public class Key : MonoBehaviour
{
    public bool doeskeygetpickedup = false;

    void Start()
    {

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            doeskeygetpickedup = true;
            gameObject.SetActive(false);
        }
    }
    public void ResetKey()
    {
        doeskeygetpickedup = false;
        gameObject.SetActive(true);
    }
}
