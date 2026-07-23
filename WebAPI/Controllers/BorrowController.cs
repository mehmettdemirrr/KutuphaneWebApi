using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        IBorrowService _borrowService;

        public BorrowController(IBorrowService borrowService)
        {
            _borrowService = borrowService;
        }

        [HttpGet("GetAllBorrows")]
        public IActionResult GetAllBorrows()
        {
            var result = _borrowService.GetAllBorrows();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetBorrowDetails")]
        public IActionResult GetBorrowDetails()
        {
            var result = _borrowService.GetBorrowDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetBorrowById")]
        public IActionResult GetBorrowById(int borrowId)
        {
            var result = _borrowService.GetBorrowById(borrowId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetActiveBorrowsByBookId")]
        public IActionResult GetActiveBorrowsByBookId(int bookId)
        {
            var result = _borrowService.GetActiveBorrowsByBookId(bookId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetActiveBorrowsByUserId")]
        public IActionResult GetActiveBorrowsByUserId(int userId)
        {
            var result = _borrowService.GetActiveBorrowsByUserId(userId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Loan")]
        public IActionResult Loan(Borrow borrow)
        {
            var result = _borrowService.Loan(borrow);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("Return")]
        public IActionResult Return(Borrow borrow)
        {
            var result = _borrowService.Return(borrow);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}