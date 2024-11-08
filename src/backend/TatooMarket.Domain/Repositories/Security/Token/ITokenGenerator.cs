﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Domain.Repositories.Security.Token
{
    public interface ITokenGenerator
    {
        public string GenerateToken(Guid uid, List<Claim>? claimsUser = null!);
    }
}
