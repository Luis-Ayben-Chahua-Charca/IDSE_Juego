using System.Collections.Generic;
using UnityEngine;

public class CulturaManager : MonoBehaviour
{
    public static CulturaManager Instance;

    [Header("Todas las culturas del juego")]
    public List<CulturaData> todasLasCulturas;

    private SaveData saveData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        CargarEstado();
    }

    private void CargarEstado()
    {
        saveData = SaveSystem.Load();

        foreach (var cultura in todasLasCulturas)
        {
            cultura.desbloqueado =
                saveData.culturasDesbloqueadas.Contains(cultura.id);
        }
    }

    public void DesbloquearCultura(CulturaData cultura)
    {
        if (cultura.desbloqueado) return;

        cultura.desbloqueado = true;
        saveData.culturasDesbloqueadas.Add(cultura.id);

        SaveSystem.Save(saveData);
    }

    public void RefrescarUI()
    {
        foreach (var ui in FindObjectsOfType<CulturaUIButton>())
        {
            ui.ActualizarEstado();
        }
    }
}
