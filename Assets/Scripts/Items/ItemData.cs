using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public string itemName;
    public bool isLegendary;
    public string ball;
    public Sprite sprite;
    public string itemDescription;
    public bool isTypeSpecific;
    public string type;
    public bool isSceneSpecific;
    public string scene;
    
    //[CustomEditor(typeof(ItemData))]
    //[CanEditMultipleObjects]
    //public class ItemDataEditor : Editor
    //{
    //    override public void OnInspectorGUI()
    //    {
    //        var data = target as ItemData;

    //        SerializedProperty sprite;

    //        sprite = serializedObject.FindProperty("sprite");

    //        EditorGUILayout.PropertyField(sprite, new GUIContent("Sprite:"));
    //        data.itemName = EditorGUILayout.TextField("Item Name:", data.itemName);
    //        data.ball = EditorGUILayout.TextField("Ball:", data.ball);
    //        data.itemDescription = EditorGUILayout.TextField("Item Description:", data.itemDescription);

    //        data.isLegendary = GUILayout.Toggle(data.isLegendary, "Is Legendary:");

    //        data.isTypeSpecific = GUILayout.Toggle(data.isTypeSpecific, "Is Type Specific:");

    //        if (data.isTypeSpecific)
    //        {
    //            data.type = EditorGUILayout.TextField("Typing:", data.type);
    //        }

    //        data.isSceneSpecific = GUILayout.Toggle(data.isSceneSpecific, "Is Scene Specific:");
    //        if (data.isSceneSpecific)
    //        {
    //            data.scene = EditorGUILayout.TextField("Scene:", data.scene);
    //        }

    //        serializedObject.ApplyModifiedProperties();
    //    }
    //}
}
