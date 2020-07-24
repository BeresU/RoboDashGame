using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class SpriteSheetManipulator 
    {
        [MenuItem("Assets/Change sprite sheet pivot to bottom left", priority = -1)]
        public static void ChangePivotToBottomLeft()
        {
            if(Selection.activeObject == null) return;

            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            var textureImporter = (TextureImporter)AssetImporter.GetAtPath(path);
            
            if (textureImporter == null)
            {
                Debug.Log($"Name: {Selection.activeObject.name} is not {nameof(TextureImporter)}");
                return;
            }

            var spriteSheet = new SpriteMetaData[textureImporter.spritesheet.Length];

            for (var i = 0; i < spriteSheet.Length; i++)
            {
                var spriteMetaData = textureImporter.spritesheet[i];
                spriteMetaData.alignment = (int)SpriteAlignment.BottomLeft;
                spriteMetaData.pivot = new Vector2(0,0);
                spriteSheet[i] = spriteMetaData;
            }
            
            textureImporter.spritesheet = spriteSheet;
            
            if (!AssetDatabase.WriteImportSettingsIfDirty(textureImporter.assetPath)) return;
            
            AssetDatabase.ImportAsset(textureImporter.assetPath, ImportAssetOptions.ForceUpdate);
            AssetDatabase.Refresh();
        }
    }
}
