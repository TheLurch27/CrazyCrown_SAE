using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public float interactionDistance = 2f; // Entfernung, in der der Spieler das Item aufsammeln kann

    private void Update()
    {
        // Überprüfen, ob der Spieler in der Nähe des Items ist und die "E"-Taste drückt
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionDistance);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Edding")) // Überprüfen, ob das berührte Objekt das Edding-Item ist
                {
                    PickUpItem(collider.gameObject); // Item aufsammeln
                    break; // Wir haben das Item gefunden und aufgehoben, also brechen wir die Schleife ab
                }
            }
        }
    }

    void PickUpItem(GameObject item)
    {
        // Hier können Sie die Logik implementieren, um das Item aufzunehmen
        // Zum Beispiel könnten Sie das Item aus der Szene entfernen und es dem Spieler hinzufügen
        Debug.Log("Item Aufgesammelt");
        Destroy(item); // Entfernen Sie das aufgesammelte Item aus der Szene
    }
}
