using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataPoint
{
	public bool selected;
	public GameObject cube;
	public GameObject cylinder;
	public GameObject sphere;

	public DataPoint()
	{
		selected = false;
	}

	public void select()
	{
		selected = true;
		cube.renderer.material.color = Color.red;
		sphere.renderer.material.color = Color.red;
		cylinder.renderer.material.color = Color.red;
	}

	public void deselect()
	{
		selected = false; //unselect
		cube.renderer.material.color = Color.white;
		sphere.renderer.material.color = Color.white;
		cylinder.renderer.material.color = Color.white;
	}
}

public class plot : MonoBehaviour {

	public Color cx = Color.red;
	public Color cy = Color.green;
	public Color cz = Color.blue;

	private GameObject centerCam;
	private int num_points = 50;
	private List<DataPoint> pointlist;
	
	// Use this for initialization
	void Start () {

		gameObject.renderer.material.color = Color.gray;

		GameObject plot1 = new GameObject ("Plot1");
		GameObject plot2 = new GameObject ("Plot2");
		GameObject plot3 = new GameObject ("Plot3");

		pointlist = new List<DataPoint> ();

		for (int i=0; i< num_points; i++) {
			DataPoint d = new DataPoint();
			pointlist.Add(d);
		}


		setupPlot1 (plot1);
		setupPlot2 (plot2);
		setupPlot3 (plot3);

		plot1.transform.position = new Vector3 (0, 0.75f, 2.0f);
		plot2.transform.rotation = Quaternion.Euler (0, -45, 0);
		plot3.transform.rotation = Quaternion.Euler (0, 90, 0);

		plot2.transform.position = new Vector3 (-2, 0.75f, 0.5f);
		plot3.transform.position = new Vector3 (2.5f, 0.75f, 0.5f);


		//
		centerCam = GameObject.Find ("LeftEyeAnchor");

	}

	void setupPlot1(GameObject plot)
	{
		for (int i=0; i< num_points; i++) {
			GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
			cube.transform.parent = plot.transform;
			cube.transform.position = new Vector3 (Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-0.15f, 0.15f));
			cube.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);

