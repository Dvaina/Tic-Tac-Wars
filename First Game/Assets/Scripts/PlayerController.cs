using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

	Vector3 veloity;
	Rigidbody myRigidbody;

	void Start ()
	{
		myRigidbody = GetComponent<Rigidbody> ();
	}

	public void Move (Vector3 _velocity)
	{
		veloity = _velocity;
	}

	public void LookAt (Vector3 lookPoint)
	{
		Vector3 hightCorrectedPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z);
		transform.LookAt (hightCorrectedPoint);
	}

	void FixedUpdate ()
	{
		myRigidbody.MovePosition (myRigidbody.position + veloity * Time.fixedDeltaTime);
	}
}
