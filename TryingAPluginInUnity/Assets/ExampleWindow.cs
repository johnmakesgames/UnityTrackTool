using UnityEngine;
using UnityEditor;

public class ExampleWindow : EditorWindow
{
    string myString = "Hello World";
    Vector3 position = new Vector3(0, 0, 0);
    Vector3 scale = new Vector3(1, 1, 1);
    Color colour;

    public GameObject InstantiatedObject;
    [MenuItem("DNA Tools/Example")]
    public static void ShowWindow()
    {
        GetWindow<ExampleWindow>("Example");
    }

    void OnGUI()
    {
        //Window Code
        GUILayout.Label("Spawn a cube!", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Name", myString);
        position = EditorGUILayout.Vector3Field("Spawn Position", position);
        scale = EditorGUILayout.Vector3Field("Spawn Scale", scale);
        colour = EditorGUILayout.ColorField("Colour", colour);
        
       // InstantiatedObject = EditorGUILayout.ObjectField(InstantiatedObject, typeof(GameObject), true);
        if (GUILayout.Button("Press Me"))
        {
            InstantiatedObject = Resources.Load("Assets/Cube") as GameObject;

            Debug.Log("Button was pressed");
            GameObject createdObject = Instantiate(Resources.Load("Cube", typeof(GameObject)) as GameObject, position, Quaternion.identity);
            createdObject.name = myString;
            createdObject.transform.localScale = scale;
            if(createdObject.GetComponent<Renderer>())
            {
                createdObject.GetComponent<Renderer>().sharedMaterial.color = colour;
            }
        }
    }
}
