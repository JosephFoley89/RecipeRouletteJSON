using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecipeRouletteJSON.Utility {
    internal class Alert {
        public bool DidUserConfirm(string action) {
            return MessageBox.Show(
                String.Format("Are you sure you want to {0}?", action),
                "Please Confirm",
                MessageBoxButton.YesNo
            ) == MessageBoxResult.Yes;
        }

        public void DisplayError(string message, string title) {
            MessageBox.Show(
                message,
                title,
                MessageBoxButton.OK
            );
        }
    }
}
