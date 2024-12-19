using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool _firstLoading = true;

    // Update is called once per frame
    void Update()
    {
        if (_firstLoading)
        {
            _firstLoading = false;
            Loader.LoaderCallback();
        }
    }
}
