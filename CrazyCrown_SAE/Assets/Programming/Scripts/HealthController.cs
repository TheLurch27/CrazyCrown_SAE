using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Slider healthSlider;
    public Gradient gradient;
    public Image fillImage;
    private float currentHealth;

    private void Start()
    {
        currentHealth = 1f; // Startwert der Lebensanzeige
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // Setze die Lebensanzeige entsprechend des aktuellen Lebens
        healthSlider.value = currentHealth;

        // Setze die Farbe des Fill-Bereichs basierend auf dem Farbverlauf
        fillImage.color = gradient.Evaluate(healthSlider.normalizedValue);
    }

    // Methode, die aufgerufen wird, wenn der Knopf gedrückt wird
    public void DecreaseHealth()
    {
        // Reduziere die Lebensanzeige um 0.25
        currentHealth -= 0.25f;

        // Stelle sicher, dass die Lebensanzeige nicht unter 0 fällt
        currentHealth = Mathf.Max(currentHealth, 0f);

        // Aktualisiere die Lebensanzeige
        UpdateHealthBar();
    }
}
