using AutoMapper;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.DTOs;
using src.Models.Entities;

namespace src.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly ApplicationDBContext dbContext;
        private readonly IMapper mapper;

        // to initialize the service with the database and mapper
        public CategoryService(ApplicationDBContext _dbContext, IMapper _mapper)
        {
            dbContext = _dbContext;
            mapper = _mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            // to get the categories from the database
            var categories = await dbContext.Category
                .Include(c => c.Status) // include related Status data for mapping
                .ToListAsync();

            // to map the database entities to our public DTOs
            return mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await dbContext.Category
            .Include(c => c.Status) // also include Status here
            .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return null;
            }

            return  mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> AddCategoryAsync(CreateCategoryDto categoryDto)
        {
            // to map the incoming DTO to our internal Category entity
            var category = mapper.Map<Category>(categoryDto);

            dbContext.Category.Add(category);
            await dbContext.SaveChangesAsync();

            // to map the new entity back to a DTO to return to the client
            return  mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateCategoryAsync(int id, UpdateCategoryDto categoryDto)
        {
            var category = await dbContext.Category.FindAsync(id);

            if (category == null)
            {
                return false;
            }
            // use automapper to update the entity from the dto in one line
            mapper.Map(categoryDto, category);

             await dbContext.SaveChangesAsync();

             return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await dbContext.Category.FindAsync(id);

            if (category == null)
            {
                return false;
            }

            category.StatusId = 3; // 3 for deleted

            await dbContext.SaveChangesAsync();

            return true;
        }




    }
}
