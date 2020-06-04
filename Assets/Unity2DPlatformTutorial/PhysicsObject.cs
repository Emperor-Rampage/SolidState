using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    /// <summary>
    /// Base Class for Custom Physics behavior designed for 2D Platforming games.
    /// </summary>


    //Allows custom control of how much gravity is affecting the object at any given time.
    public float gravityModifier = 1.0f;

    //Used to check if the player is on the ground or not. Minimum angle that the player can be considered "grounded"
    public float minGroundNormalY = 0.65f;

    //public float maxGroundNormalY = .80f;

    protected Vector2 targetVelocity; //Horizontal target motion.
    protected bool isGrounded;
    protected Vector2 groundNormal; //Stored groundNormal for vertical movement.
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter; //Contact Filters are custom collision detection leveraging Unity's built in layer system. 
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16]; //Array that holds the first 16 collision hits that are detected in a frame.
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16); //List that is used to actually check the hitBuffer array, as there may be less than 16 collisions in a frame.
    protected const float shellRadius = 0.01f; //Makes sure that the object will not slip into other colliders.
    protected const float minMoveDistance = float.Epsilon; //Makes sure that we aren't checking for movement if the object hasn't moved since last frame.



    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();

        //Checking to see if this script can actually affect the rigidbody, and setting it true if found otherwise.
        if(rb2d.isKinematic == false)
        {
            rb2d.isKinematic = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setting up the Contact Filter 2D.
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        targetVelocity = Vector2.zero; //Zero out the target velocity every frame for good housekeeping.
        ComputeVelocity(); //Method that needs to be overwritten by any derived class.
    }

    protected virtual void ComputeVelocity()
    {
        //This space intentionally left blank. Required for controlled player motion.
    }

    //Fixed Update is where all physics actions should live.
    private void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime; //Applying gravity to the rigidbody.
        velocity.x = targetVelocity.x; //Applying Horizontal motions velocity.

        isGrounded = false; //Sets isGrounded to false until a collision is found, applying gravity.

        Vector2 deltaPosition = velocity * Time.deltaTime; //Calculation for where the rigidbody should be on the next frame.

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x); //This creates a perpendicular line to the ground normal, no matter the slope, allowing smooth movement up said slopes.


        Vector2 move = moveAlongGround * deltaPosition.x; //Horizontal movement.

        Movement(move, false); //First call to movement, without vertical motion.

        move = Vector2.up * deltaPosition.y; //Vertical movement only.

        Movement(move, true); //Vertical movement call 
    }

    //Called twice, one for vertical movement, one for horizontal. Helpful for dealing with slopes!
    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude; //Storing how much the object has moved since the last call.

        if(distance > minMoveDistance) //If the player has moved enough to actually 
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius); //Checking to see how many collisions the rigidbody is colliding with.

            hitBufferList.Clear(); //Making sure that no data from the last frame is accidentally checked.

            for(int i = 0; i < count; i++) //Adding all the current collisions to the list of hits.
            {
                hitBufferList.Add(hitBuffer[i]);
            }
              
            for(int i = 0; i < hitBufferList.Count; i++) //Checking the normals of each hit to see if the object is touching the ground.
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if(currentNormal.y > minGroundNormalY) //Ground collision normal check.
                {
                    isGrounded = true;
                    if(yMovement) //Vertical motion check
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                //Getting the difference between the velocity and current normal to see if we need to stop the object from entering another collider.
                //Use case: Player jumps into a sloped ceiling, we don't want to kill their momentum and drop them, and we don't want them to fly through the ceiling.
                //This will check to see if we need to make the player slide down the sloped ceiling a bit as they move.
                float projection = Vector2.Dot(velocity, currentNormal);
                if(projection < 0) //AKA is the object the ceiling? Or is the object's normals in the negatives?
                {
                    velocity = velocity - (projection * currentNormal);
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius; //Used to make sure that we don't end up stuck inside another collider by modifying the distance to the collided object ever so slightly.

                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb2d.position = rb2d.position + move.normalized * distance; //Adding the movement vector to the position of the rigidbody.
    }
}
