using UnityEngine;

public class Game_managerscript : MonoBehaviour
{
    // i am not going to use Navmeshsystem or pathfinding or another thing

    [Header("Transforms")]
    public Transform myRobotTransform;
    public Transform myTargetTransform;


    [Header("Vectors")]

    [SerializeField] Vector3 displacementvector;
    [SerializeField] Vector3 velocity;




    [Header("Scalers")]
    [SerializeField] float distance;
    [SerializeField] float speed;
    [SerializeField] float magnitudeOFaLine;


    [Header("Directions")]
    [SerializeField] Vector3 right;
    [SerializeField] Vector3 left;



    [Header("Layermask")]
    public LayerMask obstacles;

    [Header("Robot_Movement")]
    [SerializeField] bool avoidingObstacle;
    [SerializeField] Vector3 avoidingVector;


    //   Ray ray;  A ray is an infinite line starting at origin and going in some direction. 

    /* raycast : public static bool Raycast(Vector3 origin, Vector3 direction,
    /float maxDistance = Mathf.Infinity, int layerMask = DefaultRaycastLayers,
    /QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal);
    */

    // displacemnt, magnitude , normaliezed, Time.DeltaTime
    // physics.Raycast(), transform.forword...
    // karar aloritmasi 
    // hazisa sistemi List<> Vector3 olarak kullan

    // distance 1,25 - 2 arasinda kalabilir 

    void Start()
    {
        avoidingObstacle = false;
        



    }

    void Update()
    {
        /*
        RaycastHit hit;  // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(myRobotTransform.transform.position, myRobotTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(myRobotTransform.transform.position, myRobotTransform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            
            Debug.Log("There is a ray and it´s working!");
        }
        else
        {
            Debug.DrawRay(myRobotTransform.transform.position, myRobotTransform.TransformDirection(Vector3.forward) * hit.distance, Color.white);

           
        }*/
        displacement();

       if(distance <= 2f)
        {
            velocity = Vector3.zero;
            Debug.Log("Object is near the object, it´ll not move anymore");
            return;
        }

       if(avoidingObstacle == true)
        {
            Vector3 remaining = avoidingVector - myRobotTransform.position;
            float reamingMagnitude = remaining.magnitude;   


            if(reamingMagnitude <= .2f)
            {
                avoidingObstacle = false;
                return;

            }

            Vector3 direction = remaining.normalized;
            velocity = direction * speed;


            Vector3 movement = Vector3.ClampMagnitude(velocity * Time.deltaTime, reamingMagnitude); 

            myRobotTransform.Translate(movement, Space.World);
            return; 

        }
        

       

        Ray ray = new Ray(myRobotTransform.position, displacementvector.normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, magnitudeOFaLine, obstacles))
        {
            leftandrightdirections();


            /*  
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.green);
            Debug.Log(hit.collider.gameObject.name + " " + "was hit");

            leftandrightdirections();
            // right and left Drawrays 

            Debug.DrawRay(ray.origin, right * 6f, Color.blue);   // +x
            Debug.DrawRay(ray.origin, left * 6f, Color.red); // left -x 
            */

            /*
            /*
            Vector3 direction = (myTargetTransform.position - myRobotTransform.position).normalized;

            Vector3 right = Vector3.Cross(Vector3.up, direction);

            Debug.DrawRay(myRobotTransform.position, direction * 3f, Color.green);
            Debug.DrawRay(myRobotTransform.position, right * 3f, Color.red);
            Debug.DrawRay(myRobotTransform.position, Vector3.up * 3f, Color.blue);
            */
            // bool oldugunu farz et 
            /* if (Physics.Raycast(rightRay, out RaycastHit righthit, magnitudeOFaLine, obstacles))
             {
                 //There is a obtacle in rightway

                 if(Physics.Raycast(leftRay, out RaycastHit lefthit, magnitudeOFaLine, obstacles))
                 {

                 }
                 else
                 {
                     velocity = leftRay.direction * speed;
                     myRobotTransform.Translate(velocity * Time.deltaTime, Space.World); 
                 }


             }
             else
             {
                 velocity = rightRay.direction * speed;
                 myRobotTransform.Translate(velocity * Time.deltaTime, Space.World);
             }
            */

            Ray rightRay = new Ray(myRobotTransform.position, right);
           

            if(!Physics.Raycast(rightRay, magnitudeOFaLine, obstacles))
            {   //false dönerse kisacasi 
                avoidingVector = myRobotTransform.position + right * 3f;
                avoidingObstacle = true; 


            }
            else
            {
                Ray leftRay = new Ray(myRobotTransform.position, left);

                if(!Physics.Raycast(leftRay, magnitudeOFaLine, obstacles))
                {
                    avoidingVector = myRobotTransform.position + left * 3f;
                    avoidingObstacle = true;
                }

                else 
                {

                    velocity = Vector3.zero;
                
                
                
                
                }





            }






        }

        else
        {
            direcitonandMove();
        }

       



    }


    void displacement()
    {
        // where i want to go       // where i am 
        displacementvector = myTargetTransform.position - myRobotTransform.position;
        distance = displacementvector.magnitude;  // same shit with Vector3.Distance 
        /*var a = Vector3.Distance(myTargetTransform.position, myRobotTransform.position);
          distance = a; // aradaki distance i hesaplicam ki durma noktasini bulayim  */

    }


    void direcitonandMove()
    {
        Vector3 direction = displacementvector.normalized; // 0.1 - 1 degeri getiriyor bura 
        velocity = direction * speed;
        myRobotTransform.transform.Translate(velocity * Time.deltaTime, Space.World);
    }


    void leftandrightdirections()
    {

        Vector3 direction = (myTargetTransform.position - myRobotTransform.position).normalized;
        right = Vector3.Cross(Vector3.up, direction);
        left = -right;

    }


    


}






    
