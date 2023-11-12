using API_MUSIC.Models;

namespace API_MUSIC.Data.EfCore;

public interface IAdminDao
{
    public void Register(Administrator administrator);

    public void DeleteAdminstrator(Administrator administrator);

    public Administrator QueryAdministrator(string id);

    public Administrator QueryAdministratorMat(int id);
    public List<Administrator> ReturnAllAdmin();

    public void SaveChngs();
    
}
