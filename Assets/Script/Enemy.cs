using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Vitesse de chute verticale (unités/s).")]
    public float fallSpeed = 6f;

    [Tooltip("Quand l’ennemi dépasse ce Y, on le détruit.")]
    [SerializeField] private float killY = -6f;

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);

        if (transform.position.y < killY)
            Destroy(gameObject);
    }
}
