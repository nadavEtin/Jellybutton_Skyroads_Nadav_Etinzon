using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrimitiveDataManager
{
    void SavePrimitive(string name, string data);
    void SavePrimitive(string name, int data);
    void SavePrimitive(string name, float data);

    string LoadStringPrimitive(string name);
    int LoadIntPrimitive(string name);
    float LoadFloatPrimitive(string name);
}
