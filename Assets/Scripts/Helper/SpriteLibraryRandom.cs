using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.U2D.Animation;

public class SpriteLibraryRandom : MonoBehaviour
{
    public List<SpriteLibraryAsset> spriteResolvers;
    public SpriteLibrary spriteResolver;

    private void Awake()
    {
        spriteResolver = GetComponent<SpriteLibrary>();
        SetRandomSprite();
    }

    public void SetRandomSprite()
    {
        if (spriteResolvers.Count == 0)
        {
            Debug.LogError("Sprite Resolvers is empty");
            return;
        }
        var randomIndex = Random.Range(0, spriteResolvers.Count);

        spriteResolver.spriteLibraryAsset = spriteResolvers[randomIndex];
    }
}
