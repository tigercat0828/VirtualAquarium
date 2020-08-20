using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cube))]
public class CubeEditor : Editor {

    private static float editColorR = 0f;
    private static float editColorG = 0f;
    private static float editColorB = 0f;
    private static float editBaseSize = 1f;

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();
        Cube cube = (Cube)target;
        // layout default by vertical
        GUILayout.Label("Setting");    // A label text
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Random Color")) {   // if click the button, call the function below
            cube.RandomColor();
        }
        if (GUILayout.Button("Reset Color")) {
            cube.ResetColor();
        }
        GUILayout.EndHorizontal();      // end the group

        // store RGB data into editColor
        editColorR = cube.GetComponent<Renderer>().sharedMaterial.color.r * 255;
        editColorG = cube.GetComponent<Renderer>().sharedMaterial.color.g * 255;
        editColorB = cube.GetComponent<Renderer>().sharedMaterial.color.b * 255;

        GUILayout.BeginVertical();       // create vertical control group
        editColorR = EditorGUILayout.Slider("Color Red", editColorR, 0f, 255f);     // slider value between 0~255 
        editColorG = EditorGUILayout.Slider("Color Green", editColorG, 0f, 255f);   // and return value
        editColorB = EditorGUILayout.Slider("Color Blue", editColorB, 0f, 255f);
        GUILayout.EndVertical();
        editBaseSize = EditorGUILayout.Slider("Base size", editBaseSize, 1f, 10f);
        
        // update value after setting editor
        Color color = new Color(editColorR / 255f, editColorG / 255f, editColorB / 255f);
        cube.GetComponent<Renderer>().sharedMaterial.color = color;
        cube.transform.localScale = Vector3.one * editBaseSize;
    }
}
