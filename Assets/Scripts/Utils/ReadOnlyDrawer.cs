using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GV.Extensions
{
    /// <summary>
    /// Classe de atributo usado para marcar uma variável como somente leitura.
    /// </summary>
    public class ReadOnlyAttribute : PropertyAttribute { }

#if UNITY_EDITOR
    /// <summary>
    /// Classe do desenhista personalizado para variáveis marcadas com [ReadOnly].
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }

        public override bool CanCacheInspectorGUI(SerializedProperty property)
        {
            return false;
        }
    }
#endif
}