using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShip : MonoBehaviour
{
    #region Variables
    [Header("Health Properties")]
    public int maxHealth = 200;
    public int hitDamage = 20;

    [Header("Physics Properties")]
    public float shipSpeed = 100f;
    public float rotSpeed = 5f;

    [Header("Sound Effex")]
    public AudioClip thrustSFX;
    public AudioClip deathSFX;
    public AudioClip landSFX;
    public AudioClip damageSFX;

    [Header("Particle Systems")]
    public ParticleSystem thrustFlame;
    public ParticleSystem deathExplosion;


    Rigidbody myRigBody;
    AudioSource myAudioSource;
    GameController gameController;
    HealthBar myHealthBar;
    ShakeCam shakeCam;
    CanvasFade canvasFade;


    bool isAlive = true;
    int currHealth;
    #endregion



    #region Builtin Methods
    // Start is called before the first frame update
    void Start()
    {
        myRigBody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();
        myHealthBar = FindObjectOfType<HealthBar>();
        shakeCam = FindObjectOfType<ShakeCam>();
        canvasFade = FindObjectOfType<CanvasFade>();


        currHealth = maxHealth;
        myHealthBar.SetMaxHealth(maxHealth);
        canvasFade.Fade();
    }

    // Update is called once per frame
    void Update()
    {
        SoundEffex();

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(100);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(hitDamage);
        }
    }

    
    private void LateUpdate()
    {
        // Stops the ship from continuing to rotate when it hits or is hit by another object
        // i.e. You hit an edge and try to stop, the ship will never stop. It will mantain
        // the opposite force (Newton's Third Law)
        transform.localEulerAngles = new Vector3(0, 0, transform.localEulerAngles.z);
    }


    private void FixedUpdate()
    {
        if (isAlive)
        {
            Fly();
            Rotate();
        }
    }
    #endregion



    #region Custom Methods
    public void Fly()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            myRigBody.AddRelativeForce(Vector3.up * shipSpeed);
            thrustFlame.Play();
        } else
        {
            thrustFlame.Stop();
        }

        
    }

    public void Rotate()
    {
        myRigBody.freezeRotation = true;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Rotate Right");
            //Quaternion rot = transform.rotation;
            //rot = Quaternion.Euler(0, 0, 0 + rotSpeed);
            //transform.rotation = rot;
            transform.Rotate(-Vector3.forward * rotSpeed);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotSpeed);
        }

        myRigBody.freezeRotation = false;
    }

    public void SoundEffex()
    {
        if (isAlive) 
        { 
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                myAudioSource.PlayOneShot(thrustSFX, 0.2f);
            }

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                myAudioSource.Stop();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        canvasFade.Fade();
        currHealth -= damage;
        myHealthBar.SetHealth(currHealth);

        if (currHealth <= 0)
        {
            Die();
        } else
        {
            AudioSource.PlayClipAtPoint(damageSFX, Camera.main.transform.position, 0.3f);
        }
    }

    public void Die()
    {
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position);
        deathExplosion.Play();
        shakeCam.ShakeCamera();
        isAlive = false;
        thrustFlame.Stop();
        gameController.ResetGame();
        myAudioSource.Stop();
    }
    #endregion



    #region Collisions
    private void OnCollisionEnter(Collision other)
    {
        if (!isAlive || gameController.invincible) return;

        switch(other.gameObject.tag)
        {
            case ("Friendly"):
                Debug.Log("Friendly Object");
                break;

            case ("Landing"):
                AudioSource.PlayClipAtPoint(landSFX, Camera.main.transform.position);
                myRigBody.isKinematic = true;

                gameController.NextLevel();
                break;

            case ("Fuel"):
                Debug.Log("Fueling Up!");
                break;

            case ("Danger"):
                TakeDamage(hitDamage);
                break;

            default:
                if (other.relativeVelocity.magnitude > 10)
                {
                    TakeDamage(hitDamage);
                }
                break;
        }
    }
    #endregion
}
