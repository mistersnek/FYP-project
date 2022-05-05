using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AIHealthTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.

    GameObject gameObject;
    Collider ragdollColliders;

    Rigidbody rigidBodies;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();
    }

    [UnityTest]
    public IEnumerator TakeDamage()
    {
        AIHealthForTest aiCharacter= gameObject.AddComponent<AIHealthForTest>();
        aiCharacter.currentHealth = 100f;
        aiCharacter.TakeDamage(20f);
        yield return null;

        Assert.AreEqual(aiCharacter.currentHealth = 80f, aiCharacter.currentHealth);
    }
    [UnityTest]
    public IEnumerator GiveHealth()
    {
        AIHealthForTest aiCharacter= gameObject.AddComponent<AIHealthForTest>();
        aiCharacter.currentHealth = 100f;
        aiCharacter.IncreaseHealth(20f);
        yield return null;

        Assert.AreEqual(aiCharacter.currentHealth = 120f, aiCharacter.currentHealth);
    }
    
}
