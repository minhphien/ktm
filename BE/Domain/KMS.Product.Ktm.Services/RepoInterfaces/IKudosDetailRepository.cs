using KMS.Product.Ktm.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KMS.Product.Ktm.Repository
{
    public interface IKudosDetailRepository
    {
        /// <summary>Gets the kudos detail by the list of slack emoji.</summary>
        /// <param name="emojis">The emojis string.</param>
        /// <returns></returns>
        IQueryable<KudoDetail> GetKudosDetailBySlackEmoji(IEnumerable<string> emoji);
    }
}