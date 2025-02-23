using UnityEngine;

public class MusicManagement : MonoBehaviour
{
    private static MusicManagement instance;

    private void Awake()
    {
        // Se já existe uma instância do MusicManagment, destrói este objeto para evitar duplicação.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Define esta instância como única e impede que seja destruída ao carregar novas cenas.
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
