using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
  [SerializeField]
  private float Speed;

  private Rigidbody mBody;

  public int playerID = 1;
  private string horiAxisName;
  private string vertAxisName;
  private string altHoriAxisName;
  private string altVertAxisName;


  void Start()
  {
    horiAxisName = "c" + playerID.ToString() + "_Horizontal";
    vertAxisName = "c" + playerID.ToString() + "_Vertical";
    altHoriAxisName = "c" + playerID.ToString() + "_Alt_Horizontal";
    altVertAxisName = "c" + playerID.ToString() + "_Alt_Vertical";
  }

  void Awake()
  {
    mBody = GetComponent<Rigidbody>();
  }

  void Update()
  {
    Vector3 direction = Vector3.zero;

    if (Mathf.Abs(Input.GetAxis(horiAxisName))>0.2f)
    {
      direction += Vector3.right*Input.GetAxis(horiAxisName);
    }
    else if (Mathf.Abs(Input.GetAxis(altHoriAxisName)) > 0.2f)
    {
      direction += Vector3.right * Input.GetAxis(altHoriAxisName);
    }

    if (Mathf.Abs(Input.GetAxis(vertAxisName)) > 0.2f)
    {
      direction += Vector3.forward * Input.GetAxis(vertAxisName);
    }
    else if (Mathf.Abs(Input.GetAxis(altVertAxisName)) > 0.2f)
    {
      direction += Vector3.forward * Input.GetAxis(altVertAxisName);
    }


    mBody.AddForce(direction * Speed * Time.deltaTime);
  }
}
