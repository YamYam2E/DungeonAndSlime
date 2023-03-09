using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Common.Sprite.Editor
{
    public class SpriteSheetSlicer : MonoBehaviour
    {
        [MenuItem("Custom/Setting Sprite Sheet ( 16x24 size )", false)]
        private static void Slice()
        {
            PrepareSlice();
            DoSlice();
        }

        private static void PrepareSlice()
        {
            for (var index = 0; index < Selection.objects.Length; ++index)
            {
                var assetPath = AssetDatabase.GetAssetPath(Selection.objects[index].GetInstanceID());
                var textureImporter = TextureImporter.GetAtPath(assetPath) as TextureImporter;
                var textureImporterSettings = new TextureImporterSettings();
            
                /*
                 * Set Multiple sprite mode
                 */
                textureImporter.ReadTextureSettings(textureImporterSettings);
                textureImporterSettings.spriteMode = (int)SpriteImportMode.Multiple;
                textureImporter.SetTextureSettings(textureImporterSettings);
            
                textureImporter.isReadable = true;
                textureImporter.filterMode = FilterMode.Point;
                textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
                AssetDatabase.SaveAssets();
            }
        }

        private static void DoSlice()
        {
            for (var index = 0; index < Selection.objects.Length; ++index)
            {
                var assetPath = AssetDatabase.GetAssetPath(Selection.objects[index].GetInstanceID());
                var textureImporter = TextureImporter.GetAtPath(assetPath) as TextureImporter;
                var textureImporterSettings = new TextureImporterSettings();

                /*
                 * Slice
                 */
                var texture2D = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
                var rects = InternalSpriteUtility.GenerateGridSpriteRectangles(
                    texture2D,
                    Vector2.zero,
                    new Vector2(16, 24),
                    Vector2.zero,
                    false);

                var rectsList = new List<Rect>(rects);
                var filenameNoExtension = Path.GetFileNameWithoutExtension(assetPath);
                var metas = new List<SpriteMetaData>();
                var rectNum = 0;

                foreach (var rect in rectsList)
                {
                    var meta = new SpriteMetaData
                    {
                        name = $"{filenameNoExtension}_{rectNum++}",
                        rect = rect,
                        alignment = (int)SpriteAlignment.Center
                    };

                    metas.Add(meta);
                }
                
                textureImporter.spritesheet = metas.ToArray();

                AssetDatabase.ForceReserializeAssets(new List<string>() { assetPath });
                AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
                AssetDatabase.SaveAssets();
            }
        }

        /// <summary>
        /// 아래의 코드는 하나의 sprite sheet에 2개의 애니메이션이 들어있기 때문에,
        /// 강제로 5번째 sprite부터 키 프레임을 잡도록 만듦
        /// </summary>
        [MenuItem("Custom/Create Sprite Animation")]
        private static void CreateSpriteLibrary()
        {
            foreach (var spriteObject in Selection.objects)
            {
                var assetPath = AssetDatabase.GetAssetPath(spriteObject.GetInstanceID());
                var sprites = AssetDatabase.LoadAllAssetsAtPath(assetPath);

                var clip = new AnimationClip
                {
                    name = $"{spriteObject.name}",
                    frameRate = 60,
                    wrapMode = WrapMode.Loop
                };

                var keyFrames = new List<ObjectReferenceKeyframe>();
                var timeIndex = 0;
                
                for (var i = 5; i < sprites.Length; i++)
                {
                    var keyframe = new ObjectReferenceKeyframe()
                    {
                        time = 0.1f * timeIndex++,
                        value = sprites[i]
                    };

                    keyFrames.Add(keyframe);
                }
                
                /*
                 * Add Last frame to force
                 */
                var lastKeyframe = new ObjectReferenceKeyframe()
                {
                    time = 0.1f * timeIndex++,
                    value = sprites[5]
                };
                
                keyFrames.Add(lastKeyframe);

                AnimationUtility.SetObjectReferenceCurve(
                    clip,
                    EditorCurveBinding.PPtrCurve("", typeof(SpriteRenderer), "m_Sprite"),
                    keyFrames.ToArray());

                var settings = AnimationUtility.GetAnimationClipSettings(clip);
                settings.loopTime = true;
                AnimationUtility.SetAnimationClipSettings(clip, settings);
                
                AssetDatabase.CreateAsset(clip, $"Assets/Resources/Enemies/Animation/{clip.name}_run.anim");
            }
        }
    }
}