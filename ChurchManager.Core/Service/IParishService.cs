using ChurchManager.Core.Domain;

namespace ChurchManager.Core.Service
{
    public interface IParishService : IApplicationService
    {
        Parish Save(Parish parish);
    }
}
