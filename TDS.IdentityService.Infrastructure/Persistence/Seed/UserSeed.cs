using TDS.Domain.Extensions;
using TDS.IdentityService.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDS.IdentityService.Infrastructure.Seed
{
    public static class UserSeed
    {

        public static List<User> GetSeed()
        {
            return (new User[]
            {
                new User
                { Id=1,
                     UserName="admin1",
                     Password="TDSAdmin12345".GetSHA512(),
                    DateCreated = DateTime.Now,

                },
                new User
                {
                    Id=2,
                  UserName="admin2",
                     Password="TDSAdmin12345".GetSHA512(),
                    DateCreated = DateTime.Now,
                }
            }).ToList();


        }
    }
}
