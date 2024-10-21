using AutoMapper;
using Demo.Core.DTO.Accounts;
using Demo.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Mapping.Account
{
    public class AddressProfile:Profile
    {
        public AddressProfile() { 
        
            CreateMap<Address,UserAddressDTO>().ReverseMap();
        }
    }
}
