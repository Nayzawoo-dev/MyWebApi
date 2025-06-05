using MyWebApi.Models;

namespace MyWebApi.Services
{
    public interface IPersonServices
    {
        ResponseModel DeletePerson(int id);
        ResponseModel GetPersonList();
        ResponseModel GetPersonListById(int id);
        ResponseModel PostPerson(PersonModels model);
        ResponseModel UpdateAndPostPerson(int id, PersonModels model);
        ResponseModel UpdatePerson(int id, PersonModels model);

        ResponseModel GetPersonList(int pageNo, int pageSize);
    }
}