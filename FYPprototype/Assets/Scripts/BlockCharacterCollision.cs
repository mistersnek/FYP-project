using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCharacterCollision : MonoBehaviour
{
    public CharacterController characterCollider;
    public CapsuleCollider characterBlockerCollider;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(characterCollider, characterBlockerCollider, true);
    }


}
