using CardRepository.DBContexts;
using CardRepository.Entities;
using Microsoft.EntityFrameworkCore;

namespace CardRepository.Repositories
{
    public class CardsRepository : ICardRepository
    {
        private readonly CardsDbContext _context;

        public CardsRepository(CardsDbContext context)
        {
            _context = context;
        }

        public async Task<Card> GetByIdAsync(int id) =>
            await _context.Cards.FindAsync(id);

        public async Task<IEnumerable<Card>> GetAllAsync() =>
            await _context.Cards.ToListAsync();

        public async Task AddAsync(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Card card)
        {
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var card = await _context.Cards.FindAsync(id);
            if (card != null)
            {
                _context.Cards.Remove(card);
                await _context.SaveChangesAsync();
            }
        }
    }
}
