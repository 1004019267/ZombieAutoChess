using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 不销毁
/// </summary>
public class DonDestroy : MonoBehaviour
{

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

}
