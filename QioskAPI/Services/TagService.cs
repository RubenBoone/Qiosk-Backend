using Microsoft.EntityFrameworkCore;
using QioskAPI.Data;
using QioskAPI.Interfaces;
using QioskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Services
{
    public class TagService: ITagService
    {
        private readonly QioskContext _context;
        public TagService(QioskContext qioskContext)
        {
            _context = qioskContext;
        }
        public bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.TagID == id);
        }

        public async Task DeleteTag(int id)
        {
            var company = await _context.Tags.FindAsync(id);
            _context.Tags.Remove(company);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag>> GetTags()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetTag(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task PostTag(Tag company)
        {
            _context.Tags.Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task PutTag(int id, Tag company)
        {
            _context.Entry(company).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
