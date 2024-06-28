using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject[] Productos;
    public GameObject[] Productos2;

    public Text precio1Text;
    public Text precio2Text;

    public InputField inputField;

    private GameObject ProductoRandom;
    private GameObject ProductoRandom2;

    private int precioTotal;


    // Start is called before the first frame update
    void Start()
    {
        IniciarJuego();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void IniciarJuego()
    {
        DeactivateAll();
        DeactivateAll2();

        int randomIndex1 = Random.Range(0, Productos.Length);
        int randomIndex2 = Random.Range(0, Productos2.Length);

        ProductoRandom = Productos[randomIndex1];
        ProductoRandom2 = Productos2[randomIndex2];

        ProductoRandom.SetActive(true);
        ProductoRandom2.SetActive(true);

        Producto producto1Script = ProductoRandom.GetComponent<Producto>();
        Producto producto2Script = ProductoRandom2.GetComponent<Producto>();


        int precio1 = producto1Script.precio;
        int precio2 = producto2Script.precio;
        precioTotal = precio1 + precio2;

        precio1Text.text = precio1.ToString();
        precio2Text.text = precio2.ToString();

        // Mostrar los precios en la consola
        Debug.Log("Precio del Producto 1: " + precio1);
        Debug.Log("Precio del Producto 2: " + precio2);
        Debug.Log("Suma de los precios: " + precioTotal);
    }

    
    public void OnRespond()
    {
        
        int resultado;
        if (int.TryParse(inputField.text, out resultado))
        {
            int suma = precioTotal;
            if (resultado == suma)
            {
                Debug.Log("Bien");

            }
        else
            {
                Debug.Log("Mal");
            }
        }
    }

    void DeactivateAll()
    {
        for (int i = 0; i < Productos.Length; i++)
        {
            Productos[i].SetActive(false);
        }
    }

    void DeactivateAll2()
    {
        for (int i = 0; i < Productos2.Length; i++)
        {
            Productos2[i].SetActive(false);
        }
    }

}
