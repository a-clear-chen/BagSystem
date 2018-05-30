using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour {

	public void OnBagClick()
    {
        Inventory.Instance.TransformState();
    }

}
