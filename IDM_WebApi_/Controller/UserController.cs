// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using System.IdentityModel.Tokens.Jwt;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;
// using System.Security.Claims;
// using MyApi.Data;
// using MyApi.Models;


// namespace MyApi.Controllers
// {
//     public class UserController : Controller
//     {

//         private readonly ILogger<UserController> _logger;
//         private readonly ApplicationDbContext _context;
//         private readonly IConfiguration _configuration;
//         private readonly IUserServices _userServices;

//         public UserController(ILogger<UserController> logger, ApplicationDbContext context, IConfiguration configuration)
//         {
//             _logger = logger;
//             _context = context;
//             _configuration = configuration;
//         }

//         private string GenerateJwtToken(string username, string role)
//         {
//             var claims = new[]
//             {
//             new Claim(JwtRegisteredClaimNames.Sub, username),
//             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//             new Claim(ClaimTypes.Role, role)
//         };

//             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
//             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//             var token = new JwtSecurityToken(
//                 claims: claims,
//                 expires: DateTime.Now.AddHours(1), // Token expires in 1 hour
//                 signingCredentials: creds
//             );

//             return new JwtSecurityTokenHandler().WriteToken(token);
//         }

//         [HttpPost]
//         public async Task<IActionResult> Login(User user)
//         {
//             try
//             {
//                 if (ModelState.IsValid)
//                 {
//                     var myManager = await _userServices.LogIn(user);
//                     if (myManager != null)
//                     {
//                         HttpContext.Session.SetString("Role", "Manager");
//                         HttpContext.Session.SetString("Username", myManager.ManagerName);
//                         HttpContext.Session.SetInt32("Id", myManager.Id);

//                         var token = GenerateJwtToken(myManager.ManagerName, "Manager");
//                         HttpContext.Response.Cookies.Append("TOKEN",
//                         token, new CookieOptions
//                         {
//                             HttpOnly = true,
//                             Secure = true,
//                             SameSite = SameSiteMode.Strict,
//                             Expires = DateTime.UtcNow.AddHours(1)
//                         });
//                         _logger.LogInformation($"{myManager.ManagerName} logged in successfully.");
//                         return RedirectToAction("ViewDashboard", "Manager");
//                     }

//                     var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Username == user.Username && e.Password == user.Password);
//                     if (employee != null)
//                     {
//                         var token = GenerateJwtToken(user.Username, "Employee");
//                         HttpContext.Session.SetString("Role", "Employee");
//                         HttpContext.Session.SetString("Username", employee.Username);
//                         HttpContext.Session.SetInt32("Id", employee.EmployeeId);
//                         HttpContext.Response.Cookies.Append("TOKEN",
//                         token, new CookieOptions
//                         {
//                             HttpOnly = true,
//                             Secure = true,
//                             SameSite = SameSiteMode.Strict,
//                             Expires = DateTime.UtcNow.AddHours(1)
//                         });
//                         _logger.LogInformation($" {user.Username} logged in successfully.");
//                         return RedirectToAction("ViewDashboard", "Employee");
//                     }

//                     ModelState.AddModelError(string.Empty, "Invalid username or password.");
//                 }

//                 return View(user);
//             }
//             catch (HttpRequestException httpRequestException)
//             {
//                 _logger.LogError(httpRequestException, "Error fetching employees via HTTP request.");
//                 return View("Error", new { message = "An error occurred while fetching employees." });
//             }
//             catch (InvalidOperationException nullReferenceException)
//             {
//                 _logger.LogError(nullReferenceException, "An error occurred while logging in.");
//                 return View("Error", new { message = "An error occurred while fetching employees." });
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "An error occurred while logging in.");
//                 return View("Error", new { message = "An error occurred while fetching employees." });
//             }

//         }



//         public IActionResult Logout()
//         {
//             _logger.LogInformation($" logged Out successfully.");
//             Response.Cookies.Delete("TOKEN");
//             HttpContext.Session.Clear();
//             return RedirectToAction("Login");
//         }
//     }
// }