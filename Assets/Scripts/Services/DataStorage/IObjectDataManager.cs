using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectDataManager
{
    void SaveData(BaseDataObject data);

    BaseDataObject LoadData(string indentifier);
}
