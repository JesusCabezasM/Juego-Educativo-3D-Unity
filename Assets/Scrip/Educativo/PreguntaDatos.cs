using System;
using UnityEngine;

[Serializable]
public class PreguntaDatos
{
    public string categoria;

    [TextArea(2, 4)]
    public string enunciado;

    public string opcionIzquierda;
    public string opcionDerecha;

    [Range(0, 1)]
    [Tooltip("0 = izquierda, 1 = derecha")]
    public int indiceCorrecto;

    [TextArea(1, 3)]
    public string pista;

    [TextArea(1, 3)]
    public string explicacion;

    public PreguntaDatos(
        string categoria,
        string enunciado,
        string opcionIzquierda,
        string opcionDerecha,
        int indiceCorrecto,
        string pista,
        string explicacion)
    {
        this.categoria = categoria;
        this.enunciado = enunciado;
        this.opcionIzquierda = opcionIzquierda;
        this.opcionDerecha = opcionDerecha;
        this.indiceCorrecto = indiceCorrecto;
        this.pista = pista;
        this.explicacion = explicacion;
    }
}
