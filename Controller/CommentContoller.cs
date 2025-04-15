using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [Route("api/comment")]
    public class CommentContoller:ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public CommentContoller(ICommentRepository commentRepository)
        {
            _commentRepository=commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments=await _commentRepository.GetAllAsync();
            var commentDto=comments.Select(x=>x.ToCommentDto());
            return Ok(commentDto);
        }
    }
}