using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public string itemName;
    public bool isLegendary;
    public int legendaryPKMN = 0;
    public bool twoLegends;
    public int legendaryPKMNTwo = 0;
    public bool threeLegends;
    public int legendaryPKMNThree = 0;
    public string ball;
    public Sprite sprite;
    public string itemDescription;
    public bool isTypeSpecific;
    public string type;

    [CustomEditor(typeof(ItemData))]
    [CanEditMultipleObjects]
    public class ItemDataEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            var data = target as ItemData;

            SerializedProperty sprite;

            sprite = serializedObject.FindProperty("sprite");

            EditorGUILayout.PropertyField(sprite, new GUIContent("Sprite:"));
            data.itemName = EditorGUILayout.TextField("Item Name:", data.itemName);
            data.ball = EditorGUILayout.TextField("Ball:", data.ball);
            data.itemDescription = EditorGUILayout.TextField("Item Description:", data.itemDescription);

            data.isLegendary = GUILayout.Toggle(data.isLegendary, "Is Legendary:");
            if (data.isLegendary)
            {
                data.twoLegends = GUILayout.Toggle(data.twoLegends, "Two Legendaries:");
                if (data.twoLegends)
                {
                    data.threeLegends = GUILayout.Toggle(data.threeLegends, "Three Legendaries:");
                }
            }

            if (data.isLegendary)
            {
                data.legendaryPKMN = EditorGUILayout.IntField("Legendary Num:", data.legendaryPKMN);
            }

            if (data.twoLegends)
            {
                data.legendaryPKMNTwo = EditorGUILayout.IntField("Legendary Num Two:", data.legendaryPKMNTwo);
            }

            if (data.threeLegends)
            {
                data.legendaryPKMNThree = EditorGUILayout.IntField("Legendary Num Three:", data.legendaryPKMNThree);
            }

            data.isTypeSpecific = GUILayout.Toggle(data.isTypeSpecific, "Is Type Specific:");

            if (data.isTypeSpecific)
            {
                data.type = EditorGUILayout.TextField("Typing:", data.type);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
