using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		float theta = Mathf.PI / -24.0f;
		Vector3 u = transform.right;

		float cx = Mathf.Cos(theta / 2.0f); float sx = Mathf.Sin(theta / 2.0f);
		Quaternion q = new Quaternion(sx * u.x, sx * u.y, sx * u.z, cx);

		transform.rotation = q * transform.rotation;
	}
}
