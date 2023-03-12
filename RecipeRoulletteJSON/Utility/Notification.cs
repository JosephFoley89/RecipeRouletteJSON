using RecipeRouletteJSON.ProjectData;
using RecipeRoulletteJSON.Model;
using RecipeRoulletteJSON.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RecipeRouletteJSON.Utility {
    internal class Notification {
        private Data data;
        private Load load = new Load();
        private GenerateHTML html = new GenerateHTML();
        private SmtpClient client;
        private MailMessage message;

        public Notification(Data data) {
            this.client = new SmtpClient(data.Host);
            this.client.Port = data.Port;
            this.client.Credentials = new NetworkCredential(data.Username, data.SMTPKey);
            this.client.EnableSsl = true;

            this.message = new MailMessage();
            this.message.From = new MailAddress("reciperouletteauto@gmail.com");
            message.IsBodyHtml = true;
            message.To.Add(data.ToEmail);
        }

        public void SendShoppingList(List<Recipe> recipes) {
            message.Body = html.GenerateShoppingList(recipes);
            message.Subject = "Grocery List";
            client.Send(message);
        }

        public void SendInstructions(Recipe recipe) {
            data = load.LoadConfigFile();
            message.Subject = String.Format("{0} Instructions", recipe.Name);
            message.Body = html.GenerateInstructions(recipe);
            client.Send(message);
        }
    }
}
