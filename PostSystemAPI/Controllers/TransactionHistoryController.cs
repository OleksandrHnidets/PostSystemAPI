using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.DAL.Repository;
using PostSystemAPI.Domain.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using System;
using System.Collections;

namespace PostSystemAPI.WebApi.Controllers
{
    [Route("api/transaction-history")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITransactionHistoryRepository _transactionRepo;
        private readonly IDeliveryRepo _deliveryRepo;
        private readonly IMapper _mapper;

        public TransactionHistoryController(UserManager<User> userManager, ITransactionHistoryRepository transactionRepository, IDeliveryRepo deliveryRepo, IMapper mapper)
        {
            _userManager = userManager;
            _transactionRepo = transactionRepository;
            _deliveryRepo = deliveryRepo;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TransactionHistoryViewModel>>> GetAllTransactions(string userId)
        {
            try
            {
                var currentUser = await _userManager.FindByIdAsync(userId);
                if(currentUser == null)
                    return NotFound("User was not found");


                var transactionHistory = _transactionRepo.Entities.Where(t => t.Delivery.ReceivedBy == userId).Include(r => r.Delivery).ToList();
                var transactionHistoryView = new List<TransactionHistoryViewModel>();
                foreach(var transaction in transactionHistory)
                {
                    transactionHistoryView.Add(_mapper.Map<TransactionHistoryViewModel>(transaction));
                }

                return Ok(transactionHistoryView);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
