using AutoMapper;
using Sqids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Application.Services.Validator.Finance;
using TatooMarket.Application.UseCases.Repositories.Finance;
using TatooMarket.Communication.Requests.Finance;
using TatooMarket.Communication.Responses.Finance;
using TatooMarket.Domain.Entities.Finance;
using TatooMarket.Domain.Repositories;
using TatooMarket.Domain.Repositories.Finance;
using TatooMarket.Domain.Repositories.Security.Token;
using TatooMarket.Domain.Repositories.Services;
using TatooMarket.Domain.Repositories.User;
using TatooMarket.Exception.Exceptions;

namespace TatooMarket.Application.UseCases.Finance
{
    public class CreateFinanceAccount : ICreateFinanceAccount
    {
        private readonly IMapper _mapper;
        private readonly IGetUserByToken _userByToken;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFinanceWriteOnly _financeWrite;
        private readonly SqidsEncoder<long> _sqidsEncoder;
        private readonly ISendEmailService _sendEmail;

        public CreateFinanceAccount(IMapper mapper, IGetUserByToken userByToken,
            IUnitOfWork unitOfWork, IUserWriteRepository userWrite, 
            IFinanceWriteOnly financeWrite, SqidsEncoder<long> sqidsEncoder, ISendEmailService sendEmail)
        {
            _mapper = mapper;
            _userByToken = userByToken;
            _unitOfWork = unitOfWork;
            _financeWrite = financeWrite;
            _sqidsEncoder = sqidsEncoder;
            _sendEmail = sendEmail;
        }

        public async Task<ResponseCreateFinanceAccount> Execute(RequestCreateFinanceAccount request)
        {
            Validate(request);

            var user = await _userByToken.GetUser();

            if(user.StudioId is null)
                throw new StudioException(ResourceExceptMessages.STUDIO_DOESNT_EXISTS);

            var accountBank = _mapper.Map<StudioBankAccountEntity>(request);
            accountBank.StudioId = (long)user.StudioId!;
            accountBank.OwnerName = user.UserName;
            accountBank.Active = false;

            var balance = new BalanceEntity() { StudioId = (long)user.StudioId };

            await _financeWrite.AddBankAccount(accountBank);
            await _financeWrite.AddBalance(balance);

            await _unitOfWork.Commit();

            var response = _mapper.Map<ResponseCreateFinanceAccount>(accountBank);
            response.AccountId = _sqidsEncoder.Encode(accountBank.Id);
            response.BalanceId = _sqidsEncoder.Encode(balance.Id);

            //await _sendEmail.SendEmail(request.Email, user.UserName);

            return response;
        }

        private void Validate(RequestCreateFinanceAccount request)
        {
            var validator = new CreateFinanceAccountValidator();
            var result = validator.Validate(request);
            
            if(!result.IsValid)
            {
                var errorMessages = result.Errors.Select(d => d.ErrorMessage).ToList();
                throw new FinanceException(errorMessages);
            }
        }
    }
}
