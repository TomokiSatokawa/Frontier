using R3;
using UnityEngine;

public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
{
    private ReactiveProperty<MotionType> _motionType = new(MotionType.Free);
    public ReadOnlyReactiveProperty<MotionType> Type => _motionType;

    public void SetMotionType(MotionType motionType)
    {
        _motionType.Value = motionType;
    }
}
