using UnityEngine;

public class PlataformaCultural : MonoBehaviour
{
    [Header("Dato cultural asociado")]
    [SerializeField] public CulturaData culturaData;

    private bool yaActivado = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (yaActivado) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            ActivarDato();
        }
    }

    private void ActivarDato()
    {
        CulturaManager.Instance.DesbloquearCultura(culturaData);

        yaActivado = true;

        Debug.Log("Dato desbloqueado: " + culturaData.nombreCultura);
    }
}
