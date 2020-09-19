using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreIdentity.Entity.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentity.ClaimProvider
{
    public class ClaimProvider : IClaimsTransformation
    {
        private readonly UserManager<AppUser> _userManager;

        public ClaimProvider(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }



        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            //KULLANICI ÜYE Mİ
            if (principal != null && principal.Identity.IsAuthenticated)
            {
                ClaimsIdentity identity = principal.Identity as ClaimsIdentity;

                
                var user = await _userManager.FindByIdAsync(principal.FindFirstValue(ClaimTypes.NameIdentifier));


                //KULLANICI NULL DEĞİLSE
                if (user != null)
                {

                    //DOĞUM TARİHİ BOŞ DEĞİLSE
                    if (user.BirthDay != null)
                    {

                        if (!principal.HasClaim(x => x.Type == "Violence"))
                        {
                            var age = DateTime.Today.Year - user.BirthDay?.Year;

                            if (age > 15)
                            {
                                Claim ageClaim = new Claim("Violence", "True", ClaimValueTypes.String, "Internal");
                                identity.AddClaim(ageClaim);
                            }
                        }
                        
                    }

                    //ŞEHİR ALANI BOŞ DEĞİLSE
                    if (!String.IsNullOrEmpty(user.City))
                    {

                        //BÖYLE BİR CLAİM YOKSA BU CLAİMİ EKLE
                        if (!principal.HasClaim(x => x.Type == "City"))
                        {
                            //CLAİMİ EKLE
                            Claim cityClaim = new Claim("City",user.City.ToLower(),ClaimValueTypes.String,"Internal");
                            identity.AddClaim(cityClaim);
                        }
                    }
                }
            }
            return principal;
        }
    }
}
