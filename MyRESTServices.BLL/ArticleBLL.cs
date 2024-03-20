using AutoMapper;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;
using MyRESTServices.Data;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRESTServices.BLL
{
    public class ArticleBLL : IArticleBLL
    {
        private readonly IArticleData _articleData;
        private readonly IMapper _mapper;

        public ArticleBLL(IArticleData articleData, IMapper mapper)
        {
            _articleData = articleData;
            _mapper = mapper;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var article = await _articleData.GetById(id);
                if (article == null)
                {
                    throw new ArgumentException("article not found");
                }
                return await _articleData.Delete(id);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<IEnumerable<ArticleDTO>> GetAll()
        {
            var articles = await _articleData.GetAll();
            var articleDto = _mapper.Map<IEnumerable<ArticleDTO>>(articles);
            return articleDto;
        }

        public async Task<IEnumerable<ArticleDTO>> GetArticleByCategory(int categoryId)
        {
            var articles = await _articleData.GetArticleByCategory(categoryId);
            return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
        }

        public async Task<ArticleDTO> GetArticleById(int id)
        {
            var article = await _articleData.GetById(id);
            return _mapper.Map<ArticleDTO>(article);
        }

        public async Task<IEnumerable<ArticleDTO>> GetArticleWithCategory()
        {
            var articles = await _articleData.GetArticleWithCategory();
            return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
        }

        public Task<int> GetCountArticles()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ArticleDTO>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Task> Insert(ArticleCreateDTO article)
        {
            try
            {
                var newArticle = _mapper.Map<Article>(article);
                await _articleData.Insert(newArticle);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<int> InsertWithIdentity(ArticleCreateDTO article)
        {
            throw new NotImplementedException();
        }

        public async Task<Task> Update(int id, ArticleUpdateDTO article)
        {
            var articleUpdate = await _articleData.GetById(id);
            _mapper.Map(article, articleUpdate);
            await _articleData.Update(articleUpdate);
            return Task.CompletedTask;
        }
    }
}
