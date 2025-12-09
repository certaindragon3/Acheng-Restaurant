using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Collections;

namespace AchengRestaurant.Interaction
{
    [RequireComponent(typeof(XRSocketInteractor))]
    public class CookingPot : MonoBehaviour
    {
        [Header("Cooking Settings")]
        [SerializeField] private float cookingTime = 3.0f;
        [SerializeField] private ParticleSystem cookingParticles;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip cookingSound;
        [SerializeField] private AudioClip doneSound;

        private XRSocketInteractor socket;
        private Coroutine cookingCoroutine;

        void Awake()
        {
            socket = GetComponent<XRSocketInteractor>();
        }

        void OnEnable()
        {
            socket.selectEntered.AddListener(OnItemAdded);
            socket.selectExited.AddListener(OnItemRemoved);
        }

        void OnDisable()
        {
            socket.selectEntered.RemoveListener(OnItemAdded);
            socket.selectExited.RemoveListener(OnItemRemoved);
        }

        private void OnItemAdded(SelectEnterEventArgs args)
        {
            var ingredient = args.interactableObject.transform.GetComponent<Ingredient>();
            if (ingredient != null && ingredient.currentState == IngredientState.Chopped)
            {
                StartCooking(ingredient);
            }
        }

        private void OnItemRemoved(SelectExitEventArgs args)
        {
            StopCooking();
        }

        private void StartCooking(Ingredient ingredient)
        {
            if (cookingCoroutine != null) StopCoroutine(cookingCoroutine);
            cookingCoroutine = StartCoroutine(CookingRoutine(ingredient));
            
            if (cookingParticles != null) cookingParticles.Play();
            if (audioSource != null && cookingSound != null)
            {
                audioSource.clip = cookingSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }

        private void StopCooking()
        {
            if (cookingCoroutine != null)
            {
                StopCoroutine(cookingCoroutine);
                cookingCoroutine = null;
            }

            if (cookingParticles != null) cookingParticles.Stop();
            if (audioSource != null) audioSource.Stop();
        }

        private IEnumerator CookingRoutine(Ingredient ingredient)
        {
            Debug.Log($"Started cooking {ingredient.name}...");
            yield return new WaitForSeconds(cookingTime);

            // Cooking Complete
            Debug.Log($"{ingredient.name} is cooked!");
            
            // Change state
            ingredient.currentState = IngredientState.Cooked;
            
            // Visual feedback (MVP: Change color to brown)
            var renderer = ingredient.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = new Color(0.6f, 0.3f, 0.1f); // Brownish
            }

            // Audio feedback
            if (audioSource != null && doneSound != null)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(doneSound);
            }
            
            if (cookingParticles != null) cookingParticles.Stop();
        }
    }
}