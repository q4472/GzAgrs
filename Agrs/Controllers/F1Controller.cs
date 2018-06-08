using FarmSib.AreasAgrs.Areas.Agrs.Models;
using Nskd;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FarmSib.AreasAgrs.Areas.Agrs.Controllers
{
    public class F1Controller : Controller
    {
        public Object Index(String sessionId)
        {
            F1Model m = new F1Model(sessionId);
            return View("~/Views/F1/Index.cshtml", m);
        }

        public Object GetDataForSelectorWithListBox()
        {
            var rqp = RequestPackage.ParseRequest(Request.InputStream, Request.ContentEncoding);
            var tableName = rqp["tableName"] as String;
            var filter = rqp["filter"] as String;
            Object dt = F1Model.GetDataForSelectorWithListBox(tableName, filter);
            PartialViewResult r = PartialView("~/Views/F1/DetailFields/ListBox1.cshtml", dt);
            return r;
        }

        public Object GetDataForFilteredView()
        {
            Object result = null;
            Dictionary<String, String> fs = new Dictionary<String, String>(Request.Form.Count);
            for (int i = 0; i < Request.Form.Count; i++)
            {
                fs.Add(Request.Form.Keys[i], Request.Form.GetValues(i)[0]);
            }
            F1Model m = new F1Model(fs);
            result = PartialView("~/Views/F1/FilteredView.cshtml", m);
            return result;
        }

        public Object GetDataForDetailSection()
        {
            Dictionary<String, String> fs = new Dictionary<String, String>(Request.Form.Count);
            for (int i = 0; i < Request.Form.Count; i++)
            {
                fs.Add(Request.Form.Keys[i], Request.Form.GetValues(i)[0]);
            }
            F1Model m = new F1Model(fs);
            m.LoadDictionariesAndFillPopups();
            var view = PartialView("~/Views/F1/Detail.cshtml", m);
            return view;
        }

        public Object Save()
        {
            String status = null;
            Dictionary<String, String> pars = new Dictionary<String, String>(Request.Form.Count);
            for (int i = 0; i < Request.Form.Count; i++)
            {
                pars.Add(Request.Form.Keys[i], Request.Form.GetValues(i)[0]);
            }
            if ((pars != null) && pars.ContainsKey("cmd"))
            {
                switch (pars["cmd"])
                {
                    case "save":
                        status = F1Model.Upsert(pars);
                        break;
                    case "delete":
                        status = F1Model.Delete(pars);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                status = "Ошибка в формате команды.";
            }
            return status;
        }
    }
}
