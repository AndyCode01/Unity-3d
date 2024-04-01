using UnityEngine;

public class HandleCursor : MonoBehaviour
{
    public GameObject player;
    PlayerMovement body;
    Ray rayo;
    RaycastHit hit;
    RaycastHit previousLand = new RaycastHit();
    Vector3 distance;

    void Update()
    {
        body = player.GetComponent<PlayerMovement>();
        rayo = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(rayo.origin, rayo.direction * 100, Color.red);
        IsPointingAtLand();
    }

    void IsPointingAtLand()
    {
        if (Physics.Raycast(rayo, out hit))
        {
            distance = hit.transform.position - player.transform.position;
            if (hit.transform.CompareTag("Land") && distance.magnitude <= 3f)
            {
                if (previousLand.transform != null)
                {
                    if(previousLand.transform != hit.transform)
                    {
                        previousLand.transform.Find("Selected").gameObject.SetActive(false);
                        hit.transform.Find("Selected").gameObject.SetActive(true);
                    }  
                }
                else
                {
                    hit.transform.Find("Selected").gameObject.SetActive(true); 
                }
                previousLand = hit;            
            }
            else
            {
                if (previousLand.transform != null)
                {
                    previousLand.transform.Find("Selected").gameObject.SetActive(false);
                    previousLand = new RaycastHit();
                }  
            }     
        }
        else
        {
            if (previousLand.transform != null)
            {
                previousLand.transform.Find("Selected").gameObject.SetActive(false);
                previousLand = new RaycastHit();
            }  
        }
    }

    public RaycastHit IsPointingAtHandObject()
    {
        if (Physics.Raycast(rayo, out hit))
        {
            distance = hit.transform.position - player.transform.position;
            if (hit.transform.CompareTag("HandObject") && distance.magnitude <= 1.5f)
            {
                hit.rigidbody.isKinematic = true;
                hit.transform.position = player.transform.position + player.transform.forward ;
                hit.transform.parent = player.transform;
                body.HandleHandItem();
                return hit;
            }
        }
        return new RaycastHit();
    }

    public void DropAtHandObject(RaycastHit hit)
    {
        hit.transform.rotation= Quaternion.Euler(0,0,0);
        hit.transform.position =  new Vector3(player.transform.position.x, 1, player.transform.position.z) + player.transform.forward ;
        hit.rigidbody.isKinematic = false;
        body.HandleHandItem();
        hit.transform.parent = null;
    }
}
