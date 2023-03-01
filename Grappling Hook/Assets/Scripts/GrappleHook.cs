using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    public float springNum, damperNum, massNum;
    private LineRenderer lr;
    private Vector3 grapplePoint;
    private Quaternion orginGunPos;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    private bool grappling = false;


    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        orginGunPos = this.transform.rotation;
    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    private void Update()
    {
      //  if(grappling)
      //  {
      //      this.transform.LookAt(grapplePoint);
      //  }
     //   else this.transform.localRotation = orginGunPos;
    }
    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    public void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            //joint.maxDistance = distanceFromPoint;
            //joint.minDistance = distanceFromPoint;

            //Adjust these values to fit your game.
            joint.spring = springNum;
            joint.damper = damperNum;
            joint.massScale = massNum;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
            grappling = true;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    public void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        grappling = false;
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}