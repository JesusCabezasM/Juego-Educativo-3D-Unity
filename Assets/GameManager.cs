using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Esta es la variable que todo el mundo busca
    public static GameManager instance;
    public Vector3 UltimoCheckpoint;

    void Awake()
    {
        // Esto asigna el script a la variable estática 'instance'
        instance = this;
    }

    void Start()
    {
        // Si no has guardado nada, ponemos un punto de inicio por defecto
        if (UltimoCheckpoint == Vector3.zero)
        {
            UltimoCheckpoint = new Vector3(0, 0, 0); 
        }
    }
}