			pointlist[i].cube = cube;
		}
		
		GameObject xaxis = new GameObject ("xaxis");
		LineRenderer xline = xaxis.AddComponent<LineRenderer> ();
		xline.SetColors (cx, cx);
		xline.SetWidth (0.05f, 0.05f);
		xline.SetVertexCount (2);
		xline.useWorldSpace = false;
		xline.material = new Material (Shader.Find ("Particles/Alpha Blended"));
		xline.SetPosition (0, new Vector3 (-1, -1, 0.3f));
		xline.SetPosition (1, new Vector3 (1, -1, 0.3f));
		
		GameObject yaxis = new GameObject("yaxis");
		LineRenderer yline = yaxis.AddComponent<LineRenderer> ();
		yline.SetColors (cy, cy);
		yline.SetWidth (0.05f, 0.05f);
		yline.SetVertexCount (2);
		yline.useWorldSpace = false;
		yline.material = new Material (Shader.Find ("Particles/Alpha Blended"));
		yline.SetPosition (0, new Vector3 (-1, -1, 0.3f));
		yline.SetPosition (1, new Vector3 (-1, 1, 0.3f));
		
		GameObject zaxis = new GameObject ("zaxis");
		LineRenderer zline = zaxis.AddComponent<LineRenderer> ();
		zline.SetColors (cz, cz);
		zline.SetWidth (0.05f, 0.05f);
		zline.SetVertexCount (2);
		zline.useWorldSpace = false;
		zline.material = new Material (Shader.Find ("Particles/Alpha Blended"));
		zline.SetPosition (0, new Vector3 (-1, -1, -0.3f));
		zline.SetPosition (1, new Vector3 (-1, -1, 0.3f));
		
		xaxis.transform.parent = yaxis.transform.parent = zaxis.transform.parent = plot.transform;
	}

	void setupPlot2(GameObject plot)
	{
		for (int i=0; i< num_points; i++) {
			GameObject cylinder = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
			cylinder.transform.parent = plot.transform;
			float height = Random.Range(0.0f, 0.2f);
			cylinder.transform.localScale = new Vector3 (0.1f, height, 0.1f);
			cylinder.transform.position = new Vector3 (Random.Range(-1.0f, 1.0f), height - 1.0f, Random.Range(-1.0f, 1.0f));
			
			pointlist[i].cylinder = cylinder;
		}
		
		GameObject xaxis = new GameObject ("xaxis");
		LineRenderer xline = xaxis.AddComponent<LineRenderer> ();
		xline.SetColors (cx, cx);
		xline.SetWidth (0.05f, 0.05f);
		xline.SetVertexCount (2);
		xline.useWorldSpace = false;
		xline.material = new Material (Shader.Find ("Particles/Alpha Blended"));
		xline.SetPosition (0, new Vector3 (-1, -1, 1));
		xline.SetPosition (1, new Vector3 (1, -1, 1));
		
		GameObject yaxis = new GameObject("yaxis");
		LineRenderer yline = yaxis.AddComponent<LineRenderer> ();
		yline.SetColors (cy, cy);
		yline.SetWidth (0.05f, 0.05f);
		yline.SetVertexCount (2);
		yline.useWorldSpace = false;
		yline.material = new Material (Shader.Find ("Particles/Alpha Blended"));
		yline.SetPosition (0, new Vector3 (-1, -1, 1));
		yline.SetPosition (1, new Vector3 (-1, 1, 1));
		
		GameObject zaxis = new GameObject ("zaxis");
		LineRenderer zline = zaxis.AddComponent<LineRenderer> ();
		zline.SetColors (cz, cz);
		zline.SetWidth (0.05f, 0.05f);
		zline.SetVertexCount (2);
		zline.useWorldSpace = false;
		zline.material = new Material (Shader.Find ("Particles/Alpha Blended"));
		zline.SetPosition (0, new Vector3 (-1, -1, -1));
		zline.SetPosition (1, new Vector3 (-1, -1, 1));
		
		xaxis.transform.parent = yaxis.transform.parent = zaxis.transform.parent = plot.transform;
	}

	void setupPlot3(GameObject plot)
	{
		float sx = 0.4f;
		float sy = 0.1f;

		for (int x = 0; x < num_points/10; x++) {
			for(int z = 0; z < num_points/10; z++)
			{
				GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				sphere.transform.parent = plot.transform;

				float xt = Random.Range(-1.0f, 1.0f);//x * 2.0f/10.0f -1;
				float zt = Random.Range(-1.0f, 1.0f);//z * 2.0f/10.0f -1;

				float y = 1.5f * Mathf.Exp(-(Mathf.Pow(xt, 2.0f) + Mathf.Pow(zt, 2.0f)/(2.0f*sx*sx)));

				sphere.transform.position = new Vector3 (xt, y-1, zt);
				sphere.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);

				pointlist[x*10 + z].sphere = sphere;
			}
		}

		
		GameObject xaxis = new GameObject ("xaxis");
		LineRenderer xline = xaxis.AddComponent<LineRenderer> ();
		xline.SetColors (cx, cx);
		xline.SetWidth (0.05f, 0.05f);
		xline.SetVertexCount (2);
		xline.useWorldSpace = false;
		xline.material = new Material (Shader.Find ("Particles/Alpha Blended"));
		xline.SetPosition (0, new Vector3 (-1, -1, 1));
		xline.SetPosition (1, new Vector3 (1, -1, 1));
		
		GameObject yaxis = new GameObject("yaxis");
		LineRenderer yline = yaxis.AddComponent<LineRenderer> ();
		yline.SetColors (cy, cy);
		yline.SetWidth (0.05f, 0.05f);
		yline.SetVertexCount (2);
		yline.useWorldSpace = false;
		yline.material = new Material (Shader.Find ("Particles/Alpha Blended"));
		yline.SetPosition (0, new Vector3 (-1, -1, 1));
		yline.SetPosition (1, new Vector3 (-1, 1, 1));
		
		GameObject zaxis = new GameObject ("zaxis");
		LineRenderer zline = zaxis.AddComponent<LineRenderer> ();
		zline.SetColors (cz, cz);
		zline.SetWidth (0.05f, 0.05f);
		zline.SetVertexCount (2);
		zline.useWorldSpace = false;
		zline.material = new Material (Shader.Find ("Particles/Alpha Blended"));
		zline.SetPosition (0, new Vector3 (-1, -1, -1.0f));
		zline.SetPosition (1, new Vector3 (-1, -1, 1.0f));
		
		xaxis.transform.parent = yaxis.transform.parent = zaxis.transform.parent = plot.transform;
	}

	GameObject lastObject;

	// Update is called once per frame
	void Update () {

		RaycastHit hit;

		Ray ray = centerCam.camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

		if (Physics.Raycast (ray, out hit)) {
			
			GameObject g = hit.collider.gameObject;
			if(Input.GetMouseButton (0) || Input.GetMouseButton (1))
			{
				foreach (DataPoint d in pointlist) {
					if(d.cube == g || d.cylinder == g || d.sphere == g)
					{
						d.select();
					}
				}
			}

		}
	}
}
