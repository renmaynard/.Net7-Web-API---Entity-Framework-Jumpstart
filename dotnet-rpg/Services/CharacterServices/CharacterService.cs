using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterServices
{
    public class CharacterSevice : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{Id = 1, Name = "Arthur"}
        };
        private readonly IMapper _mapper;

        public CharacterSevice(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
             var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try{
                var character = characters.FirstOrDefault(c => c.Id == id);

                if(character is null)
                {
                    throw new Exception($"Character wit Id '{id}' not found.");
                }

                characters.Remove(character);

                serviceResponse.Data = characters.Select(c =>_mapper.Map<GetCharacterDto>(c)).ToList();
            }catch(Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = characters.Select(c =>_mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var character = characters.FirstOrDefault(c => c.Id == id);
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
              var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try{
                var character = characters.FirstOrDefault(c => c.Id == updateCharacter.Id);

                if(character is null)
                {
                    throw new Exception($"Character wit Id '{updateCharacter.Id}' not found.");
                }

                _mapper.Map(updateCharacter, character);

                character.Name = updateCharacter.Name;
                character.HitPoints = updateCharacter.HitPoints;
                character.Strength = updateCharacter.Strength;
                character.Defence = updateCharacter.Defence;
                character.Intelligence = updateCharacter.Intelligence;
                character.Class = updateCharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }catch(Exception ex){
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}