using Code.Gameplay.Views;
using TMPro;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Code
{
    public class BlocksPrefabVariantCreator : EditorWindow
    {
        private GameObject basePrefab;
        private string blocksFolder = "Assets/Resources/Prefabs/Blocks/";

        [MenuItem("Tools/Blocks Variant Creator")]
        public static void ShowWindow()
        {
            GetWindow<BlocksPrefabVariantCreator>("Blocks Prefab Variant Creator");
        }

        private void OnGUI()
        {
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);

            basePrefab = (GameObject)EditorGUILayout.ObjectField("Base Prefab", basePrefab, typeof(GameObject), false);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Pick Folder"))
            {
                var path = EditorUtility.OpenFolderPanel("Select a Folder", "", "");
                if (string.IsNullOrEmpty(path) == false)
                {
                    blocksFolder = GetRelativePath(path);
                }
            }

            GUILayout.Label(blocksFolder);
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Create Prefab Variants"))
            {
                CreatePrefabVariants();
            }
        }

        private string GetRelativePath(string absolutePath)
        {
            var assetsPath = Application.dataPath;

            if (absolutePath.StartsWith(assetsPath))
            {
                return "Assets" + absolutePath.Substring(assetsPath.Length);
            }

            return absolutePath;
        }

        private void CreatePrefabVariants()
        {
            if (basePrefab == null)
            {
                Debug.LogError("Please assign the base prefab.");
                return;
            }

            var colors = new Color[]
            {
                // Scheme 1: Orange
                new Color(249f / 255f, 191f / 255f, 135f / 255f),
                new Color(249f / 255f, 174f / 255f, 111f / 255f),
                new Color(249f / 255f, 157f / 255f, 87f / 255f),
                new Color(249f / 255f, 139f / 255f, 63f / 255f),
                new Color(247f / 255f, 122f / 255f, 39f / 255f),
                new Color(230f / 255f, 105f / 255f, 26f / 255f),
                new Color(212f / 255f, 88f / 255f, 13f / 255f),
                new Color(194f / 255f, 71f / 255f, 0f / 255f),

                // Scheme 2: Blue
                new Color(135f / 255f, 199f / 255f, 249f / 255f),
                new Color(111f / 255f, 180f / 255f, 249f / 255f),
                new Color(87f / 255f, 159f / 255f, 249f / 255f),
                new Color(63f / 255f, 139f / 255f, 249f / 255f),
                new Color(39f / 255f, 118f / 255f, 249f / 255f),
                new Color(26f / 255f, 102f / 255f, 230f / 255f),
                new Color(13f / 255f, 85f / 255f, 212f / 255f),
                new Color(0f / 255f, 68f / 255f, 194f / 255f),

                // Scheme 3: Green
                new Color(135f / 255f, 249f / 255f, 166f / 255f),
                new Color(111f / 255f, 249f / 255f, 140f / 255f),
                new Color(87f / 255f, 249f / 255f, 114f / 255f),
                new Color(63f / 255f, 249f / 255f, 87f / 255f),
                new Color(39f / 255f, 249f / 255f, 61f / 255f),
                new Color(26f / 255f, 230f / 255f, 51f / 255f),
                new Color(13f / 255f, 212f / 255f, 41f / 255f),
                new Color(0f / 255f, 194f / 255f, 31f / 255f),

                // Scheme 4: Purple
                new Color(196f / 255f, 135f / 255f, 249f / 255f),
                new Color(178f / 255f, 111f / 255f, 249f / 255f),
                new Color(161f / 255f, 87f / 255f, 249f / 255f),
                new Color(143f / 255f, 63f / 255f, 249f / 255f),
                new Color(126f / 255f, 39f / 255f, 249f / 255f),
                new Color(108f / 255f, 26f / 255f, 230f / 255f),
                new Color(91f / 255f, 13f / 255f, 212f / 255f),
                new Color(73f / 255f, 0f / 255f, 194f / 255f),

                // Scheme 5: Red
                new Color(249f / 255f, 135f / 255f, 135f / 255f),
                new Color(249f / 255f, 111f / 255f, 111f / 255f),
                new Color(249f / 255f, 87f / 255f, 87f / 255f),
                new Color(249f / 255f, 63f / 255f, 63f / 255f),
                new Color(249f / 255f, 39f / 255f, 39f / 255f),
                new Color(230f / 255f, 26f / 255f, 26f / 255f),
                new Color(212f / 255f, 13f / 255f, 13f / 255f),
                new Color(194f / 255f, 0f / 255f, 0f / 255f),

                // Scheme 6: Yellow
                new Color(249f / 255f, 243f / 255f, 135f / 255f),
                new Color(249f / 255f, 232f / 255f, 111f / 255f),
                new Color(249f / 255f, 221f / 255f, 87f / 255f),
                new Color(249f / 255f, 210f / 255f, 63f / 255f),
                new Color(249f / 255f, 199f / 255f, 39f / 255f),
                new Color(230f / 255f, 183f / 255f, 26f / 255f),
                new Color(212f / 255f, 167f / 255f, 13f / 255f),
                new Color(194f / 255f, 151f / 255f, 0f / 255f),

                // Scheme 7: Pink
                new Color(249f / 255f, 135f / 255f, 197f / 255f),
                new Color(249f / 255f, 111f / 255f, 181f / 255f),
                new Color(249f / 255f, 87f / 255f, 165f / 255f),
                new Color(249f / 255f, 63f / 255f, 149f / 255f),
                new Color(249f / 255f, 39f / 255f, 133f / 255f),
                new Color(230f / 255f, 26f / 255f, 117f / 255f),
                new Color(212f / 255f, 13f / 255f, 101f / 255f),
                new Color(194f / 255f, 0f / 255f, 85f / 255f),

                // Scheme 8: Teal
                new Color(135f / 255f, 249f / 255f, 244f / 255f),
                new Color(111f / 255f, 249f / 255f, 231f / 255f),
                new Color(87f / 255f, 249f / 255f, 219f / 255f),
                new Color(63f / 255f, 249f / 255f, 207f / 255f),
                new Color(39f / 255f, 249f / 255f, 194f / 255f),
                new Color(26f / 255f, 230f / 255f, 176f / 255f),
                new Color(13f / 255f, 212f / 255f, 158f / 255f),
                new Color(0f / 255f, 194f / 255f, 140f / 255f)
            };

            double value = 2;

            foreach (var color in colors)
            {
                var newPrefabName = value.ToString("F0");
                var variantPrefab = CreateVariantPrefab(basePrefab, newPrefabName, value, color);
                PrefabUtility.SaveAsPrefabAsset(variantPrefab, $"Assets/Resources/Prefabs/Blocks/{newPrefabName}.prefab");
                DestroyImmediate(variantPrefab); // Clean up after creating the variant

                value *= 2;
            }

            Debug.Log("Prefab variants created successfully!");
        }

        private GameObject CreateVariantPrefab(GameObject basePrefab, string newName, double value, Color color)
        {
            var instance = (GameObject)PrefabUtility.InstantiatePrefab(basePrefab);
            instance.name = newName;

            var blockScript = instance.GetComponent<BlockView>(); // Replace 'BlockScript' with your actual script
            if (blockScript != null)
            {
                blockScript.Value = value;
            }
            else
            {
                Debug.LogWarning("BlockScript not found on prefab.");
            }

            var spriteRenderer = instance.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = color;
            }
            else
            {
                Debug.LogWarning("SpriteRenderer not found on prefab.");
            }

            var text = instance.GetComponentInChildren<TMP_Text>();

            if (text != null)
            {
                text.text = value.ToString("N0");
            }
            else
            {
                Debug.LogWarning("TMP_Text not found on prefab.");
            }

            return instance;
        }
    }
}
#endif