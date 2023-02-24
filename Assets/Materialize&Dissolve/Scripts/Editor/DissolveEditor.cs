using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine.UIElements;

[CustomEditor(typeof(Dissolver))]
[CanEditMultipleObjects]
public class DissolverEditor : Editor
{
    SerializedProperty duration;
    SerializedProperty ShouldMaterializeFirst;
    SerializedProperty meshesDetection;
    SerializedProperty renderersList;

    void OnEnable()
    {
        duration = serializedObject.FindProperty("Duration");
        ShouldMaterializeFirst = serializedObject.FindProperty("ShouldMaterializeFirst");
        meshesDetection = serializedObject.FindProperty("meshesDetection");
        renderersList = serializedObject.FindProperty("renderers");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(duration);
        EditorGUILayout.PropertyField(ShouldMaterializeFirst);
        EditorGUILayout.PropertyField(meshesDetection);
        EditorGUILayout.PropertyField(renderersList,true);

        Dissolver d = (Dissolver)target;
        if (GUILayout.Button("Find Renderers"))
        {
            d.FindRenderers();
        }

        //if (GUILayout.Button("Change Materials"))
        //{
        //    d.ReplaceMaterials();
        //}

       
        serializedObject.ApplyModifiedProperties();
    }
}

[CustomPropertyDrawer(typeof(Dissolver.RendererMaterials))]
public class IngredientDrawerUIE : PropertyDrawer
{
    const int PropetyHeightOffset = 20;
    const int RenderHeight = 20;

    const int MaterialsHeight = 25;
    const float MaterialsWidthScale = 3;

    const float MaterialsToRenderListWidthScale = 2;

    const int XSpacing = 10;
    const int ObjectsOnTop = 3;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int height = 0;
        SerializedProperty materials = property.FindPropertyRelative("materials");
        SerializedProperty shouldReplace = property.FindPropertyRelative("shouldReplaceMaterials");


        height += RenderHeight* (ObjectsOnTop-1);


        if (shouldReplace.boolValue)
        {
            height += PropetyHeightOffset;
            height += RenderHeight;
            height += materials.arraySize * MaterialsHeight;
        }

        return height;
    }

    public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
    {
        SerializedProperty renderer = property.FindPropertyRelative("renderer");
        SerializedProperty shouldReplace = property.FindPropertyRelative("shouldReplaceMaterials");
        SerializedProperty materials = property.FindPropertyRelative("materials");
        SerializedProperty materialsToReplace = property.FindPropertyRelative("materialsToReplace");

        // Objects on top
        EditorGUI.PropertyField(new Rect(pos.x, pos.y, pos.width, RenderHeight), renderer);
        EditorGUI.PropertyField(new Rect(pos.x, pos.y + RenderHeight, pos.width, RenderHeight), shouldReplace);

        if (shouldReplace.boolValue)
        {
            EditorGUI.LabelField(new Rect(pos.x + pos.width / MaterialsWidthScale + XSpacing, pos.y + RenderHeight * 2, pos.width, RenderHeight), new GUIContent("Renplacement Materials"), EditorStyles.boldLabel);

            GUI.enabled = false;
            for (int i = 0; i < materials.arraySize; i++)
            {
                SerializedProperty MyListRef = materials.GetArrayElementAtIndex(i);

                var rect = new Rect(
                    pos.x, 
                    pos.y + i * MaterialsHeight + XSpacing + RenderHeight * ObjectsOnTop,
                    pos.width / MaterialsWidthScale,
                    MaterialsHeight);

                EditorGUI.PropertyField(rect, MyListRef, GUIContent.none);
            }
            GUI.enabled = true;

            for (int i = 0; i < materials.arraySize; i++)
            {
                if (materialsToReplace.arraySize < i + 1)
                {
                    materialsToReplace.InsertArrayElementAtIndex(i);
                }
                SerializedProperty MyListRef = materialsToReplace.GetArrayElementAtIndex(i);

                var rect = new Rect(
                    pos.x + pos.width / MaterialsWidthScale + XSpacing,
                    pos.y + i * MaterialsHeight + XSpacing + RenderHeight * ObjectsOnTop,
                    pos.width / MaterialsToRenderListWidthScale, 
                    MaterialsHeight);

                EditorGUI.PropertyField(rect, MyListRef, GUIContent.none);
            }
        }

        
    }
}