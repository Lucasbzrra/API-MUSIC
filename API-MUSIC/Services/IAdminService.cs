using API_MUSIC.Models;

namespace API_MUSIC.Services;

public interface IAdminService
{
    Administrator ConsultAdministratorIdUser(string id);

    Administrator AdministratorMat(int id);
    List<Administrator> RetornarAllAdmin();

    void Cadastrar(Administrator administrator);

    void DeleteAdminstrator (Administrator administrator);
    void Save();
}
