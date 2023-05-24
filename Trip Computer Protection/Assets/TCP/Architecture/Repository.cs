using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Repository
{
    public abstract void Initialize();
    public abstract void Save(); 
    public abstract void OnCreate(); 
    public abstract void OnStart();
}
