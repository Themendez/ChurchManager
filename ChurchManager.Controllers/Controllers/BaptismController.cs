using System.Web.Mvc;
using AutoMapper;
using ChurchManager.Controllers.Models;
using ChurchManager.Core.Domain;
using ChurchManager.Core.Service;

namespace ChurchManager.Controllers.Controllers
{
    public class BaptismController : ControllerBase
    {
        private readonly IBaptismService _baptismService;

        public BaptismController(IBaptismService baptismService)
        {
            _baptismService = baptismService;
        }

        [HttpPost]
        public ActionResult Save(BaptismModel model)
        {
            return ProcessRequest(
                () =>
                    {
                        Baptism baptism = Mapper.Map<BaptismModel, Baptism>(model);
                        _baptismService.Save(baptism);
                        return Success();
                    });

        }
    }
}
