using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartX.BL.ExternalServices.Abstractions;

public interface IEmailService
{
    void SendWelcomeEmail(string toUser);
    void SendConfirmEmail(string toUser, string confirmUrl);
    void SendForgotPassword(string toUser, string resetUrl);
}
