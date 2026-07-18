using UnityEngine;

[DisallowMultipleComponent]
public class PlataformaMovil : MonoBehaviour
{
    [SerializeField] private Vector3 desplazamiento = new Vector3(3f, 0f, 0f);
    [SerializeField, Min(0.05f)] private float velocidad = 1f;

    private Vector3 posicionInicial;
    private Rigidbody cuerpo;

    private void Awake()
    {
        posicionInicial = transform.position;
        cuerpo = GetComponent<Rigidbody>();

        if (cuerpo != null)
        {
            cuerpo.isKinematic = true;
        }
    }

    private void FixedUpdate()
    {
        float t = (Mathf.Sin(Time.time * velocidad) + 1f) * 0.5f;
        Vector3 destino = Vector3.Lerp(
            posicionInicial,
            posicionInicial + desplazamiento,
            t);

        if (cuerpo != null)
        {
            cuerpo.MovePosition(destino);
        }
        else
        {
            transform.position = destino;
        }
    }
}
