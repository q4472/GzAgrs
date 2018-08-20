using Agrs.Data;
using Nskd;
using System;
using System.Data;

namespace Agrs.Models
{
    public class F1Model
    {
        public DataTable NetSqlГарзаДоговоры { get; private set; }
        public F1Model()
        {
            // заход при первом обращении (без фильтра)
            NetSqlГарзаДоговоры = F1HomeData.F1GetДоговоры();
        }
        public F1Model(RequestPackage rqp)
        {
            // заход с фильтром для "filtered_view"
            NetSqlГарзаДоговоры = F1HomeData.F1GetДоговоры(rqp);
        }
        public F1Model(String f0)
        {
            // заход с 'id' для 'detail'
            NetSqlГарзаДоговоры = F1HomeData.F1GetДоговоры(f0);
            if (NetSqlГарзаДоговоры != null && NetSqlГарзаДоговоры.Rows.Count > 0)
            {
                NetSqlГарзаДоговоры.Columns.Add("ДатаОкончания", typeof(DateTime));
                NetSqlГарзаДоговоры.Columns.Add("Пролонгация", typeof(String));
                NetSqlГарзаДоговоры.Columns.Add("ОтсрочкаПлатежа", typeof(String));
                var code = NetSqlГарзаДоговоры.Rows[0]["f14"];
                if (code != DBNull.Value)
                {
                    ДозагрузитьДанныеИз1сГарза(code.ToString());
                }
            }
        }
        private void ДозагрузитьДанныеИз1сГарза(String code)
        {
            try
            {
                if (NetSqlГарзаДоговоры != null && NetSqlГарзаДоговоры.Rows.Count > 0)
                {
                    DataTable dt = F1HomeData.F1GetAgrByCode(code);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        NetSqlГарзаДоговоры.Rows[0]["ДатаОкончания"] = dt.Rows[0]["ДатаОкончания"];
                        NetSqlГарзаДоговоры.Rows[0]["Пролонгация"] = dt.Rows[0]["Пролонгация"];
                        NetSqlГарзаДоговоры.Rows[0]["ОтсрочкаПлатежа"] = dt.Rows[0]["ОтсрочкаПлатежа"];
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
        public static DataTable GetDataForSelectorWithListBox(RequestPackage rqp)
        {
            DataTable dt = null;
            var tableName = rqp["tableName"] as String;
            var filter = rqp["filter"] as String;
            switch (tableName)
            {
                case "Клиенты":
                case "Представители":
                    dt = F1HomeData.F1GetCustTable(filter);
                    break;
                case "Сотрудники":
                    dt = F1HomeData.F1GetStuffTable(filter);
                    break;
                default:
                    break;
            }
            return dt;
        }
        public static String Upsert(RequestPackage rqp0)
        {
            String status = "error";
            if (rqp0 != null && rqp0.Parameters != null)
            {
                // надо сохранить в 1с, в xl, в sql и потом обновить sql из xl и 1с
                // первое сохранение в 1с потому, что если это новая запись, то получим код который нужен для xl

                if (String.IsNullOrWhiteSpace(rqp0["f0"] as String))
                {
                    // для новой записи - надо получить внутренний номер договора из sql
                    String agrNum = F1HomeData.F1GetAgrNumSql();
                    rqp0["f2"] = agrNum;
                    // если нет внешнего номера договора, то назначаем ему внутренний
                    if (String.IsNullOrWhiteSpace(rqp0["f3"] as String))
                    {
                        rqp0["f3"] = agrNum;
                    }
                }
                Int32 code = -1;
                Int32.TryParse(rqp0["f14"] as String, out code);
                RequestPackage rqp1 = new RequestPackage();
                if (code > 0)
                {
                    rqp1.Command = "Обновить";
                }
                else
                {
                    rqp1.Command = "Добавить";
                }
                rqp1.Parameters = new RequestParameter[] 
                {
                    new RequestParameter("Тип", "Справочник"),
                    new RequestParameter("Вид", "Договоры"),
                    new RequestParameter("Код", code.ToString()),
                    new RequestParameter("Примечание", rqp0["f1"] as String),
                    new RequestParameter("Наименование", rqp0["f3"] as String),
                    new RequestParameter("Владелец", rqp0["f4"] as String),
                    new RequestParameter("ВладелецКод", rqp0["f4c"] as String),
                    new RequestParameter("ДатаДоговора", rqp0["f6"] as String),
                    new RequestParameter("ОтветЛицо", rqp0["f12"] as String),
                    new RequestParameter("СуммаДоговора", rqp0["f13"] as String),
                    new RequestParameter("НомерТоргов", rqp0["f15"] as String),
                    new RequestParameter("ДатаОкончания", rqp0["ДатаОкончания"] as String),
                    new RequestParameter("ОтсрочкаПлатежа", rqp0["ОтсрочкаПлатежа"] as String),
                    //{ "Представитель", rqp["Представитель"] as String },
                    //{ "ДопСоглашение", rqp["ДопСоглашение"] as String },
                    new RequestParameter("Пролонгация", rqp0["Пролонгация"] as String),
                    new RequestParameter("ГосударственныйИдентификатор", rqp0["f17"] as String),
                    new RequestParameter("ВключитьРассылкуДокументов", (Boolean)rqp0["ur"]),
                    new RequestParameter("ВключитьВРассылкуСчёт", (Boolean)rqp0["urd1"]),
                    new RequestParameter("ВключитьВРассылкуНакладную", (Boolean)rqp0["urd2"]),
                    new RequestParameter("ВключитьВРассылкуАкт", (Boolean)rqp0["urd3"]),
                    new RequestParameter("ВключитьВРрассылкуСчётФактуру", (Boolean)rqp0["urd4"]),
                    new RequestParameter("ВключитьВРассылкуДокументыКачества", (Boolean)rqp0["urd5"]),
                };

                code = F1HomeData.F1Upsert1c(rqp1);
                if (code >= 0)
                {
                    rqp0["f14"] = code.ToString();
                }

                String cmd;
                if (String.IsNullOrWhiteSpace(rqp0["f0"] as String))
                {
                    cmd = "Добавить";
                }
                else
                {
                    cmd = "Обновить";
                }
                F1HomeData.F1UpsertSql(cmd, rqp0);
                status = "ok";
            }
            return status;
        }
        public static String Delete(RequestPackage rqp0)
        {
            String status = "error";
            if (rqp0 != null)
            {
                String f0 = rqp0["f0"] as String;
                if (!String.IsNullOrWhiteSpace(f0))
                {
                    RequestPackage rqp = new RequestPackage
                    {
                        Command = "[dbo].[договоры_удалить_2]",
                        Parameters = new RequestParameter[]
                        {
                            new RequestParameter { Name = "f0", Value = f0 }
                        }
                    };
                    rqp.GetResponse("http://127.0.0.1:11012/");
                    status = "sql ok";
                }
                String f14 = rqp0["f14"] as String;
                if (!String.IsNullOrWhiteSpace(f14))
                {
                    // есть ссылка на 1c
                    RequestPackage rqp = new RequestPackage();
                    Int32 code = -1;
                    Int32.TryParse(f14, out code);
                    if (code > 0)
                    {
                        rqp.Command = "Удалить";
                        rqp.Parameters = new RequestParameter[] 
                        {
                            new RequestParameter("Тип", "Справочник"),
                            new RequestParameter("Вид", "Договоры"),
                            new RequestParameter("Код", code.ToString())
                        };
                        code = F1HomeData.F1Upsert1c(rqp);
                    }
                    status += " 1c ok";
                }
            }
            return status;
        }
    }
}
