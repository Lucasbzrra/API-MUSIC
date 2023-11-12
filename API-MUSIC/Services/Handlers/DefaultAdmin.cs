using API_MUSIC.Data.EfCore;
using API_MUSIC.Models;

namespace API_MUSIC.Services.Handlers;

public class DefaultAdmin : IAdminService
{
    private readonly IAdminDao _AdminDao;
    public DefaultAdmin(IAdminDao adminDao)
    {
        _AdminDao = adminDao;
    }

    public void Cadastrar(Administrator administrator)
    {
        _AdminDao.Register(administrator);
    }

    public void DeleteAdminstrator(Administrator administrator)
    {
        _AdminDao.DeleteAdminstrator(administrator);
    }

    public Administrator ConsultAdministrator(string id)
    {
       return _AdminDao.QueryAdministrator(id);
    }

    public List<Administrator> RetornarAllAdmin()
    {
       return _AdminDao.ReturnAllAdmin();
    }

    public void Save()
    {
        _AdminDao.SaveChngs();
    }

    public Administrator ConsultAdministratorIdUser(string id)
    {
        return _AdminDao.QueryAdministrator(id);
    }

    public Administrator AdministratorMat(int id)
    {
        return _AdminDao.QueryAdministratorMat(id);
    }
}
