using Expro.Data.Infrastructure;
using Expro.Data.Repository.Interfaces;
using Expro.Models;
using Expro.Services.Interfaces;
using System.IO;
using System.Linq;

namespace Expro.Services
{
    public class AttachmentService : BaseAuthorableService<Attachment>, IAttachmentService
    {
        //private readonly IFileManagement FileManagement;
        public AttachmentService(IAttachmentRepository repository,
                           IUnitOfWork unitOfWork
                           /*IFileManagement fileManagement*/)
            : base(repository, unitOfWork)
        {
            //FileManagement = fileManagement;
        }

        //public void AddAndSaveFile(Attachment model, FileStream stream, string creatorID)
        //{
        //    //TO-DO: save file
        //    var bytes = FileManagement.ToByteArray(stream);
        //    string path = model.Path + "/" + model.GUID + "." + model.Extension;
        //    FileManagement.SaveFile(bytes, path);

        //    base.Add(model, creatorID);
        //}
    }
}