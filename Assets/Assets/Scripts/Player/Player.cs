using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {
    public float moveSpeed = 3f;
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public float BasicCooldown { get; set; }
    public float LastFired { get; set; }


    private Vector3 lastDir = new Vector3(0, 0, 0);
    [SerializeField]
    private GameObject projectile;

    GameManager gm;



    // Use this for initialization
    void Start () {
        BasicCooldown = .5f;
        LastFired = 0f;
        MaxHealth = 3;
        Health = MaxHealth;
        //access GameManager in the scene
        GameObject gmo = GameObject.Find("GameManager");
        gm = gmo.GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        HandleInput();
        HandleMovement();
        Shoot();
	}

    /// <summary>
    /// Creates a new vector and assigns x and y values based on player input
    /// </summary>
    void HandleInput()
    {
        lastDir.x = Input.GetAxisRaw("Horizontal");
        lastDir.y = Input.GetAxisRaw("Vertical");

        //ToDo: Handle Firing
    }

    /// <summary>
    /// Moves player according to the normalized input 
    /// </summary>
    void HandleMovement()
    {
        lastDir = Vector3.Normalize(lastDir);
        transform.Translate(lastDir * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Reduces player health by the amount of damage.
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(int amount)
    {
        this.Health -= amount;
        if (this.Health <= 0)
        {
            gm.GameOver(); //calls gameover method from GameManager when health reaches 0
        }
    }

    /// <summary>
    /// Instantiates a projectile if the left mouse Button is held down
    /// </summary>
    private void Shoot()
    {
        if (Input.GetMouseButton(0) && CanFire())
        {
            Instantiate(projectile, new Vector3(this.transform.position.x, this.transform.position.y + .153f, 0), Quaternion.identity);
            LastFired = Time.time;
        }
    }

    /// <summary>
    /// Checks to see if the player can shoot based on the cooldown time
    /// </summary>
    /// <returns>True if the cooldown has been achieved</returns>
    private bool CanFire()
    {
        if (LastFired + BasicCooldown <= Time.time)
            return true;
        else
            return false;
    }


}
