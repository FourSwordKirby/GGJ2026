using UnityEngine;

public class TextureCube : MonoBehaviour
{
    void Start()
    {
        int textureWidth = 64;
        Texture2D rainbowTex = new Texture2D(textureWidth, 1);

        for (int x = 0; x < textureWidth; x++)
        {
            float hue = (float)x / textureWidth;
            
            Color color = Color.HSVToRGB(hue, 1f, 1f);

            rainbowTex.SetPixel(x, 0, color);
        }
        rainbowTex.Apply();

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material.SetTexture("_MainTex", rainbowTex);  

        renderer.material.SetTextureScale("_BaseMap", new Vector2(1, 1));
    }
}
