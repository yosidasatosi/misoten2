using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpriteGlow
{
    public class SpriteGlowMaterial : Material
    {
        //SpriteTextureにmainTextureを代入
        public Texture SpriteTexture => mainTexture;

        public bool DrawOutside => IsKeywordEnabled(outsideMaterialKeyword);
        public bool InstancingEnable => enableInstancing;

        private const string outlineShaderName = "Sprites/Outline";
        private const string outsideMaterialKeyword = "SPRITE_OUTLINE_OUTSIDE";

        private static readonly Shader outlineShader = Shader.Find(outlineShaderName);
        private static readonly List<SpriteGlowMaterial> shaderMaterials = new List<SpriteGlowMaterial>();

        public SpriteGlowMaterial(Texture spriteTexture, bool drawOutside = false, bool instancingEnabled = false)
            : base(outlineShader)
        {
            if (!outlineShader) Debug.LogError($"`{outlineShaderName}` shader not found. Make sure the shader is included to the build.");

            mainTexture = SpriteTexture;
            if (drawOutside)
            {
                EnableKeyword(outsideMaterialKeyword);
            }

            if (instancingEnabled)
            {
                enableInstancing = true;
            }
        }

        public static Material GetShaderFor(SpriteGlowEffect spriteGlow)
        {
            for (int i = 0; i < shaderMaterials.Count; i++)
            {
                if(shaderMaterials[i].SpriteTexture==spriteGlow.Renderer.sprite.texture&&
                    shaderMaterials[i].DrawOutside==spriteGlow.DrawOutside&&
                    shaderMaterials[i].InstancingEnable == spriteGlow.EnableInstancing)
                {
                    return shaderMaterials[i];
                }
            }

            var material = new SpriteGlowMaterial(spriteGlow.Renderer.sprite.texture, spriteGlow.DrawOutside, spriteGlow.EnableInstancing);
            material.hideFlags = HideFlags.DontSaveInBuild | HideFlags.DontSaveInEditor | HideFlags.NotEditable;
            shaderMaterials.Add(material);

            return material;
        }


    }

}
