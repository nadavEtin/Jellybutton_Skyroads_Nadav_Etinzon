using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//data manager for saving non-primitive data objects
public interface IObjectDataManager
{
    void SaveData(BaseDataObject data);

    BaseDataObject LoadData(string indentifier);
}
