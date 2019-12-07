using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpriteGlow
{
    [AddComponentMenu("Effects/Sprite Glow")]
    [RequireComponent(typeof(SpriteRenderer)),DisallowMultipleComponent,ExecuteInEditMode]
    public class SpriteGlowEffect : MonoBehaviour
    {
        public SpriteRenderer Renderer { get; private set; }
        public Color GlowColor
        {
            get => glowColor;
            set { if (glowColor != value) { glowColor = value; SetMaterialProperties(); } }
        }
        public float GlowBrightness
        {
            get => glowBrightness;
            set { if (glowBrightness != value) { glowBrightness = value; SetMaterialProperties(); } }
        }
        public int OutlineWidth
        {
            get => OutlineWidth;
            set { if (outlineWidth != value) { outlineWidth = value;SetMaterialProperties(); } }
        }
        public float AlphaThreshold
        {
            get => alphaThreshold;
            set { if (alphaThreshold != value) { alphaThreshold = value;SetMaterialProperties(); } }
        }
        public bool DrawOutside
        {
            get => drawOutside;
            set { if (drawOutside != value) { drawOutside = value;SetMaterialProperties(); } }
        }
        public bool EnableInstancing
        {
            get => enableInstancing;
            set { if (enableInstancing != value) { enableInstancing = value;SetMaterialProperties(); } }
        }

        [Tooltip("グローの基本色")]
        [SerializeField] private Color glowColor = Color.white;
        [Tooltip("グローの明るさ"), Range(1, 10)]
        [SerializeField] private float glowBrightness = 2f;
        [Tooltip("テクセル単位のアウトラインの幅"), Range(0, 10)]
        [SerializeField] private int outlineWidth = 1;
        [Tooltip("スプライトの境界を決定する為の値"), Range(0f, 1f)]
        [SerializeField] private float alphaThreshold = .01f;
        [Tooltip("アウトラインをスプライトの境界の外側にのみ描画するか")]
        [SerializeField] private bool drawOutside = false;
        [Tooltip("GPUのインスタンス化を有効にするか")]
        [SerializeField] private bool enableInstancing = false;

        private static readonly int isOutlineEnabledId = Shader.PropertyToID("_IsOutlineEnabled");
        private static readonly int outlineColorId = Shader.PropertyToID("_OutlineColor");
        private static readonly int outlineSizeId = Shader.PropertyToID("_OutlineSize");
        private static readonly int alphaThresholdId = Shader.PropertyToID("_AlphaThreshold");

        private MaterialPropertyBlock materialProperties;

        private void Awake()
        {
            Renderer = GetComponent<SpriteRenderer>();
        }

        private void OnDisable()
        {
            SetMaterialProperties();
        }

        private void OnEnable()
        {
            SetMaterialProperties();
        }

        private void OnValidate()
        {
            if (!isActiveAndEnabled) return;

            SetMaterialProperties();
        }

        private void OnDidApplyAnimationProperties()
        {
            SetMaterialProperties();
        }


        private void SetMaterialProperties()
        {
            //RendererがNULLなら
            if (!Renderer) return;

            Renderer.sharedMaterial = SpriteGlowMaterial.GetShaderFor(this);

            if (materialProperties == null)
            {
                materialProperties = new MaterialPropertyBlock();
            }

            if (materialProperties == null)
            {
                materialProperties.SetFloat(isOutlineEnabledId, isActiveAndEnabled ? 1 : 0);
                materialProperties.SetColor(outlineColorId, GlowColor * GlowBrightness);
                materialProperties.SetFloat(outlineSizeId, OutlineWidth);
                materialProperties.SetFloat(alphaThresholdId, AlphaThreshold);

                Renderer.SetPropertyBlock(materialProperties);
            }



        }
    }

}
