﻿using Core.DataAccess.Concrete;
using Core.Entities.Concrete;
using DAL.Abstract;
using DAL.Concrete.Ef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DataAccess.Concrete.EF
{
    public class EfUserDal(CNBlogContext context) : BaseRepository<User, CNBlogContext>(context), IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new CNBlogContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }
    }
}