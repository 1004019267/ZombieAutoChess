/// <summary>
/// ui的状态
/// </summary>
public class UI_State
{
    public BaseUI _instance;
    public UI_Type _type;
    //唯一id
    public int id = 0;
    public UI_State()
    {
        _type = UI_Type.normal;
    }
    public BaseUI getMe()
    {
        return _instance;
    }
}

public enum UI_Type
{
    normal,
    
}