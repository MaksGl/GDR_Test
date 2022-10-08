using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    [SerializeField]
    private int circleSize;
    [SerializeField]
    private int circleSpeed;
    [SerializeField]
    private int moneyCount;
    [SerializeField]
    private int spikeCount;

    [SerializeField]
    private GameObject money;
    [SerializeField]
    private GameObject spike;
    [SerializeField]
    private GameObject pathPoint;


    public int CircleSize => circleSize;
    public int CircleSpeed => circleSpeed;

    public int MoneyCount => moneyCount;

    public int SpikeCount => spikeCount;

    public GameObject Money => money;

    public GameObject Spike => spike;

    public GameObject PathPoint => pathPoint;
}
