using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Der Spieler, dem die Kamera folgen soll
    public float smoothSpeed = 0.125f; // Die Geschwindigkeit, mit der die Kamera dem Spieler folgt
    public Vector3 offset; // Der Abstand zwischen Spieler und Kamera
    public float leftLimit; // Linker Begrenzungspunkt der Kamera
    public float rightLimit; // Rechter Begrenzungspunkt der Kamera
    public float minHeight; // Mindesthöhe der Kamera

    void FixedUpdate()
    {
        if (target != null)
        {
            // Berechne die Zielposition der Kamera
            Vector3 desiredPosition = target.position + offset;
            // Begrenze die Kamera horizontal
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, leftLimit, rightLimit);
            // Begrenze die Kamera vertikal
            desiredPosition.y = Mathf.Max(desiredPosition.y, minHeight);
            // Interpoliere die aktuelle Position der Kamera zur Zielposition, um eine weiche Bewegung zu erzielen
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Setze die Position der Kamera auf die berechnete Position
            transform.position = smoothedPosition;
        }
    }
}
