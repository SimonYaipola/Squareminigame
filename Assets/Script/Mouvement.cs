using UnityEngine;                 // Importation de l’API Unity de base (GameObjects, Transform, etc.)
using UnityEngine.SceneManagement; // Importation du module de gestion des scènes (pour recharger "Main")

public class Mouvement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;   // Vitesse horizontale du joueur
    [SerializeField] private float xLimit = 8.5f; // Limite de déplacement en X (gauche/droite)

    [Header("Audio")]
    [SerializeField] private AudioClip deathClip; // Son joué à la mort du joueur

    private AudioSource audiosound; // Référence à l’AudioSource du Player
    private bool isDead = false;    // Flag pour bloquer les contrôles une fois mort

    void Start()
    {
        // Récupère automatiquement le composant AudioSource attaché au Player
        audiosound = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Si le joueur est mort, on bloque tout
        if (isDead) return;

        // Récupère l’input horizontal (flèches ou touches A/D)
        float input = Input.GetAxisRaw("Horizontal");

        // Calcule le déplacement (vecteur sur X) en fonction de la vitesse et du deltaTime
        Vector2 move = new Vector2(input, 0f) * speed * Time.deltaTime;

        // Applique le déplacement au Transform du Player
        transform.Translate(move);

        // Récupère la position actuelle et limite le X dans [-xLimit, xLimit]
        var p = transform.position;
        p.x = Mathf.Clamp(p.x, -xLimit, xLimit);

        // Réapplique la position corrigée
        transform.position = p;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si déjà mort, on ignore les autres collisions
        if (isDead) return;

        // Vérifie si la collision est avec un objet taggé "Enemy"
        if (other.CompareTag("Enemy"))
        {
            isDead = true; // On tue le joueur

            // Si un AudioSource et un clip sont définis, joue le son de mort
            if (audiosound && deathClip)
                audiosound.PlayOneShot(deathClip);

            // Redémarre la scène  après 2 sec
            Invoke(nameof(RestartScene), 2f);
        }
    }

    private void RestartScene()
    {
        // Recharge la scène
        SceneManager.LoadScene("Main");
    }
}
