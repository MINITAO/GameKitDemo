#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class LineRendererEditor : MonoBehaviour {

	public Vector3[] path = new Vector3[]{
		new Vector3(0,0,0),
		new Vector3(0,-1,0),

	};
	public bool isLocalPosition = true;
	public bool isBesizer = false;

	[Space]
	public Color gizmosColor = Color.red;
	public bool isEdit = true;

	void Start(){
		if(!Application.isPlaying){
			SetLineRenderer();
		}
	}

	void OnDrawGizmos()  
	{  
		if(Application.isPlaying){ return;}

		if(isEdit && path!=null)
		{
			Gizmos.color=gizmosColor;
			Matrix4x4 cacheMat = Gizmos.matrix;

			if(isLocalPosition){
				Gizmos.matrix = transform.localToWorldMatrix;
			}
			if(path.Length>1)
			{
				Vector3 prev=path[0];  
				for(int i=1;i<path.Length;++i){  
					Gizmos.DrawLine(path[i],prev);
					prev = path[i];
				}
			}
			for(int i=0;i<path.Length;++i){  
				Gizmos.DrawWireSphere(path[i],0.1f);
			}
			Gizmos.matrix = cacheMat;
		}

	}

	public void SetLineRenderer(){
		Vector3[] roads = path;
		if(isBesizer && roads.Length>2){
			List<Vector3> vertices = new List<Vector3>();
			int count = path.Length;
			//第一个点
			Vector3 v = path[0]-new Vector3(0.0001f,0f,0f);
			CatmulRom(vertices,ref v,ref path[0],ref path[1],ref path[2]);
			for(int i = 0;i<count-3;++i){
				CatmulRom(vertices,ref path[i],ref path[i+1],ref path[i+2],ref path[i+3]);
			}
			//最后一个
			v = (path[count-1]-new Vector3(0.0001f,0f,0f));
			CatmulRom(vertices,ref path[count-3],ref path[count-2],ref path[count-1],ref v);

			roads = vertices.ToArray();
		}

		LineRenderer lr = GetComponent<LineRenderer>();
		if(lr){
			lr.positionCount = roads.Length;
			lr.SetPositions(roads);
		}
	}

	void CatmulRom(List<Vector3> newPoints, ref Vector3 p0,ref Vector3 p1,ref Vector3 p2,ref Vector3 p3,float amountOfPoints=10f)
	{
		float t0 = 0.0f;
		float t1 = GetT(t0, p0, p1);
		float t2 = GetT(t1, p1, p2);
		float t3 = GetT(t2, p2, p3);

		for(float t=t1; t<t2; t+=((t2-t1)/amountOfPoints))
		{
			Vector2 A1 = (t1-t)/(t1-t0)*p0 + (t-t0)/(t1-t0)*p1;
			Vector2 A2 = (t2-t)/(t2-t1)*p1 + (t-t1)/(t2-t1)*p2;
			Vector2 A3 = (t3-t)/(t3-t2)*p2 + (t-t2)/(t3-t2)*p3;

			Vector2 B1 = (t2-t)/(t2-t0)*A1 + (t-t0)/(t2-t0)*A2;
			Vector2 B2 = (t3-t)/(t3-t1)*A2 + (t-t1)/(t3-t1)*A3;

			Vector2 C = (t2-t)/(t2-t1)*B1 + (t-t1)/(t2-t1)*B2;

			newPoints.Add(C);
		}
	}

	float GetT(float t, Vector2 p0, Vector2 p1)
	{
		float alpha = 0.5f;
		float a = Mathf.Pow((p1.x-p0.x), 2.0f) + Mathf.Pow((p1.y-p0.y), 2.0f);
		float b = Mathf.Pow(a, 0.5f);
		float c = Mathf.Pow(b, alpha);

		return (c + t);
	}
}



[CustomEditor(typeof(LineRendererEditor))]
public class LineRendererEditorGUI : Editor {

	void OnSceneGUI(){
		if(Application.isPlaying){ return;}

		LineRendererEditor pathCollect = target as LineRendererEditor;

		if(pathCollect.isEdit){
			Tools.current = Tool.None;
			if(pathCollect.path!=null)
			{
				for(int i=0;i<pathCollect.path.Length;++i){
					if(pathCollect.isLocalPosition){
						Vector3 worldPos = Handles.DoPositionHandle(pathCollect.transform.TransformPoint(pathCollect.path[i]),Quaternion.identity);
						Handles.Label(worldPos,i+":"+pathCollect.path[i]);
						pathCollect.path[i] = pathCollect.transform.InverseTransformPoint(worldPos);
					}else{
						pathCollect.path[i] = Handles.DoPositionHandle(pathCollect.path[i],Quaternion.identity);
						Handles.Label(pathCollect.path[i],i+":"+pathCollect.path[i]);
					}
				}
				SceneView.RepaintAll();

				pathCollect.SetLineRenderer();
			}
		}
	}
}

#endif