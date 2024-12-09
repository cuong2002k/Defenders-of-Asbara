using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopupGenerator : Singleton<DamagePopupGenerator>
{
    [Tooltip("Drag popup damage UI")]
    [SerializeField] private GameObject _damagePopuPrefabs;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetPopupDamage(this.transform.position, Random.Range(1, 1000).ToString(), Color.yellow);
        }
    }

    public void GetPopupDamage(Vector3 position, string text, Color color)
    {
        // GameObject damagePopupInstance = PoolManager.Instance.GetObjectPool(_damagePopuPrefabs);
        // damagePopupInstance.transform.position = position;
        // TextMeshProUGUI dameText = damagePopupInstance.GetComponentInChildren<TextMeshProUGUI>();
        // dameText.text = text;
        // dameText.color = color;
    }


}
