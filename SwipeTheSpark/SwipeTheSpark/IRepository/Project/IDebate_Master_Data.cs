using SwipeTheSpark.Models.Project;

namespace SwipeTheSpark.IRepository.Project
{
    public interface IDebate_Master_Data
    {
        public List<dynamic> AddUpdate_Debate_Master_Data(Debate_Master_DTO model);
        public List<dynamic> Get_Debate_Master_DetailsDTO(Debate_Master_DTO_Input model);
    }
}
