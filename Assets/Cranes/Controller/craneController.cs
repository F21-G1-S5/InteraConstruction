using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class craneController : MonoBehaviour
{
	[System.Serializable]
	public class LayerInfo
	{
		public string name;
		public string axisName;
		public string paramName;
		public bool isControlNormalizedTime;

		[HideInInspector]
		public float curValue;
	}

	public string[] layersNamesToInitAtHalf;

	[SerializeField]
	private LayerInfo[] layers;


	[SerializeField]
	private Animator animator;

	void Awake()
	{
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}
	}

	void Start ()
	{
		Init();
	}

	void Init()
	{
		if (layersNamesToInitAtHalf != null)
		{
			for (int i = 0; i < layersNamesToInitAtHalf.Length; i++)
			{
				string currentLayerName = layersNamesToInitAtHalf[i];
				int currentLayerIndex = animator.GetLayerIndex(currentLayerName);
				AnimatorStateInfo curStateInfo = animator.GetCurrentAnimatorStateInfo(currentLayerIndex);

				animator.Play(curStateInfo.shortNameHash, currentLayerIndex, 0.5f);
			}
		}
	}

	public void Update ()
	{
		if (layers != null)
		{
			for (int i = 0; i < layers.Length; i++)
			{
				LayerInfo curLayerInfo = layers[i];

				curLayerInfo.curValue = Input.GetAxis(curLayerInfo.axisName);

				if (curLayerInfo.isControlNormalizedTime)
				{
					int currentLayerIndex = animator.GetLayerIndex(curLayerInfo.name);
					AnimatorStateInfo curStateInfo = animator.GetCurrentAnimatorStateInfo(currentLayerIndex);

					float normalizedTime = curStateInfo.normalizedTime;
					if (normalizedTime > 1 ||
						normalizedTime < 0)
					{
						curLayerInfo.curValue = 0;
						animator.Play(curStateInfo.shortNameHash, currentLayerIndex, Mathf.Clamp01(normalizedTime));
					}
				}
			}
		}
	}

	public void FixedUpdate()
	{
		if (layers != null)
		{
			for (int i = 0; i < layers.Length; i++)
			{
				LayerInfo curLayerInfo = layers[i];

				animator.SetFloat(curLayerInfo.paramName, curLayerInfo.curValue);
			}
		}
	}			
}