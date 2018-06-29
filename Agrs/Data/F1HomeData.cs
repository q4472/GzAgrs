using Nskd;
using System;
using System.Data;

namespace Agrs.Data
{
    public class F1HomeData
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
            dt = GetFirstTable(rqp.GetResponse("http://127.0.0.1:11014/").Data);
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
            dt = GetFirstTable(rqp.GetResponse("http://127.0.0.1:11014/").Data);
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
            dt = GetFirstTable(rqp.GetResponse("http://127.0.0.1:11014/").Data);
            return dt;
        }
        public static Int32 F1Upsert1c(RequestPackage rqp)
        {
            Int32 code = -1;
            ResponsePackage rsp = rqp.GetResponse("http://127.0.0.1:11014/");
            if ((rsp != null) && (rsp.Data != null))
            {
                Object v = GetScalar(rsp.Data);
                if (v != null && v is Int32)
                {
                    code = (Int32)v;
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
            rqp.GetResponse("http://127.0.0.1:11002/");
        }
        public static String F1GetAgrNumSql()
        {
            String agrNum = "0-" + (DateTime.Now.Year - 2000).ToString();
            RequestPackage rqp = new RequestPackage();
            rqp.Command = "[dbo].[договоры_получить_номер_1]";
            ResponsePackage rsp = rqp.GetResponse("http://127.0.0.1:11002/");
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
        public static DataTable F1GetДоговоры(RequestPackage rqp0 = null)
        {
            DataTable dt = null;
            RequestPackage rqp = new RequestPackage
            {
                Command = "[dbo].[договоры_покупатели_select_2]"
            };
            if (rqp0 != null && rqp0.Parameters != null && rqp0.Parameters.Length > 0)
            {
                rqp.Parameters = new RequestParameter[0];
                foreach (var p in rqp0.Parameters)
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
                        Array.Resize(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter(name, p.Value);
                    }
                }
            }
            var rsp = rqp.GetResponse("http://127.0.0.1:11012/");
            dt = GetFirstTable(rsp.Data);
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
                dt = GetFirstTable(rqp.GetResponse("http://127.0.0.1:11012/").Data);
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
        private static Object GetScalar(DataSet ds)
        {
            Object value = null;
            DataTable dt = GetFirstTable(ds);
            if ((dt != null) && (dt.Columns.Count > 0) && (dt.Rows.Count > 0))
            {
                value = dt.Rows[0][0];
                if (value == DBNull.Value) value = null;
            }
            return value;
        }
    }
}
