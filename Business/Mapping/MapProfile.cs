using AutoMapper;
using Business.Dtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Business.Mapping
{
	public class MapProfile : Profile
	{
		public MapProfile()
		{
			CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<Category, CategoryDto>().ReverseMap();
			CreateMap<Product, ProductWithCategoryDto>();
			CreateMap<Category, CategoryWithProductsDto>();
		}
	}
}
