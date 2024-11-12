using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.User;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Azure;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.User;

namespace TatooMarket.Application.UseCases.User
{
    public class DeleteUserRequest : IDeleteUserRequest
    {
        private readonly IUserWriteRepository _userWrite;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetUserByToken _userByToken;
        private readonly IDeleteUserSender _sender;

        public DeleteUserRequest(IUserWriteRepository userWrite, IUnitOfWork unitOfWork, 
            IGetUserByToken userByToken, IAzureStorageService azureStorage, IDeleteUserSender sender)
        {
            _userWrite = userWrite;
            _unitOfWork = unitOfWork;
            _userByToken = userByToken;
            _sender = sender;
        }

        public async Task Execute()
        {
            var user = await _userByToken.GetUser();

            user!.Active = false;

            await _unitOfWork.Commit();


            await _sender.SendMessage(user);
        }
    }
}
