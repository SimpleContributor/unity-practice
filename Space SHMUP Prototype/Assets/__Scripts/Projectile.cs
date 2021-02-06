using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float waveFrequency = 2;
    public float waveWidth = 5;

    [SerializeField]
    private WeaponType _type;

    private float x0;
    private float birthTime;

    public WeaponType type
    {
        get {   return (_type);   }
        set {   SetType(value);   }
    }

    private void Awake()
    {
        // Test to see whether this has passed off screen every 2 seconds
        InvokeRepeating("CheckOffscreen", 2f, 2f);
    }

    public void SetType(WeaponType eType)
    {
        // Set the _type
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        GetComponent<Renderer>().material.color = def.projectileColor;
    }

    void CheckOffscreen()
    {
        if (Utils.ScreenBoundsCheck(GetComponent<Collider>().bounds, BoundsTest.offScreen) != Vector3.zero)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        x0 = this.transform.position.x;

        birthTime = Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // This will move the phaser projectiles in a sine wave
        if (_type == WeaponType.phaser)
        {
            
            Vector3 tempPos = this.transform.position;

            float age = Time.time - birthTime;
            float theta = Mathf.PI * 2 * age / waveFrequency;
            float sin = Mathf.Sin(theta);
            tempPos.x = x0 + waveWidth * sin;
            this.transform.position = tempPos;

        }
    }
}
