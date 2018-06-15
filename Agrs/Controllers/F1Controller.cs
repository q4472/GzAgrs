using Agrs.Models;
using Nskd;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Agrs.Controllers
{
    public class F1Controller : Controller
    {
        public Object Index(String sessionId)
        {
            F1Model m = new F1Model();
            return View("~/Views/F1/Index.cshtml", m);
        }

        public Object GetDataForFilteredView()
        {
            var rqp = RequestPackage.ParseRequest(Request.InputStream, Request.ContentEncoding);
            F1Model m = new F1Model(rqp);
            var view = PartialView("~/Views/F1/FilteredView.cshtml", m);
            return view;
        }

        public Object GetDataForDetailSection()
        {
            var rqp = RequestPackage.ParseRequest(Request.InputStream, Request.ContentEncoding);
            F1Model m = new F1Model(rqp["f0"] as String);
            var view = PartialView("~/Views/F1/Detail.cshtml", m);
            return view;
        }

        public Object GetDataForSelectorWithListBox()
        {
            var rqp = RequestPackage.ParseRequest(Request.InputStream, Request.ContentEncoding);
            Object dt = F1Model.GetDataForSelectorWithListBox(rqp);
            PartialViewResult r = PartialView("~/Views/F1/ListBox1.cshtml", dt);
            return r;
        }

        public Object Save()
        {
            String status = null;
            var rqp = RequestPackage.ParseRequest(Request.InputStream, Request.ContentEncoding);
            if (rqp != null)
            {
                switch (rqp.Command)
                {
                    case "save":
                        status = F1Model.Upsert(rqp);
                        break;
                    case "delete":
                        status = F1Model.Delete(rqp);
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
