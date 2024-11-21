using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.UseCases.Repositories.Tattoo;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Azure;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.Tattoo;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Tattoo
{
    public class DeleteTattooRequest : IDeleteTattooRequest
    {
        private readonly IGetUserByToken _userByToken;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITattooReadOnly _tattooRead;
        private readonly ITattooWriteOnly _tattooWrite;
        private readonly IDeleteTattooSender _tattooSender;

        public DeleteTattooRequest(IGetUserByToken userByToken, IUnitOfWork unitOfWork, 
            ITattooReadOnly tattooRead, ITattooWriteOnly tattooWrite, IDeleteTattooSender tattooSender)
        {
            _userByToken = userByToken;
            _unitOfWork = unitOfWork;
            _tattooRead = tattooRead;
            _tattooWrite = tattooWrite;
            _tattooSender = tattooSender;
        }

        public async Task Execute(long id)
        {
            var user = await _userByToken.GetUser();

            var tattoo = await _tattooRead.TattooById(id);

            var isStudiosTattoo = await _tattooRead.StudioIsOwnTattoo(user.Studio, id);

            if (isStudiosTattoo == false)
                throw new TattooException(ResourceExceptMessages.TATTOO_DOESNT_EXISTS);

            tattoo.Active = false;

            _tattooWrite.Update(tattoo);
            await _unitOfWork.Commit();

            await _tattooSender.SendMessage(id);
        }
    }
}
