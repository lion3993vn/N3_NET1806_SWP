using AutoMapper;
using NET1806_LittleJoy.Repository.Commons;
using NET1806_LittleJoy.Repository.Entities;
using NET1806_LittleJoy.Repository.Repositories;
using NET1806_LittleJoy.Repository.Repositories.Interface;
using NET1806_LittleJoy.Service.BusinessModels;
using NET1806_LittleJoy.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET1806_LittleJoy.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<Pagination<CategoryModel>> GetAllCategoryPagingAsync(PaginationParameter paginationParameter)
        {
            var listCate = await _categoryRepo.GetAllCategoryPagingAsync(paginationParameter);

            if (!listCate.Any())
            {
                return null;
            }

            var listCateModels = listCate.Select(c => new CategoryModel
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
            }).ToList();


            return new Pagination<CategoryModel>(listCateModels,
                listCate.TotalCount,
                listCate.CurrentPage,
                listCate.PageSize);
        }

        public async Task<CategoryModel?> GetCategoryByIdAsync(int cateId)
        {
            var cateDetail = await _categoryRepo.GetCategoryByIdAsync(cateId);

            if (cateDetail == null)
            {
                return null;
            }

            var cateDetailModel = _mapper.Map<CategoryModel>(cateDetail);

            return cateDetailModel;
        }

        public async Task<bool?> AddCategoryAsync(CategoryModel categoryModel)
        {
            try
            {
                if(categoryModel.CategoryName.Equals(""))
                {
                    throw new Exception("Không được tạo categoryName trống");
                }

                var listCate = await _categoryRepo.GetAllCate();

                foreach (var item1 in listCate)
                {
                    if (item1.CategoryName.Equals(categoryModel.CategoryName))
                    {
                        throw new Exception("Không được tạo categoryName trùng lặp");
                    }
                }

                var cateInfo = _mapper.Map<Category>(categoryModel);

                var item = await _categoryRepo.AddCategoryAsync(cateInfo);

                if (item == null)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fail to add Category {ex.Message}");
                throw ex;
                
            }
        }

        public async Task<bool> RemoveCategoryByIdAsync(int cateId)
        {
            try
            {
                var productsCate = await _categoryRepo.GetProductsByCategoryIdAsync(cateId);

                if (productsCate.Any())
                {
                    throw new Exception("Đã có sản phẩm nên không thể xóa");
                }

                var item = await _categoryRepo.GetCategoryByIdAsync(cateId);

                if (item == null)
                {
                    throw new Exception("Category không tồn tại");
                }

                return await _categoryRepo.RemoveCategoryAsync(item);
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<CategoryModel> UpdateCategoryAsync(CategoryModel cateModel)
        {
            try
            {
                if (cateModel.CategoryName != null)
                {
                    if (cateModel.CategoryName.Equals(""))
                    {
                        throw new Exception("Không được tạo category trống");
                    }

                    var listCate = await _categoryRepo.GetAllCate();

                    foreach (var cate in listCate)
                    {
                        if (cate.CategoryName.Equals(cateModel.CategoryName) && cate.Id != cateModel.Id)
                        {
                            throw new Exception("Không được thay đổi category trùng lặp");

                        }
                    }

                    var cateModify = _mapper.Map<Category>(cateModel);

                    var catePlace = await _categoryRepo.GetCategoryByIdAsync(cateModel.Id);

                    if (catePlace == null)
                    {
                        return null;
                    }
                    else
                    {
                        catePlace.CategoryName = cateModify.CategoryName;
                        var updateCate = await _categoryRepo.UpdateCategoryAsync(catePlace);

                        if (updateCate != null)
                        {
                            return _mapper.Map<CategoryModel>(updateCate);
                        }
                    }
                }
                return null;
            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
