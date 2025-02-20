using System.Collections;
using System.Linq;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public static Spawn instance;
    private GameObject[] alphabetFiltered;
    public GameObject[] numberList;  // Array de números (prefabs com imagens de números)
    public GameObject[] alphabet;  // Array de letras (prefabs com imagens de letras)
    public Transform spawnArea;
    public float spawnInterval;  // Tempo entre os spawns
    private float spawnAreaMinX; // Ponto mínimo da largura
    private float spawnAreaMaxX; // Ponto máximo da largura
    private float lastSpawnX = Mathf.NegativeInfinity;
    private int dropRate = 0;

    void Start()
    {
        instance = this;
        Collider2D spawnAreaCollider = spawnArea.GetComponent<Collider2D>();
        // Calculando os limites esquerdo e direito da área de spawn com base na posição e escala
        spawnAreaMinX = spawnArea.position.x - spawnAreaCollider.bounds.size.x / 2;
        spawnAreaMaxX = spawnArea.position.x + spawnAreaCollider.bounds.size.x / 2;

        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        int randomChoice = Random.Range(0, 2); // 0 ou 1 (50% de chance)

        GameObject selectedPrefab;

        if (randomChoice == 0)
        {
            // Escolher aleatoriamente um número do array
            int randomIndex = Random.Range(0, numberList.Length);
            selectedPrefab = numberList[randomIndex];
        }
        else
        {
            // Filtra a lista do Alfabeto para as proximas 3 letras somente
            if (alphabet.Length >= 3) {
                alphabetFiltered = alphabet.Take(3).ToArray();
            }

            // Escolher aleatoriamente uma letra do array
            int randomIndex = Random.Range(0, alphabetFiltered.Length);
            
            if (dropRate == 4) {
                selectedPrefab = alphabetFiltered[0];
                dropRate = 0;
            } else {
                selectedPrefab = alphabetFiltered[randomIndex];
                dropRate += 1;
            }
        }

        // Gerar uma posição aleatória na área de spawn usando os limites calculados
        float randomX = Random.Range(spawnAreaMinX, spawnAreaMaxX);

        // Garantir que a posição do novo objeto seja suficientemente afastada da anterior
        while (Mathf.Abs(randomX - lastSpawnX) < spawnArea.localScale.x)
        {
            randomX = Random.Range(spawnAreaMinX, spawnAreaMaxX);
        }

        lastSpawnX = randomX; // Atualiza a última posição gerada

        // Gerar a posição final para o objeto
        Vector3 spawnPosition = new Vector3(randomX, spawnArea.position.y, 0);

        // Instanciar o objeto na posição calculada
        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }
}
