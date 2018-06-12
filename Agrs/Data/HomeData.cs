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
            public static DataTable F0GetCustTable(String filter = null)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[oc_клиенты_select]";
                if (!String.IsNullOrWhiteSpace(filter))
                {
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter("DESCR", filter)
                    };
                }
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static DataTable F0GetStuffTable(String filter = null)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[oc_сотрудники_select]";
                if (!String.IsNullOrWhiteSpace(filter))
                {
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter("DESCR", filter)
                    };
                }
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static DataTable F0GetAgrsTable(Dictionary<String, String> pars = null)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[договоры_покупатели_select]";
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
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static Int32 F0Upsert1c(String cmd, Dictionary<String, String> pars = null)
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
                ResponsePackage rsm = ExecuteIn1c(rqp);
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
            /*
            public static String F0UpsertExcel(String cmd, Dictionary<String, String> pars = null)
            {
                String status = "ok";
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
                ResponsePackage rsm = ExecuteInXl(rqp);
                if ((rsm != null) && (rsm.Status != null))
                {
                    status = rsm.Status;
                }
                return status;
            }
            */
            public static void F0UpsertSql(String cmd, Dictionary<String, String> pars = null)
            {
                RequestPackage rqp = new RequestPackage();
                switch (cmd)
                {
                    case "Добавить":
                        rqp.Command = "[dbo].[договоры_добавить]";
                        break;
                    case "Обновить":
                        rqp.Command = "[dbo].[договоры_обновить]";
                        break;
                    default:
                        rqp.Command = cmd;
                        break;
                }
                if (pars != null)
                {
                    rqp.Parameters = new RequestParameter[pars.Count];
                    int pi = 0;
                    foreach (var p in pars)
                    {
                        rqp.Parameters[pi++] = new RequestParameter(p.Key, p.Value);
                    }
                }
                ExecuteInSql(rqp);
            }
            public static String F0GetAgrNumSql()
            {
                String agrNum = "0-" + (DateTime.Now.Year - 2000).ToString();
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[договоры_получить_номер]";
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
            /*
            public static Boolean F0RefreshXl()
            {
                Boolean result = false;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "RefreshSqlFromXl";
                ResponsePackage rsm = ExecuteInXl(rqp);
                if ((rsm != null) && (rsm.Status == "ok"))
                {
                    result = true;
                }
                return result;
            }
            */


            public static DataTable F1GetCustTable(String filter = null)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[oc_клиенты_select_1]";
                if (!String.IsNullOrWhiteSpace(filter))
                {
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter("DESCR", filter)
                    };
                }
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static DataTable F1GetStuffTable(String filter = null)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[oc_сотрудники_select_1]";
                if (!String.IsNullOrWhiteSpace(filter))
                {
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter("DESCR", filter)
                    };
                }
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static DataTable F1GetAgrsTable(Dictionary<String, String> pars = null)
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
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
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
            /*
            public static String F1UpsertExcel(String cmd, Dictionary<String, String> pars = null)
            {
                String status = "ok";
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
                ResponsePackage rsm = ExecuteInXl(rqp);
                if ((rsm != null) && (rsm.Status != null))
                {
                    status = rsm.Status;
                }
                return status;
            }
            */
            public static void F1UpsertSql(String cmd, Dictionary<String, String> pars = null)
            {
                RequestPackage rqp = new RequestPackage();
                switch (cmd)
                {
                    case "Добавить":
                        rqp.Command = "[dbo].[договоры_добавить_1]";
                        break;
                    case "Обновить":
                        rqp.Command = "[dbo].[договоры_обновить_1]";
                        break;
                    default:
                        rqp.Command = cmd;
                        break;
                }
                if (pars != null)
                {
                    rqp.Parameters = new RequestParameter[pars.Count];
                    int pi = 0;
                    foreach (var p in pars)
                    {
                        rqp.Parameters[pi++] = new RequestParameter(p.Key, p.Value);
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
            /*
            public static Boolean F1RefreshXl()
            {
                Boolean result = false;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "RefreshSqlFromXl";
                ResponsePackage rsm = ExecuteInXl(rqp);
                if ((rsm != null) && (rsm.Status == "ok"))
                {
                    result = true;
                }
                return result;
            }
            */
        }
        public class Deposits
        {
            public class F0
            {
                public static DataTable GetCustTotals(String sd, String ed, String cust, String trNum)
                {
                    RequestPackage rqp = new RequestPackage();
                    rqp.Command = "[phs_oc8].[dbo].[Хозрасчетный Обороты по обеспечению Выбрать]";
                    rqp.Parameters = new RequestParameter[0];
                    if (!String.IsNullOrWhiteSpace(sd))
                    {
                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("ПериодНачало", sd);
                    }
                    if (!String.IsNullOrWhiteSpace(ed))
                    {
                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("ПериодКонец", ed);
                    }
                    if (!String.IsNullOrWhiteSpace(cust))
                    {
                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("Контрагент", cust);
                    }
                    if (!String.IsNullOrWhiteSpace(trNum))
                    {
                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("НомерТоргов", trNum);
                    }
                    DataTable dt = GetFirstTable(Execute(rqp));
                    return dt;
                }
                public static DataTable GetCustData(Int32 id)
                {
                    RequestPackage rqp = new RequestPackage();
                    rqp.Command = "[phs_oc8].[dbo].[Контрагенты Выбрать]";
                    rqp.Parameters = new RequestParameter[0];
                    if (id > 0)
                    {
                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("id", id);
                    }
                    DataTable dt = GetFirstTable(Execute(rqp));
                    return dt;
                }
                public static DataTable GetTradeTotals(Int32 id, Boolean flag11, Boolean flag12, Boolean flag21, Boolean flag22, Boolean flag31, Boolean flag32)
                {
                    RequestPackage rqp = new RequestPackage();
                    rqp.Command = "[phs_oc8].[dbo].[Хозрасчетный Обороты клиента по аукционам Выбрать]";
                    rqp.Parameters = new RequestParameter[0];
                    if (id > 0)
                    {
                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("id", id);

                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("НомерТорговЕсть", flag11);
                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("НомерТорговНет", flag12);
                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("ДолгНаКонецПериодаЕсть", flag21);
                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("ДолгНаКонецПериодаНет", flag22);
                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("ИсполнениеДоговораЕсть", flag31);
                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("ИсполнениеДоговораНет", flag32);
                    }
                    DataTable dt = GetFirstTable(Execute(rqp));
                    return dt;
                }
                public static DataTable GetEntriesData(Int32 id, String trNum)
                {
                    RequestPackage rqp = new RequestPackage();
                    rqp.Command = "[phs_oc8].[dbo].[Хозрасчетный Движения по клиенту и аукциону Выбрать]";
                    rqp.Parameters = new RequestParameter[0];
                    if (id > 0)
                    {
                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("Контрагент_id", id);
                    }
                    if (!String.IsNullOrWhiteSpace(trNum))
                    {
                        Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("НомерТоргов", trNum);
                    }
                    DataTable dt = GetFirstTable(Execute(rqp));
                    return dt;
                }
            }
        }
        public class Docs
        {
            public class Ct
            {
                public static DataTable GetFsInfo(String fileId, String link, String type)
                {
                    return GetFsInfoCommon(fileId, link, type);
                }
            }
            public class Rd
            {
                public static DataTable GetFsInfo(String fileId, String link, String type)
                {
                    return GetFsInfoCommon(fileId, link, type);
                }
            }
        }
        public class Docs1c
        {
            public static DataTable LoadDH355(Dictionary<String, String> fs)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[документы_1с_расходная_select]";
                if (fs != null)
                {
                    rqp.Parameters = new RequestParameter[fs.Count];
                    int pi = 0;
                    foreach (var f in fs)
                    {
                        String v = f.Value;
                        if (!String.IsNullOrWhiteSpace(v))
                        {
                            rqp.Parameters[pi++] = new RequestParameter(f.Key, v);
                        }
                        else // это только для заполнения rqp.Parameters
                        {
                            rqp.Parameters[pi++] = new RequestParameter(f.Key, null);
                        }
                    }
                }
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static DataTable LoadDH355Detail(Dictionary<String, String> fs)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[документы_1с_расходная_рассылка_select]";
                if (fs != null)
                {
                    rqp.Parameters = new RequestParameter[fs.Count];
                    int pi = 0;
                    foreach (var f in fs)
                    {
                        String v = f.Value;
                        if (!String.IsNullOrWhiteSpace(v))
                        {
                            rqp.Parameters[pi++] = new RequestParameter(f.Key, v);
                        }
                        else // это только для заполнения rqp.Parameters
                        {
                            rqp.Parameters[pi++] = new RequestParameter(f.Key, null);
                        }
                    }
                }
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static void Save(Dictionary<String, String> fs)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "Docs1c/F0/Save";
                if (fs != null)
                {
                    rqp.Parameters = new RequestParameter[fs.Count];
                    int pi = 0;
                    foreach (var p in fs)
                    {
                        rqp.Parameters[pi++] = new RequestParameter(p.Key, p.Value);
                    }
                }
                ResponsePackage rsm = ExecuteIn1c(rqp);
            }
        }
        public class Env
        {
            public static ResponsePackage Get(RequestPackage rqp)
            {
                ResponsePackage rsp = ExecuteInSql(rqp);
                return rsp;
            }
            public static String Set(RequestPackage rqp)
            {
                String msg = "HomeData.Env.Set: ";
                ResponsePackage rsp = ExecuteInSql(rqp);
                msg += rsp.Status;
                return msg;
            }
        }
        public class Home
        {
            public static void CreateSession(Guid sessionId, String userHostAddress)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[phs_s].[dbo].[session_insert]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter ( "id", sessionId.ToString() ),
                    new RequestParameter ( "user_host_address", userHostAddress )
                };
                ExecuteInSql(rqp);
            }
            public static void UpdateSession(
                String userToken,
                Guid sessionId,
                String cryptKey
                )
            {
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[phs_s].[dbo].[session_update]";
                DataTable dt = new DataTable();
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter ( "user_token", userToken ),
                    new RequestParameter ( "session_id", sessionId.ToString() ),
                    new RequestParameter ( "crypt_key", cryptKey )
                };
                ExecuteInSql(rqp);
            }
            public static DataTable GetSessionById(Guid id)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[session_get_by_id]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter ("id", id)
                };
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static DataTable GetClientData(String filter)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[oc_клиенты_select]";
                rqp.Parameters = new RequestParameter[] {
                new RequestParameter ( "DESCR", filter)
            };
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static DataTable GetManagerData(String filter)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[oc_сотрудники_select]";
                rqp.Parameters = new RequestParameter[] {
                new RequestParameter ("DESCR", filter)
            };
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static DataTable GetMnnItems(String[] srcs)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage
                {
                    Command = "[dbo].[мнн]",
                };
                if (srcs != null)
                {
                    rqp.Parameters = new RequestParameter[srcs.Length];
                    for (int i = 0; i < Math.Min(6, srcs.Length); i++)
                    {
                        String name = "src" + (i + 1).ToString();
                        String value = srcs[i];
                        rqp.Parameters[i] = new RequestParameter(name, value);
                    }
                }
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static DataSet GetTnItems(Guid session_id, String[] srcs, String[] terms, String mnn, Boolean exactly, Boolean eRu, Int32 pageNumber)
            {
                DataSet ds = null;
                RequestPackage rqp = new RequestPackage
                {
                    Command = "[dbo].[тн]"
                };
                Int32 pi = 0;
                if (srcs != null)
                {
                    Int32 len =
                        1 +                                                 // session_id
                        Math.Min(6, ((srcs == null) ? 0 : srcs.Length)) +   // srcs
                        Math.Min(9, ((terms == null) ? 0 : terms.Length)) + // terms
                        ((mnn == null) ? 0 : 1) +                           // mnn
                        1 +                                                 // exactly
                        1 +                                                 // eRu
                        1;                                                  // pageNumber
                    rqp.Parameters = new RequestParameter[len];
                    rqp.Parameters[pi++] = new RequestParameter("session_id", session_id);
                    for (int i = 0; i < Math.Min(6, srcs.Length); i++)
                    {
                        String name = "src" + (i + 1).ToString();
                        String value = srcs[i];
                        rqp.Parameters[pi++] = new RequestParameter(name, value);
                    }
                }
                if (terms != null)
                {
                    for (int i = 0; i < Math.Min(9, terms.Length); i++)
                    {
                        String name = "term" + (i + 1).ToString();
                        String value = terms[i];
                        rqp.Parameters[pi++] = new RequestParameter(name, value);
                    }
                }
                if (mnn != null)
                {
                    rqp.Parameters[pi++] = new RequestParameter("mnn", mnn);
                }
                rqp.Parameters[pi++] = new RequestParameter("exactly", exactly);
                rqp.Parameters[pi++] = new RequestParameter("eRu", eRu);
                rqp.Parameters[pi++] = new RequestParameter("PageNumber", pageNumber);
                ds = Execute(rqp);
                return ds;
            }
            public static DataSet GetTnItemsLP(String rn)
            {
                DataSet ds = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[тн_цены]";
                rqp.Parameters = new RequestParameter[1];
                rqp.Parameters[0] = new RequestParameter("rn", rn);
                ds = Execute(rqp);
                return ds;
            }
        }
        public class Items
        {
            public static DataTable LoadGroups(RequestPackage rqp)
            {
                DataTable dt = null;
                // меняем только команду
                rqp.Command = "[Goods].[dbo].[item_groups_get]";
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static DataTable LoadItems(RequestPackage rqp)
            {
                DataTable dt = null;
                // меняем только команду
                rqp.Command = "[Goods].[dbo].[items_get]";
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static DataTable UpsertGroup(RequestPackage rqp)
            {
                DataTable dt = null;
                /*
                switch (rqp.Command)
                {
                    case "Items.Groups.Index.InsertGroup":
                        // меняем только команду
                        rqp.Command = "[Pharm-Sib].[dbo].[item_groups_insert]";
                        dt = GetFirstTable(Execute(rqp));
                        break;
                    default:
                        break;
                }
                */
                return dt;
            }
            public static DataTable SelectGroup(RequestPackage rqp)
            {
                DataTable dt = null;
                switch (rqp.Command)
                {
                    case "Items.Groups.GroupPart.SelectGroup":
                        // меняем только команду
                        rqp.Command = "[Goods].[dbo].[item_groups_get_items_in_group]";
                        dt = GetFirstTable(Execute(rqp));
                        break;
                    default:
                        break;
                }
                return dt;
            }
            public static void AddItemIntoGroup(Guid sessionId, Guid groupId, Int32 srcId, Int32 itemId)
            {
                /*
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "[Pharm-Sib].[dbo].[item_groups_insert_item]";
                rqp.Parameters = new RequestParameter[] { 
                    new RequestParameter { Name = "session_id", Value = sessionId },
                    new RequestParameter { Name = "group_id", Value = groupId },
                    new RequestParameter { Name = "src_id", Value = srcId },
                    new RequestParameter { Name = "item_id", Value = itemId }
                };
                ResponsePackage rsp = ExecuteInSql(rqp);
                */
            }
            public static void RemoveItemFromGroup(Guid sessionId, Guid groupId, Int32 srcId, Int32 itemId)
            {
                /*
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "[Pharm-Sib].[dbo].[item_groups_remove_item]";
                rqp.Parameters = new RequestParameter[] { 
                    new RequestParameter { Name = "session_id", Value = sessionId },
                    new RequestParameter { Name = "group_id", Value = groupId },
                    new RequestParameter { Name = "src_id", Value = srcId },
                    new RequestParameter { Name = "item_id", Value = itemId }
                };
                ResponsePackage rsp = ExecuteInSql(rqp);
                */
            }
        }
        public class Reports
        {

            public static DataTable GetReport1(
                String clientFilter = null,
                String clientCode = null,
                String managerFilter = null,
                String managerCode = null,
                String managerMultiCode = null,
                Int32? pr1 = null, Int32? pr2 = null,
                DateTime? onDate = null,
                Boolean sud = true,
                Boolean ord = true,
                Boolean sec = false
                )
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[Отчёт по неоплаченным отгрузкам]";
                rqp.Parameters = new RequestParameter[] {
                new RequestParameter("client_filter", clientFilter),
                new RequestParameter("client_code", clientCode),
                new RequestParameter("manager_filter", managerFilter),
                new RequestParameter("manager_code", managerCode),
                new RequestParameter("emploee_multi_code", managerMultiCode),
                new RequestParameter("pr1", pr1),
                new RequestParameter("pr2", pr2),
                new RequestParameter("date", onDate),
                new RequestParameter("sud", sud),
                new RequestParameter("ord", ord),
                new RequestParameter("sec", sec)
            };
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }

            public static DataTable GetReport2(
                String emploeeFilter = null,
                String emploeeCode = null,
                String emploeeMultiCode = null,
                Boolean marketer = true,
                Boolean manager = true,
                DateTime? beginDate = null,
                DateTime? endDate = null,
                Boolean sud = true,
                Boolean ord = true
                )
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[ОтчетПоОплатам]";
                rqp.Parameters = new RequestParameter[] {
                new RequestParameter("НачПериода", beginDate),
                new RequestParameter("КонПериода", endDate),
                new RequestParameter("emploee_filter", emploeeFilter),
                new RequestParameter("emploee_code", emploeeCode),
                new RequestParameter("emploee_multi_code", emploeeMultiCode),
                new RequestParameter("marketer", marketer),
                new RequestParameter("manager", manager),
                new RequestParameter("sud", sud),
                new RequestParameter("ord", ord)
            };
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static void UpsertSud(
                String iddoc,
                Int32 status_id,
                Int32 stage_id,
                Int32 set_of_docs_id,
                Int32 payments_status_id,
                String payment_date,
                String note,
                String sessionId)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[судебные_документы_upsert]";
                rqp.Parameters = new RequestParameter[] {
                new RequestParameter("iddoc", iddoc),
                new RequestParameter("status_id", status_id),
                new RequestParameter("stage_id", stage_id),
                new RequestParameter("set_of_docs_id", set_of_docs_id),
                new RequestParameter("payments_status_id", payments_status_id),
                new RequestParameter("payment_date", payment_date),
                new RequestParameter("note", note),
                new RequestParameter("session_id", sessionId)
            };
                Execute(rqp);
            }

            public static DataTable GetR3DataList(DateTime? d1 = null, DateTime? d2 = null)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[судебные_документы_отчёт]";
                rqp.Parameters = new RequestParameter[] {
                new RequestParameter("d1", d1),
                new RequestParameter("d2", d2)
            };
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }

            public static DataTable GetR3HistoryList(String iddoc)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[судебные_документы_отчёт_по_истории_документа]";
                rqp.Parameters = new RequestParameter[] {
                new RequestParameter("iddoc", iddoc)
            };
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }

            public static DataTable GetR3CrossTable()
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[судебные_документы_отчёт_по_истории]";
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
        }
        public class Spec // common for Order and Prep
        {
            public static DataSet GetById(Guid sessionId, Int32 specId)
            {
                DataSet ds = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[спецификации_get_by_id]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter("id", specId ),
                    new RequestParameter("session_id", sessionId )
                };
                ds = Execute(rqp);
                return ds;
            }
            public static Int32 CreateOrderSpecFromPrepSpec(Guid sessionId, String auctionNumber)
            {
                Int32 id = -1;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[спецификации_create_order_from_prep]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter { Name = "session_id", Value = sessionId },
                    new RequestParameter { Name = "auction_number", Value = auctionNumber }
                };
                try
                {
                    id = (Int32)(GetFirstTable(Execute(rqp)).Rows[0][0]);
                }
                catch (Exception) { }
                return id;
            }
            public class Head
            {
                public static DataTable Get(RequestPackage rqp)
                {
                    DataTable dt = null;
                    Int32.TryParse(rqp["id"] as String, out Int32 id);
                    if (id != 0)
                    {
                        rqp.Command = "[dbo].[спецификации_шапка_get]";
                        AddSessionIdToParameters(rqp);
                        dt = GetFirstTable(Execute(rqp));
                    }
                    return dt;
                }
                public static DataTable Update(RequestPackage rqp)
                {
                    DataTable dt = null;
                    Int32.TryParse(rqp["id"] as String, out Int32 id);
                    if (id != 0)
                    {
                        rqp.Command = "[dbo].[спецификации_шапка_update_from_not_null_values]";
                        AddSessionIdToParameters(rqp);
                        dt = GetFirstTable(Execute(rqp));
                    }
                    return dt;
                }
                public static DataTable GetHeadAndAuction(Int32 id)
                {
                    DataTable dt = null;
                    RequestPackage rqp = new RequestPackage();
                    rqp.Command = "[dbo].[спецификации_шапка_и_аукцион_get_by_id]";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter("id", id )
                    };
                    dt = GetFirstTable(Execute(rqp));
                    return dt;
                }
                public static Int32 Insert(DataRow dr, Guid sessionId)
                {
                    Int32 id = -1;
                    if (dr != null)
                    {
                        DataTable dt = dr.Table;
                        RequestPackage rqp = new RequestPackage();
                        rqp.Command = "[dbo].[спецификации_шапка_добавить]";
                        rqp.Parameters = new RequestParameter[dt.Columns.Count + 1];
                        for (int ci = 0; ci < dt.Columns.Count; ci++)
                        {
                            rqp.Parameters[ci] = new RequestParameter(dt.Columns[ci].ColumnName, dt.Rows[0][ci]);
                        }
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("session_id", sessionId);
                        try
                        {
                            id = (Int32)(GetFirstTable(Execute(rqp)).Rows[0][0]);
                        }
                        catch (Exception) { }
                    }
                    return id;
                }
                public static DataTable Save(DataTable dt, Guid sessionId)
                {
                    DataTable result = null;
                    if ((dt != null) && (dt.Rows.Count > 0))
                    {
                        RequestPackage rqp = new RequestPackage();
                        rqp.Command = "[dbo].[спецификации_шапка_save]";
                        rqp.Parameters = new RequestParameter[dt.Columns.Count + 1];
                        for (int ci = 0; ci < dt.Columns.Count; ci++)
                        {
                            DataColumn col = dt.Columns[ci];
                            rqp.Parameters[ci] = new RequestParameter(col.ColumnName, dt.Rows[0][col]);
                        }
                        rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter("session_id", sessionId);
                        result = GetFirstTable(Execute(rqp));
                    }
                    return result;
                }
                public static DataTable Delete(Guid sessionId, Int32 specId)
                {
                    DataTable dt = null;
                    RequestPackage rqp = new RequestPackage();
                    rqp.SessionId = sessionId;
                    rqp.Command = "[dbo].[спецификации_шапка_delete]";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter("id", specId )
                    };
                    dt = GetFirstTable(Execute(rqp));
                    return dt;
                }
                public static String CalcOutgoingPrices(Guid sessionId, Int32 specId)
                {
                    String status = "Start MvcApplication2.Data.HomeData.Spec.Head.CalcOutgoingPrices().";
                    RequestPackage rqp = new RequestPackage();
                    rqp.SessionId = sessionId;
                    rqp.Command = "[dbo].[спецификации_расчёт_цен_продажи]";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter { Name = "session_id", Value = sessionId },
                        new RequestParameter { Name = "spec_id", Value = specId }
                    };
                    ResponsePackage rsp = ExecuteInSql(rqp);
                    status = rsp.Status;
                    return status;
                }
            }
            public class Table
            {
                public static DataSet InsertRow(DataRow dr)
                {
                    DataSet ds = null;
                    RequestPackage rqp = new RequestPackage();
                    rqp.Command = "[dbo].[спецификации_таблица_insert]";
                    rqp.Parameters = new RequestParameter[dr.Table.Columns.Count];
                    int pi = 0;
                    for (int ci = 0; ci < dr.Table.Columns.Count; ci++)
                    {
                        rqp.Parameters[pi++] = new RequestParameter(dr.Table.Columns[ci].ColumnName, dr[ci]);
                    }
                    Execute(rqp);
                    return ds;
                }
                public static DataSet DeleteRow(DataRow dr)
                {
                    DataSet ds = null;
                    RequestPackage rqp = new RequestPackage();
                    rqp.Command = "[dbo].[спецификации_таблица_delete]";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter("uid", dr["uid"] )
                    };
                    Execute(rqp);
                    return ds;
                }
            }
        }
        public class Order
        {
            private static String normString(String s)
            {
                String r = s;
                if (!String.IsNullOrWhiteSpace(r))
                {
                    r = r.Replace("&nbsp;", " ").Replace("&laquo;", "«").Replace("&raquo;", "»").Replace("&#034;", "\"").Replace("&#34;", "\"");
                    r = r.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ");
                    while (r.Contains("  ")) r = r.Replace("  ", " ");
                    r = r.Trim();
                }
                return r;
            }
            public static DataTable F1GetGoodsItems(String[] srcs, String mnn, String value)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[товары]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter("src1", srcs[0] ),
                    new RequestParameter("src2", srcs[1] ),
                    new RequestParameter("src3", srcs[2] ),
                    new RequestParameter("src4", srcs[3] ),
                    new RequestParameter("src5", srcs[4] ),
                    new RequestParameter("mnn", mnn ),
                    new RequestParameter("descr", value )
                };
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static DataSet F1SaveShedule(Int32 id, DataTable shedule)
            {
                DataSet ds = null;

                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[спецификации_к_контракту_график_delete]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter("код_спецификации", id )
                };
                Execute(rqp);

                rqp.Command = "[dbo].[спецификации_к_контракту_график_insert]";
                for (int ri = 0; ri < shedule.Rows.Count; ri++)
                {
                    DataRow dr = shedule.Rows[ri];
                    rqp.Parameters = new RequestParameter[shedule.Columns.Count];
                    int pi = 0;
                    for (int ci = 0; ci < shedule.Columns.Count; ci++)
                    {
                        rqp.Parameters[pi++] = new RequestParameter(shedule.Columns[ci].ColumnName, dr[ci]);
                    }
                    Execute(rqp);
                }

                return ds;
            }
            public static DataSet GetXlsTables(Stream fileStream)
            {
                DataSet ds = null;
                ResponsePackage rsp = new ResponsePackage();
                rsp.Status = "Start reading 'xls' tables.";
                try
                {
                    Int32 len = (Int32)fileStream.Length;
                    Byte[] bytes = new Byte[len];
                    fileStream.Read(bytes, 0, len);
                    RequestPackage rqp = new RequestPackage();
                    rqp.Command = "Prep.F4.GetXlsTables";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter("fileStream", bytes )
                    };
                    rsp = ExecuteInXl(rqp);
                    ds = rsp.Data;
                }
                catch (Exception e) { rsp.Status = e.ToString(); }
                if (ds != null) { rsp.Status = "Ok"; }
                return ds;
            }
            public static DataSet GetDocTables(Stream fileStream)
            {
                DataSet ds = null;
                ResponsePackage rsp = new ResponsePackage();
                rsp.Status = "Start reading 'doc' tables.";
                try
                {
                    Int32 len = (Int32)fileStream.Length;
                    Byte[] bytes = new Byte[len];
                    fileStream.Read(bytes, 0, len);
                    RequestPackage rqp = new RequestPackage();
                    rqp.Command = "Prep.F4.GetDocTables";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter("fileStream", bytes )
                    };
                    rsp = ExecuteInXl(rqp);
                    ds = rsp.Data;
                }
                catch (Exception e) { rsp.Status = e.ToString(); }
                if (ds != null) { rsp.Status = "Ok"; }
                return ds;
            }
            public static ResponsePackage SendEmail(RequestPackage rqp)
            {
                ResponsePackage rsp = ExecuteInMail(rqp);
                return rsp;
            }
            public static ResponsePackage LoadS(RequestPackage rqp)
            {
                ResponsePackage rsp = ExecuteInSql(rqp);
                return rsp;
            }
            public static Boolean ExistsAuctionInf(Guid sessionId, String auctionNumber)
            {
                Boolean r = false;
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "[Auctions].[dbo].[exists_auction_inf]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter { Name = "session_id", Value = sessionId },
                    new RequestParameter { Name = "auction_number", Value = auctionNumber }
                };
                Object o = GetScalar(ExecuteSssp(rqp).Data);
                if (o.GetType() == typeof(Boolean))
                {
                    r = (Boolean)o;
                }
                return r;
            }
            public static DataSet LoadSpec(Guid sessionId, Int32 specId, Int32 specRowId)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "dbo.[спецификации_get_by_id]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter("session_id", sessionId ),
                    new RequestParameter("id", specId ),
                    new RequestParameter("row_id", specRowId )
                };
                DataSet ds = Execute(rqp);
                return ds;
            }
            public static Guid SaveCommonInf(Guid sessionId, Object[][] md)
            {
                Guid oUid = new Guid();
                String eMsg = null;
                try
                {
                    RequestPackage rqp = new RequestPackage();
                    rqp.SessionId = sessionId;
                    rqp.Command = "[Auctions].[dbo].[save_auction_inf]";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter { Name = "session_id", Value = sessionId }
                    };
                    foreach (Object[] nx in md)
                    {
                        String name = (String)nx[0];
                        XmlNode node = (XmlNode)nx[1];
                        if (node != null)
                        {
                            node = node.SelectSingleNode((String)nx[2]);
                            if (node != null)
                            {
                                String value = normString(node.InnerText);
                                if (name == "номер") value = (new Regex(@"\d{19}")).Match(value).Value;
                                if (name == "кооператив") value = "1"; // иначе сюда вообще не дойдёт и value окажется равным 0 по умолчанию
                                if (name == "дата_размещения") value = value.Replace("Размещено: ", String.Empty);
                                Int32 size = rqp.Parameters.Length;
                                Array.Resize<RequestParameter>(ref rqp.Parameters, size + 1);
                                rqp.Parameters[size] = new RequestParameter { Name = name, Value = value };
                            }
                        }
                    }
                    Object o = GetScalar(Execute(rqp));
                    if (o != null && o.GetType() == typeof(Guid))
                    {
                        oUid = (Guid)o;
                    }
                }
                catch (Exception e) { eMsg = e.Message; }
                return oUid;
            }
            public static String SaveCustomerRequirement(Guid sessionId, Guid oUid, Object[][] md)
            {
                String eMsg = null;
                try
                {
                    RequestPackage rqp = new RequestPackage();
                    rqp.SessionId = sessionId;
                    rqp.Command = "[Auctions].[dbo].[save_customer_requirement]";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter { Name = "session_id", Value = sessionId },
                        new RequestParameter { Name = "закупка_uid", Value = oUid }
                    };
                    foreach (Object[] nx in md)
                    {
                        String name = (String)nx[0];
                        XmlNode node = (XmlNode)nx[1];
                        if (node != null)
                        {
                            node = node.SelectSingleNode((String)nx[2]);
                            if (node != null)
                            {
                                String value = normString(node.InnerText);
                                if (name == "наименование_заказчика") value = value.Replace("Требования заказчика ", String.Empty);
                                Int32 size = rqp.Parameters.Length;
                                Array.Resize<RequestParameter>(ref rqp.Parameters, size + 1);
                                rqp.Parameters[size] = new RequestParameter { Name = name, Value = value };
                            }
                        }
                    }
                    ExecuteInSql(rqp);
                }
                catch (Exception e) { eMsg = e.Message; }
                return eMsg;
            }
            public static void AddSpec(Guid sessionId, Guid oUid, Int32 headType)
            {
                String eMsg = null;
                try
                {
                    RequestPackage rqp = new RequestPackage();
                    rqp.SessionId = sessionId;
                    rqp.Command = "[Pharm-Sib].[dbo].[спецификации_шапка_добавить]";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter { Name = "session_id", Value = sessionId },
                        new RequestParameter { Name = "закупка_uid", Value = oUid },
                        new RequestParameter { Name = "type", Value = headType }
                    };
                    ExecuteInSql(rqp);
                }
                catch (Exception e) { eMsg = e.Message; }

            }
            public static void AddContractDirectory(Guid sessionId, Guid oUid)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "Order.AddContractDirectory";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter { Name = "auction_uid", Value = oUid }
                };
                ExecuteSssp(rqp);
            }
            public static String PassToTender(Guid sessionId, String auctionNumber)
            {
                String r = "MvcApplication2.Data.HomeData.Order.PassToTender()";
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "Order.PassToTender";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter { Name = "auction_number", Value = auctionNumber }
                };
                ResponsePackage rsp = ExecuteSssp(rqp);
                if (rsp != null)
                {
                    r = rsp.Status;
                }
                return r;
            }
            public static Int32 CreateSpec(Guid sessionId)
            {
                Int32 specId = 0;
                String status = String.Format("Запись создана вручную {0:yyyy-MM-dd HH:mm}.", DateTime.Now);
                RequestPackage rqp = new RequestPackage()
                {
                    SessionId = sessionId,
                    Command = "[Pharm-Sib].[dbo].[спецификации_шапка_добавить]",
                    Parameters = new RequestParameter[] {
                        new RequestParameter { Name = "session_id", Value = sessionId },
                        new RequestParameter { Name = "type", Value = 2 },
                        new RequestParameter { Name = "статус_аукциона", Value = status }
                    }
                };
                try { specId = (Int32)GetScalar(Execute(rqp)); } catch (Exception) { }
                return specId;
            }
            public class SpecTable
            {
                public static void InsertRow(DataRow dr)
                {
                    RequestPackage rqp = new RequestPackage();
                    rqp.Command = "[dbo].[спецификации_таблица_insert]";
                    rqp.Parameters = new RequestParameter[dr.Table.Columns.Count];
                    int pi = 0;
                    foreach (DataColumn col in dr.Table.Columns)
                    {
                        if (!col.ColumnName.StartsWith("xxx"))
                        {
                            rqp.Parameters[pi++] = new RequestParameter(col.ColumnName, dr[col]);
                        }
                    }
                    Array.Resize<RequestParameter>(ref rqp.Parameters, pi);
                    Execute(rqp);
                }
            }
        }
        public class Fs
        {
            public static DataSet GetDirectoryInfo(Guid sessionId, String alias, String path)
            {
                DataSet ds = null;
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "GetDirectoryInfo";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter("alias", alias),
                    new RequestParameter("path", path)
                };
                ResponsePackage rsp = ExecuteInFs(rqp);
                ds = rsp.Data;
                return ds;
            }
            public static Byte[] GetFileContents(Guid sessionId, String alias = null, String path = null)
            {
                Byte[] contents = null;
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "GetFileContents";
                rqp.Parameters = new RequestParameter[2];
                rqp.Parameters[0] = new RequestParameter("alias", alias);
                rqp.Parameters[1] = new RequestParameter("path", path);
                ResponsePackage rsp = ExecuteInFs(rqp);
                if (rsp.Data != null)
                {
                    if (rsp.Data.Tables.Count > 0)
                    {
                        DataTable dt = rsp.Data.Tables[0];
                        if (dt.Columns.Count > 0)
                        {
                            if ((dt.Columns[0].ColumnName == "contents") && (dt.Columns[0].DataType == typeof(String)))
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    String base64String = dt.Rows[0][0] as String;
                                    if (!String.IsNullOrWhiteSpace(base64String))
                                    {
                                        contents = System.Convert.FromBase64String(base64String);
                                    }
                                }
                            }
                        }
                    }
                }
                return contents;
            }
            public static void AddDirectory(Guid sessionId, String alias, String path)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "AddDirectory";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter { Name = "alias", Value = alias },
                    new RequestParameter { Name = "path", Value = path }
                };
                ResponsePackage rsp = ExecuteInFs(rqp);
            }
        }
        public class Dgv
        {
            public static Int32 Add(String fileId)
            {
                Int32 id = 0;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[file_info_add]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter("file_id", fileId )
                };
                DataSet ds = Execute(rqp);
                DataTable dt = GetFirstTable(ds);
                if ((dt != null) && (dt.Rows.Count > 0))
                {
                    Object r0c0 = dt.Rows[0][0];
                    if (r0c0.GetType() == typeof(Int32))
                    {
                        id = (Int32)r0c0;
                    }
                }
                return id;
            }
            public static void ChangeName(Int32 id, String value)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[file_nvp_name_change]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter("id", id ),
                    new RequestParameter("value", value )
                };
                Execute(rqp);
            }
            public static void ChangeValue(Int32 id, String value)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[file_nvp_value_change]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter("id", id ),
                    new RequestParameter("value", value )
                };
                Execute(rqp);
            }
            public static void Delete(Int32 id)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[file_info_delete]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter("id", id )
                };
                DataSet ds = Execute(rqp);
            }
        }
        public class Mail
        {
            public static DataTable GetSudAndEmail(String iddoc)
            {
                DataTable dt = null;
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "[dbo].[судебный_статус_и_email_by_iddoc]";
                rqp.Parameters = new RequestParameter[] {
                        new RequestParameter("iddoc", iddoc )
                    };
                dt = GetFirstTable(Execute(rqp));
                return dt;
            }
            public static void Send(String msg, String address)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "";
                rqp.Parameters = new RequestParameter[] {
                        new RequestParameter("msg", msg ),
                        new RequestParameter("address", address )
                };
                ExecuteInMail(rqp);
            }
        }
        public class Log
        {
            public static void WriteToConsole(String sessionId, String msg)
            {
                Guid sId;
                Guid.TryParse(sessionId, out sId);
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sId;
                rqp.Command = "WriteToConsole";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter { Name = "msg", Value = msg }
                };
                ExecuteSssp(rqp);
            }
        }
        public class Settings
        {
            public static DataSet Get(Guid sessionId)
            {
                DataSet ds = null;
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "[dbo].[settings_get]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter("session_id", sessionId)
                };
                ds = Execute(rqp);
                return ds;
            }
            public static void UpdateRow(Guid sessionId, Int32 id, String value)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "[dbo].[settings_update_row]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter("session_id", sessionId),
                    new RequestParameter("id", id),
                    new RequestParameter("value", value)
                };
                Execute(rqp);
            }
        }
        public class Prep
        {
            private static String NormString(String s)
            {
                String r = s;
                if (!String.IsNullOrWhiteSpace(r))
                {
                    r = r.Replace("&nbsp;", " ").Replace("&laquo;", "«").Replace("&raquo;", "»").Replace("&#034;", "\"").Replace("&#34;", "\"");
                    r = r.Replace("\t", " ").Replace("\n", " ").Replace("\r", " ");
                    while (r.Contains("  ")) r = r.Replace("  ", " ");
                    r = r.Trim();
                }
                return r;
            }
            public static DataSet GetXlsTables(Stream fileStream)
            {
                DataSet ds = null;
                ResponsePackage rsp = new ResponsePackage();
                rsp.Status = "Start reading 'xls' tables.";
                try
                {
                    Int32 len = (Int32)fileStream.Length;
                    Byte[] bytes = new Byte[len];
                    fileStream.Read(bytes, 0, len);
                    RequestPackage rqp = new RequestPackage();
                    rqp.Command = "Prep.F4.GetXlsTables";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter("fileStream", bytes )
                    };
                    rsp = ExecuteInXl(rqp);
                    ds = rsp.Data;
                }
                catch (Exception e) { rsp.Status = e.ToString(); }
                if (ds != null) { rsp.Status = "Ok"; }
                return ds;
            }
            public static DataSet GetDocTables(Stream fileStream)
            {
                DataSet ds = null;
                ResponsePackage rsp = new ResponsePackage();
                rsp.Status = "Start reading 'doc' tables.";
                try
                {
                    Int32 len = (Int32)fileStream.Length;
                    Byte[] bytes = new Byte[len];
                    fileStream.Read(bytes, 0, len);
                    RequestPackage rqp = new RequestPackage();
                    rqp.Command = "Prep.F4.GetDocTables";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter("fileStream", bytes )
                    };
                    rsp = ExecuteInXl(rqp);
                    ds = rsp.Data;
                }
                catch (Exception e) { rsp.Status = e.ToString(); }
                if (ds != null) { rsp.Status = "Ok"; }
                return ds;
            }
            public static ResponsePackage SendEmail(RequestPackage rqp)
            {
                ResponsePackage rsp = ExecuteInMail(rqp);
                return rsp;
            }
            public static ResponsePackage LoadS(RequestPackage rqp)
            {
                ResponsePackage rsp = ExecuteInSql(rqp);
                return rsp;
            }
            public static DataSet LoadSpec(Guid sessionId, Int32 specId, Int32 specRowId)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.Command = "dbo.[спецификации_get_by_id]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter("session_id", sessionId ),
                    new RequestParameter("id", specId ),
                    new RequestParameter("row_id", specRowId )
                };
                DataSet ds = Execute(rqp);
                return ds;
            }
            public static Guid SaveCommonInf(Guid sessionId, Object[][] md)
            {
                Guid oUid = new Guid();
                String eMsg = null;
                try
                {
                    RequestPackage rqp = new RequestPackage();
                    rqp.SessionId = sessionId;
                    rqp.Command = "[Auctions].[dbo].[save_auction_inf]";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter { Name = "session_id", Value = sessionId }
                    };
                    foreach (Object[] nx in md)
                    {
                        String name = (String)nx[0];
                        XmlNode node = (XmlNode)nx[1];
                        if (node != null)
                        {
                            node = node.SelectSingleNode((String)nx[2]);
                            if (node != null)
                            {
                                String value = NormString(node.InnerText);
                                if (name == "номер") value = (new Regex(@"\d{19}")).Match(value).Value;
                                if (name == "кооператив") value = "1"; // иначе сюда вообще не дойдёт и value окажется равным 0 по умолчанию
                                if (name == "дата_размещения") value = value.Replace("Размещено: ", String.Empty);
                                Int32 size = rqp.Parameters.Length;
                                Array.Resize<RequestParameter>(ref rqp.Parameters, size + 1);
                                rqp.Parameters[size] = new RequestParameter { Name = name, Value = value };
                            }
                        }
                    }
                    Object o = GetScalar(Execute(rqp));
                    if (o != null && o.GetType() == typeof(Guid))
                    {
                        oUid = (Guid)o;
                    }
                }
                catch (Exception e) { eMsg = e.Message; }
                return oUid;
            }
            public static String SaveCustomerRequirement(Guid sessionId, Guid oUid, Object[][] md)
            {
                String eMsg = null;
                try
                {
                    RequestPackage rqp = new RequestPackage();
                    rqp.SessionId = sessionId;
                    rqp.Command = "[Auctions].[dbo].[save_customer_requirement]";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter { Name = "session_id", Value = sessionId },
                        new RequestParameter { Name = "закупка_uid", Value = oUid }
                    };
                    foreach (Object[] nx in md)
                    {
                        String name = (String)nx[0];
                        XmlNode node = (XmlNode)nx[1];
                        if (node != null)
                        {
                            node = node.SelectSingleNode((String)nx[2]);
                            if (node != null)
                            {
                                String value = NormString(node.InnerText);
                                if (name == "наименование_заказчика") value = value.Replace("Требования заказчика ", String.Empty);
                                Int32 size = rqp.Parameters.Length;
                                Array.Resize<RequestParameter>(ref rqp.Parameters, size + 1);
                                rqp.Parameters[size] = new RequestParameter { Name = name, Value = value };
                            }
                        }
                    }
                    ExecuteInSql(rqp);
                }
                catch (Exception e) { eMsg = e.Message; }
                return eMsg;
            }
            public static Boolean ExistsAuctionInf(Guid sessionId, String auctionNumber)
            {
                Boolean r = false;
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "[Auctions].[dbo].[exists_auction_inf]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter { Name = "session_id", Value = sessionId },
                    new RequestParameter { Name = "auction_number", Value = auctionNumber }
                };
                Object o = GetScalar(ExecuteSssp(rqp).Data);
                if (o.GetType() == typeof(Boolean))
                {
                    r = (Boolean)o;
                }
                return r;
            }
            public static void AddSpec(Guid sessionId, Guid oUid, Int32 headType)
            {
                String eMsg = null;
                try
                {
                    RequestPackage rqp = new RequestPackage();
                    rqp.SessionId = sessionId;
                    rqp.Command = "[Pharm-Sib].[dbo].[спецификации_шапка_добавить]";
                    rqp.Parameters = new RequestParameter[] {
                        new RequestParameter { Name = "session_id", Value = sessionId },
                        new RequestParameter { Name = "закупка_uid", Value = oUid },
                        new RequestParameter { Name = "type", Value = headType }
                    };
                    ExecuteInSql(rqp);
                }
                catch (Exception e) { eMsg = e.Message; }

            }
            public static void AddContractDirectory(Guid sessionId, Guid oUid)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "Prep.AddContractDirectory";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter { Name = "auction_uid", Value = oUid }
                };
                ExecuteSssp(rqp);
            }
            public static String PassToTender(Guid sessionId, String auctionNumber)
            {
                String r = "MvcApplication2.Data.HomeData.Prep.PassToTender()";
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "Prep.PassToTender";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter { Name = "auction_number", Value = auctionNumber }
                };
                ResponsePackage rsp = ExecuteSssp(rqp);
                if (rsp != null)
                {
                    r = rsp.Status;
                }
                return r;
            }
        }
        public class Tn
        {
            public static String GetRegCopyUri(Int32 copyCode)
            {
                String uri = "{q}/" + copyCode.ToString();
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = new Guid();
                rqp.Command = "[Grls].[dbo].[рз_получить_сслылку_на_копию_ру]";
                rqp.Parameters = new RequestParameter[] { new RequestParameter("id", copyCode) };
                ResponsePackage rsp = ExecuteInSql(rqp);
                uri = GetScalar(rsp.Data) as String;
                return uri;
            }
            public static String[] GetInstructionsLinks(Guid id)
            {
                String[] iLinks = new String[] { null, null };
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = new Guid();
                rqp.Command = "[Grls].[dbo].[рз_получить_сслылки_на_инструкции]";
                rqp.Parameters = new RequestParameter[] { new RequestParameter("Код_в_источнике", id) };
                ResponsePackage rsp = ExecuteInSql(rqp);
                if (rsp != null)
                {
                    DataTable dt = GetFirstTable(rsp.Data);
                    if (dt != null && dt.Rows.Count == 1 && dt.Columns.Count == 2)
                    {
                        iLinks[0] = dt.Rows[0][0] as String;
                        iLinks[1] = dt.Rows[0][1] as String;
                    }
                }
                return iLinks;
            }
            public static DataSet Get1cData(Guid sessionId, String rCode, String fCode, String uCode)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "[Pharm-Sib].[dbo].[item_get_1c_data]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter { Name = "session_id", Value = sessionId },
                    new RequestParameter { Name = "r_code", Value = rCode },
                    new RequestParameter { Name = "f_code", Value = fCode },
                    new RequestParameter { Name = "u_code", Value = uCode }
                };
                DataSet ds = Execute(rqp);
                return ds;
            }
            public static DataSet GetUData(Guid sessionId, String fCode)
            {
                RequestPackage rqp = new RequestPackage();
                rqp.SessionId = sessionId;
                rqp.Command = "[Pharm-Sib].[dbo].[item_get_u_data]";
                rqp.Parameters = new RequestParameter[] {
                    new RequestParameter { Name = "session_id", Value = sessionId },
                    new RequestParameter { Name = "f_code", Value = fCode }
                };
                DataSet ds = Execute(rqp);
                return ds;
            }
        }

        private static void AddSessionIdToParameters(RequestPackage rqp)
        {
            if (rqp == null) { throw new ArgumentNullException(); }

            if (rqp.Parameters == null)
            {
                rqp.Parameters = new RequestParameter[] {
                            new RequestParameter { Name = "session_id", Value = rqp.SessionId }
                        };
            }
            else
            {
                Boolean existsSessionId = false;
                foreach (RequestParameter p in rqp.Parameters)
                {
                    if (p.Name == "session_id")
                    {
                        existsSessionId = true;
                        break;
                    }
                }
                if (!existsSessionId)
                {
                    Array.Resize<RequestParameter>(ref rqp.Parameters, rqp.Parameters.Length + 1);
                    rqp.Parameters[rqp.Parameters.Length - 1] = new RequestParameter { Name = "session_id", Value = rqp.SessionId };
                }
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
