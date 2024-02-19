using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    public float interactionDistance = 2f; // Entfernung, in der der Spieler das Item aufsammeln kann

    private void Update()
    {
        // �berpr�fen, ob der Spieler in der N�he des Items ist und die "E"-Taste dr�ckt
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionDistance);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Edding")) // �berpr�fen, ob das ber�hrte Objekt das Edding-Item ist
                {
                    PickUpItem(collider.gameObject); // Item aufsammeln
                    break; // Wir haben das Item gefunden und aufgehoben, also brechen wir die Schleife ab
                }
            }
        }
    }

    void PickUpItem(GameObject item)
    {
        // Hier k�nnen Sie die Logik implementieren, um das Item aufzunehmen
        // Zum Beispiel k�nnten Sie das Item aus der Szene entfernen und es dem Spieler hinzuf�gen
        Debug.Log("Item Aufgesammelt");
        Destroy(item); // Entfernen Sie das aufgesammelte Item aus der Szene
    }
}
