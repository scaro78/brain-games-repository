using UnityEngine;

public class NavegacionManager : MonoBehaviour
{
    [Header("Paneles de la Aplicación")]
    public GameObject panelInicio;
    public GameObject panelMenuHamburguesa;
    public GameObject panelHistorial;
    public GameObject panelConfiguracion;
    public GameObject panelResultados;

    void Start()
    {
        // Al iniciar la app, solo se muestra la pantalla de inicio con los juegos
        MostrarPantallaUnica(panelInicio);
        panelMenuHamburguesa.SetActive(false);
    }

    // Método para abrir/cerrar el menú hamburguesa sin ocultar lo que está de fondo
    public void AlternarMenuHamburguesa()
    {
        panelMenuHamburguesa.SetActive(!panelMenuHamburguesa.activeSelf);
    }

    // Métodos para cambiar entre secciones
    public void IrAInicio() => MostrarPantallaUnica(panelInicio);
    public void IrAHistorial() => MostrarPantallaUnica(panelHistorial);
    public void IrAConfiguracion() => MostrarPantallaUnica(panelConfiguracion);
    public void IrAResultados() => MostrarPantallaUnica(panelResultados);

    private void MostrarPantallaUnica(GameObject pantallaActiva)
    {
        // Aseguramos cerrar el menú al cambiar de sección
        panelMenuHamburguesa.SetActive(false); 

        // Apaga todas las pantallas principales
        panelInicio.SetActive(false);
        panelHistorial.SetActive(false);
        panelConfiguracion.SetActive(false);
        panelResultados.SetActive(false);

        // Enciende solo la que necesitamos
        pantallaActiva.SetActive(true);
    }
}
