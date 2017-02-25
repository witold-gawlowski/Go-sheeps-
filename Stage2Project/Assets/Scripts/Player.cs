using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
  [SerializeField]
  private float Speed;

  private Rigidbody mBody;
  private DogPackManager packManager;

  public int playerID = 1;
  private string horiAxisName;
  private string vertAxisName;
  private string altHoriAxisName;
  private string altVertAxisName;
  private string stayButtonName;
  private string stayAltButtonName;
  private string buyButtonName;
  private string buyAltButtonName;
  private string summonButtonName;
  private string summonAltButtonName;



  void Start()
  {
    horiAxisName = "c" + playerID.ToString() + "_Horizontal";
    vertAxisName = "c" + playerID.ToString() + "_Vertical";
    altHoriAxisName = "c" + playerID.ToString() + "_Alt_Horizontal";
    altVertAxisName = "c" + playerID.ToString() + "_Alt_Vertical";

    stayButtonName = "c" + playerID.ToString() + "_Stay";
    stayAltButtonName = "c" + playerID.ToString() + "_Alt_Stay";
    buyButtonName = "c" + playerID.ToString() + "_Buy";
    buyAltButtonName = "c" + playerID.ToString() + "_Alt_Buy";
    summonButtonName = "c" + playerID.ToString() + "_Summon";
    summonAltButtonName = "c" + playerID.ToString() + "_Alt_Summon";
  }

  void Awake()
  {
    mBody = GetComponent<Rigidbody>();
    packManager = GetComponent<DogPackManager>();
  }

  void Update()
  {
    Vector3 direction = Vector3.zero;

    if (Mathf.Abs(Input.GetAxis(horiAxisName)) > 0.2f)
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

    if (Input.GetButtonDown(stayButtonName) || Input.GetButtonDown(stayAltButtonName)) { 
       packManager.SetGuard();
    }

    if (Input.GetButtonDown(buyButtonName) || Input.GetButtonDown(buyAltButtonName))
    {
      packManager.Purchase();
    }

    if (Input.GetButtonDown(summonButtonName) || Input.GetButtonDown(summonAltButtonName))
    {
      packManager.Summon();
    }

    mBody.AddForce(direction * Speed * Time.deltaTime, ForceMode.VelocityChange);
  }
}
