using System;
using UnityEngine;

public class AutoScriptDestruct : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		this._elapsedTime += Time.deltaTime;
		if (this._elapsedTime > this.time)
		{
			try
			{
				if (this.targetScript != null)
				{
					UnityEngine.Object.Destroy(this.targetScript);
				}
			}
			catch
			{
			}
			UnityEngine.Object.Destroy(this);
		}
	}

	public MonoBehaviour targetScript;

	public float time = 0.25f;

	private float _elapsedTime;
}
