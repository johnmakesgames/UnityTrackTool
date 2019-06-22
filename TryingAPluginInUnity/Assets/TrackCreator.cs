using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

public class TrackCreator : EditorWindow
{
    int roadSize = 0;
    Vector3 position = new Vector3(0, 0, 0);
    Vector3 scale = new Vector3(1, 1, 1);
    Color colour;
    Socket previousObject = null;
    Stack<GameObject> roadPieces = new Stack<GameObject>();
    string roadCode = "";

    string roadToLoad = "";

    public string[] trackPieceLocations = new string[] { "Prefabs/FinalRoadPieces/Vertacle Straight", "Prefabs/FinalRoadPieces/LeftTurn", "Prefabs/FinalRoadPieces/RightTurn", "Prefabs/FinalRoadPieces/VertacleStraightRampUp", "Prefabs/FinalRoadPieces/VertacleStraightRampDown", "Prefabs/FinalRoadPieces/VertacleStraightBump", "Prefabs/FinalRoadPieces/RoadStart", "Prefabs/FinalRoadPieces/Boost", "Prefabs/FinalRoadPieces/JumpRamp", "Prefabs/FinalRoadPieces/Gap" };
    public int index = 0;


    public GameObject InstantiatedObject;
    [MenuItem("DNA Tools/TrackCreationToolFast")]
    public static void ShowWindow()
    {
        GetWindow<TrackCreator>("Track Creator Fast!");
    }

