using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShepherdShop : MonoBehaviour
{
  [SerializeField]
  private GameObject ui;

  [SerializeField]
  private List<int> shephardCosts;

  [SerializeField]
  private Text cost;

  private DogPackManager customer;

  public int GetPrice()
  {
    return shephardCosts[customer.GetPackSize()];
  }

  public void UpdateGUI()
  {
    cost.text = GetPrice().ToString() + "x";
  }

  void OnTriggerEnter(Collider other)
  {
    if (customer == null)
    {
      customer = other.GetComponent<DogPackManager>();
      if (customer)
      {
        ui.SetActive(true);
        UpdateGUI();
        customer.CurrentShop = this;
      }
    }
  }

  void OnTriggerExit(Collider other)
  {
    DogPackManager exitingPlayer = other.GetComponent<DogPackManager>();
    if (exitingPlayer && customer && customer == exitingPlayer)
    {
      ui.SetActive(false);
      customer.CurrentShop = null;
      customer = null;
    }
  }
}

