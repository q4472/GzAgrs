using Nskd;
using System;
using System.Collections.Generic;
using System.Data;

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
        public static ResponsePackage ExecuteIn1c1(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + dataServicesHost + ":11014/");
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
    class Env
    {
        public static String dataServicesHost = "127.0.0.1"; // localhost
    }
    class Garza1Cv77
    {
        public static DataTable F1GetAgrByCode(String code)
        {
            DataTable dt = null;
            RequestPackage rqp = new RequestPackage
            {
                Command = "ПолучитьДоговорПоКоду",
                Parameters = new RequestParameter[]
                {
                        new RequestParameter { Name = "Код", Value = code }
                }
            };
            dt = GetFirstTable(Execute14(rqp));
            return dt;
        }

        public static DataTable F1GetCustTable(String filter)
        {
            DataTable dt = null;
            RequestPackage rqp = new RequestPackage();
            rqp.Command = "[dbo].[oc_клиенты_select_1]";
            if (!String.IsNullOrWhiteSpace(filter))
            {
                rqp.Parameters = new RequestParameter[]
                {
                    new RequestParameter("DESCR", filter)
                };
            }
            dt = GetFirstTable(Execute14(rqp));
            return dt;
        }

        public static DataTable F1GetStuffTable(String filter)
        {
            DataTable dt = null;
            RequestPackage rqp = new RequestPackage();
            rqp.Command = "[dbo].[oc_сотрудники_select_1]";
            if (!String.IsNullOrWhiteSpace(filter))
            {
                rqp.Parameters = new RequestParameter[]
                {
                    new RequestParameter("DESCR", filter)
                };
            }
            dt = GetFirstTable(Execute14(rqp));
            return dt;
        }

        private static DataTable GetFirstTable(DataSet ds)
        {
            DataTable dt = null;
            if ((ds != null) && (ds.Tables.Count > 0))
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        private static DataSet Execute14(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + Env.dataServicesHost + ":11014/");
            return rsp.Data;
        }
    }
    class GarzaSql
    {
        public static DataTable F1GetДоговоры(RequestPackage rqp = null)
        {
            DataTable dt = null;
            RequestPackage rqp1 = new RequestPackage
            {
                Command = "[dbo].[договоры_покупатели_select_2]"
            };
            if (rqp != null && rqp.Parameters != null && rqp.Parameters.Length > 0)
            {
                rqp1.Parameters = new RequestParameter[0];
                foreach (var p in rqp.Parameters)
                {
                    String name = null;
                    switch (p.Name)
                    {
                        case "f0": name = "f0"; break;
                        case "Вид контракта:": name = "f1"; break;
                        case "№ п/п (внутр):": name = "f2"; break;
                        case "№ договора:": name = "f3"; break;
                        case "Клиент:": name = "f4"; break;
                        case "Дата договора:.min": name = "f6min"; break;
                        case "Дата договора:.max": name = "f6max"; break;
                        case "Менеджер:": name = "f12"; break;
                        case "Сумма:.min": name = "f13min"; break;
                        case "Сумма:.max": name = "f13max"; break;
                        case "№ торгов:": name = "f15"; break;
                        default: break;
                    }
                    if (name != null && !String.IsNullOrWhiteSpace(p.Value as String))
                    {
                        Array.Resize(ref rqp1.Parameters, rqp1.Parameters.Length + 1);
                        rqp1.Parameters[rqp1.Parameters.Length - 1] = new RequestParameter(name, p.Value);
                    }
                }
            }
            dt = GetFirstTable(Execute12(rqp1));
            return dt;
        }

        public static DataTable F1GetДоговоры(String f0)
        {
            DataTable dt = null;
            if (!String.IsNullOrWhiteSpace(f0))
            {
                RequestPackage rqp = new RequestPackage
                {
                    Command = "[dbo].[договоры_покупатели_select_2]",
                    Parameters = new RequestParameter[]
                    {
                        new RequestParameter { Name = "f0", Value = f0 }
                    }
                };
                dt = GetFirstTable(Execute12(rqp));
            }
            return dt;
        }

        private static DataTable GetFirstTable(DataSet ds)
        {
            DataTable dt = null;
            if ((ds != null) && (ds.Tables.Count > 0))
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public static DataSet Execute12(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + Env.dataServicesHost + ":11012/");
            return rsp.Data;
        }
    }
}
