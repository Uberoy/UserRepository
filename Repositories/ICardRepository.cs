using CardRepository.Entities;

namespace CardRepository.Repositories
{
    public interface ICardRepository
    {
        Task<Card> GetByIdAsync(int id);
        Task<IEnumerable<Card>> GetAllAsync();
        Task AddAsync(Card card);
        Task UpdateAsync(Card card);
        Task DeleteAsync(int id);
    }
}
