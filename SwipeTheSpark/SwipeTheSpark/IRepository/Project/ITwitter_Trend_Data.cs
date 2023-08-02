using Microsoft.AspNetCore.Components.Forms;
using SwipeTheSpark.Models.Project;

namespace SwipeTheSpark.IRepository.Project
{
    public interface ITwitter_Trend_Data
    {
        Task<List<dynamic>> CreateUpdate_Twitter_TrendDetails();
        Task<List<dynamic>> Get_Twitter_Trend_Data(Trend_DTO_Input InputData);
    }
}