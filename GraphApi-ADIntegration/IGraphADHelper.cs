using Microsoft.Graph.Models;

namespace GraphApi_ADIntegration
{
    public interface IGraphADHelper
    {

        Task<List<User>?> ListUsersAsync();
 
       
    }
}
