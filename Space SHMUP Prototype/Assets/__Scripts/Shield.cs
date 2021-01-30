using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float rotationsPerSecond = 0.1f;
    public bool __________________________;
    public int levelShown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Readthe current shield level from the Hero Singleton
        int currLevel = Mathf.FloorToInt(Hero.S.shieldLevel);
        // If this is different from levelShown...
        if (levelShown != currLevel)
        {
            levelShown = currLevel;
            Material mat = this.GetComponent<Renderer>().material;
            // Adjust the texture offset to show different shield level
            mat.mainTextureOffset = new Vector2(0.2f*levelShown, 0);
        }
        // Rotate the shield a bit every second
        float rZ = (rotationsPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
