﻿using AutoMapper;
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

                if (categoryModel.CategoryName != null)
                {

                    if (categoryModel.CategoryName.Equals(""))
                    {
                        throw new Exception("Không được tạo category trống");
                    }

                    var listCate = await _categoryRepo.GetAllCateAsync();

                    foreach (var cate in listCate)
                    {
                        if (cate.CategoryName.Equals(categoryModel.CategoryName) && cate.Id != categoryModel.Id)
                        {
                            throw new Exception("Không được tạo category trùng lặp");
                        }
                    }

                    var cateInfo = _mapper.Map<Category>(categoryModel);

                    var item = await _categoryRepo.AddCategoryAsync(cateInfo);

                    if (item == null)
                    {
                        return false;
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveCategoryByIdAsync(int cateId)
        {
            var productsCate = await _categoryRepo.GetProductsByCategoryIdAsync(cateId);

            if (productsCate.Any())
            {
                return false;
            }

            var item = await _categoryRepo.GetCategoryByIdAsync(cateId);

            if (item == null)
            {
                return false;
            }

            return await _categoryRepo.RemoveCategoryAsync(item);
        }

        public async Task<CategoryModel> UpdateCategoryAsync(CategoryModel cateModel)
        {

            if (cateModel.CategoryName != null)
            {
                if (cateModel.CategoryName.Equals(""))
                {
                    throw new Exception("Không được tạo category trống");
                }

                var listCate = await _categoryRepo.GetAllCateAsync();

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
        }
    }
}
