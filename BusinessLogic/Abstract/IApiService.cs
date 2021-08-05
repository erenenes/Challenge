using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BusinessLogic.Abstract
{
    public interface IApiService
    {
        Task<string> GetAllMatch(string Date);
        Task<string> GetAllRounds();
        Task<string> GetAllStatus();
        Task<string> GetTournamentById(int TournamentId);
    }
}
