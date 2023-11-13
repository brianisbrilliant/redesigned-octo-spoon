using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour, IItem
{
	private Rigidbody rb;

	void Start() {
		rb = this.GetComponent<Rigidbody>();
	}


	public void Pickup(Transform hand) {
		Debug.Log("Pick up Flashlight");
		//MAke Kinematic
		rb.isKinematic = true;
		//Move to hand rotation
		this.transform.SetParent(hand);
		this.transform.localPosition = Vector3.zero;
		this.transform.localRotation = Quaternion.identity;
		// make held item
	}

	public void Drop() {
		Debug.Log("Drop Flashlight");
		//Dynamic rigid
		rb.isKinematic = false;
		//THriow from player
		// ste hgelf
	}

	public void PrimaryAction() {
		Debug.Log("Turn On/Off Flashlight");
		//Ste light active ot not

	}

	public void SecondaryAction() {
		Debug.Log("Toggle Brighness for Flashlight");
		// chnage light intencity
	}
}
