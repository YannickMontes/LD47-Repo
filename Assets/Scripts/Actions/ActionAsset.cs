using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionAsset : ScriptableObject
{
	public abstract ActionInstance CreateInstance();
}