using GlobalCollege.Data;
using System;
using System.Data.SqlClient;


namespace GlobalCollege.Infrastructure
{
    public interface IDatabaseFactory //: IDisposable
    {
        ApplicationDbContext Get();
    }
}



