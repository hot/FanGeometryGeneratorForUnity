using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FanGeneratorClass : MonoBehaviour {

	public Mesh FanPlaneGenerator(float r, float angleInDegree, int step)
	{
		Mesh ret = new Mesh();
		LinkedList<Vector3> points = new LinkedList<Vector3>();
		points.AddLast(new Vector3(0, 0, r));
		float stepAng = Mathf.Deg2Rad * (angleInDegree/step);
		for(int i = 1; i <= step; i++)
		{
			float angNow = stepAng * i;
			float halfAng = angNow/2.0f;
			float x = r * Mathf.Sin(halfAng);
			float z = r * Mathf.Cos(halfAng);
			points.AddFirst(new Vector3(-x, 0f, z));
			points.AddLast(new Vector3(x, 0f, z));
		}
		
		List<int> idx = new	List<int>();
		for(int j = 1; j < points.Count; j++)
		{
			idx.Add(j);
			idx.Add(j+1);
			idx.Add(0);
		}
		
		points.AddFirst(new Vector3(0, 0, 0));
		
		ret.vertices = points.ToArray();
		ret.triangles = idx.ToArray();
		
		ret.RecalculateNormals();
		ret.RecalculateBounds();
		
		return ret;
		
	}


	public Mesh FanGenerator(float r, float angleInDegree, float height, int step)
	{
		Mesh ret = new Mesh();
		LinkedList<Vector3> points = new LinkedList<Vector3>();
		points.AddLast(new Vector3(0, height, r));
		points.AddLast(new Vector3(0, 0, r));
		float stepAng = Mathf.Deg2Rad * (angleInDegree/step);
		for(int i = 1; i <= step; i++)
		{
			float angNow = stepAng * i;
			float halfAng = angNow/2.0f;
			float x = r * Mathf.Sin(halfAng);
			float z = r * Mathf.Cos(halfAng);
			points.AddFirst(new Vector3(-x, 0f, z));
			points.AddFirst(new Vector3(-x, height, z));
			points.AddLast(new Vector3(x, height, z));
			points.AddLast(new Vector3(x, 0f, z));
		}

		List<int> idx = new	List<int>();
		for(int j = 1; j < step * 2 + 1; j++)
		{
			idx.Add(j * 2);
			idx.Add(j * 2 + 2);
			idx.Add(0);

			idx.Add(j * 2 + 3);
			idx.Add(j * 2 + 1);
			idx.Add(1);

		}
		for(int j = 2; j < step * 4 + 2; j+=2)
		{
			idx.Add(j);
			idx.Add(j+1);
			idx.Add(j+2);

			idx.Add(j+1);
			idx.Add(j+3);
			idx.Add(j+2);
		}

		idx.Add(2);
		idx.Add(0);
		idx.Add(1);
		idx.Add(2);
		idx.Add(1);
		idx.Add(3);

		idx.Add(0);
		idx.Add(step * 4 + 2);
		idx.Add(step * 4 + 3);
		idx.Add(0);
		idx.Add(step * 4 + 3);
		idx.Add(1);

		points.AddFirst(new Vector3(0, 0, 0));
		points.AddFirst(new Vector3(0, height, 0));

		ret.vertices = points.ToArray();
		ret.triangles = idx.ToArray();

		ret.RecalculateNormals();
		ret.RecalculateBounds();

		return ret;

	}


	#region test

	public float r = 1f; 
	public float angleInDegree = 90f;
	public float height = 1f;
	public int step = 2;
	// Use this for initialization
	void Update () {
		MeshFilter mf = gameObject.GetComponent<MeshFilter>();
		Mesh mesh = FanGenerator(r, angleInDegree, height, step);
		mf.mesh = mesh;
		MeshCollider mc = gameObject.GetComponent<MeshCollider>();
		mc.sharedMesh = mesh;
	
	}

	void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);
	}

	#endregion


}
