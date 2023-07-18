using System;
using System.Reflection;  //런타임중에 어셈블리 등의 코드에 접근하는 용도(메타데이터)

public class SingletonBase<T>
    where T : SingletonBase<T>
{
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                //ConstructorInfo constructorInfo = typeof(T).GetConstructor(new Type[] { });
                //_instance = constructorInfo.Invoke(new object[] { }) as T;

                _instance = Activator.CreateInstance<T>();  //타입에 해당하는 디폴트 생성자를 알아서 읽어옴
                _instance.Init();
            }

            return _instance;
        }

    }
    private static T _instance;

    protected virtual void Init() { }
}
