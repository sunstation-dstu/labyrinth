using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

[ExecuteInEditMode]
public class SwitchSuit : MonoBehaviour
{
    private SpriteResolver[] spriteResolverObjects;
    // FIXME Another hardcode and hack due deadline 
    private SpriteRenderer neckRenderer;
    public bool isCostume;
    private bool isAlreadyCostume;
    
    private void Switch(string label)
    {
        isAlreadyCostume = label == "Costume";
        foreach (var spriteResolver in spriteResolverObjects)
        {
            var category = spriteResolver.GetCategory();
            spriteResolver.SetCategoryAndLabel(category, label);

            // FIXME Another hardcode and hack due deadline 
            if (spriteResolver.name != "Neck") continue;
            neckRenderer.sortingOrder = isCostume ? 20 : 12;
        }
    }

    
    private void Start()
    {
        var gameObjects = GameObject.FindGameObjectsWithTag("SwitchableSuite");
        spriteResolverObjects = new SpriteResolver[gameObjects.Length];
        
        // FIXME Another hardcode and hack due deadline 
        neckRenderer = GameObject.Find("Neck").GetComponent<SpriteRenderer>();

        for (var i = 0; i < gameObjects.Length; i++)
            spriteResolverObjects[i] = gameObjects[i].GetComponent<SpriteResolver>();
    }

    private void Update()
    {
        if (isCostume && !isAlreadyCostume)
            Switch("Costume");
        else if (!isCostume && isAlreadyCostume)
            Switch("Usual");
    }
}