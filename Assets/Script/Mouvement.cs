using UnityEngine;
using UnityEngine.SceneManagement;
public class Mouvement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float xLimit = 8.5f; // à ajuster selon ta caméra

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal"); // flèches ← →
        Vector2 move = new Vector2(input, 0f ) * speed * Time.deltaTime;
        transform.Translate(move);

        // Clamp dans l'écran
        Vector2 p = transform.position;
        p.x = Mathf.Clamp(p.x, -xLimit, xLimit);
        transform.position = p;
    }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Recharge directement la scène "Main"
            SceneManager.LoadScene("Main");
        }
    }
}