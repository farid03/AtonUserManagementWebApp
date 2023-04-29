using Aton.UserManagement.Bll.Models;
using Aton.UserManagement.Bll.Services.Interfaces;
using MediatR;

namespace Aton.UserManagement.Bll.Queries;

public record GetUsersQuery(
    long? UserId,
    int Take,
    int Skip,
    long[]? CalculationIds = null
) : IRequest<GetUsersQueryResult>;

// public class GetCalculationHistoryQueryHandler
//     : IRequestHandler<GetCalculationHistoryQuery, GetHistoryQueryResult>
// {
//     private readonly IUserCreationService _userCreationService;
//
//     public GetCalculationHistoryQueryHandler(
//         IUserCreationService userCreationService)
//     {
//         _userCreationService = userCreationService;
//     }
//
//     public async Task<GetHistoryQueryResult> Handle(
//         GetCalculationHistoryQuery request,
//         CancellationToken cancellationToken)
//     {
//         var query = new QueryCalculationFilter(
//             request.UserId,
//             request.Take,
//             request.Skip,
//             request.CalculationIds);
//
//         // var log = await _calculationService.QueryCalculations(query, cancellationToken);
//
//         // return new GetHistoryQueryResult(
//             // log.Select(x => new GetHistoryQueryResult.HistoryItem(
//                     // x.TotalVolume,
//                     // x.TotalWeight,
//                     // x.Price,
//                     // x.GoodIds))
//                 // .ToArray());
//                 return null;
//     }
// }