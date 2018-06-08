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
