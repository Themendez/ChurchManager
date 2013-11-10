using System.Web.Mvc;
using AutoMapper;
using ChurchManager.Controllers.Models;
using ChurchManager.Core.Domain;
using ChurchManager.Core.Service;

namespace ChurchManager.Controllers.Controllers
{
    public class ParishController : ControllerBase
    {
        private readonly IParishService _parishService;

        public ParishController(IParishService parishService)
        {
            _parishService = parishService;
        }
        
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(ParishModel model)
        {
            return ProcessRequest(
                () =>
                {
                    Parish parish = Mapper.Map<ParishModel, Parish>(model);
                    _parishService.Save(parish);
                    return Success();
                });
        }
    }
}
