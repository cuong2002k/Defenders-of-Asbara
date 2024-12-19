using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingProcessBar : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
    }
    // Update is called once per frame
    void Update()
    {
        slider.value = Loader.GetLoadingProcess;
    }
}
