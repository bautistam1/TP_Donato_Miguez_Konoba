using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] productos;
    public GameObject[] productos2;  // Segunda lista de productos
    
    public Text ganastePerdisteText;
   
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

    private GameObject ProductoRandom1;
    private GameObject ProductoRandom2;

    private int precioTotal;

    void Start()
    {
        InicializarJuego();
    }

    public void InicializarJuego()
    {
        DeactivateAll();
        DeactivateAll2();

        int randomIndex1 = Random.Range(0, productos.Length);
        int randomIndex2 = Random.Range(0, productos2.Length);

        ProductoRandom1 = productos[randomIndex1];
        ProductoRandom2 = productos2[randomIndex2];

        ProductoRandom1.SetActive(true);
        ProductoRandom2.SetActive(true);

        Producto producto1Script = ProductoRandom1.GetComponent<Producto>();
        Producto producto2Script = ProductoRandom2.GetComponent<Producto>();


        int precio1 = producto1Script.precio;
        int precio2 = producto2Script.precio;
        precioTotal = precio1 + precio2;

        precio1Text.text = precio1.ToString();
        precio2Text.text = precio2.ToString();

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
            int suma = precioTotal;
            if (resultado == suma)
            {
                MostrarNotificacion(true); 
                retryButton.gameObject.SetActive(true);
                ReHacerButton.gameObject.SetActive(false);
                ganastePerdisteText.GetComponentInChildren<Text>().text = "¡Ganaste!";
                ProductoRandom1.gameObject.SetActive(false);
                ProductoRandom2.gameObject.SetActive(false);

            }
            else
            {
                MostrarNotificacion(false);
                retryButton.gameObject.SetActive(false);
                ReHacerButton.gameObject.SetActive(true);
                ganastePerdisteText.GetComponentInChildren<Text>().text = "¡Perdiste!";
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
            ReHacerButton.GetComponentInChildren<Text>().text = "Volver a intentarlo";
        }
    }

    private void MostrarPanelLeyenda(string mensaje)
    {
        panelLeyendaText.text = mensaje;
        panelLeyenda.SetActive(true);
    }

    public void OnRetry()
    {
        Destroy(ProductoRandom1.gameObject);
        Destroy(ProductoRandom2.gameObject);
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

    void DeactivateAll()
    {
        for (int i = 0; i < productos.Length; i++)
        {
            productos[i].SetActive(false);
        }
    }

    void DeactivateAll2()
    {
        for (int i = 0; i < productos2.Length; i++)
        {
            productos2[i].SetActive(false);
        }
    }
}

     

