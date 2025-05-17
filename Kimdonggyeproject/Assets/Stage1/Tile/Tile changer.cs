using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    public MeshRenderer meshRenderer; // 메쉬 렌더러
    public Texture origin;
    public Texture one;
    public Texture two;
    public Texture three;

    void Start()
    {
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeTexture(int n)
    {
        Texture target = origin;

        if (n == 1)
            target = one;
        else if (n == 2)
            target = two;
        else if (n == 3)
            target = three;

        meshRenderer.material.mainTexture = target;
    }
}
