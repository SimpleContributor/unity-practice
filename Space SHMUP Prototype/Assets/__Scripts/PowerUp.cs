using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // This is a different way of using a Vector2 to store two values as a min and max
    // to find a value using Random.Range()
    public Vector2 rotMinMax = new Vector2(15, 90);
    public Vector2 driftMinMax = new Vector2(0.25f, 2);
    public float lifetime = 6f;     // Seconds the PowerUp exists
    public float fadeTime = 4f;     // Seconds it will then fade
    public bool __________________________;
    public WeaponType type;     // The type of power up
    public GameObject cube;     // Reference to the PowerUp Cube child
    public TextMesh letter;     // Reference to the PowerUp TextMesh letter
    public Vector3 rotPerSecond;    // Euler rotation speed
    public float birthTime;


    private void Awake()
    {
        // Find the cube reference
        cube = transform.Find("Cube").gameObject;
        // Find the TextMesh
        letter = GetComponent<TextMesh>();

        // Set a random velocity
        Vector3 vel = Random.onUnitSphere; // get random XYZ velocity
        // Random.onUnitSphere give you a vector point that is somewhere on
        // the surface of the sphere with a radius of 1m around the origin
        vel.z = 0; // Flatten the velocity to the XY plane
        vel.Normalize(); // make the length (or magnitude) of the vel 1
        // Normalizing a Vector3 makes its length 1m

        // Set velocity length to something between x & y of driftMinMax
        vel *= Random.Range(driftMinMax.x, driftMinMax.y);
        GetComponent<Rigidbody>().velocity = vel;


        // Set the rotation of this GameObject to R: [0,0,0]
        // Quaternion.identity is equal to no rotation
        transform.rotation = Quaternion.identity;

        // Set up the rotPerSecond for the Cube child using rotMinMax x & y
        rotPerSecond = new Vector3(Random.Range(rotMinMax.x, rotMinMax.y),
                                   Random.Range(rotMinMax.x, rotMinMax.y),
                                   Random.Range(rotMinMax.x, rotMinMax.y));

        // CheckOffScreen() every 2 seconds
        InvokeRepeating("CheckOffscreen", 2f, 2f);

        birthTime = Time.time;
    }



    // Update is called once per frame
    void Update()
    {
        // Manually rotate the Cube child every Update()
        // Multiplying it by Time.time causes the rotation to be time-based
        cube.transform.rotation = Quaternion.Euler(rotPerSecond * Time.time);

        // Fade out the PowerUp over time
        // Given the default values, a PowerUp will exist for 10 seconds and then
        // fade out over 4 seconds
        float u = (Time.time - (birthTime + lifetime)) / fadeTime;
        // For lifeTime seconds, u will be <= 0. Then it will transition to 1 over fadeTime seconds
        // if u >= 1, destroy this PowerUp
        if (u >= 1)
        {
            Destroy(this.gameObject);
            return;
        }
        // Use u to determine the alpha value of the Cube & Letter
        if (u > 0)
        {
            Color c = cube.GetComponent<Renderer>().material.color;
            c.a = 1f - u;
            cube.GetComponent<Renderer>().material.color = c;
            // Fade the Letter too, just not as much
            c = letter.color;
            c.a = 1f - (u * 0.5f);
            letter.color = c;
        }
    }


    // This SetType() differs from those on Weapon and Projectile
    public void SetType(WeaponType wt)
    {
        // Grab the weaponDefinition from Main
        WeaponDefinition def = Main.GetWeaponDefinition(wt);
        // Set the color of the Cube child
        cube.GetComponent<Renderer>().material.color = def.color;
        //letter.color = def.color;     // We could colorize the letter too
        letter.text = def.letter;    // Set the letter that is shown
        type = wt;    // Finally, set the type
    }

    public void AbsorbedBy(GameObject target)
    {
        // This function is called by the Hero class when a PowerUp is collected
        // We could tween into the target and shrink in size, but just destroy for now
        Destroy(this.gameObject);
    }

    void CheckOffscreen()
    {
        // If the PowerUp has drifted entirely off screen...
        if (Utils.ScreenBoundsCheck(cube.GetComponent<Collider>().bounds, BoundsTest.offScreen) != Vector3.zero)
        {
            // ... then destroy this GameObject
            Destroy(this.gameObject);
        }
    }
}
