using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath =>
        Application.persistentDataPath + "/savegame.json";

    // =======================
    // GUARDAR
    // =======================
    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Juego guardado en: " + savePath);
    }

    // =======================
    // CARGAR
    // =======================
    public static SaveData Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No existe archivo de guardado");
            return new SaveData();
        }

        string json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<SaveData>(json);
    }

    // =======================
    // BORRAR (opcional)
    // =======================
    public static void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save eliminado");
        }
    }
}
