using UnityEngine;

[CreateAssetMenu(
    fileName = "NuevaCultura",
    menuName = "Cultura/Data Cultural"
)]
public class CulturaData : ScriptableObject
{
    [Header("Identificación")]
    public string id;                  // Ej: "mesopotamia_01"
    public string nombreCultura;        // Mesopotamia

    [Header("Contenido")]
    [TextArea(3, 6)]
    public string descripcion;

    public Sprite imagen;

    [Header("Estado (NO persistente aún)")]
    public bool desbloqueado;
}
