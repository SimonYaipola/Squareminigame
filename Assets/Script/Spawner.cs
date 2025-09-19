using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private Enemy enemyPrefab;   // drag le prefab ici

    [Header("Zone de spawn")]
    [SerializeField] private float xRange = 8.5f; // moitié de la largeur visible
    [SerializeField] private float ySpawn = 6f;   // hauteur de spawn (au-dessus de la caméra)

    [Header("Cadence")]
    [SerializeField] private Vector2 intervalRange = new Vector2(0.5f, 1.2f);

    [Header("Difficulté")]
    [SerializeField] private Vector2 fallSpeedRange = new Vector2(4f, 10f);

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnOne();
            yield return new WaitForSeconds(Random.Range(intervalRange.x, intervalRange.y));
        }
    }

    private void SpawnOne()
    {
        float x = Random.Range(-xRange, xRange);
        Vector3 pos = new Vector3(x, ySpawn, 0f);

        Enemy e = Instantiate(enemyPrefab, pos, Quaternion.identity);
        e.fallSpeed = Random.Range(fallSpeedRange.x, fallSpeedRange.y);
    }
}
