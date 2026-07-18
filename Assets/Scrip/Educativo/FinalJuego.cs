using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalJuego : MonoBehaviour
{
    [Header("Interfaz final")]
    [SerializeField] private GameObject panelFinal;
    [SerializeField] private TMP_Text textoFinal;

    [Header("Configuración")]
    [SerializeField] private float segundosParaReiniciar = 6f;

    private bool juegoTerminado;

    private void Start()
    {
        if (panelFinal != null)
        {
            panelFinal.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform jugador = other.transform.root;

        if (juegoTerminado || !jugador.CompareTag("Player"))
        {
            return;
        }

        juegoTerminado = true;

        if (panelFinal != null)
        {
            panelFinal.SetActive(true);
        }

        if (textoFinal != null)
        {
            textoFinal.text =
                "¡Felicitaciones!\n" +
                "Completaste todos los retos educativos.";
        }

        Invoke(nameof(ReiniciarJuego), segundosParaReiniciar);
    }

    private void ReiniciarJuego()
    {
        Scene escenaActual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escenaActual.buildIndex);
    }
}