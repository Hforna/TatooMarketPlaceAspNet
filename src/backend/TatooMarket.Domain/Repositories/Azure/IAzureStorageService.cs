using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Domain.Entities.Identity;

namespace TatooMarket.Domain.Repositories.Azure
{
    public interface IAzureStorageService
    {
        public Task UploadUser(UserEntity user, Stream file, string fileName);
        public Task<string> GetImage(string containerName, string fileName);
    }
}
