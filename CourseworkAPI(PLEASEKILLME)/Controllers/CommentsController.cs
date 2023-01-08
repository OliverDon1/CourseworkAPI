using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseworkAPI_PLEASEKILLME_.Models;

namespace CourseworkAPI_PLEASEKILLME_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly COMP2001_ODonnellyContext _context;

        public CommentsController(COMP2001_ODonnellyContext context)
        {
            _context = context;
        }

        [HttpGet("SP:storeComment")]
        public async Task<ActionResult<List<COMP2001_ODonnellyContext>>> storeCommentSP(string text , DateTime date , int trailID , int userID)
        {
            var result = await _context.Database.ExecuteSqlRawAsync($"CW2.storeComment '{text}' ,'{date}', {trailID}, {userID}");
            return Ok(result);
        }
        [HttpGet("SP:updateComment")]
        public async Task<ActionResult<List<Comment>>> updateCommentSP(int id , string text, DateTime date, int trailID, int userID)
        {
            var result = await _context.Database.ExecuteSqlRawAsync($"CW2.updateComment {id},'{text}', '{date}' , {trailID}, {userID}");
            return Ok(result);
        }
        [HttpGet("SP:deleteComment")]
        public async Task<ActionResult<List<Comment>>> deleteCommentSP(int commentID)
        {
            var result = await _context.Database.ExecuteSqlRawAsync($"CW2.deleteComment {commentID}");
            return Ok(result);
        }
        [HttpGet("SP:viewComments")]
        public async Task<ActionResult<List<Comment>>> viewCommentSP()
        {
            var result = await _context.Comments.FromSqlRaw("CW2.ViewComments").ToListAsync();
            return Ok(result);
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            return await _context.Comments.ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment , string text , DateTime date , int trail , int user)
        {
            comment.CommentId = id;
            comment.CommentText = text;
            comment.CommentDate = date;
            comment.TrailId = trail;
            comment.UserId = user;
            
            if (id != comment.CommentId)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Trail>> PostTrail(Comment comment, string text , DateTime date , int trail , int user)
        {
            comment.CommentText = text;
            comment.CommentDate = date;
            comment.TrailId = trail;
            comment.UserId = user;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.CommentId }, comment);
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}
