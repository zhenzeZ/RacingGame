using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

	public Color lineColor;

	private List<Transform> nodes = new List<Transform> ();

	void OnDrawGizmos(){
		Gizmos.color = lineColor;

	Transform[] pathTransforms = GetComponentsInChildren<Transform>();
	nodes = new List<Transform>();

		for (int i = 0; i < pathTransforms.Length; i++) {
			if (pathTransforms [i] != transform) {
				nodes.Add (pathTransforms [i]);
			}
		}

		int count = nodes.Count;
		for (int i = 0; i < count; i++) {
			Vector3 currentNode = nodes [i].position;
			Vector3 previousNode = nodes [(count - 1 + i) % count].position;
			Gizmos.DrawLine (previousNode, currentNode);
		}
	}
}
