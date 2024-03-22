using AutoMapper;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRESTServices.BLL
{
    public class UserBLL : IUserBLL
    {
        private readonly IUserData _userData;
        private readonly IMapper _mapper;

        public UserBLL(IUserData userData, IMapper mapper)
        {
            _userData = userData;
            _mapper = mapper;
        }
        public Task<Task> ChangePassword(string username, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<Task> Delete(string username)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> GetAllWithRoles()
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUserWithRoles(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<Task> Insert(UserCreateDTO entity)
        {
            var Password = Helper.GetHash(entity.Password);
            entity.Password = Password;
            var map = _mapper.Map<User>(entity);
            var add = await _userData.Insert(map);
            return Task.FromResult(add);
        }

        public async Task<UserDTO> Login(string username, string password)
        {
            var pass = Helper.GetHash(password);
            var loginUser = await _userData.Login(username, pass);
            var loginMap= _mapper.Map<UserDTO>(loginUser);
            return loginMap;
        }

        public async Task<UserDTO> LoginMVC(LoginDTO loginDTO)
        {
            var pass = Helper.GetHash(loginDTO.Password);
            var loginUser = await _userData.Login(loginDTO.Username, pass);
            var loginMap = _mapper.Map<UserDTO>(loginUser);
            return loginMap;
        }
    }
}
