using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(FadeImage))]
public class FadeImageEditor : ImageEditor
{
    protected SerializedProperty FadeMode;
    protected SerializedProperty FadeStartAlpha;
    protected SerializedProperty FadeEndAlpha;

    protected override void OnEnable()
    {
        base.OnEnable();
        FadeMode = serializedObject.FindProperty("FadeMode");
        FadeStartAlpha = serializedObject.FindProperty("FadeStartAlpha");
        FadeEndAlpha = serializedObject.FindProperty("FadeEndAlpha");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(FadeMode);
        EditorGUILayout.PropertyField(FadeStartAlpha);
        EditorGUILayout.PropertyField(FadeEndAlpha);
        serializedObject.ApplyModifiedProperties();
    }
}