using UnityEngine;
using System.Collections;

public class FlyCam : MonoBehaviour {
	
	
	public float speed = 10.0f;		// max speed of camera
	public float sensitivity = 0.25f; 		// keep it from 0..1
	public bool inverted = false;
	
	
	private Vector3 lastMouse = new Vector3(255, 255, 255);
	
	
	// smoothing
	public bool smooth = true;
	public float acceleration = .1f;
	private float actSpeed = 0.0f;			// keep it from 0 to 1
	private Vector3 lastDir = new Vector3();
    int calls = 0;
	
	// Use this for initialization
	void Start ()
    {
        transform.position = new Vector3(0, 0, 0);
        
        transform.Translate(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {

        if (calls<200)
        {
            transform.Translate(new Vector3(0, 0, -1) * speed * Time.deltaTime);
            calls++;
            return;
        }
        // Mouse Look
        lastMouse = Input.mousePosition - lastMouse;
		if ( ! inverted ) lastMouse.y = -lastMouse.y;
		lastMouse *= sensitivity;
		lastMouse = new Vector3( transform.eulerAngles.x + lastMouse.y, transform.eulerAngles.y + lastMouse.x, 0);
		transform.eulerAngles = lastMouse;
		lastMouse = Input.mousePosition;




        // Movement of the camera

        Vector3 dir = new Vector3();			// create (0,0,0)
		
		if (Input.GetKey(KeyCode.W)) dir.z += 1.0f;
		if (Input.GetKey(KeyCode.S)) dir.z -= 1.0f;
		if (Input.GetKey(KeyCode.A)) dir.x -= 1.0f;
		if (Input.GetKey(KeyCode.D)) dir.x += 1.0f;
        if (Input.GetKey(KeyCode.R)) {
            transform.position = new Vector3(0, 0, 0);
            transform.Translate(0, 0, 0);
        }
		dir.Normalize();
		
		
		if (dir != Vector3.zero) {
			// some movement 
			if (actSpeed < .7)
				actSpeed += acceleration * Time.deltaTime * 10;
			else 
				actSpeed = 1f;
			
			lastDir = dir;
		} else {
			// should stop
			if (actSpeed > 0)
				actSpeed -= acceleration * Time.deltaTime * 5;
			else 
				actSpeed = 0f;
		}
		
		
		
		
		if (smooth) 
			transform.Translate( lastDir * actSpeed * speed * Time.deltaTime );
		
		else 
			transform.Translate ( dir * speed * Time.deltaTime );
		
		
	}
	
	void OnGUI() {
		GUILayout.Box ("actSpeed: " + actSpeed.ToString());
	}
}
