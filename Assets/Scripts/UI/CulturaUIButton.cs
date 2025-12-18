using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CulturaUIButton : MonoBehaviour
{
    [Header("Referencia de datos")]
    [SerializeField] public CulturaData culturaData;

    [Header("UI")]
    [SerializeField] public Button button;
    [SerializeField] public TextMeshProUGUI texto;

    [Header("Estados visuales")]
    [SerializeField] public Color colorBloqueado = Color.gray;
    [SerializeField] public Color colorDesbloqueado = Color.white;

    [Header("Panel de info")]
    [SerializeField] private CulturaInfoPanel infoPanel;

    private void Start()
    {
        ActualizarEstado();
    }

    public void ActualizarEstado()
    {
        if (culturaData == null) return;

        bool desbloqueado = culturaData.desbloqueado;

        button.interactable = desbloqueado;
        texto.color = desbloqueado ? colorDesbloqueado : colorBloqueado;
    }

    public void OnClick()
    {
        if (!culturaData.desbloqueado) return;
        infoPanel.Mostrar(culturaData);
    }


}
