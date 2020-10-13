using System.Linq;
using Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    public class SpriteSheetMenuActions
    {
        [MenuItem("Assets/SpriteActions/Change sprite sheet pivot to bottom left", false, priority = -1)]
        public static void ChangePivotToBottomLeft()
        {
            if (Selection.activeObject == null) return;

            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            var textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            var spriteSheet = new SpriteMetaData[textureImporter.spritesheet.Length];

            for (var i = 0; i < spriteSheet.Length; i++)
            {
                var spriteMetaData = textureImporter.spritesheet[i];
                spriteMetaData.alignment = (int) SpriteAlignment.BottomLeft;
                spriteMetaData.pivot = new Vector2(0, 0);
                spriteSheet[i] = spriteMetaData;
            }

            textureImporter.spritesheet = spriteSheet;

            if (!AssetDatabase.WriteImportSettingsIfDirty(textureImporter.assetPath)) return;

            AssetDatabase.ImportAsset(textureImporter.assetPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/SpriteActions/Instantiate sprites to current scene", false, priority = -1)]
        public static void InstantiateSpriteSheetInScene()
        {
            if (Selection.activeObject == null) return;

            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            var sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(path).OfType<Sprite>().ToArray();

            if (sprites.IsNullOrEmpty())
            {
                Debug.LogError($"No sprites in current path: {path}");
                return;
            }

            const string parentName = "Sky";
            const string spritePrefix = "Cloud";
            
            var scale = new Vector3(0.15f, 0.15f, 0.15f);

            var root = GameObject.Find(parentName);

            if (root == null)
            {
                Debug.LogWarning($"No root with name: {parentName} Creating new");
                root = new GameObject("SpritesRoot");
            }

            var currentScene = SceneManager.GetActiveScene();

            for (var i = 0; i < sprites.Length; i++)
            {
                var spriteRenderer = new GameObject($"{spritePrefix}_{i}").AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprites[i];
                spriteRenderer.transform.SetParent(root.transform);
                spriteRenderer.transform.ResetPosAndRot(true);
                spriteRenderer.transform.localScale = scale;
            }

            var sceneObjs = AssetDatabase.FindAssets($"t:Scene {currentScene.name}");
            
            if (sceneObjs.IsNullOrEmpty())
            {
                Debug.LogError($"Didn't found scene: {currentScene.name}");
                return;
            }

            var scenePath = AssetDatabase.GUIDToAssetPath(sceneObjs.First());
            var sceneAsset = AssetDatabase.LoadAssetAtPath<Object>(scenePath);
            
            if (sceneAsset == null)
            {
                Debug.LogError($"Couldn't load scene asset");
                return;
            }
            
            EditorUtility.SetDirty(sceneAsset);
            AssetDatabase.SaveAssets();
        }


        [MenuItem("Assets/SpriteActions/Change sprite sheet pivot to bottom left", true)]
        [MenuItem("Assets/SpriteActions/Instantiate sprites to current scene", true)]
        private static bool Validate_ChangePivotToBottomLeft()
        {
            if (Selection.activeObject == null) return false;

            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            var textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            return textureImporter != null;
        }
    }
}