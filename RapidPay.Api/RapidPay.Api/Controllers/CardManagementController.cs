using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RapidPay.CardManagement;
using RapidPay.Domain.Requests;
using RapidPay.Domain.Responses;
using System.Net.Mime;
using System.Threading.Tasks;

namespace RapidPay.Api.Controllers
{
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class CardManagementController : ControllerBase
    {
        private readonly ICardManagementService _cardManagementService;
        private readonly ILogger<CardManagementController> _logger;

        public CardManagementController(
            ICardManagementService cardManagementService, 
            ILogger<CardManagementController> logger)
        {
            _cardManagementService = cardManagementService;
            _logger = logger;
        }

        /// <summary>
        /// POST: Created new Card for RapidPay System
        /// </summary>
        /// <param name="request">CreateCardRequest</param>
        /// <returns>Created Card database Id and Number</returns>
        [HttpPost("new-card")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateCardResponse>> CreatesCardAsync(CreateCardRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var response = await _cardManagementService.CreateNewCardAsync(request);
                return CreatedAtAction("GetsCardBalance", new { cardNumber = response.CardNumber }, response);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error when creating new card with number: {request.Number}", ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            
        }

        /// <summary>
        /// PUT: Execute a payment using a given card number and the payment
        /// </summary>
        /// <param name="cardNumber">Card Number</param>
        /// <param name="amount">Amount to be paid</param>
        /// <returns></returns>
        [HttpPut("card/{cardNumber}/payment")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CardPaymentResponse>> PaymentAsync(
            [FromRoute] string cardNumber, 
            [FromRoute] decimal amount)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length != 15 || amount <= 0)
            {
                return BadRequest();
            }
            var response = new CardPaymentResponse();
            return Accepted(response);
        }

        /// <summary>
        /// GET: Gets Card Balance
        /// </summary>
        /// <param name="cardNumber">15 digits card number</param>
        /// <returns>CardBalanceResponse</returns>
        [HttpGet("card/{cardNumber}/balance")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CardBalanceResponse>> GetsCardBalance([FromRoute] string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length != 15)
            {
                return BadRequest();
            }
            try
            {
                var card = await _cardManagementService.GetCardBalance(cardNumber);
                if(card == null)
                {
                    return NotFound(new { message = "Card with provided userName does not exist" });
                }

                return Ok(card);
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Error retrieving card with number: {cardNumber}", ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
          
        }
    }
}
