using UnityEngine;
using TMPro;

public class OpcionRespuesta : MonoBehaviour
{
    public TextMeshProUGUI textoInterfaz; 
    public GameObject panelAOcultar;      
    public bool esCorrecto;               
    public GameObject panelMensajeFinal;  

    public static int totalPuntos = 0;    

    private bool esperandoMovimiento = false; // Variable para saber si estamos en pausa

    void Start()
    {
        // totalPuntos = 0;
    }

    void Update()
    {
        // Si el juego está pausado esperando que el jugador lea el mensaje...
        if (esperandoMovimiento)
        {
            // Detectamos si presiona alguna tecla de movimiento (W, A, S, D o las Flechas)
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || 
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || 
                Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                ReanudarJuego();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si choca el jugador y no estamos ya en pausa
        if (other.gameObject.name == "Jugador" && !esperandoMovimiento)
        {
            // 1. Sumamos o restamos puntos
            if (esCorrecto)
            {
                totalPuntos += 2;
            }
            else
            {
                totalPuntos -= 1;
            }

            textoInterfaz.text = "Puntos: " + totalPuntos;

            // 2. Ocultamos el panel de la pregunta matemática
            if (panelAOcultar != null)
            {
                panelAOcultar.SetActive(false);
            }

            // 3. Mostramos el panel de "¡Felicidades!" o "¡Error!"
            if (panelMensajeFinal != null)
            {
                panelMensajeFinal.SetActive(true); 
            }

            // 4. Pausamos el juego
            Time.timeScale = 0f; 
            esperandoMovimiento = true; // Le avisamos al Update que empiece a escuchar las teclas

            // 5. Hacemos el rombo invisible en lugar de destruirlo (para que el código siga funcionando)
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            
            // Apagamos los efectos mágicos (las partículas) que tenga adentro el rombo
            foreach (Transform hijo in transform)
            {
                hijo.gameObject.SetActive(false);
            }
        }
    }

    private void ReanudarJuego()
    {
        // 1. Descongelamos el juego
        Time.timeScale = 1f; 
        
        // 2. Ocultamos el mensaje de Felicidades o Error
        if (panelMensajeFinal != null)
        {
            panelMensajeFinal.SetActive(false); 
        }

        // 3. AHORA SÍ, destruimos el rombo por completo
        Destroy(gameObject); 
    }
}