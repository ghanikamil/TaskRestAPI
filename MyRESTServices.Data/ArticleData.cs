using Microsoft.EntityFrameworkCore;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain;
using MyRESTServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRESTServices.Data
{
    public class ArticleData : IArticleData
    {
        private readonly AppDbContext _context;
        public ArticleData(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var article = await GetById(id);
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            var articles = await _context.Articles.OrderBy(a => a.ArticleId).ToListAsync();
            return articles;
        }

        public async Task<IEnumerable<Article>> GetArticleByCategory(int categoryId)
        {
            var articles = await _context.Articles.Include(a => a.Category).Where(a => a.Category.CategoryId == categoryId).ToListAsync();
            return articles;
        }

        public async Task<IEnumerable<Article>> GetArticleWithCategory()
        {
            var articles = await _context.Articles.Include(a => a.Category).ToListAsync();
            return articles;
        }

        public async Task<Article> GetById(int id)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a=>a.ArticleId == id);

            if (article == null)
            {
                throw new ArgumentException("id not found");
            }
            return article;
        }

        public Task<int> GetCountArticles()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Article>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Article> Insert(Article entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw new ArgumentException($"{ex.Message}");
            }
        }

        public Task<Task> InsertArticleWithCategory(Article article)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertWithIdentity(Article article)
        {
            throw new NotImplementedException();
        }

        public async Task<Article> Update(Article entity)
        {
            _context.Articles.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
