using ChurchManager.Core.Domain;

namespace ChurchManager.Core.Service
{
    public interface IBaptismService : IApplicationService
    {
        Baptism Save(Baptism baptism);
    }
}
