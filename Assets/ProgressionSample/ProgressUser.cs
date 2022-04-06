using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class defines behavior for an object that has some kind of progress we want to track
public class ProgressUser : MonoBehaviour
{
    // we'll give this object a reference to 3 progress points in the unity editor
    [SerializeField] private SOProgressPoint checkpoint1;
    [SerializeField] private SOProgressPoint checkpoint2;
    [SerializeField] private SOProgressPoint checkpoint3;

    /* NOTE: multiple objects can observe the same progress point.
     * 
     * Think of it like if there were multiple bulldozers but only one progress point
     * (lifting the bulldozer blade).
     * If the player lifts the blade on one bulldozer and the progress point is marked as complete,
     * the other bulldozer controllers can see that the objective was completed.
     * 
     * This progress demo scene will use this by having multiple cubes each with this same script.
     * The 3 progress points will be completed in order, but across the 3 different cubes.
     */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // if we're on points 1 or 2, destroy the cube on collision and mark the progress point as completed
            if (!checkpoint1.IsCompleted)
            {
                checkpoint1.SetCompleted();
                Destroy(this.gameObject);
            }
            else if (!checkpoint2.IsCompleted)
            {
                checkpoint2.SetCompleted();
                Destroy(this.gameObject);
            }
        }
    }

    void Update()
    {
        // if the first 2 checkpoints are done, check if the cube was pushed off the platform
        if (checkpoint2.IsCompleted)
        {
            if (transform.position.y < 0)
            {
                checkpoint3.SetCompleted();
                Destroy(this.gameObject);
            }
        }
    }
}
