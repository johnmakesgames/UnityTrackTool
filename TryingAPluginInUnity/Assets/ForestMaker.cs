using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ForestMaker : EditorWindow
{
    Stack<GameObject> trees = new Stack<GameObject>();
    float forestWidth = 0;
    float forestDensity = 0;
    Vector3 forestCenter = new Vector3(0, 0, 0);

    [MenuItem("DNA Tools/ForestMaker")]
    public static void ShowWindow()
    {
        GetWindow<ForestMaker>("Forest Maker");
    }

    void OnGUI()
    {
        GUILayout.Label("Forest Creator", EditorStyles.boldLabel);

        forestCenter = EditorGUILayout.Vector3Field("Forest Center", forestCenter);


        GUILayout.Label("Forest Width:" + forestWidth.ToString());
        forestWidth = GUILayout.HorizontalScrollbar(forestWidth, 0.0f, 0.0f, 100.0f);

        GUILayout.Label("Forest Density:" + forestDensity.ToString());
        forestDensity = GUILayout.HorizontalScrollbar(forestDensity, 0.0f, 0.0f, 10.0f);

        if(GUILayout.Button("Generate Forest"))
        {
            GenerateForest();
        }

        if(GUILayout.Button("Delete Forest"))
        {
            DeleteForest();
        }
    }

    void GenerateForest()
    {

        DeleteForest();
        float totalTrees = forestWidth * forestDensity;
        Mathf.Round(totalTrees);
        for (int i = 0; i < totalTrees; i++)
        {
            Vector3 offset = new Vector3(0, 0, 0);
            offset.x = Random.Range(forestWidth * -0.5f, forestWidth * 0.5f);
            offset.z = Random.Range(forestWidth * -0.5f, forestWidth * 0.5f);

            GameObject createdObject = Instantiate(Resources.Load("Tree", typeof(GameObject)) as GameObject, forestCenter + offset, Quaternion.identity);
            createdObject.transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
            trees.Push(createdObject);
        }
    }

    void DeleteForest()
    {
        while (trees.Count > 0)
        {
            GameObject objectToDelete = trees.Pop();
            DestroyImmediate(objectToDelete.gameObject);
        }
    }
}
