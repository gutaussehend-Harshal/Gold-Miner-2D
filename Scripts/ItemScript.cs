using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public float hook_Speed;
    [SerializeField] private int scoreValue;

    private void OnDisable()
    {
        GameplayManager.instance.DisplayScore(scoreValue);
    }

}
