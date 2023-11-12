using API_MUSIC.Models;

namespace API_MUSIC.Data.EfCore;

public class AdminDaoComEfCore : IAdminDao
{
    private UsersDbContext _context;
    public AdminDaoComEfCore(UsersDbContext context)
    {
        _context = context;
    }

    public void DeleteAdminstrator(Administrator administrator)
    {
        _context.Remove(administrator);
    }

    public Administrator QueryAdministrator(string id)
    {
        var query = _context.ADMINISTRATORS.FirstOrDefault(x => x.IdUser.Contains(id));
       // var query = _context.ADMINISTRATORS.FromSqlRaw($"select * from ADMINISTRATORS where idUser=={id}").ToList();
       
        return query;
    }

    public Administrator QueryAdministratorMat(int id)
    {
        var query = _context.ADMINISTRATORS.FirstOrDefault(x => x.Mat == id);
        return query;
    }

    public void Register(Administrator administrator)
    {
        _context.Add(administrator);
    }

    public List<Administrator> ReturnAllAdmin()
    {
            return _context.ADMINISTRATORS.ToList();
        
    }

    public void SaveChngs()
    {
       _context.SaveChanges();
    }
}
