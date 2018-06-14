using Nskd;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Base.Data
{
    public class HomeData
    {
        public class Agrs
        {
            public static Int32 F1Upsert1c(String cmd, Dictionary<String, String> pars = null)
            {
                Int32 code = -1;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = cmd;
                if (pars != null)
                {
                    rqp.Parameters = new RequestParameter[pars.Count];
                    int pi = 0;
                    foreach (var p in pars)
                    {
                        rqp.Parameters[pi++] = new RequestParameter(p.Key, p.Value);
                    }
                }
                ResponsePackage rsm = ExecuteIn1c1(rqp);
                code = -1;
                if ((rsm != null) && (rsm.Data != null) && (rsm.Data.Tables.Count > 0))
                {
                    DataTable dt = rsm.Data.Tables[0];
                    if ((dt.Rows.Count > 0) && (dt.Columns.Count > 0))
                    {
                        Object value = dt.Rows[0][0];
                        if (value != DBNull.Value)
                        {
                            code = System.Convert.ToInt32(value);
                        }
                    }
                }
                return code;
            }
            public static void F1UpsertSql(String cmd, RequestPackage rqp0)
            {
                RequestPackage rqp = new RequestPackage();
                switch (cmd)
                {
                    case "Добавить":
                        rqp.Command = "[dbo].[договоры_добавить_2]";
                        break;
                    case "Обновить":
                        rqp.Command = "[dbo].[договоры_обновить_2]";
                        break;
                    default:
                        rqp.Command = cmd;
                        break;
                }
                if (rqp0 != null && rqp0.Parameters != null && rqp0.Parameters.Length > 0)
                {
                    rqp.Parameters = new RequestParameter[0];
                    foreach (var p in rqp0.Parameters)
                    {
                        if (p != null && p.Name != null && p.Name.Length > 0 && p.Name[0] == 'f')
                        {
                            Array.Resize(ref rqp.Parameters, rqp.Parameters.Length + 1);
                            rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter(p.Name, p.Value);
                        }
                    }
                }
                ExecuteInSql(rqp);
            }
            public static String F1GetAgrNumSql()
            {
                String agrNum = "0-" + (DateTime.Now.Year - 2000).ToString();
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[договоры_получить_номер_1]";
                ResponsePackage rsp = ExecuteInSql(rqp);
                if ((rsp != null) && (rsp.Data != null) && (rsp.Data.Tables.Count > 0))
                {
                    DataTable dt = rsp.Data.Tables[0];
                    if ((dt.Rows.Count > 0) && (dt.Columns.Count > 0))
                    {
                        agrNum = dt.Rows[0][0] as String;
                    }
                }
                return agrNum;
            }
        }

        private static String dataServicesHost = "127.0.0.1"; // localhost

        public static ResponsePackage ExecuteInSql(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + dataServicesHost + ":11002/");
            return rsp;
        }
        public static ResponsePackage ExecuteInXl(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + dataServicesHost + ":11003/");
            return rsp;
        }
        public static ResponsePackage ExecuteIn1c(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + dataServicesHost + ":11004/");
            return rsp;
        }
        public static ResponsePackage ExecuteIn1c1(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + dataServicesHost + ":11014/");
            return rsp;
        }
        public static ResponsePackage ExecuteInFs(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + dataServicesHost + ":11005/");
            return rsp;
        }
        public static ResponsePackage ExecuteInMail(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + dataServicesHost + ":11007/");
            return rsp;
        }
        public static ResponsePackage ExecuteSssp(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + dataServicesHost + ":11008/");
            return rsp;
        }
        public static DataSet Execute(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + dataServicesHost + ":11002/");
            return rsp.Data;
        }
        public static DataTable GetFirstTable(DataSet ds)
        {
            DataTable dt = null;
            if ((ds != null) && (ds.Tables.Count > 0))
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public static Object GetScalar(DataSet ds)
        {
            Object r = null;
            DataTable dt = GetFirstTable(ds);
            if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count >= 0)
            {
                r = dt.Rows[0][0];
            }
            return r;
        }
        public static DataTable GetFsInfoCommon(String fileId, String link, String type = null)
        {
            RequestPackage rqp = new RequestPackage();
            rqp.Command = "[dbo].[file_info_get]";
            rqp.Parameters = new RequestParameter[] {
                new RequestParameter("file_id", fileId ),
                new RequestParameter("link", link ),
                new RequestParameter("type", type )
            };
            DataTable dt = GetFirstTable(Execute(rqp));
            return dt;
        }
    }
}
