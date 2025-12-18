using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CulturaInfoPanel : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI titulo;
    [SerializeField] private TextMeshProUGUI descripcion;
    [SerializeField] private Image imagen;
    [SerializeField] public GameObject botonesCulturas;

    public void Mostrar(CulturaData data)
    {
        if (data == null) return;

        titulo.text = data.nombreCultura;
        descripcion.text = data.descripcion;
        imagen.sprite = data.imagen;

        botonesCulturas.SetActive(false);
        gameObject.SetActive(true);
    }

    public void Ocultar()
    {
        gameObject.SetActive(false);
    }
}
