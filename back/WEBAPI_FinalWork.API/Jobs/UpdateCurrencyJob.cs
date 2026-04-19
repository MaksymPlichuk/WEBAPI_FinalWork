using Quartz;
using WEBAPI_FinalWork.BLL.Services;

namespace WEBAPI_FinalWork.API.Jobs
{
    public class UpdateCurrencyJob : IJob
    {
        private readonly CurrencyUpdateService _currencyUpdateService;
        private readonly ILogger<UpdateCurrencyJob> _logger;
        public UpdateCurrencyJob(CurrencyUpdateService currencyUpdateService, ILogger<UpdateCurrencyJob> logger)
        {
            _currencyUpdateService = currencyUpdateService;
            _logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _currencyUpdateService.UpdateUAHAsync();
            Console.BackgroundColor = ConsoleColor.Green;
            _logger.LogInformation($"Successfully got USD-UAH rate: {_currencyUpdateService.GetCurrentRate()}");
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
