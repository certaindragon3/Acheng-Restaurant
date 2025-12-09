using UnityEngine;

namespace AchengRestaurant.Interaction
{
    public enum IngredientState
    {
        Raw,
        Chopped,
        Cooked,
        Burnt
    }

    public class Ingredient : MonoBehaviour
    {
        public IngredientState currentState = IngredientState.Raw;
        public string ingredientName;
    }
}