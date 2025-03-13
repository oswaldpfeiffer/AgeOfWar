using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureArrayGenerator : EditorWindow
{
    private Texture2D[] spriteSheets;
    private string savePath = "Assets/MunMun/GeneratedTextures/Texture2DArray.asset";

    [MenuItem("Tools/Texture Array Generator")]
    public static void ShowWindow()
    {
        GetWindow<TextureArrayGenerator>("Texture Array Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Générateur de Texture2DArray", EditorStyles.boldLabel);

        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty spriteSheetsProperty = serializedObject.FindProperty("spriteSheets");

        EditorGUILayout.PropertyField(spriteSheetsProperty, new GUIContent("Spritesheets"), true);
        serializedObject.ApplyModifiedProperties();

        savePath = EditorGUILayout.TextField("Save Path", savePath);

        if (GUILayout.Button("Générer Texture2DArray"))
        {
            GenerateTexture2DArray();
        }
    }

    private void GenerateTexture2DArray()
    {
        if (spriteSheets == null || spriteSheets.Length == 0)
        {
            Debug.LogError("Aucun spritesheet fourni.");
            return;
        }

        Texture2DArray textureArray = TextureArrayImporter.CreateTexture2DArray(spriteSheets);
        if (textureArray == null) return;

        Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        AssetDatabase.CreateAsset(textureArray, savePath);
        AssetDatabase.SaveAssets();

        Debug.Log("Texture2DArray généré et sauvegardé à : " + savePath);
    }
}