using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // Riferimento al prefab della piattaforma da assegnare nell'Inspector
    public GameObject platformPrefab;
    
    // Numero di piattaforme da generare
    public int numberOfPlatforms = 20;
    
    // Distanza tra le piattaforme lungo l'asse Z
    public float gapBetweenPlatforms = 2.0f;
    
    // Offset iniziale sul percorso (può essere usato per regolare la prima piattaforma)
    public Vector3 startPosition = Vector3.zero;

    void Start()
    {
        // Ciclo per generare le piattaforme
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            // Calcola la posizione per ogni piattaforma lungo l'asse Z
            Vector3 spawnPosition = startPosition + new Vector3(0, 0, i * gapBetweenPlatforms);
            
            // Instanzia il prefab
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
