using System;

[Serializable]
public class ParameterData
{
    #region Variables
    public EnumParameters id;
    public float value;
    #endregion

    #region Public methods
    public ParameterData(EnumParameters _id, float _value = 0.0f)
    {
        id = _id;
        value = _value;
    }
    #endregion
}
