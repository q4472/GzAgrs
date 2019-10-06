using Agrs.Models;
using Nskd;
using System;
using System.Web.Mvc;

namespace Agrs.Controllers
{
    public class F1Controller : Controller
    {
        public Object Index(String sessionId)
        {
            Object v = null;
            F1Model m = new F1Model();
            if (ControllerContext.HttpContext.IsDebuggingEnabled)
                v = View("~/Views/F1/Index.cshtml", m); // _ViewStart.cshtml
            else
                v = PartialView("~/Views/F1/Index.cshtml", m);
            return v;
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
            Object result = null;
            var rqp = RequestPackage.ParseRequest(Request.InputStream, Request.ContentEncoding);
            if (rqp != null)
            {
                switch (rqp.Command)
                {
                    case "insert":
                        var dt = F1Model.Insert(rqp);
                        F1Model m = new F1Model(dt);
                        result = PartialView("~/Views/F1/FilteredView.cshtml", m);
                        break;
                    case "update":
                        result = F1Model.Update(rqp);
                        break;
                    case "delete":
                        result = F1Model.Delete(rqp);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                result = "Ошибка в формате команды.";
            }
            return result;
        }
    }
}
