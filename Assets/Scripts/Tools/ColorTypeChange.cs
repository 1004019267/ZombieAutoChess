using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTypeChange : Singleton<ColorTypeChange>
{
    public Color HexColorToColor(string colorStr)
    {
        if (string.IsNullOrEmpty(colorStr))
        {
            return new Color();
        }
        int colorInt = int.Parse(colorStr, System.Globalization.NumberStyles.AllowHexSpecifier);
        return IntToColor(colorInt);
    }
    //[NoToLuaAttribute]
    public Color IntToColor(int colorInt)
    {
        float basenum = 255;

        int b = 0xFF & colorInt;
        int g = 0xFF00 & colorInt;
        g >>= 8;
        int r = 0xFF0000 & colorInt;
        r >>= 16;
        return new Color((float)r / basenum, (float)g / basenum, (float)b / basenum, 1);

    }


}
