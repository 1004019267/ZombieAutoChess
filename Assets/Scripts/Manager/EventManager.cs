using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public delegate void FloatDelegate(float val);
public delegate void StringDelegate(string val);



class EventManager:Singleton<EventManager>
{
    public FloatDelegate GoldAdd;
    public FloatDelegate GoldRemove;
    public StringDelegate TextTipsShow;
}

