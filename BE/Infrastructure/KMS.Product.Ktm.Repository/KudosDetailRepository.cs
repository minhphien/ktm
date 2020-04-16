using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using AutoMapper;
using KMS.Product.Ktm.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace KMS.Product.Ktm.Repository
{
    public class KudosDetailRepository : BaseRepository<KudoDetail>, IKudosDetailRepository
    {
        private readonly KtmDbContext context;
        public readonly DbSet<KudoDetail> KudosDetail;
        private readonly IMapper mapper;
        public KudosDetailRepository(KtmDbContext context, IMapper mapper, ILogger<KudoDetail> logger) : base(context, logger)
        {
            this.context = context;
            KudosDetail = context.Set<KudoDetail>();
            this.mapper = mapper;
        }

        /// <summary>Gets the kudos detail by the list of slack emoji.</summary>
        /// <param name="emojis">The emojis string.</param>
        /// <returns></returns>
        public IQueryable<KudoDetail> GetKudosDetailBySlackEmoji(IEnumerable<string> emojis)
        {
            return KudosDetail.Where(o => emojis.Contains(o.SlackEmoji));
        }
    }
}
