using System;
using System.Web.Mvc;
using ChurchManager.Controllers.Models;

namespace ChurchManager.Controllers.Controllers
{
    public class ControllerBase : Controller
    {

        public ActionResult ProcessRequest(Func<ActionResult> function)
        {
            try
            {
                return function();
            }
            catch (ApplicationException ex)
            {
                OperationResult result = new OperationResult
                                             {
                                                 Success = false,
                                                 Errors = new[] {ex.Message}
                                             };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new OperationResult
                                {
                                    Success = false,
                                    Errors = new[] {ex.Message}
                                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Success(object result = null)
        {
            return Json(new OperationResult
                            {
                                Success = true,
                                Data = result
                            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Fail(params string[] errors)
        {

            return Json(new OperationResult
                            {
                                Success = false,
                                Errors = errors
                            }, JsonRequestBehavior.AllowGet);
        }
    }
}
