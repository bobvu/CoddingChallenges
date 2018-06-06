using System;
using System.Collections.Generic;
using System.Text;
using CodingChallenges.Domains;
namespace CodingChallenges.Services
{
    public interface IService<TEntity> where TEntity:AuditableEntity
    {

    }
}
