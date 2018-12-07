using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour {
    public Transform MinuteHand, HourHand;

    public Vector3[] rotations;

    public void SetMinuteHand(int val)
    {
        MinuteHand.localRotation = Quaternion.Euler(rotations[val]);
    }

    public void SetHourHand(int val)
    {
        HourHand.localRotation = Quaternion.Euler(rotations[val]);
    }
}
