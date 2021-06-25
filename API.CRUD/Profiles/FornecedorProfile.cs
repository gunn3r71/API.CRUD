using API.CRUD.Domain.Models;
using API.CRUD.Models.InputModels;
using API.CRUD.Models.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CRUD.Profiles
{
    public class FornecedorProfile : Profile
    {
        public FornecedorProfile()
        {
            CreateMap<Fornecedor, FornecedorViewModel>();
            CreateMap<CreateFornecedorInputModel, Fornecedor>();
            CreateMap<UpdateFornecedorInputModel, Fornecedor>();
        }
    }
}
