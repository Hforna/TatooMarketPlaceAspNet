﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatooMarket.Communication.Requests.User;
using TatooMarket.Communication.Responses.User;

namespace TatooMarket.Application.UseCases.Repositories.User
{
    public interface ICreateUser
    {
        public Task<ResponseCreateUser> Execute(RequestCreateUser request);
    }
}