    void OnGUI()
    {
        //Window Code
        GUILayout.Label("Track Maker!", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Left Turn", GUILayout.Width(115), GUILayout.Height(30)))
        {
            Place(1);
        }
        if (GUILayout.Button("Straight", GUILayout.Width(115), GUILayout.Height(30)))
        {
            Place(0);
        }
        if (GUILayout.Button("Right Turn", GUILayout.Width(115), GUILayout.Height(30)))
        {
            Place(2);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Ramp Up", GUILayout.Width(115), GUILayout.Height(30)))
        {
            Place(3);
        }
        if (GUILayout.Button("Ramp Down", GUILayout.Width(115), GUILayout.Height(30)))
        {
            Place(4);
        }
        if (GUILayout.Button("Bump", GUILayout.Width(115), GUILayout.Height(30)))
        {
            Place(5);
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Boost", GUILayout.Width(115), GUILayout.Height(30)))
        {
            Place(7);
        }
        if (GUILayout.Button("Jump Ramp", GUILayout.Width(115), GUILayout.Height(30)))
        {
            Place(8);
        }
        if (GUILayout.Button("Gap", GUILayout.Width(115), GUILayout.Height(30)))
        {
            Place(9);
        }
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Undo"))
        {
            Undo();
        }
        if (GUILayout.Button("Reset"))
        {
            Reset();
        }

        EditorGUILayout.TextField("Road Code: ", CompressString(roadCode));
        GUILayout.Space(10);

        GUILayout.Label("Track Loader!", EditorStyles.boldLabel);
        roadToLoad = EditorGUILayout.TextField("Road Text: ", roadToLoad);
        
        if (GUILayout.Button("Load Road"))
        {
            LoadRoad(roadToLoad);
        }
    }

    void Place(int choice)
    {
        if (roadCode.Length == 0)
            choice = 6;
        GameObject createdObject = Instantiate(Resources.Load(trackPieceLocations[choice], typeof(GameObject)) as GameObject, position, Quaternion.identity);
        if (roadPieces.Count > 0)
        {
            createdObject.GetComponent<Socket>().InformOfPreviousPosition(roadPieces.Peek().GetComponent<Transform>().position);
            if (roadCode[roadCode.Length - 1] == 'u')
                createdObject.transform.position += new Vector3(0, 3.723f, 0);
            if (choice == 4)
                createdObject.transform.position += new Vector3(0, -3.723f, 0);
        }
        createdObject.transform.localScale = scale;
        roadPieces.Push(createdObject);
        if (createdObject != null)
        {
            position = createdObject.GetComponent<Socket>().GetSocketPosition();
        }
        roadSize++;

        if (createdObject.GetComponent<Renderer>())
        {
            createdObject.GetComponent<Renderer>().sharedMaterial.color = colour;
        }

        switch (choice)
        {
            case 0:
                roadCode += "s";
                break;
            case 1:
                roadCode += "l";
                break;
            case 2:
                roadCode += "r";
                break;
            case 3:
                roadCode += "u";
                break;
            case 4:
                roadCode += "d";
                break;
            case 5:
                roadCode += "b";
                break;
            case 6:
                roadCode += "o";
                break;
            case 7:
                roadCode += "p";
                break;
            case 8:
                roadCode += "j";
                break;
            case 9:
                roadCode += "g";
                break;
        }
    }

    void Undo()
    {
        string tempRoadCode = "";
        for (int i = 0; i < roadCode.Length - 1; i++)
        {
            tempRoadCode += roadCode[i];
        }
        roadCode = tempRoadCode;

        GameObject objectToDelete = roadPieces.Pop();
        DestroyImmediate(objectToDelete.gameObject);
        position = roadPieces.Peek().GetComponent<Socket>().GetSocketPosition();
    }

    void Reset()
    {
        position = new Vector3(0, 0, 0);
        previousObject = null;
        while (roadPieces.Count > 0)
        {
            GameObject objectToDelete = roadPieces.Pop();
            DestroyImmediate(objectToDelete.gameObject);
        }
        roadCode = "";
        roadToLoad = "";
    }

    void LoadRoad(string road)
    {
        string decompressedRoad = DecompressString(road);
        for(int i = 0; i < decompressedRoad.Length; i++)
        {
            ProcessLetter(decompressedRoad[i]);
        }
    }

    void ProcessLetter(char letter)
    {
        switch (letter)
        {
            case 's':
                Place(0);
                break;
            case 'l':
                Place(1);
                break;
            case 'r':
                Place(2);
                break;
            case 'u':
                Place(3);
                break;
            case 'd':
                Place(4);
                break;
            case 'b':
                Place(5);
                break;
            case 'o':
                Place(6);
                break;
            case 'p':
                Place(7);
                break;
            case 'j':
                Place(8);
                break;
            case 'g':
                Place(9);
                break;
            default:
                break;
        }
    }

    string CompressString(string stringToCompress)
    {
        string compressed = "";
        if(stringToCompress.Length > 0)
        {
            char currentLetter = stringToCompress[0];
        int counter = 1;
        for(int i = 1; i < stringToCompress.Length; i++)
        {
            if(currentLetter == stringToCompress[i])
            {
                counter++;
            }
            else
            {
                if (counter > 1)
                    compressed += counter.ToString() + currentLetter;
                else
                    compressed += currentLetter;
                currentLetter = stringToCompress[i];
                counter = 1;
            }
        }
        if (counter > 1)
            compressed += counter.ToString() + currentLetter;
        else
            compressed += currentLetter;
        }
        return compressed;
    }

    string DecompressString(string stringToDecompress)
    {
        string decompressed = "";
        List<int> amounts = new List<int>();
        string numAsString = "";
        for (int i = 0; i < stringToDecompress.Length; i++)
        {
            if(!IsLetter(stringToDecompress[i]))
            {
                numAsString += stringToDecompress[i];
            }
            else
            {
                int num = 1;
                if(numAsString != "")
                {
                    num = Convert.ToInt32(numAsString);
                }
                for(int j = 0; j < num; j++)
                {
                    decompressed += stringToDecompress[i];
                }
                numAsString = "";
            }
        }
        return decompressed;
    }

    bool IsLetter(char letter)
    {
        switch(letter)
        {
            case 'a':
                return true;
                break;
            case 'b':
                return true;
                break;
            case 'c':
                return true;
                break;
            case 'd':
                return true;
                break;
            case 'e':
                return true;
                break;
            case 'f':
                return true;
                break;
            case 'g':
                return true;
                break;
            case 'h':
                return true;
                break;
            case 'i':
                return true;
                break;
            case 'j':
                return true;
                break;
            case 'k':
                return true;
                break;
            case 'l':
                return true;
                break;
            case 'm':
                return true;
                break;
            case 'n':
                return true;
                break;
            case 'o':
                return true;
                break;
            case 'p':
                return true;
                break;
            case 'q':
                return true;
                break;
            case 'r':
                return true;
                break;
            case 's':
                return true;
                break;
            case 't':
                return true;
                break;
            case 'u':
                return true;
                break;
            case 'v':
                return true;
                break;
            case 'w':
                return true;
                break;
            case 'x':
                return true;
                break;
            case 'y':
                return true;
                break;
            case 'z':
                return true;
                break;
            default:
                return false;
                break;
        }
        return false;
    }
}