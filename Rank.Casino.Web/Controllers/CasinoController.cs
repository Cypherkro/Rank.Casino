using Microsoft.AspNetCore.Mvc;
using Rank.Casino.API.Rank.Casino.API;
using Rank.Casino.Data.Types;

namespace Rank.Casino.Web.Controllers
{
    [ApiController]
    [Route("casino")]
    public class CasinoController
    {
        public const string baseRoute = "";
        private readonly ILogger<CasinoController> _logger;
        private Slot _slot = new Slot("SQLCONNSTRING");//todo connection string to be pulled from Config, for now, Data sits in memory

        public CasinoController(ILogger<CasinoController> logger)
        {
            _logger = logger;
        }

        #region API Functions

        [HttpGet(Name = "GetBalance")]
        [Route("player/{playerId}/balance")]
        public Player GetPlayerBalance(int playerId)
        {
            return _slot.GetPlayerBalance(playerId);
        }


        //TODO playerId param is redundant in the CALL as its already included in the PATH.
        //Query with DagaCube
        [Route("player/{playerId}/balance/update")]
        public Data.Types.Balance PostUpdateBalance(int playerId, float amount, string type)
        {
            try
            {
                ValidateParameters(playerId, amount, type, _slot);
                return _slot.UpdatePlayerBalance(playerId, amount, type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Get the Players last 10 Transactions
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        /// <exception cref="Exception"></exception>
        [Route("admin/player/transactions")]
        public List<Transaction> PostLast10Transactions(string username)
        {
            try
            {
                Slot slot = new Slot("SQLCONNSTRING");
                return slot.GetPlayerTransactions(username, 10);//todo count shoud be loaded from Configuration
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case InvalidDataException:
                        throw new BadHttpRequestException("Invalid Username");

                    default:
                        throw new Exception(ex.ToString());
                }
            }
        }

        #endregion

        private void ValidateParameters(int playerId, float amount, string type, Slot slot)
        {
            if (amount < 0)
                throw new BadHttpRequestException("Amount cannot be negative");

            Player player;

            try
            {
                player = slot.GetPlayer(playerId);
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }

            if (amount > player.Balance)
            {
                throw new BadHttpRequestException("Amount cannot be more than the Current Balance", 418);
            }

            if (type != "WAGER" && (type != "WIN"))
            {
                throw new BadHttpRequestException("Invalid Transaction Type");
            }
        }
    }
}