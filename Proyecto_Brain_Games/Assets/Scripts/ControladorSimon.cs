using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Necesario para controlar el texto de Score

public class ControladorSimon : MonoBehaviour
{
    [Header("Configuración del Tablero")]
    public Button[] botones;             
    public Sprite[] spritesNormales;    
    public Sprite[] spritesEncendidos;  

    [Header("UI del Juego")]
    public TextMeshProUGUI textoScore;   // Casilla para arrastrar tu texto de Score
    public GameObject panelGameOver;     // Casilla para arrastrar tu Panel-GameOver

    private List<int> secuencia = new List<int>();
    private int pasoUsuario = 0;
    private int puntuacion = 0;          // Registro de los puntos
    private bool puedeJugar = false;

    void Start()
    {
        panelGameOver.SetActive(false); // Nos aseguramos de que empiece oculto
        puntuacion = 0;
        ActualizarTextoScore();
        ApagarTodosLosBotones();
        NuevaRonda();
    }

    void ApagarTodosLosBotones()
    {
        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].image.sprite = spritesNormales[i];
        }
    }

    void NuevaRonda()
    {
        pasoUsuario = 0;
        puedeJugar = false;
        secuencia.Add(Random.Range(0, botones.Length)); 
        StartCoroutine(ReproducirSecuencia());
    }

    IEnumerator ReproducirSecuencia()
    {
        yield return new WaitForSeconds(1f); 

        foreach (int indice in secuencia)
        {
            botones[indice].image.sprite = spritesEncendidos[indice];
            yield return new WaitForSeconds(0.6f); 
            botones[indice].image.sprite = spritesNormales[indice];
            yield return new WaitForSeconds(0.2f); 
        }

        puedeJugar = true; 
    }

    public void ClickBoton(int id)
    {
        if (!puedeJugar) return;

        StartCoroutine(FlashBotonJugador(id));

        if (id == secuencia[pasoUsuario])
        {
            pasoUsuario++;
            if (pasoUsuario >= secuencia.Count)
            {
                // ¡Acierto completo de combinación! Sumamos puntos basados en el nivel actual
                puntuacion += 10; 
                ActualizarTextoScore();
                NuevaRonda();
            }
        }
        else
        {
            // El jugador pierde: Activamos el menú de derrota y bloqueamos el juego
            puedeJugar = false;
            panelGameOver.SetActive(true);
        }
    }

    void ActualizarTextoScore()
    {
        // Esto cambia el texto en pantalla. Adaptalo si quieres que diga "SCORE: " u otra cosa
        textoScore.text = "SCORE: " + puntuacion;
    }

    // Esta función la llamará el botón de reiniciar del menú de derrota
    public void ReiniciarJuego()
    {
        secuencia.Clear();
        puntuacion = 0;
        ActualizarTextoScore();
        panelGameOver.SetActive(false);
        NuevaRonda();
    }

    IEnumerator FlashBotonJugador(int id)
    {
        botones[id].image.sprite = spritesEncendidos[id];
        yield return new WaitForSeconds(0.2f);
        botones[id].image.sprite = spritesNormales[id];
    }
}