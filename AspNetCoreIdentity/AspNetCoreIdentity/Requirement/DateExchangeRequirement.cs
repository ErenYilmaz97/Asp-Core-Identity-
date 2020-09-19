using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentity.Requirement
{
    public class DateExchangeRequirement : IAuthorizationRequirement
    {
    }

    public class DateExchangeHandler : AuthorizationHandler<DateExchangeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DateExchangeRequirement requirement)
        {
            if (context.User != null && context.User.Identity != null)
            {
                Claim claim = context.User.Claims.Where(x=>x.Type == "DateExchange" && x.Value != null).FirstOrDefault();

                //CLAİM NULL DEĞİLSE, DAHA ÖNCE ERİŞİM SAĞLANMIŞ VE CLAİM OLUŞTURULMUŞTUR.
                //ERİŞİM SÜRESİ BİTMİŞ Mİ? KONTROL ET
                if (claim != null)
                {

                    //ŞUANKİ TARİH, CLAİMDEKİ SINIR DEĞERİNDEN KÜÇÜKSE
                    if (DateTime.Now < Convert.ToDateTime(claim.Value))
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
