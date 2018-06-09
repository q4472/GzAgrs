using Nskd;
using System;
using System.Collections.Generic;
using System.Data;

namespace FarmSib.AreasAgrs.Areas.Agrs.Data
{
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
        public static DataTable F1GetДоговоры(Dictionary<String, String> pars)
        {
            DataTable dt = null;
            RequestPackage rqp = new RequestPackage();
            rqp.Command = "[dbo].[договоры_покупатели_select_1]";
            if (pars != null)
            {
                rqp.Parameters = new RequestParameter[pars.Count];
                int pi = 0;
                foreach (var p in pars)
                {
                    String v = p.Value;
                    if (!String.IsNullOrWhiteSpace(v))
                    {
                        rqp.Parameters[pi++] = new RequestParameter(p.Key, v);
                    }
                    else
                    {
                        rqp.Parameters[pi++] = new RequestParameter(p.Key, null);
                    }
                }
            }
            dt = GetFirstTable(Execute12(rqp));
            return dt;
        }

        public static DataTable F1GetДоговоры(RequestPackage rqp)
        {
            DataTable dt = null;
            RequestPackage rqp1 = new RequestPackage();
            rqp1.Command = "[dbo].[договоры_покупатели_select_1]";
            if (rqp != null && rqp.Parameters != null && rqp.Parameters.Length > 0)
            {
                rqp1.Parameters = new RequestParameter[0];
                foreach (var p in rqp.Parameters)
                {
                    String name = null;
                    switch (p.Name)
                    {
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

        private static DataTable GetFirstTable(DataSet ds)
        {
            DataTable dt = null;
            if ((ds != null) && (ds.Tables.Count > 0))
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        private static DataSet Execute12(RequestPackage rqp)
        {
            ResponsePackage rsp = rqp.GetResponse("http://" + Env.dataServicesHost + ":11012/");
            return rsp.Data;
        }
    }
}
