using System.Collections;
using System.Collections.Generic;
using ObserverExtentision;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class StartWave : MonoBehaviour
{
    [Header("reference")]
    private Button button;

    void Start()
    {
      button = GetComponent<Button>();
      if(button != null)
      {
        button.onClick.AddListener(StartWaveAction);
      }
      else
      {

        Common.Log("Cannot find Button component in object {0}", this.gameObject.name);
        this.gameObject.AddComponent<Button>();
      }
      
    }
    /// <summary>
    /// Start wave 
    /// </summary>
    private void StartWaveAction()
    {
      this.gameObject.SetActive(false);
      this.PostEvent(EventID.OnStartWave);
      UIManager.Instance.WaveUI.gameObject.SetActive(true);
    }
}
