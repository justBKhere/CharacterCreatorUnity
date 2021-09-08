using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerForUMA : MonoBehaviour
{
    public RuntimeAnimatorController characterAnimator;
    public GameObject UMAgameObject;
    
    // Start is called before the first frame update
    void Start()
    {
        Animator animator;
        animator = UMAgameObject.GetComponent<Animator>();
        animator.runtimeAnimatorController = characterAnimator;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
