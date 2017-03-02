using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChangeCloudScript : MonoBehaviour {
  void TriggerScreenChange()
  {
    GetComponentInParent<ScreenManager>().MakeTransition();
  }
}
