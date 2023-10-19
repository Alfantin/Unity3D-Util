using UnityEngine;

public class TextureBuild {

    public string Name;
    public int Size;
    public Material Mat;
    private Color[] Pixels;
    private Texture2D Texture;

    public TextureBuild(string name, Material mat, int size) {
        Name = name;
        Mat = mat;
        Size = size;
        Texture = new Texture2D(size, size);
        Pixels = new Color[size * size];
        mat.SetTexture(name, Texture);
    }

    public Color GetPixel(int x, int y) {
        return Pixels[Size * y + x];
    }

    public void SetPixel(int x, int y, Color color) {
        Pixels[Size * y + x] = color;
    }

    public void Apply() {
        Texture.SetPixels(Pixels);
        Texture.Apply();
        Mat.SetTexture(Name, Texture);
    }

}