using Base.Data;
using Nskd;
using System;
using System.Collections.Generic;
using System.Data;

namespace AreasAgrs.Areas.Agrs.Models
{
    public class F1Model
    {
        public DataTable NetSqlГарзаДоговоры { get; private set; }

        // заход при первом обращении (без фильтра)
        public F1Model()
        {
            NetSqlГарзаДоговоры = GarzaSql.F1GetДоговоры();
        }

        // заход с фильтром или по "f0" для "detail" или по полям фильтра для "filtered_view"
        public F1Model(RequestPackage rqp)
        {
            NetSqlГарзаДоговоры = GarzaSql.F1GetДоговоры(rqp);
        }

        public F1Model(String f0)
        {
            NetSqlГарзаДоговоры = GarzaSql.F1GetДоговоры(f0);
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
                    DataTable dt = Garza1Cv77.F1GetAgrByCode(code);
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
                    dt = Data.Garza1Cv77.F1GetCustTable(filter);
                    break;
                case "Сотрудники":
                    dt = Data.Garza1Cv77.F1GetStuffTable(filter);
                    break;
                default:
                    break;
            }
            return dt;
        }

        public static String Upsert(RequestPackage rqp)
        {
            String status = "error";
            if (rqp != null && rqp.Parameters != null)
            {
                // надо сохранить в 1с, в xl, в sql и потом обновить sql из xl и 1с
                // первое сохранение в 1с потому, что если это новая запись, то получим код который нужен для xl

                if (String.IsNullOrWhiteSpace(rqp["f0"] as String))
                {
                    // для новой записи - надо получить внутренний номер договора из sql
                    String agrNum = HomeData.Agrs.F1GetAgrNumSql();
                    rqp["f2"] = agrNum;
                    // если нет внешнего номера договора, то назначаем ему внутренний
                    if (String.IsNullOrWhiteSpace(rqp["f3"] as String))
                    {
                        rqp["f3"] = agrNum;
                    }
                }
                Int32 code = -1;
                String cmd = null;
                Int32.TryParse(rqp["f14"] as String, out code);
                if (code > 0)
                {
                    cmd = "Обновить";
                }
                else
                {
                    cmd = "Добавить";
                }
                Dictionary<String, String> ocPars = new Dictionary<string, string>
                {
                    { "Тип", "Справочник" },
                    { "Вид", "Договоры" },
                    { "Код", code.ToString() },
                    { "Примечание", rqp["f1"] as String },
                    { "Наименование", rqp["f3"] as String },
                    { "Владелец", rqp["f4"] as String },
                    { "ВладелецКод", rqp["f4c"] as String },
                    { "ДатаДоговора", rqp["f6"] as String },
                    { "ОтветЛицо", rqp["f12"] as String },
                    { "СуммаДоговора", rqp["f13"] as String },
                    { "НомерТоргов", rqp["f15"] as String },
                    { "ДатаОкончания", rqp["ДатаОкончания"] as String },
                    { "ОтсрочкаПлатежа", rqp["ОтсрочкаПлатежа"] as String },
                    //{ "Представитель", rqp["Представитель"] as String },
                    //{ "ДопСоглашение", rqp["ДопСоглашение"] as String },
                    { "Пролонгация", rqp["Пролонгация"] as String },
                    { "ГосударственныйИдентификатор", rqp["f17"] as String }
                };
                code = HomeData.Agrs.F1Upsert1c(cmd, ocPars);
                if (code >= 0)
                {
                    rqp["f14"] = code.ToString();
                }

                if (String.IsNullOrWhiteSpace(rqp["f0"] as String))
                {
                    cmd = "Добавить";
                }
                else
                {
                    cmd = "Обновить";
                }
                HomeData.Agrs.F1UpsertSql(cmd, rqp);
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
                    GarzaSql.Execute12(rqp);
                    status = "sql ok";
                }
                String f14 = rqp0["f14"] as String;
                if (!String.IsNullOrWhiteSpace(f14))
                {
                    // есть ссылка на 1c
                    String cmd = null;
                    Int32 code = -1;
                    Int32.TryParse(f14, out code);
                    if (code > 0)
                    {
                        cmd = "Удалить";

                        Dictionary<String, String> ocPars = new Dictionary<string, string>();
                        ocPars.Add("Тип", "Справочник");
                        ocPars.Add("Вид", "Договоры");
                        ocPars.Add("Код", code.ToString());

                        code = HomeData.Agrs.F1Upsert1c(cmd, ocPars);
                    }
                    status += " 1c ok";
                }
            }
            return status;
        }
    }
}
