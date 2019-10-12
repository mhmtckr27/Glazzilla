using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            float unitWidth = spriteRenderer.sprite.textureRect.width / spriteRenderer.sprite.pixelsPerUnit;
            float unitHeight = spriteRenderer.sprite.textureRect.height / spriteRenderer.sprite.pixelsPerUnit;
            spriteRenderer.transform.localScale = new Vector3(width / unitWidth, height / unitHeight);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
