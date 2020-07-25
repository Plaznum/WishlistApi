using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WishlistApi.Models;

namespace WishlistApi.Controllers
{
    [Route("api/WishlistItems")]
    [ApiController]
    public class WishlistItemsController : ControllerBase
    {
        private readonly WishlistContext _context;

        public WishlistItemsController(WishlistContext context)
        {
            _context = context;
        }

        // GET: api/WishlistItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WishlistItem>>> GetWishlistItems()
        {
            return await _context.WishlistItems.ToListAsync();
        }

        // GET: api/WishlistItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WishlistItem>> GetWishlistItem(long id)
        {
            var wishlistItem = await _context.WishlistItems.FindAsync(id);

            if (wishlistItem == null)
            {
                return NotFound();
            }

            return wishlistItem;
        }

        // PUT: api/WishlistItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWishlistItem(long id, WishlistItem wishlistItem)
        {
            if (id != wishlistItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(wishlistItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WishlistItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WishlistItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WishlistItem>> PostWishlistItem(WishlistItem wishlistItem)
        {
            _context.WishlistItems.Add(wishlistItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWishlistItem), new { id = wishlistItem.Id }, wishlistItem);
        }

        // DELETE: api/WishlistItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WishlistItem>> DeleteWishlistItem(long id)
        {
            var wishlistItem = await _context.WishlistItems.FindAsync(id);
            if (wishlistItem == null)
            {
                return NotFound();
            }

            _context.WishlistItems.Remove(wishlistItem);
            await _context.SaveChangesAsync();

            return wishlistItem;
        }

        private bool WishlistItemExists(long id)
        {
            return _context.WishlistItems.Any(e => e.Id == id);
        }
    }
}
