using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefenderOfAsbara.UI
{
    public class RadiusVisualized : ViewBase
    {
        [SerializeField] private GameObject _RadiusVisual;
        private float _radiusVisualHeight = 0.15f;

        private void Start()
        {
            Initialize();
        }

        public void SetupRadiusVisual(Transform parent, float radius, Color color)
        {

            this.Show();
            this._RadiusVisual.transform.parent = parent;
            this._RadiusVisual.transform.localPosition = new Vector3(0f, -_radiusVisualHeight, 0f);
            this._RadiusVisual.transform.localRotation = Quaternion.Euler(Vector3.zero);
            this._RadiusVisual.transform.localScale = Vector3.one * radius;

            Renderer renderer = this._RadiusVisual.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }

        }

        public override void Initialize()
        {
            this.Hide();
        }

        public override void Hide()
        {
            base.Hide();
            this._RadiusVisual.transform.parent = this.transform;
        }
    }
}

