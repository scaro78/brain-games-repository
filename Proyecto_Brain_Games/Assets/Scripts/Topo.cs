using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Topo : MonoBehaviour
{
    public Sprite spriteNormal;   
    public Sprite spriteGolpeado; 
    
    private Image imagen;
    private Button boton;
    private bool estaActivo = false;

    void Awake()
    {
        imagen = GetComponent<Image>();
        boton = GetComponent<Button>();
        boton.onClick.AddListener(AlSerGolpeado);
    }

    void Start() { Esconder(); }

    public void Aparecer(float tiempo)
    {
        if (estaActivo) return; 
        estaActivo = true;
        
        imagen.sprite = spriteNormal;
        Color c = imagen.color; c.a = 1f; imagen.color = c; 
        boton.interactable = true;

        StartCoroutine(EsconderAutomatico(tiempo));
    }

    void AlSerGolpeado()
    {
        if (!estaActivo) return;
        estaActivo = false;

        imagen.sprite = spriteGolpeado; 
        boton.interactable = false;
        
        // Sumar punto al controlador global
        ControladorTopo.instancia.SumarPunto(); 
        StartCoroutine(EsperarYEsconder());
    }

    IEnumerator EsconderAutomatico(float t)
    {
        yield return new WaitForSeconds(t);
        if (estaActivo) Esconder();
    }

    IEnumerator EsperarYEsconder()
    {
        yield return new WaitForSeconds(0.5f); 
        Esconder();
    }

    void Esconder()
    {
        estaActivo = false;
        boton.interactable = false;
        Color c = imagen.color; c.a = 0f; imagen.color = c; 
    }
}
