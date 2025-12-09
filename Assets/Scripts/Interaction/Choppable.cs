using UnityEngine;

namespace AchengRestaurant.Interaction
{
    /// <summary>
    /// Handles ingredient cutting logic.
    /// Replaces the current object with a "chopped" version when hit by a knife.
    /// </summary>
    public class Choppable : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("The prefab to spawn when this ingredient is chopped")]
        [SerializeField] private GameObject choppedPrefab;
        
        [Tooltip("Sound to play when chopped")]
        [SerializeField] private AudioClip chopSound;

        private void OnTriggerEnter(Collider other)
        {
            // Check if the object hitting us is a knife
            var knife = other.GetComponent<SimpleKnife>();
            if (knife != null)
            {
                PerformChop();
            }
        }

        private void PerformChop()
        {
            if (choppedPrefab != null)
            {
                // Spawn the chopped version at the same position and rotation
                Instantiate(choppedPrefab, transform.position, transform.rotation);
            }

            // Play sound if available (using PlayClipAtPoint since we are destroying this object)
            if (chopSound != null)
            {
                AudioSource.PlayClipAtPoint(chopSound, transform.position);
            }

            // Destroy the raw ingredient
            Destroy(gameObject);
        }
    }
}