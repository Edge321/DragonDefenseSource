using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalGameMechanics;

public class PlayerController : MonoBehaviour
{
    public float flightSpeed = 0.35f;
    public float areaXLimit = 22.0f; //Limit the object by the X coordinate
    public float attackDelay = 1.0f;
    public float projectileSpeed = 300.0f;

    public int totalHealth = 300;

    public GameObject projectile;

    public AudioClip fireSound;
    public AudioClip gameOverClip;

    public float FlightSpeedGetSet { get; set; }
    public float AttackDelayGetSet { get; set; }
    public float ModifiedMaxHealthGetSet { get; set; }
    
    private float horizontalWalk;
    private float fire;
    private float tempDelay;
    private float currentHealth;

    private float minAttackDelay = 0.4f;
    private float maxFlightSpeed = 0.5f;
    private int maxMaxHealth = 500;

    private Vector3 limit;
    private Vector3 negativeLimit;

    private Animator animator;
    private void Start()
    {
        float startingZ = transform.position.z;
        float startingY = transform.position.y;

        limit = new Vector3(areaXLimit, startingY, startingZ);
        negativeLimit = new Vector3(-areaXLimit, startingY, startingZ);

        animator = GetComponent<Animator>();

        GameBehaviors.RandomX = (int)areaXLimit;

        tempDelay = attackDelay;
        currentHealth = totalHealth;

        FlightSpeedGetSet = flightSpeed;
        AttackDelayGetSet = attackDelay;
        ModifiedMaxHealthGetSet = totalHealth;
    }
    // FixedUpdate is called once per fixed frame
    void FixedUpdate()
    {
        horizontalWalk = Input.GetAxis("Horizontal");
        fire = Input.GetAxis("Fire1");

        if (GameBehaviors.Moveable) //If player can move
        {
            transform.Translate(Vector3.right * horizontalWalk * FlightSpeedGetSet);
            animator.SetFloat("Speed", horizontalWalk);

            tempDelay -= Time.deltaTime;
            //If the player can shoot in a set time interval to prevent spamming
            if (fire > 0 && tempDelay < 0)
            {
                tempDelay = AttackDelayGetSet;
                ProjectileCreator();
            }

            HealthTextBehavior.instance.SetHealthText(currentHealth, ModifiedMaxHealthGetSet);

            animator.SetBool("Death", false);
        }

        AreaCheck();

        if (currentHealth <= 0)
        {
            currentHealth++; //Makes sure the if statement isn't accidently repeated in FixedUpdate
            GameOver();
        }


    }
    ///<summary>
    ///Creates player's projectile
    ///</summary>
    private void ProjectileCreator()
    {
        Vector3 projectileOffset = new Vector3(0, 5, 1);

        GameObject projecter = Instantiate(projectile, 
                                        transform.localPosition + projectileOffset, 
                                        projectile.transform.rotation);
        ProjectileBehavior pb = projecter.GetComponent<ProjectileBehavior>();

        pb.Launch(Vector3.forward, projectileSpeed);

        GameBehaviors.objectList.Add(projecter);

        AudioBehavior.instance.PlayAudio(fireSound);

        animator.SetTrigger("Shoot");
    }
    ///<summary>
    ///Limits player in the specified x-coordinate boundary
    ///</summary>
    private void AreaCheck()
    {
        if (transform.position.x > limit.x) 
            transform.position = limit;
        if (transform.position.x < -(limit.x))
            transform.position = negativeLimit;
    }
    ///<summary>
    ///<list type="bullet">
    ///<item>
    ///Sets necessary bools for a gameover.
    ///</item>
    ///<item>
    ///Sets death animation for dragon.
    ///</item>
    ///<item>
    ///Plays game over music.
    ///</item>
    ///</list>
    ///</summary>
    private void GameOver()
    {
        GameBehaviors.GameOver = true;
        GameBehaviors.Moveable = false;

        AudioBehavior.instance.StopAudio();
        AudioBehavior.instance.PlayAudio(gameOverClip);

        animator.SetBool("Death", true);
    }
    /// <summary>
    /// Checks if current health equals max health
    /// </summary>
    /// <returns>true if it is equal. false if it is not equal</returns>
    public bool HealthEqualsMax()
    {
        if (currentHealth == ModifiedMaxHealthGetSet)
            return true;
        else
            return false;
    }
    ///<summary>
    ///Changes player's health. 
    ///</summary>
    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, ModifiedMaxHealthGetSet);
        HealthBehavior.instance.SetValue(currentHealth / (float)ModifiedMaxHealthGetSet);
    }
    ///<summary>
    ///Modifies player's flight speed and returns a bool if the upgrade is maxed out
    ///</summary>
    public bool FlightSpeedModifier(float modifier)
    {
        bool disable;
        FlightSpeedGetSet = Mathf.Clamp(FlightSpeedGetSet + modifier, 
                                                    flightSpeed, maxFlightSpeed);

        disable = EqualClamp(FlightSpeedGetSet, maxFlightSpeed);
        return disable;
    }
    ///<summary>
    ///Modifies player's attack delay and returns a bool if the upgrade is maxed out
    ///</summary>
    public bool AttackDelayModifier(float modifier)
    {
        bool disable;
        AttackDelayGetSet = Mathf.Clamp(AttackDelayGetSet - modifier,
                                                    minAttackDelay, attackDelay);

        disable = EqualClamp(AttackDelayGetSet, minAttackDelay);
        return disable;
    }
    ///<summary>
    ///Modifies player's max health and returns a bool if the upgrade is maxed out
    ///</summary>
    public bool MaxHealthModifier(int modifier)
    {
        bool disable;

        ModifiedMaxHealthGetSet = (int) Mathf.Clamp(ModifiedMaxHealthGetSet + modifier, 
                                                totalHealth, maxMaxHealth);

        disable = EqualClamp(ModifiedMaxHealthGetSet, maxMaxHealth);
        return disable;
    }
    ///<summary>
    ///If the player's stat is roughly equal to the clamp, it will return <c>true</c>.
    ///Otherwise, it is <c>false</c>
    ///</summary>
    private bool EqualClamp(float stat, float clamp)
    {
        return Mathf.Approximately(stat, clamp);
    }
    ///<summary>
    ///Resets player's stats
    ///</summary>
    public void ResetStats()
    {
        FlightSpeedGetSet = flightSpeed;
        AttackDelayGetSet = attackDelay;
        ModifiedMaxHealthGetSet = totalHealth;
        ChangeHealth(totalHealth);

        //Checks if current game score beat highscore
        if (GameBehaviors.HighScore < GameBehaviors.GameScore)
            GameBehaviors.HighScore = GameBehaviors.GameScore;

        GameBehaviors.GameScore = 0;
    }
}
