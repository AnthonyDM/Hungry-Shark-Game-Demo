using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Fish"))
        {
            Fish fish = other.GetComponent<Fish>();

            if (fish.badFish)
            {
                GameManager.Instance.LoseLife();
            }
            else
            {
                GameManager.Instance.GainScore(fish.scoreAmount);
            }

            Destroy(other.gameObject);
        }
    }
}
