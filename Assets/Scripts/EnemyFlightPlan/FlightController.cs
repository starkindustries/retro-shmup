﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    public GameObject ship;
    private FlightPlan flightPlan;

    // Start is called before the first frame update
    void Start()
    {
        Drivable drivableShip = ship.GetComponent<Drivable>();
        if (drivableShip == null)
        {
            Debug.LogError("ERROR: Drivable ship is null!");
        }

        // Initialize Ship 
        flightPlan = new FlightPlan(drivableShip, position: new Vector2(0, 0), rotation: Quaternion.Euler(x: 0f, y: 0f, z: 90f));

        // flight plan
        flightPlan.AddInstructionSetVelocity(5f);
        flightPlan.AddInstructionWait(3f);
        flightPlan.AddInstructionSetVelocity(0.5f);
        flightPlan.AddInstructionWait(3f);
        flightPlan.AddInstructionSetAngularVelocity(100f);

        // run flight plan!
        flightPlan.ExecuteFlightPlan(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
}