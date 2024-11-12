using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.User;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Azure;
using TatooMarket.Domain.Repositories.User;

namespace TatooMarket.Application.UseCases.User
{
    public class DeleteUser : IDeleteUser
    {
        private readonly IUserReadRepository _userRead;
        private readonly IUserWriteRepository _userWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAzureStorageService _azureStorage;

        
        public DeleteUser(IUserReadRepository userRead, IUserWriteRepository userWrite, IUnitOfWork unitOfWork, IAzureStorageService azureStorage)
        {
            _userRead = userRead;
            _userWrite = userWrite;
            _unitOfWork = unitOfWork;
            _azureStorage = azureStorage;
        }

        public async Task Execute(Guid uid)
        {
            var user = await _userRead.UserByUid(uid);

            if (user is null)
                return;

            _userWrite.Delete(user);

            await _unitOfWork.Commit();

            await _azureStorage.DeleteContainer(user.UserIdentifier.ToString());
        }
    }
}
