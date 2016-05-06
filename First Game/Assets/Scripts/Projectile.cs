using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

	public LayerMask collisonMask;
	public Color trailColor;

	float speed = 10;
	float damage = 1;

	float lifeTime = 3;
	float skinWidth = .1f;

	void Start ()
	{
		Destroy (gameObject, lifeTime);

		Collider[] initalCollisions = Physics.OverlapSphere (transform.position, .1f, collisonMask);
		if (initalCollisions.Length > 0) {
			OnHitObject (initalCollisions [0], transform.position);
		}
		GetComponent<TrailRenderer> ().material.SetColor ("_TintColor", trailColor);
	}

	public void SetSpeed (float newSpeed)
	{
		speed = newSpeed;
	}


	void Update ()
	{
		float moveDistance = speed * Time.deltaTime;
		CheckColisions (moveDistance);
		transform.Translate (Vector3.forward * moveDistance);
	}

	void CheckColisions (float moveDistance)
	{
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, moveDistance + skinWidth, collisonMask, QueryTriggerInteraction.Collide)) {
			OnHitObject (hit.collider, hit.point);
		}
	
	}

	void OnHitObject (Collider c, Vector3 hitPoint)
	{
		IDamageable damageableObject = c.GetComponent<IDamageable> ();
		if (damageableObject != null) {
			damageableObject.TakeHit (damage, hitPoint, transform.forward);
		}
		GameObject.Destroy (gameObject);
	}
}
