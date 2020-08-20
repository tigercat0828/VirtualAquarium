using UnityEngine;

public class Cube : MonoBehaviour {

    public void RandomColor() {
        GetComponent<Renderer>().sharedMaterial.color = Random.ColorHSV();
    }
    public void ResetColor() {
        GetComponent<Renderer>().sharedMaterial.color = Color.white;
    }
    private void Start() {
        GetComponent<Renderer>().sharedMaterial.color = Color.white;
    }
}
