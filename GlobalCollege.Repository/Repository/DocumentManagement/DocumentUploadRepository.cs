using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalCollege.Entity;
using GlobalCollege.Entity.DTO;
using GlobalCollege.Infrastructure;
using System.Data.Entity;


namespace GlobalCollege.Repository
{
    public class DocumentUploadRepository : RepositoryBase<DocumentUpload, DocumentUploadDTO>, IDocumentUploadRepository
    {
        public DocumentUploadRepository(IDatabaseFactory databaseFactory, IAuthenticationHelper authenticationHelper)
            : base(databaseFactory, authenticationHelper)
        {

        }

        public async Task<bool> UpdateDocumentPath(Guid Id, string Path)
        {
            try
            {
                DocumentUpload document = await this.DataContext.DocumentUploads.Where(x => x.Id == Id).FirstOrDefaultAsync();
                document.FilePath = Path;
                await this.DataContext.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public interface IDocumentUploadRepository : IRepository<DocumentUpload, DocumentUploadDTO>
    {
        Task<bool> UpdateDocumentPath(Guid Id, string Path);
    }
}