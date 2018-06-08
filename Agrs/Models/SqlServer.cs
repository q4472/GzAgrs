using FarmSib.Base.Data;
using Nskd;
using System;
using System.Collections.Generic;
using System.Data;

namespace FarmSib.AreasAgrs.Areas.Agrs.Models
{
    public class Ss //: IDisposable
    {
        /*
        private MsSqlServer ss = null;

        public Ss()
        {
            ss = new MsSqlServer();
        }

        public void Dispose()
        {
            if (ss != null)
            {
                ss.Dispose();
                ss = null;
            }
        }
        */
        public static void FillSsAgrTable(NpcDataTable table, Dictionary<String, String> fs = null)
        {
            DataTable dt = HomeData.Agrs.F0GetAgrsTable(fs);

            foreach (DataRow dr in dt.Rows)
            {
                NpcDataRow row = table.NewRow();
                foreach (NpcDataColumn column in table.Columns)
                {
                    String dbName = (String)column.ExtendedProperties["dbName"];
                    String columnName = (String)column.ColumnName;
                    row[columnName] = dr[dbName];
                }
                table.Rows.Add(row);
            }
        }
        public static void FillSsAgrTable1(NpcDataTable table, Dictionary<String, String> fs = null)
        {
            DataTable dt = HomeData.Agrs.F1GetAgrsTable(fs);

            foreach (DataRow dr in dt.Rows)
            {
                NpcDataRow row = table.NewRow();
                foreach (NpcDataColumn column in table.Columns)
                {
                    String dbName = (String)column.ExtendedProperties["dbName"];
                    String columnName = (String)column.ColumnName;
                    row[columnName] = dr[dbName];
                }
                table.Rows.Add(row);
            }
        }
        /*
            public NpcDataTable GetSsCustTable(NpcDataTable filterTable)
            {
                NpcDataTable table = CreateNpcDataTableWithMd(SsCustTable.Md);

                DataTable fd = (filterTable == null) ? null : filterTable.Rows[0].ToDataTable();

                DataTable dt = ss.GetCustData(fd);

                DataView dv = dt.DefaultView;
                dv.Sort = "DESCR";
                foreach (DataRowView drv in dv)
                {
                    NpcDataRow row = table.NewRow();
                    foreach (NpcDataColumn column in table.Columns)
                    {
                        String dbName = (String)column.ExtendedProperties["dbName"];
                        String columnName = (String)column.ColumnName;
                        row[columnName] = drv[dbName];
                    }
                    table.Rows.Add(row);
                }
                return table;
            }

            public NpcDataTable GetSsStuffTable(NpcDataTable filterTable)
            {
                NpcDataTable table = CreateNpcDataTableWithMd(SsStuffTable.Md);

                DataTable fd = (filterTable == null) ? null : filterTable.Rows[0].ToDataTable();

                DataTable dt = ss.GetStuffData(fd);

                DataView dv = dt.DefaultView;
                dv.Sort = "DESCR";
                dv.RowFilter = "Descr <> ''";
                foreach (DataRowView drv in dv)
                {
                    NpcDataRow row = table.NewRow();
                    foreach (NpcDataColumn column in table.Columns)
                    {
                        String dbName = (String)column.ExtendedProperties["dbName"];
                        String columnName = (String)column.ColumnName;
                        row[columnName] = drv[dbName];
                    }
                    table.Rows.Add(row);
                }
                return table;
            }

            public void UpsertSsAgrTable(NpcDataRow row)
            {
                DataTable dt = row.ToDataTable();
                //Console.WriteLine(((Guid)dt.Rows[0][0]).ToString());
                ss.UpsertAgrTable(dt);
            }

            public void UpsertXlAgrTable(DataTable dt)
            {
                ss.UpsertXlAgrTable(dt);
            }
            */
        public static NpcDataTable CreateNpcDataTableWithMd(Object[] md)
        {
            NpcDataTable table = new NpcDataTable();
            foreach (Object[] colMd in md)
            {
                String typeName = (String)colMd[2];
                if (typeName.Substring(typeName.Length - 1, 1) == "?")
                {
                    typeName = "System.Nullable`1[System." + typeName.Substring(0, typeName.Length - 1) + "]";
                }
                else
                {
                    typeName = "System." + typeName;
                }
                NpcDataColumn column = new NpcDataColumn((String)colMd[0], Type.GetType(typeName));
                column.Caption = (String)colMd[1];
                column.ExtendedProperties.Add("width", (Int32)colMd[3]);
                column.ExtendedProperties.Add("alignment", (String)colMd[4]);
                column.ExtendedProperties.Add("readonly", (Boolean)colMd[5]);
                column.ExtendedProperties.Add("hidden", (Boolean)colMd[6]);
                column.ExtendedProperties.Add("dbName", (String)colMd[7]);
                column.ExtendedProperties.Add("relTable", (String)colMd[8]);
                table.Columns.Add(column);
            }
            return table;
        }
        /*
        public String GetXlSetting(String name)
        {
            return ss.GetXlSetting(name);
        }
        public void SetXlSetting(String name, String value)
        {
            ss.SetXlSetting(name, value);
        }

        public void RehreshXl()
        {
            DataTable dt = new DataTable();
            {
                // Загрузка из Excel новых данных
                String _xl_cnString =
                    "Provider=Microsoft.ACE.OLEDB.12.0;" +
                    "Data Source=" + App.Settings.XlFileName + ";" + // ss.GetXlSetting("file name")
                    "Extended Properties='Excel 12.0 Macro;HDR=No;IMEX=1;';";
                OleDbConnection _xl_cn = new OleDbConnection(_xl_cnString);
                _xl_cn.Open();
                try
                {
                    DateTime xlUpdated = DateTime.Parse(ss.GetXlSetting("last update date"));
                    ss.SetXlSetting("last update date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    TimeSpan ts = xlUpdated - new DateTime(2000, 1, 1);

                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = _xl_cn;
                    cmd.CommandText =
                        "select * " +
                        "from [" + App.Settings.XlSheetName + "$A:S] " + // ss.GetXlSetting("sheet name")
                        "where f2 > '" + ts.TotalSeconds.ToString() + "'";
                    (new OleDbDataAdapter(cmd)).Fill(dt);
                }
                catch (Exception exc) { Console.WriteLine(exc.Message); }
                _xl_cn.Close();
                _xl_cn.Dispose();
            }
            // Обновление на Sql Server
            if (dt.Rows.Count > 1) ss.UpsertXlAgrTable(dt); // первая строка заголовок
        }
    */
    }
}
