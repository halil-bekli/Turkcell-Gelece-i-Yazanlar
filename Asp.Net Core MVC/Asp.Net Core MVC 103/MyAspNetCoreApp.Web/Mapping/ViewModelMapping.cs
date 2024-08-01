using AutoMapper;
using MyAspNetCoreApp3.Web.Models;
using MyAspNetCoreApp3.Web.ViewModels;

namespace MyAspNetCoreApp3.Web.Mapping {
    public class ViewModelMapping : Profile {
        public ViewModelMapping() {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
