using UnityEngine;
using TMPro;

public class VidaTMP : MonoBehaviour
{
    [SerializeField] private ControlDeNave nave;
    [SerializeField] private TextMeshProUGUI textoVida;

    private void Start()
    {
        if (nave == null)
        {
            Debug.LogError("Nave no asignada");
            return;
        }

        textoVida.text = nave.vida.ToString();
    }

    private void Update()
    {
        if (nave == null) return;

        textoVida.text = nave.vida.ToString();
    }
}
