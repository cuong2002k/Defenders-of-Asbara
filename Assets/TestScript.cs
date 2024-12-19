using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float start = 20.0f;
    public float end = 40.0f;
    public float currentProgress = 22.0f;
    // Start is called before the first frame update

    [ContextMenu("Call InverseLerp")]
    void onInverseLerp()
    {
        // the progress between start and end is stored as a 0-1 value, in 'i'
        float i = Mathf.Lerp(start, end, currentProgress);

        Debug.Log(i);
    }



}
