using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTest : MonoBehaviour {

    private Renderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("StandardOutline");
    }
	
	// Update is called once per frame
	void Update () {
        float thickness = 0.2f * Mathf.Sin(Time.time) + 1;
        rend.material.SetFloat("_OutlineWidth", thickness);
        print(thickness);
	}
}
