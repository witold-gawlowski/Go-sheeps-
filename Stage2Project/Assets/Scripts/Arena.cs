using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Arena : MonoBehaviour
{
    [SerializeField]
    private Camera Cam;

    public static float Width { get; private set; }
    public static float Height { get; private set; }


    void Update()
    {
//I needed to comment it as otherwise it wouldnt calculate the proper size of arena in game builds. 
//#if UNITY_EDITOR
    Calculate();
    //Todo: why not working with this if statement?
    //if (!Application.isPlaying)
    //    {
    //  Calculate();
    //    }
//#endif
    }

    public void Calculate()
    {

        if (Cam != null)
        {
            Height = CameraUtils.FrustumHeightAtDistance(Cam.farClipPlane - 1.0f, Cam.fieldOfView) * 7 / 10;
            Width = Height * Cam.aspect;
            transform.localScale = new Vector3(Width * 0.1f, 1.0f, Height * 0.1f );
        }
    }
}
