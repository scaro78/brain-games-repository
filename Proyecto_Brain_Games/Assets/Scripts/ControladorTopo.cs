using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControladorTopo : MonoBehaviour
{
    public static ControladorTopo instancia;

    public List<Topo> topos = new List<Topo>();
    public float tiempoEntreTopos = 1.5f;
    public float tiempoVisibleTopo = 1.0f;

    private int puntaje = 0;
    private bool juegoActivo = true;

    void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(CicloDelJuego());
    }

    IEnumerator CicloDelJuego()
    {
        while (juegoActivo)
        {
            yield return new WaitForSeconds(tiempoEntreTopos);

            if (topos.Count > 0)
            {
                int indiceAleatorio = Random.Range(0, topos.Count);
                if (topos[indiceAleatorio] != null)
                {
                    topos[indiceAleatorio].Aparecer(tiempoVisibleTopo);
                }
            }
        }
    }

    public void SumarPunto()
    {
        puntaje++;
        Debug.Log($"¡Punto anotado! Puntaje actual: {puntaje}");
    }
}
