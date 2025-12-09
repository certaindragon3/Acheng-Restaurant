using UnityEngine;

namespace AchengRestaurant.Interaction
{
    /// <summary>
    /// Marks an object as a knife for cutting ingredients.
    /// </summary>
    public class SimpleKnife : MonoBehaviour
    {
        [SerializeField] private float cutForceThreshold = 0.5f;
        
        public bool CanCut(Vector3 velocity)
        {
            // Simple check: is the knife moving fast enough?
            // For MVP, we might just return true if it touches.
            return true; 
        }
    }
}