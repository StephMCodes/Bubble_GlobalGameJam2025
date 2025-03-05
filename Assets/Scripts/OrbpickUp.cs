using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrbpickUp : MonoBehaviour
{
    private OrbManager orbManager;

    private void Start()
    {
        orbManager = FindObjectOfType<OrbManager>(); // Find the OrbManager in the scene
    }

    private void OnTriggerEnter2D(Collider2D other) // For 2D
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collected an orb.");
            if (orbManager != null)
            {
                orbManager.CollectOrb(); // Notify the OrbManager
            }

            gameObject.SetActive(false);  // the orb disappears
        }
    }
}
