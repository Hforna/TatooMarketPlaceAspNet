using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests;
using TatooMarket.Communication.Responses;

namespace TatooMarket.Application.UseCases.Repositories
{
    public interface ICreateUser
    {
        public Task<ResponseCreateUser> Execute(RequestCreateUser request);
    }
}
