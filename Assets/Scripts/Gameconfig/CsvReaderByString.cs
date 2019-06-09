using UnityEngine;
using System.Collections;
using System.IO;
using System.Data;

public class CsvReaderByString
{
    private  ArrayList  rowAL;        //行链表,CSV文件的每一行就是一个链

    public CsvReaderByString(string parsestr)
    {
        rowAL = new ArrayList();
        StringReader _reader = new StringReader(parsestr);
        string csvdatabase = "";
        while (true)
        {
            string fileDataLine;
            fileDataLine = _reader.ReadLine();
            if (fileDataLine == null)
            {
                break;
            }
            if (csvdatabase == "")
            {
                csvdatabase = fileDataLine;
            }
            else
            {
                csvdatabase += "/r/n" + fileDataLine;
            }
            //如果包含偶数个引号，说明该行数据中出现回车符或包含逗号
            if (!IfOddQuota(csvdatabase))
            {
                AddNewDataLine(csvdatabase);
                csvdatabase = "";
            }
        }
        _reader.Close();
        if (csvdatabase.Length > 0)
        {
            throw new UnityException("CSV文件的格式有错误");
        }

    }
    /// <summary>
    /// 判断字符串是否包含奇数个引号
    /// </summary>
    /// <param name="dataLine">数据行</param>
    /// <returns>为奇数时，返回为真；否则返回为假</returns>
    private bool IfOddQuota(string dataLine)
    {
        int count = 0;
        bool oddQuota = false;
        for (int i = 0; i < dataLine.Length; i++)
        {
            if (dataLine[i] == '\"')
            {
                count++;
            }
        }
        if (count %2 == 1)
        {
            oddQuota = true;
        }
        return oddQuota;
    }
    
    /// <summary>
    /// 判断是否以奇数个引号开始
    /// </summary>
    /// <param name="dataCell"></param>
    /// <returns></returns>
    private bool IfOddStartQuota(string dataLine)
    {
        int count = 0;
        bool oddQuota = false;
        for (int i = 0; i < dataLine.Length; i++)
        {
            if (dataLine[i] != '\"')
            {
                break;
            }
            count++;
        }
        if (count % 2 == 1)
        {
            oddQuota = true;
        }
        return oddQuota;
    }
    /// <summary>
    /// 判断是否以奇数个引号结尾
    /// </summary>
    /// <param name="dataCell"></param>
    /// <returns></returns>
    private bool IfOddEndQuota(string dataLine)
    {
        int count = 0;
        bool oddQuota = false;
        for (int i = dataLine.Length-1; i >= 0; i--)
        {
            if (dataLine[i] != '\"')
            {
                break;
            }
            count++;
        }
        if (count % 2 == 1)
        {
            oddQuota = true;
        }
        return oddQuota;
    }
    
    /// <summary>
    /// 去掉格子的首尾引号，把双引号变成单引号
    /// </summary>
    /// <param name="fileCellData"></param>
    /// <returns></returns>
    private string GetHandleData(string fileCellData)
    {
        if (IfOddStartQuota(fileCellData))
        {
            if (IfOddEndQuota(fileCellData))
            {
                return fileCellData.Substring(1,fileCellData.Length -2).Replace("\"\"","\"");
            }
            else
            {
                throw new UnityException("数据引号无法匹配" + fileCellData);
            }
        }
        else
        {
            if (fileCellData.Length >2 && fileCellData[0] == '\"')
            {
                return fileCellData.Substring(1,fileCellData.Length -2).Replace("\"\"","\"");
            }
        }
        return fileCellData;
    }

