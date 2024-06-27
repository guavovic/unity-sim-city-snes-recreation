using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GV.Extensions
{
    [System.Serializable]
    public class SceneField
    {
        [SerializeField] private UnityEngine.Object sceneAsset;
        [SerializeField] private string sceneName = "";
       
        public SceneName SceneName
        {
            get
            {
                if (Enum.TryParse(sceneName, true, out SceneName parsedSceneName))
                {
                    return parsedSceneName;
                }
                else
                {
                    Debug.LogError($"Invalid scene name: {sceneName}");
                    return SceneName.None;
                }
            }
        }

        //make it work with existing Unity methods (LoadLevel/LoadScene)
        public static implicit operator string(SceneField sceneField) { return sceneField.SceneName.ToString(); }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, GUIContent.none, property);

            SerializedProperty sceneAsset = property.FindPropertyRelative("sceneAsset");
            SerializedProperty sceneName = property.FindPropertyRelative("sceneName");

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            if (sceneAsset != null)
            {
                sceneAsset.objectReferenceValue = EditorGUI.ObjectField(position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);

                if (sceneAsset.objectReferenceValue != null)
                    sceneName.stringValue = (sceneAsset.objectReferenceValue as SceneAsset).name;
            }

            EditorGUI.EndProperty();
        }
    }
#endif
}