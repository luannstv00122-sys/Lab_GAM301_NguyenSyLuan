using UnityEngine;

public class Coin : MonoBehaviour
{
    public int pointValue = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(pointValue);

            Destroy(gameObject);
        }
    }
}