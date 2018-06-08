using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmSib.AreasAgrs.Areas.Agrs.Models
{
    public class SsAgrTable
    {
        public static Object[] Md = new Object[] { 
            //            0 field, 1 header, 2 type, 3 width, 4 alignment, 5 readonly, 6 hidden, 7 dbName, 8 relTable
            new Object[] {"Id", "id", "Guid?", 0, "Left", true, true, "id", null},
            new Object[] {"Timestamp", "timestamp", "DateTime?", 0, "Left", true, true, "timestamp", null},
            new Object[] {"Hash", "hash", "Byte[]", 0, "Left", true, true, "hash", null},
            new Object[] {"F1", "Вид контракта", "String", 80, "Left", false, false, "f1", "enu1"},
            new Object[] {"F2", "№ п/п (внутр)", "String", 60, "Right", true, false, "f2", null},
            new Object[] {"F3", "№ договора (внешн.)", "String", 180, "Left", false, false, "f3", null},
            new Object[] {"F4", "Наименование клиента", "String", 240, "Left", false, false, "f4", "Customers"},
            new Object[] {"F5", "Город (населенный пункт)", "String", 0, "Left", false, false, "f5", null},
            new Object[] {"F6", "Дата договора", "DateTime?", 80, "Right", false, false, "f6", null},
            new Object[] {"F7", "Дата внесения в реестр", "DateTime?", 0, "Right", false, false, "f7", null},
            new Object[] {"F8", "Контрольная дата возврата", "DateTime?", 0, "Right", false, false, "f8", null},
            new Object[] {"F9", "Дата возврата", "DateTime?", 0, "Right", false, false, "f9", null},
            new Object[] {"F10", "Нал", "String", 0, "Left", false, false, "f10", null},
            new Object[] {"F11", "Лицензия", "String", 0, "Left", false, true, "f11", null},
            new Object[] {"F12", "Ответственный менеджер", "String", 80, "Left", false, false, "f12", "Stuff"},
            new Object[] {"F13", "Сумма", "Double?", 80, "Right", false, false, "f13", null},
            new Object[] {"F14", "Код 1С", "Int32?", 0, "Right", true, true, "f14", null},
            new Object[] {"F15", "№ торгов", "String", 140, "Left", false, false, "f15", null},
            new Object[] {"F16", "Комментарий", "String", 0, "Left", false, false, "f16", null},
            new Object[] {"SP2283", "Дата окончания", "DateTime?", 0, "Right", false, false, "SP2283", null},
            new Object[] {"SP3581", "Отсрочка платежа", "Double?", 0, "Right", false, false, "SP3581", null},
            new Object[] {"pres", "Представитель", "String", 0, "Left", true, true, "pres", "pres"},
            new Object[] {"aa", "Доп. соглашение", "String", 0, "Left", true, true, "aa", null},
            new Object[] {"SP3578", "Пролонгация", "String", 0, "Left", false, false, "SP3578", null},
            new Object[] {"F17", "Государственный идентификатор", "String", 0, "Left", false, false, "f17", null}
        };
    }
    public class SsCustTable
    {
        public static Object[] Md = new Object[] { 
            //            0 field, 1 header, 2 type, 3 width, 4 alignment, 5 readonly, 6 hidden, 7 dbName, 8 RelTable
            new Object[] {"Code", "Код", "String", 100, "Left", true, false, "CODE", null},
            new Object[] {"Descr", "Наименование", "String", 100, "Left", false, false, "DESCR", null}
        };
    }
    public class SsStuffTable
    {
        public static Object[] Md = new Object[] { 
            //            0 field, 1 header, 2 type, 3 width, 4 alignment, 5 readonly, 6 hidden, 7 dbName, 8 RelTable
            new Object[] {"Code", "Код", "String", 100, "Left", true, false, "CODE", null},
            new Object[] {"Descr", "Наименование", "String", 100, "Left", false, false, "DESCR", null}
        };
    }
}
