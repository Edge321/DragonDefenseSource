using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalGameMechanics;

public class EnemyController : MonoBehaviour
{
    public float distanceAttack = 10.0f;
    public float runSpeed = 1.0f;
    public float attackDelay = 1.0f;
    public float projectileSpeed = 150.0f;

    public GameObject projectile;
    public GameObject castle;

    public AudioClip fireSound;

    private float castleZ;
    private float tempAttackDelay;

    private float easyRunSpeed = 0.5f;
    private float hardRunSpeed = 5.0f;
    private float easyAttackSpeed = 1.2f;
    private float hardAttackSpeed = 0.9f;

    private Transform castleCoordinates;

    private Vector3 speedVector;

    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        castleCoordinates = castle.GetComponent<Transform>();
        castleZ = castleCoordinates.transform.localPosition.z;

        speedVector = new Vector3(0, 0, runSpeed);
        tempAttackDelay = attackDelay;

        animator = GetComponent<Animator>();

        SetDifficulty();
    }
    private void FixedUpdate()
    {
        float enemyZ = transform.position.z;

        //Finds the distance between enemy and castle using distance formula on 1 dimension (z-coordinate)
        float distance = Mathf.Sqrt(Mathf.Pow(enemyZ - castleZ, 2)); 
        if (distance < distanceAttack)
        {
            animator.SetBool("Speed", true);
            tempAttackDelay -= Time.deltaTime;
            if (tempAttackDelay < 0)
            {
                tempAttackDelay = attackDelay;
                ProjectileSpawner();
            }  
        }
        else
        {
            transform.position -= speedVector;
        }
            
    }
    /// <summary>
    /// Spawns an enemy's projectile. Uses <c>GameObject projectile</c>
    /// </summary>
    private void ProjectileSpawner()
    {
        Vector3 projectileOffset = new Vector3(0, 1.5f, -1.5f);
        GameObject projectileObject = Instantiate(projectile,
                                                transform.localPosition + projectileOffset, 
                                                Quaternion.identity);
        ProjectileBehavior projecter = projectileObject.GetComponent<ProjectileBehavior>();

        projecter.Launch(-Vector3.forward, projectileSpeed);

        GameBehaviors.objectList.Add(projectileObject);

        AudioBehavior.instance.PlayAudio(fireSound);

        animator.SetTrigger("Attack");
    }
    private void SetDifficulty()
    {
        switch(GameBehaviors.Difficulty)
        {
            case Difficulties.EASY:
                runSpeed *= easyRunSpeed;
                attackDelay *= easyAttackSpeed;
                break;
            case Difficulties.NORMAL:
                break;
            case Difficulties.HARD:
                runSpeed *= hardRunSpeed;
                attackDelay *= hardAttackSpeed;
                break;
        }
    }
}
