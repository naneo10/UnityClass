using UnityEngine;

//스위치로 켜고 끌 수 있는 장치라는 공통된 개념을 추상화
//스위치가 구체적인 장치를 몰라도 이 인터페이스만 알면 작동
public interface ISwitchble
{
    public bool IsActive { get; }
    public void Activate();
    public void Deactivate();
}

//스위치
//무엇을 켜고 끄는지 몰라도 된다.
//단지 ISwitchble 인터페이스를 통해 작동만 시킨다.
//구체적인 장치(문, 조명 등)에 직접 의존하지 않는다.
public class SWitchDIP : MonoBehaviour
{
    private ISwitchble switchbleDevice; //인터페이스에 의존

    //생성자를 통해 외부에서 스위치로 제어할 대상을 주입받음(DI)
    public SWitchDIP(ISwitchble device)
    {
        switchbleDevice = device;
    }

    public void Toggle()
    {
        if (switchbleDevice.IsActive)
        {
            switchbleDevice.Deactivate();
        }
        else
        {
            switchbleDevice.Activate();
        }
    }
}

public class DoorDIP : MonoBehaviour, ISwitchble
{
    public bool isActive;

    //public bool IsActive
    //{
    //  get { return isActivate; }
    //}
    public bool IsActive => isActive;

    public void Activate()
    {
        isActive = true;
    }
    public void Deactivate()
    {
        isActive = false;
    }
}
