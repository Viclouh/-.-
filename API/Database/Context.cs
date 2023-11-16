using Microsoft.EntityFrameworkCore;

namespace API.Database
{
	public class Context : DbContext
	{
		private Context _instance;

		public Context Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new Context();
				}
				return _instance;
			}
		}

        public Context(DbContextOptions<Context> options)
        : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
