using OutReachDataAccessLayer.Generic_Repository;
using OutReachDataAccessLayer.Models;

namespace OutReachDataAccessLayer.Repository
{
    public class ExceptionRepository
    {
        private readonly IGenericRepository<ExceptionLogger> IExceptionRepository = null;
        public ExceptionRepository()
        {
            IExceptionRepository = new GenericRepository<ExceptionLogger>();
        }
        public void AddException(ExceptionLogger exp)
        {
            IExceptionRepository.Insert(exp);
            IExceptionRepository.Save();
        }
    }
}
