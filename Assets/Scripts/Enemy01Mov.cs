using System.Collections;
using UnityEngine;

public class MoverEnemigos : MonoBehaviour
{
    public float tiempoDeMovimiento = 2f;
    public float distancia = 5f;
    private Vector3 posicionOriginal;

    void Start()
    {
        posicionOriginal = transform.position;
        if (CompareTag("EnemigoX"))
        {
            StartCoroutine(MoverEntrePosiciones(Vector3.right));
        }
        else if (CompareTag("EnemigoY"))
        {
            StartCoroutine(MoverEntrePosiciones(Vector3.up));
        }
        else if (CompareTag("EnemigoZ"))
        {
            StartCoroutine(MoverEntrePosiciones(Vector3.forward));
        }
    }

    IEnumerator MoverEntrePosiciones(Vector3 direccion)
    {
        while (true)
        {
            Vector3 posicionFinal = posicionOriginal + direccion * distancia;
            yield return StartCoroutine(MoverDeAtrasHaciaAdelante(posicionOriginal, posicionFinal));
            yield return StartCoroutine(MoverDeAtrasHaciaAdelante(posicionFinal, posicionOriginal));
        }
    }

    IEnumerator MoverDeAtrasHaciaAdelante(Vector3 desde, Vector3 hasta)
    {
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < tiempoDeMovimiento)
        {
            transform.position = Vector3.Lerp(desde, hasta, tiempoTranscurrido / tiempoDeMovimiento);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        transform.position = hasta;
    }
}