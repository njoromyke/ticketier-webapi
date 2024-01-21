using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ticketier_webapi.Core.Context;
using ticketier_webapi.Core.Dtos;
using ticketier_webapi.Core.Entities;

namespace ticketier_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TicketsController(ApplicationDbContext _context, IMapper mapper)
        {
            this._context = _context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateTicket(CreateTicketDto createTicketDto)
        {
            var ticket = new Ticket
            {
                PassengerName = createTicketDto.PassengerName,
                PassengerSSN = createTicketDto.PassengerSSN,
                From = createTicketDto.From,
                To = createTicketDto.To,
                Price = createTicketDto.Price,
            };
            _mapper.Map(createTicketDto, ticket);

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }
        //Read All 
        [HttpGet]

        public async Task<ActionResult<IEnumerable<GetTicketDto>>> GetTickets(string? filterText)
        {
            IQueryable<Ticket> query = _context.Tickets;

            if (!string.IsNullOrEmpty(filterText))
            {
                query = query.Where(t => t.PassengerName.Contains(filterText));
            }


            var tickets = await query.ToListAsync();
            var ticketsDto = _mapper.Map<IEnumerable<GetTicketDto>>(tickets);
            return Ok(ticketsDto);
        }

        //Read One
        [HttpGet("{id}")]
        public async Task<ActionResult<GetTicketDto>> GetTicketById(long id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound("Ticket not found");
            }
            var ticketDto = _mapper.Map<GetTicketDto>(ticket);
            return Ok(ticketDto);
        }

        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(long id, [FromBody] UpdateTicketDto updateTicketDto)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound("Ticket not found");
            }

            _mapper.Map(updateTicketDto, ticket);
            ticket.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(ticket);
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(long id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound("Ticket not found");
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}

