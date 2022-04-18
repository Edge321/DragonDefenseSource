using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalGameMechanics;

public class ProjectileBehavior : MonoBehaviour
{
    public float lifeTime = 10.0f;

    public int damage = -5;

    public AudioClip enemyKilled;
    public AudioClip playerDamaged;
    public AudioClip enemyFuckingDies;

    public GameObject bloodEffects;
    public GameObject hitEffects;

    private Rigidbody rigid;

    private float tempLife;

    private float easyModifier = 0.5f;
    private float hardModifier = 1.5f;

    private int soulAmount = 1;

    private int plusGameScore = 1;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        tempLife = lifeTime;
        SetDifficulty();
    }
    private void FixedUpdate()
    {
        tempLife -= Time.deltaTime;
        if (tempLife < 0) //If the object reached its lifetime
        {
            GameBehaviors.objectList.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Launches the projectile in the chosen direction and force
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="force"></param>
    public void Launch(Vector3 direction, float force)
    {
        rigid.AddForce(direction * force);
    }
    /// <summary>
    /// When a projectile collides with player to reduce health, or an enemy to destroy
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();
        GameObject enemy = collision.gameObject;

        if (player) //If the collision was with the player
        {
            DamagePlayer(player);
        }

        if (enemy && !player) //If the collision was with an enemy
        {
            KillEnemy(enemy);
        }

        GameBehaviors.objectList.Remove(gameObject);
        Destroy(gameObject);
    }
    /// <summary>
    /// If player is collided with, brings down health
    /// </summary>
    /// <param name="player">Player gameobject</param>
    private void DamagePlayer(PlayerController player)
    {
        player.ChangeHealth(damage);
        AudioBehavior.instance.PlayAudio(playerDamaged);

        Instantiate(hitEffects, transform.position, Quaternion.identity);
    }
    /// <summary>
    /// Kills an enemy that was collided with
    /// </summary>
    /// <param name="enemy">Enemy gameobject</param>
    private void KillEnemy(GameObject enemy)
    {
        GameBehaviors.objectList.Remove(enemy);
        SoulShopBehavior.instance.AddSouls(soulAmount);
        GameBehaviors.GameScore += plusGameScore;

        Instantiate(bloodEffects, enemy.transform.position, Quaternion.identity);
        Instantiate(hitEffects, transform.position, Quaternion.identity);

        AudioBehavior.instance.PlayAudio(enemyKilled);
        AudioBehavior.instance.PlayAudio(enemyFuckingDies);

        Destroy(enemy);
    }
    /// <summary>
    /// Sets the damage modifier depending on the difficulty chosen
    /// </summary>
    private void SetDifficulty()
    {
        switch (GameBehaviors.Difficulty)
        {
            case Difficulties.EASY:
                damage = (int)(damage * easyModifier);
                break;
            case Difficulties.NORMAL:
                return;
            case Difficulties.HARD:
                damage = (int)(damage * hardModifier);
                break;
        }
    }
}