    private void AddNewDataLine(string newdataLine)
    {
        ArrayList colAL = new ArrayList();
        string[] dataArray = newdataLine.Split(',');
        bool  oddStartQuota = false;       //是否以奇数个引号开始
        string cellData = "";
		
        for (int i = 0 ;i < dataArray.Length;i++)
        {
            if (oddStartQuota)
            {
                //因为前面用逗号分割,所以要加上逗号
                cellData += "," + dataArray[i];
                //是否以奇数个引号结尾
                if (IfOddEndQuota(dataArray[i]))
                {
                    colAL.Add(GetHandleData(cellData));
                    oddStartQuota = false;
                    continue;
                }
            }
            else
            {
                //是否以奇数个引号开始
				
                if (IfOddStartQuota(dataArray[i]))
                {
                    //是否以奇数个引号结尾,不能是一个双引号,并且不是奇数个引号
					
                    if (IfOddEndQuota(dataArray[i]) && dataArray[i].Length > 2 && !IfOddQuota(dataArray[i]))
                    {
                        colAL.Add(GetHandleData(dataArray[i]));
                        oddStartQuota = false;
                        continue;
                    }
                    else
                    {
						
                        oddStartQuota = true;  
                        cellData = dataArray[i];
                        continue;
                    }
                } 
                else
                {
                    colAL.Add(GetHandleData(dataArray[i])); 
                }
            }           
        }
        if (oddStartQuota)
        {
            throw new UnityException("数据格式有问题");
        }
        this.rowAL.Add(colAL);
    }
    
    
    /// <summary>
    /// 获取行数
    /// </summary>
    public int RowCount
    {
        get
        {
            return this.rowAL.Count;
        }
    }

    public int ColCount
    {
        get
        {
            int maxcount = 0;
            for (int i = 0; i < this.rowAL.Count; i++)
            {
                ArrayList colAL = (ArrayList) this.rowAL[i];
                maxcount = maxcount > colAL.Count ? maxcount : colAL.Count;
            }
            return maxcount;
        }
    }
    
    
    public string this[int row,int col]
    {
        get
        {   
            //数据有效性验证
			
            CheckRowValid(row);
            CheckColValid(col);
            ArrayList colAL = (ArrayList) this.rowAL[row-1];
			
            //如果请求列数据大于当前行的列时,返回空值
			
            if (colAL.Count < col)
            {
                return "";
            }
			
            return colAL[col-1].ToString();    
        }
    }
    /// <summary>
    /// 根据最小行，最大行，最小列，最大列，来生成一个DataTable类型的数据
	
    /// 行等于1代表第一行
	
    /// 列等于1代表第一列
	
    /// maxrow: -1代表最大行
    /// maxcol: -1代表最大列
    /// </summary>
    public DataTable this[int minRow,int maxRow,int minCol,int maxCol]
    {
        get
        {
            //数据有效性验证
			
            CheckRowValid(minRow);
            CheckMaxRowValid(maxRow);
            CheckColValid(minCol);
            CheckMaxColValid(maxCol);
            if (maxRow == -1)
            {
                maxRow = RowCount;
            }
            if (maxCol == -1)
            {
                maxCol = ColCount;
            }
            if (maxRow < minRow)
            {
                throw new UnityException("最大行数不能小于最小行数");
            }
            if (maxCol < minCol)
            {
                throw new UnityException("最大列数不能小于最小列数");
            }
            DataTable csvDT = new DataTable();
            int   i;
            int   col;
            int   row;
			
            //增加列
			
            for (i = minCol;i <= maxCol;i++)
            {
                csvDT.Columns.Add(i.ToString());
            }
            for (row = minRow;row <= maxRow;row++)
            {
                DataRow csvDR = csvDT.NewRow();
				
                i = 0;
                for (col = minCol;col <=maxCol;col++)
                {
                    csvDR[i] = this[row,col];
                    i++;
                }
                csvDT.Rows.Add(csvDR);
            }
			
            return csvDT;
        }
    }
    private void CheckRowValid(int row)
    {
        if (row <= 0)
        {
            throw  new UnityException("行数不能小于0");
        }

        if (row > RowCount)
        {
            throw new UnityException("没有当前行的数据");
        }
    }

    private void CheckMaxRowValid(int maxrow)
    {
        if (maxrow <= 0 && maxrow != -1)
        {
            throw new UnityException();
        }

        if (maxrow > RowCount)
        {
            throw new UnityException("没有当前行的数据");
        }
    }
    private void CheckColValid(int col)
    {
        if (col <= 0)
        {
            throw new UnityException("列数不能小于0");    
        } 
        if (col > ColCount)
        {
            throw new UnityException("没有当前列的数据");   
        }
    }
    
    private void CheckMaxColValid(int maxCol)
    {
        if (maxCol <= 0 && maxCol != -1)
        {
            throw new UnityException("列数不能等于0或小于-1");    
        } 
        if (maxCol > ColCount)
        {
            throw new UnityException("没有当前列的数据");   
        }
    }
    
    
}