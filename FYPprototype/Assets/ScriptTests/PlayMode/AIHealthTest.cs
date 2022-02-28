using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AIHealthTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TakeDamage()
    {
        GameObject gameObject = new GameObject();
        AIHealth aiCharacter= gameObject.AddComponent<AIHealth>();
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        
        aiCharacter.currentHealth = 100;

        aiCharacter.TakeDamage(20);

        yield return null;

        Assert.AreEqual(aiCharacter.currentHealth = 80, aiCharacter.currentHealth);
    }
}
