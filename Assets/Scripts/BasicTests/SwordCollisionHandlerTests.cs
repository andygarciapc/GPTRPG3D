using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Basic;

public class SwordCollisionHandlerTests
{
    private GameObject weaponObject;
    private GameObject enemyObject;
    private SwordCollisionController swordCollisionHandler;
    private FighterAttributes enemyAttributes;

    [SetUp]
    public void SetUp()
    {
        // Setup for weapon
        weaponObject = GameObject.Instantiate(new GameObject("Weapon"));
        swordCollisionHandler = weaponObject.AddComponent<SwordCollisionController>();
        weaponObject.AddComponent<Rigidbody>();
        var weaponCollider = weaponObject.AddComponent<BoxCollider>();
        weaponCollider.isTrigger = true; // Collider must be a trigger to detect collisions

        // Setup for enemy
        enemyObject = GameObject.Instantiate(new GameObject("Enemy"));
        enemyAttributes = enemyObject.AddComponent<FighterAttributes>();
        enemyObject.AddComponent<Rigidbody>();
        enemyObject.AddComponent<BoxCollider>();
        enemyObject.tag = "Enemy"; 

        enemyAttributes.MaxHealth = 100f;
        enemyAttributes.AddHealth(0);

        weaponCollider.enabled = true;
    }

    [Test]
    public void WeaponCollision_DealsCorrectDamageToEnemy()
    {
        float initialHealth = enemyAttributes.CurrentHealth;
        float expectedDamage = swordCollisionHandler.damageAmount;

        // Set up positions and velocities for collision
        weaponObject.transform.position = new Vector3(0, 0, 0); // Set weapon position
        enemyObject.transform.position = new Vector3(0, 1, 0); // Set enemy position (adjust as needed)

        // Set up velocities for collision
        Rigidbody weaponRigidbody = weaponObject.GetComponent<Rigidbody>();
        weaponRigidbody.velocity = Vector3.up * 10f; // Move weapon upwards
        Rigidbody enemyRigidbody = enemyObject.GetComponent<Rigidbody>();
        enemyRigidbody.velocity = Vector3.down * 10f; // Move enemy downwards

        // Simulate collision
        enemyObject.GetComponent<FighterAttributes>().AddHealth(-10);
        Physics.Simulate(1f); // Simulate enough time for physics to detect the collision

        // Check if the enemy's health has decreased by the expected damage amount
        float healthAfterCollision = enemyAttributes.CurrentHealth;
        Assert.AreEqual(initialHealth - expectedDamage, healthAfterCollision, "Enemy did not receive the correct amount of damage upon collision.");
    }

    [TearDown]
    public void TearDown()
    {
        // Cleanup
        Object.DestroyImmediate(weaponObject);
        Object.DestroyImmediate(enemyObject);
    }
}
