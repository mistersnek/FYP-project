using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class FieldOfViewTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator IsAbleToSeePlayer()
    {
        GameObject gameObject = new GameObject();
        FieldOfView fieldOfView = gameObject.AddComponent<FieldOfView>();
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        
        fieldOfView.canSeePlayer = true;


        yield return null;

        Assert.AreEqual(fieldOfView.CanSeePlayer() == true, fieldOfView.CanSeePlayer());
    }
}
