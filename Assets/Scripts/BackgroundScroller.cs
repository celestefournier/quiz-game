using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float speed = 1;

    Vector2 textureOffset;
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        textureOffset.x += speed * Time.deltaTime;
        textureOffset.y += speed * Time.deltaTime;
        image.material.mainTextureOffset = textureOffset;
    }
}
