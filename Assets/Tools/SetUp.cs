using static System.IO.Directory;
using UnityEngine;
using UnityEditor;
using static System.IO.Path;
using static UnityEditor.AssetDatabase;


namespace MyTools
{
    public static class Setup
    {
        [MenuItem("Tools/Setup/Create Default Folders")]
        public static void CreateDefaultFolders()
        {
            Folders.CreateDefault("_Project", "Animation", "Art", "Materials", "Prefabs", "Scripts/ScriptableObjects", "Scripts/UI");
            Refresh();
        }

        

        static class Folders
        {
            public static void CreateDefault(string root, params string[] folders)
            {
                var fullpath = Combine(Application.dataPath, root);
                
                foreach (var folder in folders)
                {
                    var path = Combine(fullpath, folder);
                    if (!Exists(path))
                    {
                        CreateDirectory(path);
                    }
                }
            }
        }

     

    }
}