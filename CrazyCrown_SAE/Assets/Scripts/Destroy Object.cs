using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DestroyObject : MonoBehaviour
{
    public float pickupRadius = 2f; // Radius, in dem der Spieler das Objekt aufnehmen kann
    public GameObject inventorySlotPrefab; // Prefab für ein Slot im Inventar
    public Transform inventoryPanel; // Panel, das die Slots im Inventar enthält

    private List<GameObject> inventory = new List<GameObject>(); // Liste der aufgenommenen Items

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Item"))
                {
                    // Objekt aufnehmen
                    inventory.Add(collider.gameObject); // Füge das aufgenommene Item zum Inventar hinzu
                    Destroy(collider.gameObject); // Objekt aus der Szene entfernen
                    UpdateInventoryUI(); // Aktualisiere das Inventar-UI
                    break; // Stoppe die Schleife, wenn das Item aufgenommen wurde
                }
            }
        }
    }

    void UpdateInventoryUI()
    {
        // Lösche alle vorhandenen Slots im Inventar-UI
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        // Erstelle ein Slot für jedes aufgenommene Item im Inventar
        foreach (GameObject item in inventory)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryPanel);
            Image image = slot.GetComponent<Image>();
            if (image != null)
            {
                // Setze das Bild des Items im Slot
                // Hier könntest du den Sprite des Items verwenden
                // Du musst sicherstellen, dass das Item ein Sprite-Renderer-Komponente hat oder auf andere Weise ein Bild darstellen kann
            }
        }
    }

    // Zeige den Pickup-Radius im Editor an
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
