using UnityEngine;

public class Util
{
    public static void SetLayerRecursively(GameObject _obj, int _newLayer)
    {
        if(_obj == null)
        {
            return;
        }

        _obj.layer = _newLayer;
        foreach (Transform child in _obj.transform)
        {
            SetLayerRecursively(child.gameObject, _newLayer);
        }
    }
}
