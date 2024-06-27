using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Producto[] productos;
    public Producto[] productos2;  // Segunda lista de productos
    public Transform producto1Position;
    public Transform producto2Position;
    public Text precio1Text;
    public Text precio2Text;
    public InputField inputField;
    public GameObject notificationPanel;  // Panel que se muestra cuando la respuesta es correcta o incorrecta
    public GameObject panelLeyenda;       // Panel que se muestra cuando no se ingresa ningún valor
    public GameObject panel;       
    public Text panelLeyendaText;         // Texto dentro del panel de leyenda
    public Button retryButton;
    public Button ReHacerButton;
    public Button exitButton;

    private Producto producto1;
    private Producto producto2;

    void Start()
    {
        InicializarJuego();
    }

    public void InicializarJuego()
    {
        producto1 = Instantiate(productos[Random.Range(0, productos.Length)], producto1Position.position, Quaternion.identity);
        producto2 = Instantiate(productos2[Random.Range(0, productos2.Length)], producto2Position.position, Quaternion.identity);

        precio1Text.text = producto1.precio.ToString();
        precio2Text.text = producto2.precio.ToString();

        inputField.text = "";
        inputField.placeholder.GetComponent<Text>().text = "?";

        notificationPanel.SetActive(false);
        panelLeyenda.SetActive(false);
    }

    public void OnResponder()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            MostrarPanelLeyenda("Debes ingresar un resultado");
            return;
        }

        int resultado;
        if (int.TryParse(inputField.text, out resultado))
        {
            int suma = producto1.precio + producto2.precio;
            if (resultado == suma)
            {
                MostrarNotificacion(true); 
                retryButton.gameObject.SetActive(true);
                ReHacerButton.gameObject.SetActive(false);

            }
            else
            {
                MostrarNotificacion(false);
              
                retryButton.gameObject.SetActive(false);
                ReHacerButton.gameObject.SetActive(true);
            }
        }
        else
        {
            MostrarPanelLeyenda("Entrada no válida. Ingresa un número.");
        }
    }

    private void MostrarNotificacion(bool esCorrecto)
    {
        notificationPanel.SetActive(true);
        if (esCorrecto)
        {
            retryButton.GetComponentInChildren<Text>().text = "Reiniciar el desafío";
        }
        else
        {
            retryButton.GetComponentInChildren<Text>().text = "Volver a intentarlo";
        }
    }

    private void MostrarPanelLeyenda(string mensaje)
    {
        panelLeyendaText.text = mensaje;
        panelLeyenda.SetActive(true);
    }

    public void OnRetry()
    {
        Destroy(producto1.gameObject);
        Destroy(producto2.gameObject);
        InicializarJuego();
    }

    public void OnExit()
    {
        //code para cargar la escena "SeleccionarJuegos"
    }
    public void OnCerrarPanel()
    {
        panelLeyenda.SetActive(false);
        panel.SetActive(true);
    }
    public void OnReiniciarJuego()
    {
        InicializarJuego();

    }

    public void OnVolverAInterntar()
    {
        notificationPanel.SetActive(false);
        panel.SetActive(true);

    }
}

     

