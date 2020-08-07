using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class SpriteSheetManipulator 
    {
        [MenuItem("Assets/SpriteActions/Change sprite sheet pivot to bottom left" ,false,priority = -1)]
        public static void ChangePivotToBottomLeft()
        {
            if(Selection.activeObject == null) return;

            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            var textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            
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
        
        [MenuItem("Assets/SpriteActions/Change sprite sheet pivot to bottom left" ,true)]
        private static bool Validate_ChangePivotToBottomLeft()
        {
            if(Selection.activeObject == null) return false;
            
            var path = AssetDatabase.GetAssetPath(Selection.activeObject);
            var textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            
            return textureImporter != null;
        }
    }
}
