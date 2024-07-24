using BackIngE_N.Context;
using BackIngE_N.Models.BD;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BackIngE_N.Logic
{
    public class SecurityLogic {

        private readonly IngenieriaeynContext _context;

        public SecurityLogic(IngenieriaeynContext context) {
            _context = context;
        }

        /// <summary>
        /// Verify if an IP is blocked.
        /// </summary>
        /// <param name="ip">The ip to verify</param>
        /// <returns>A boolean if is blocked or not</returns>
        public async Task<bool> IsBlockedIP(IPAddress ip) {
            if (ip == null) return false;
            BlockedIp b = await _context.BlockedIps.Where(b => b.Ip == ip.ToString()).FirstOrDefaultAsync();
            return b != null;
        }

        /// <summary>
        /// Unblock an IP.
        /// </summary>
        /// <param name="ip">The ip to unblock</param>
        public async void UnblockIP(IPAddress ip) {
            if (ip == null) return;

            BlockedIp b = _context.BlockedIps.Where(b => b.Ip == ip.ToString()).FirstOrDefault();

            if (b != null) {
                _context.BlockedIps.Remove(b);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Block an IP.
        /// </summary>
        /// <param name="ip">The ip to block</param>
        public async Task BlockIP(IPAddress ip) {
            await _context.BlockedIps.AddAsync(new BlockedIp() {
                Ip = ip.ToString(),
                BlockTime = DateTime.Now
            });

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Validate an IP.
        /// </summary>
        /// <param name="ip">Validates if an IP has many requests and blocks them</param>
        public async Task ValidateIP(IPAddress ip) {

            if (ip == null) return;

            int attempts = 1000; 
            int blockDurationMinutes = 5; // Tiempo en el cual se bloquea la ip si se excede el numero de intentos
            DateTime blockStartTime = DateTime.Now.AddMinutes(-blockDurationMinutes);

            int count = await _context.Securities
                .Where(s => s.Ip == ip.ToString() && s.LoginTime > blockStartTime && s.StatusLogin == false)
                .CountAsync();

            if (count >= attempts) {
                await BlockIP(ip);
            }
        }

        /// <summary>
        /// Save an IP.
        /// </summary>
        /// <param name="ip">The ip to save on bd</param>
        /// <returns>A boolean if the ip was saved</returns>
        public async Task<bool> SaveIP(IPAddress ip, bool status) {
            await _context.Securities.AddAsync(new Security() {
                Ip = ip.ToString(),
                LoginTime = DateTime.Now,
                StatusLogin = status
            });

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
