using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Tool.CSV
{
    public static class CsvHepler
    {
        public static void WriteCSV(string filename,string Data) //文件创建及数据保存
        {
            try
            {
                if (!File.Exists(filename))
                {
                    #region 创建文件
                    FileStream fs = new FileStream(filename,FileMode.Create,FileAccess.ReadWrite,FileShare.Read);
                    fs.Close();
                    //File.Create(filename); //创建文件路径               
                    #endregion
                }

                #region 写入数据
                //设置文件访问权限，ReadWrite可读写，FileShare.Read 允许其他同时读取
                FileInfo fl = new  FileInfo(filename);
                fl.Attributes = FileAttributes.Normal;
                StreamWriter sw;
                sw = new StreamWriter(filename,true, Encoding.Default);
                sw.WriteLine(Data);
                sw.Close();
                fl.Attributes = FileAttributes.ReadOnly;
                #endregion

            }
            catch (Exception ex)
            {
                ; //throw ex;
            }
        }

        private static void SaveCsv(DataTable dt, string filePath)
        {
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(filePath + dt.TableName + ".csv", FileMode.Create, FileAccess.Write);
                sw = new StreamWriter(fs, Encoding.Default);
                var data = string.Empty;
                //写出列名称
                for (var i = 0; i < dt.Columns.Count; i++)
                {
                    data += dt.Columns[i].ColumnName;
                    if (i < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
                //写出各行数据
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    data = string.Empty;
                    for (var j = 0; j < dt.Columns.Count; j++)
                    {
                        data += dt.Rows[i][j].ToString();
                        if (j < dt.Columns.Count - 1)
                        {
                            data += ",";
                        }
                    }
                    sw.WriteLine(data);
                }
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex);
            }
            finally
            {
                if (sw != null) sw.Close();
                if (fs != null) fs.Close();
            }
        }

        public static List<string> ReadCSV(string Path)
        {
            List<string> re = new List<string>();
            var fs = new FileStream(Path, FileMode.Open, FileAccess.Read);
            var reader = new StreamReader(fs, Encoding.Default);

            while (reader != null)
            {
                var temp = reader.ReadLine();
                re.Add(temp);
                if (temp == null)
                {
                    break;
                }
            }

            return re;
        }
    }
}
