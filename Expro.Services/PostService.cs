using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;

namespace Expro.Services
{
    public class PostService : BaseCRUDService<Post>, IPostService
    {
        public PostService(IPostRepository repository,
                           IUnitOfWork unitOfWork)
            : base(repository, unitOfWork)
        {
        }
    }
}