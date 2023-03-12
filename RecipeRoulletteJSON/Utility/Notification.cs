﻿using RecipeRouletteJSON.ProjectData;
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

        public void SendShoppingList(List<Recipe> recipes) {
            data = load.LoadConfigFile();

            SmtpClient client = new SmtpClient(data.Host);
            client.Port = data.Port;
            client.Credentials = new NetworkCredential(data.Username, data.SMTPKey);
            client.EnableSsl = true;

            MailMessage message = new MailMessage();
            message.From = new MailAddress("ShoppingList@RecipeRoulette.com");
            message.Subject = "Grocery List";
            message.Body = html.GenerateShoppingList(recipes);
            message.IsBodyHtml = true;
            message.To.Add(data.ToEmail);

            client.Send(message);
        }

        public void SendInstructions(Recipe recipe) {
            data = load.LoadConfigFile();

            SmtpClient client = new SmtpClient(data.Host);
            client.Port = data.Port;
            client.Credentials = new NetworkCredential(data.Username, data.SMTPKey);
            client.EnableSsl = true;

            MailMessage message = new MailMessage();
            message.From = new MailAddress("Instructions@RecipeRoulette.com");
            message.Subject = String.Format("{0} Instructions", recipe.Name);
            message.Body = html.GenerateInstructions(recipe);
            message.IsBodyHtml = true;
            message.To.Add(data.ToEmail);

            client.Send(message);
        }
    }
}
