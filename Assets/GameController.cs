using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Producto[] productos;  
    public Transform producto1Position;
    public Transform producto2Position;
    public Text precio1Text;
    public Text precio2Text;
    public InputField inputField;
    public GameObject notificationPanel;
    public Text notificationText;
    public Button retryButton;
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
        producto2 = Instantiate(productos[Random.Range(0, productos.Length)], producto2Position.position, Quaternion.identity);

        precio1Text.text = producto1.precio.ToString();
        precio2Text.text = producto2.precio.ToString();

        inputField.text = "";
        inputField.placeholder.GetComponent<Text>().text = "?";

        notificationPanel.SetActive(false);
    }

    public void OnResponder()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            MostrarNotificacion("Debes ingresar un resultado");
            return;
        }

        int resultado;
        if (int.TryParse(inputField.text, out resultado))
        {
            int suma = producto1.precio + producto2.precio;
            if (resultado == suma)
            {
                MostrarNotificacion("¡Correcto!");
                retryButton.GetComponentInChildren<Text>().text = "Reiniciar el desafío";
            }
            else
            {
                MostrarNotificacion("Incorrecto. Inténtalo de nuevo.");
                retryButton.GetComponentInChildren<Text>().text = "Volver a intentarlo";
            }
        }
        else
        {
            MostrarNotificacion("Entrada no válida. Ingresa un número.");
        }
    }

    private void MostrarNotificacion(string mensaje)
    {
        notificationText.text = mensaje;
        notificationPanel.SetActive(true);
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
}
