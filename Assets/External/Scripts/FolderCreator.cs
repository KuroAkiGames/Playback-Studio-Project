using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class FolderCreator : MonoBehaviour
{


    [MenuItem("Tools/Create Folders")]
    public static void CreateFolders()
    {
        string[] folders ={ 
            "Animation",
            "Audio/SFX",
            "Audio/Music",
            "Audio/Voice",
            "Audio/Voice",
            "Materials",
            "Models",
            "Textures",
            "UI",
            "Prefabs",
            "Scenes",
            "Scripts/AI",
            "Scripts/Controllers",
            "Scripts/Managers",
            "Scripts/UI",
            "Scripts/Utilities",
        };


        foreach (var folder in folders)
        {
            string path = Path.Combine("Assets", "Main", folder);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        AssetDatabase.Refresh();
    }
}
