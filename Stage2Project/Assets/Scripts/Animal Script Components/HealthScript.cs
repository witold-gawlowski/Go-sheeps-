using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
  [SerializeField]
  public int health = 4;


  public void SetHealth(int count)
  {
    health = count;
  }

  public void LooseHealth()
  {
    health--;
    if (health <= 0)
    {
      Destroy(this.gameObject);
    }
  }
}
