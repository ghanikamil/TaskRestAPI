

using AutoMapper;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain;

namespace MyRESTServices.BLL
{
    public class CategoryBLL : ICategoryBLL
    {
        private readonly ICategoryData _categoryData;
        private readonly IMapper _mapper;

        public CategoryBLL(ICategoryData categoryData, IMapper mapper)
        {
            _categoryData = categoryData;
            _mapper = mapper;
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var category = await _categoryData.GetById(id);
                if (category == null)
                {
                    throw new ArgumentException("category not found");
                }
                return await _categoryData.Delete(id);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var categories = await _categoryData.GetAll();
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return categoriesDto;
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var category = await _categoryData.GetById(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public Task<IEnumerable<CategoryDTO>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountCategories(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Task> Insert(CategoryCreateDTO entity)
        {
            try
            {
                var category = _mapper.Map<Category>(entity);
                var addCategory = await _categoryData.Insert(category);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Task> Update(int id, CategoryUpdateDTO entity)
        {
            var category = await _categoryData.GetById(id);
            _mapper.Map(entity, category);
            await _categoryData.Update(category);
            return Task.CompletedTask;
        }
    }
}
