using System.IO;
using UnityEditor;
using UnityEngine;

namespace PrototypeEditor
{
    public static partial class MeshTools
    {
        [MenuItem("Assets/Prototype/Mesh/Toggle Readable")]
        public static void MenuToggle()
        {
            var selections = Selection.objects;
            if (selections == null || selections.Length == 0)
            {
                Debug.LogError("No object selected object to alter");
                return;
            }

            foreach (var selection in selections)
            {
                if (selection is not Mesh mesh)
                {
                    Debug.LogError($"Selected object {selection.name} is not Mesh");
                    return;
                }

                ToggleReadable(mesh);
            }

            AssetDatabase.Refresh();
        }

        public static void ToggleReadable(Mesh mesh)
        {
            const string SearchPhrase = "m_IsReadable: ";

            var path = AssetDatabase.GetAssetPath(mesh);
            var lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; ++i)
            {
                var line = lines[i];
                if (line.Contains(SearchPhrase))
                {
                    var index = line.IndexOf(SearchPhrase) + SearchPhrase.Length;
                    var current = line.Substring(index, 1);
                    var toggled = current == "1" ? "0" : "1";
                    lines[i] = line.Replace(SearchPhrase + current, SearchPhrase + toggled);

                    File.WriteAllLines(path, lines);
                    break;
                }
            }
        }
    }
}